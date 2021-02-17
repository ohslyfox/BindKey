using BindKey.KeyActions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace BindKey.Util
{
    internal class KeyActionData
    {
        private static KeyActionData SingletonInstance = null;
        public static KeyActionData GetInstance()
        {
            if (SingletonInstance == null)
            {
                throw new Exception("Attempted to access instance of KeyActionData before it was instantiated.");
            }
            return SingletonInstance;
        }

        private string _filePath = string.Empty;
        private string FilePath { get => string.IsNullOrEmpty(_filePath) ? "save.bk" : _filePath; }

        public Dictionary<string /* profile name */, Dictionary<string /* action GUID */, IKeyAction>> ProfileMap { get; private set; }

        public event EventHandler<ProfileNameChangedEventArgs> ProfileChanged;
        private string _selectedProfile = string.Empty;
        public string SelectedProfile { get => _selectedProfile; set => SelectProfile(value); }
        public Dictionary<string, IKeyAction> SelectedActionMap
        {
            get
            {
                ProfileMap.TryGetValue(SelectedProfile, out var res);
                return res;
            }
        }
        public List<IKeyAction> SelectedActionList { get => SelectedActionMap?.Values.ToList(); }
        public List<IKeyAction> PinnedActions
        {
            get => ProfileMap.Values.SelectMany(d => d.Values)
                                    .Where(ka => ka.Pinned)
                                    .GroupBy(ka => ka.GUID)
                                    .Select(grp => grp.FirstOrDefault()).ToList();
        }
        public IEnumerable<string> ProfileNames { get => ProfileMap.Keys; }
        public IEnumerable<IKeyAction> ActionsToHook
        {
            get => SelectedActionMap?.Select(kvp => kvp.Value)
                                     .Where(ka => ka.Enabled && string.IsNullOrWhiteSpace(ka.KeyCombo) == false);
        }

        public KeyActionData(string filePath)
        {
            this._filePath = filePath;
            this.ProfileMap = new Dictionary<string, Dictionary<string, IKeyAction>>();
            this.LoadData();
            this.RefreshNextKeyActions();
            SingletonInstance = this;
        }

        public void RefreshNextKeyActions()
        {
            foreach (var dict in this.ProfileMap.Values)
            {
                foreach (var keyAction in dict.Values)
                {
                    if (keyAction.NextKeyActionGUID != null)
                    {
                        if (dict.TryGetValue(keyAction.NextKeyActionGUID, out var nextAction))
                        {
                            if (keyAction.Pinned && !nextAction.Pinned)
                            {
                                nextAction = null;
                            }
                        }
                        keyAction.SetNextAction(nextAction);
                    }
                }
            }
        }

        public void AddProfile(string profileName)
        {
            if (ProfileMap.TryGetValue(profileName, out var dict) == false)
            {
                ProfileMap[profileName] = new Dictionary<string, IKeyAction>();
            }

            foreach (var kvp in ProfileMap)
            {
                foreach (IKeyAction action in PinnedActions)
                {
                    ProfileMap[kvp.Key][action.GUID] = action;
                }
            }
        }

        public bool RemoveProfile(string profileToRemove)
        {
            bool res = false;
            if (profileToRemove != null)
            {
                res = ProfileMap.Remove(profileToRemove);
            }
            return res;
        }

        public bool RemoveKeyAction(IKeyAction keyAction)
        {
            bool res = false;
            foreach (var kvp in this.ProfileMap)
            {
                res |= kvp.Value.Remove(keyAction.GUID);
            }
            RefreshNextKeyActions();
            return res;
        }

        public void PinUnpinKeyAction(IKeyAction keyAction)
        {
            if (keyAction.Pinned)
            {
                foreach (var kvp in this.ProfileMap.Where(kvp => kvp.Key != SelectedProfile))
                {
                    kvp.Value.Remove(keyAction.GUID);
                }
            }
            else
            {
                // clear any matching key combos
                var keyActionsToClear = this.ProfileMap.Where(kvp => kvp.Key != SelectedProfile)
                                                       .SelectMany(kvp => kvp.Value)
                                                       .Select(kvp => kvp.Value)
                                                       .Where(ka => ka.KeyCombo == keyAction.KeyCombo && ka.GUID != keyAction.GUID);
                foreach (IKeyAction action in keyActionsToClear)
                {
                    action.ClearKeyCombo();
                }

                foreach (var kvp in this.ProfileMap)
                {
                    if (kvp.Value.ContainsKey(keyAction.GUID) == false)
                    {
                        kvp.Value[keyAction.GUID] = keyAction;
                    }
                }
            }
            keyAction.Pinned = !keyAction.Pinned;
            RefreshNextKeyActions();
        }

        public void EnableDisableKeyAction(IKeyAction keyAction)
        {
            bool enabled = !keyAction.Enabled;
            keyAction.Enabled = enabled;
            if (keyAction.Pinned)
            {
                foreach (var kvp in this.ProfileMap.Where(kvp => kvp.Key != SelectedProfile))
                {
                    if (kvp.Value.ContainsKey(keyAction.GUID))
                    {
                        kvp.Value[keyAction.GUID].Enabled = enabled;
                    }
                }
            }
        }

        private void SelectProfile(string profileName)
        {
            if (ProfileNames.Contains(profileName))
            {
                this._selectedProfile = profileName;
                ProfileChanged?.Invoke(this, new ProfileNameChangedEventArgs(profileName));
            }
        }

        private void LoadData()
        {
            if (File.Exists(FilePath) == false) return;
            try
            {
                string rawText = ReadAndDecryptSaveFile(FilePath);
                XDocument doc = XDocument.Parse(rawText);
                
                foreach (XElement profile in doc.Descendants("Profile"))
                {
                    string profileName = profile.Element("Name").Value;
                    var actionMap = GetOrCreateProfileMap(profileName);
                    
                    foreach (XElement action in profile.Descendants("Action"))
                    {
                        var propDict = action.Descendants("Property").ToDictionary(k => k.Element("PropertyName").Value,
                                                                                   v => v.Element("PropertyValue").Value);

                        LoadSingleAction(actionMap, propDict);
                    }
                }
                this.SelectedProfile = doc.Descendants("SelectedProfile").First().Value;
            }
            catch
            {
                this.ProfileMap = new Dictionary<string, Dictionary<string, IKeyAction>>();
                this.SelectedProfile = string.Empty;
                MessageBox.Show($"Failed to load from {FilePath}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Dictionary<string, IKeyAction> GetOrCreateProfileMap(string profileName)
        {
            if (this.ProfileMap.TryGetValue(profileName, out var currentActionMap) == false)
            {
                ProfileMap[profileName] = currentActionMap = new Dictionary<string, IKeyAction>();
            }
            return currentActionMap;
        }

        private void LoadSingleAction(Dictionary<string, IKeyAction> actionMap, Dictionary<string, string> propDict)
        {
            try
            {
                if (propDict.Count > 0)
                {
                    IKeyAction ka = KeyActionFactory.GetKeyActionFromPropertyMap(propDict);
                    if (ka != null)
                    {
                        actionMap[ka.GUID] = ka;
                    }
                }
            }
            catch
            {
                throw new Exception("Failed to load single action.");
            }
        }

        private string ReadAndDecryptSaveFile(string filePath)
        {
            string rawText = string.Empty;
            using (StreamReader sr = new StreamReader(filePath))
            {
                rawText = sr.ReadToEnd();
                sr.Close();
            }

            return SecretIO.GetInstance().Decrypt(rawText);
        }

        public void SaveData()
        {
            try
            {
                string saveText = GetXMLSaveString();
                saveText = SecretIO.GetInstance().Encrypt(saveText);

                StreamWriter sw = File.CreateText(FilePath);
                sw.Write(saveText);
                sw.Close();
            }
            catch
            {
                MessageBox.Show($"Could not save to file {FilePath}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetXMLSaveString()
        {
            string res = null;
            try
            {
                XDocument doc = new XDocument();

                XElement root = new XElement("SaveFile");
                doc.Add(root);

                XElement selectedProfile = new XElement("SelectedProfile", this.SelectedProfile);
                root.Add(selectedProfile);

                foreach (var kvp in this.ProfileMap)
                {
                    XElement profile = new XElement("Profile",
                                       new XElement("Name", kvp.Key));

                    foreach (var action in kvp.Value.Values)
                    {
                        XElement act = new XElement("Action");
                        XElement properties = new XElement("Properties");
                        foreach (var prop in action.Properties)
                        {
                            properties.Add(new XElement("Property",
                                           new XElement("PropertyName", prop.Key),
                                           new XElement("PropertyValue", prop.Value)));
                        }
                        act.Add(properties);
                        profile.Add(act);
                    }
                    root.Add(profile);
                }
                res = doc.ToString();
            }
            catch (Exception e)
            {
                BindKey.ShowBalloonTip("BindKey", e.Message, ToolTipIcon.Error);
            }
            return res;
        }
    }

    internal class ProfileNameChangedEventArgs : EventArgs
    {
        public string NewProfileName { get; }
        public ProfileNameChangedEventArgs(string newProfileName)
        {
            this.NewProfileName = newProfileName;
        }
    }
}
