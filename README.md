# What is it?

Bindkey is a background application designed to quickly execute common Windows operations via a single key press. 

<br/>

# Getting started

BindKey uses profiles to categorize keybinds. Only the currently selected profile's bindings will be active at a given time.

To begin, create a profile

![](https://media1.giphy.com/media/awyEBo0EALJms5nR1m/giphy.gif)

<br/>

Next, add an event. A dialog-box will prompt you to specify a key binding and select the type of action you wish to bind.

![](https://media4.giphy.com/media/ljkCD4wFpa1nTPwzay/giphy.gif)

<br/>

BindKey also supports chaining actions together. In this example, a kill-process action is chained with an open process action allowing for a kill-and-restart action to be performed from a single key press.

<img src="https://media3.giphy.com/media/gWpTCHWG67PeeveGuz/giphy.gif" width="480"/>

<br/>

Actions can be pinned, making them available across all profiles.

<img src="https://media0.giphy.com/media/xGGGbf70EGOOOz1PmC/giphy.gif" width="480"/>

<br/>

Right click on actions to edit or remove them.

![](https://media0.giphy.com/media/gHCTYirJmpyvvKU2N8/giphy.gif)

<br/>

# Tips

* Closing BindKey will cause the application to run in the background and can be accessed via the system tray.

* A key binding is not required for an action. This can be helpful if you wish to create an action for the sole purpose of being chained together with a parent action.

* BindKey will search for a 'save.bk' file in the root directory which will store the data from the last session. BindKey must be closed or saved via the system tray to create a save file.
