# osuMusicLibExtractor

Copies all your osu! music to a new folder, while applying metadata to the mp3

---

# !! Warning !!

## This program COPIES the mp3s in bulk, this means it will effectively double the space used by these songs. If your songs dir has 75GB of mp3s, you will be copying 75GB of mp3s. With some users dirs being upwards of 500GB it may be inadvised to use this if you are low on space or worried about limited reads/writes. 

---

# For users

## GUI

![UI Image](https://github.com/Altrentorae/osuMusicLibExtractor/blob/main/oseUI.png?raw=true)

Enter your osu! song directory and your destination directory. You can open a file explorer window for this by clicking the `?` to the right of each text box.

~~Search type is recommended to be left set to `.ini index`, as this is the more accurate method. However you can attempt to use `Hierarchical`, but do not expect good results.~~

Hierarchical is no longer supported, ini is now default.

For using the UI this is all there really is to it, if there are any errors they should be displayed or logged.

## CLI

### Arguments as below

`musicLibGenerator.exe <arg1> <arg2> <arg3> <arg4>` 

* Arg 1 = your osu! songs directory, for example: `E:\osu!\Songs`
* Arg 2 = directory for the results to be copied to, for example: `E:\Music`
* Arg 3 = True or False, whether to skip songs that share a folder ID. if unsure use `True`
* Arg 4 = True or False, whether to log skipped or failed songs to file (Log.log)
* Arg 5 = True or False, whether to force existing metadata from the ORIGINAL file to be replaced

--

# For Contributors

The main project is split into two main solutions, the one doing the heavily lifting is MusicLibGenerator. This holds all the base code that actually copies the information MusicLibGeneratorGUI is just a winform wrapper that calls the main program.

The MusicLibGenerator solution only contains itself, and should only be used individually for legacy purposes. Instead open the MusicLibGeneratorGUI solution, which should in turn open the other solution, allowing you to properly change and build both more easily.
