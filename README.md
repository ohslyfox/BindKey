# What is it?

Bindkey is a background application designed to quickly execute common Windows operations via a single keybind press. 


# Getting started

BindKey supports profiles. Profiles can be used to categorize groups of key bindings. Only the currently selected profile's bindings will be active at a given time.

To begin, create a profile

![](resources/1.gif)

<br/>

Next, add an event. A dialog-box will prompt you to specify a key binding and select the type of action you wish to bind.

![](resources/2.gif)

<br/>

BindKey also supports chaining actions together. In this example, a kill-process action is chained with an open process action allowing for a kill-and-restart action to be performed from a single key press.

![](resources/3.gif)

<br/>

Actions can be pinned, making them available across all profiles.

![](resources/4.gif)

<br/>

Right click on actions to edit or remove them.

![](resources/5.gif)


# Tips

* Closing BindKey will cause the application to run in the background and can be accessed via the system tray.

* A key binding is not required for an action. This can be helpful if you wish to create an action for the sole purpose of being chained together with a parent action.

* BindKey will search for a 'save.bk' file in the root directory which will store the data from the last session. BindKey must be closed or saved via the system tray to create a save file.
