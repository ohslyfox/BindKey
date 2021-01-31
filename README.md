# What is it?

Bindkey is a background application designed to quickly execute common Windows operations via a single keybind press. 


# Getting started

BindKey supports profiles. Profiles can be used to categorize groups of key bindings. Only the currently selected profile's bindings will be active at a given time.

To begin, create a profile

<center>
    <img src="https://media4.giphy.com/media/2wHwdPXrkJnvGMu24l/giphy.gif">
</center>

<br/>

Next, add an event. A dialog-box will prompt you to specify a key binding and select the type of action you wish to bind.

<center>
    <img src="https://media3.giphy.com/media/mZOxON9Taib3HWXp1I/giphy.gif">
</center>

<br/>

BindKey also supports chaining actions together. In this example, a kill-process action is chained with an open process action allowing for a kill-and-restart action to be performed from a single key press.

<center>
    <img src="https://media2.giphy.com/media/Wz1dltZfvm1jTidwjk/giphy.gif">
</center>

<br/>

Actions can be pinned, making them available across all profiles.

<center>
    <img src="https://media1.giphy.com/media/aEmVU6biPDEVrEVvwq/giphy.gif">
</center>

<br/>

Right click on actions to edit or remove them.

<center>
    <img src="https://media0.giphy.com/media/tHiPAVrTKrKFMdNDXI/giphy.gif">
</center>


# Tips

* Closing BindKey will cause the application to run in the background and can be accessed via the system tray.

* A key binding is not required for an action. This can be helpful if you wish to create an action for the sole purpose of being chained together with a parent action.

* BindKey will search for a 'save.bk' file in the root directory which will store the data from the last session. BindKey must be closed or saved via the system tray to create a save file.
