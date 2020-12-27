using BindKey.KeyActions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace BindKey.Util
{
    internal class KeyActionData
    {
        private string _filePath = string.Empty;
        private string FilePath { get => string.IsNullOrEmpty(_filePath) ? "save.bk" : _filePath; }

        public Dictionary<string /* profile name */, Dictionary<string /* action GUID */, IKeyAction>> ProfileMap { get; private set; }
        public string SelectedProfile { get; set; }
        public Dictionary<string, IKeyAction> SelectedActionMap
        {
            get
            {
                ProfileMap.TryGetValue(SelectedProfile, out var res);
                return res;
            }
        }
        public List<IKeyAction> SelectedActionList { get => SelectedActionMap?.Values.ToList(); }
        public List<IKeyAction> PinnedActions { get => ProfileMap.Values.SelectMany(d => d.Values).Where(ka => ka.Pinned).ToList(); }
        public IEnumerable<string> ProfileNames { get => ProfileMap.Keys; }
        public IEnumerable<IKeyAction> ActionsToHook { get => SelectedActionMap?.Select(kvp => kvp.Value).Where(ka => ka.Enabled && string.IsNullOrWhiteSpace(ka.KeyCombo) == false); }

        public KeyActionData(string filePath)
        {
            this._filePath = filePath;
            this.ProfileMap = new Dictionary<string, Dictionary<string, IKeyAction>>();
            this.SelectedProfile = string.Empty;
            this.LoadData();
        }

        public void RefreshNextKeyActions()
        {
            foreach (var dict in this.ProfileMap.Values)
            {
                foreach (var keyAction in dict.Values)
                {
                    if (keyAction.NextKeyActionGUID != null)
                    {
                        dict.TryGetValue(keyAction.NextKeyActionGUID, out var nextAction);
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
            return ProfileMap.Remove(profileToRemove);
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
                foreach (var kvp in this.ProfileMap)
                {
                    if (kvp.Value.ContainsKey(keyAction.GUID) == false)
                    {
                        kvp.Value[keyAction.GUID] = keyAction;
                    }
                }
            }
            keyAction.Pinned = !keyAction.Pinned;
        }

        private void LoadData()
        {
            if (File.Exists(FilePath) == false) return;

            try
            {
                StreamReader sr = new StreamReader(FilePath);
                string rawText = sr.ReadToEnd();
                sr.Close();

                rawText = SecretIO.GetInstance().Decrypt(rawText);
                string[] lines = rawText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length > 0)
                {
                    this.SelectedProfile = lines[0];
                    for (int i = 1; i < lines.Length; i++)
                    {
                        List<string> items = DefaultKeyAction.REGEX_DELIMITER.Split(lines[i]).ToList();
                        string profileName = items[0];

                        if (this.ProfileMap.TryGetValue(profileName, out var currentActionMap) == false)
                        {
                            ProfileMap[profileName] = currentActionMap = new Dictionary<string, IKeyAction>();
                        }

                        var propDict = items.GetRange(1, items.Count - 1)
                                            .Select(s => s.Split(',').ToList())
                                            .ToDictionary(k => k[0], v => string.Join(",", v.GetRange(1, v.Count - 1)));
                        if (propDict.Count > 0)
                        {
                            IKeyAction action = KeyActionFactory.GetNewKeyActionOfType(propDict);
                            if (action != null)
                            {
                                currentActionMap[action.GUID] = action;
                            }
                        }
                    }
                }
            }
            catch
            {
                this.ProfileMap = new Dictionary<string, Dictionary<string, IKeyAction>>();
                this.SelectedProfile = string.Empty;
                MessageBox.Show($"Failed to load from {FilePath}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void SaveData()
        {
            try
            {
                string rawText = string.Empty;
                rawText += $"{SelectedProfile}{Environment.NewLine}";
                foreach (var kvp in ProfileMap)
                {
                    if (kvp.Value.Count > 0)
                    {
                        foreach (var actionMapKvp in kvp.Value)
                        {
                            rawText += $"{kvp.Key}{DefaultKeyAction.DELIMITER}{actionMapKvp.Value.SaveString}{Environment.NewLine}";
                        }
                    }
                    else
                    {
                        rawText += kvp.Key;
                    }
                }
                rawText = SecretIO.GetInstance().Encrypt(rawText);

                StreamWriter sw = File.CreateText(FilePath);
                sw.Write(rawText);
                sw.Close();
            }
            catch
            {
                MessageBox.Show($"Could not save to file {FilePath}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
