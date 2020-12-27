# BindKey - What is it?

Bindkey is a background application designed to quickly execute common Windows operations via a single keybind press. 

# Getting started

BindKey supports profiles. Profiles can be used to categorize groups of key bindings. Only the currently selected profile's bindings will be active at a given time.

To begin, first create a profile

![alt text](https://i.imgur.com/hvmE67q.gif)

---

Next, add an event. A dialog-box will prompt you to specify a key binding and select the type of action you wish to bind.

![alt text](https://i.imgur.com/7v56QXT.gif)

---

BindKey also supports chaining actions together. In this example, a kill-process action is chained with an open process action allowing for a kill-and-restart action to be performed from a single key press.

![alt text](https://i.imgur.com/M0wNJ8Z.gif)

# Tips

* Closing BindKey will cause the application to run in the background and can be accessed via the system tray.

* A key binding is not required for an action. This can be helpful if you wish to create an action for the sole purpose of being chained together with a parent action.

* BindKey will search for a 'save.bk' file in the same folder it is located which will store the data from the last session. BindKey must be closed or saved via the system tray to create a save file.
