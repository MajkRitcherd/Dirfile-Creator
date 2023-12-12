# Dirfile-Creator

## About
Dirfile-Creator allows you to create a desired file system structure from one input string. It allows you to create multiple directories, files (can have initial specified text) at once. No need to manually create directories and files one-by-one in FileExplorer. Only prepare a query in form of string to the Dirfile-Creator and it'll create them.

**Dirfile-Creator** allows you:
- **Create/Delete files** (when creating, initial text can be set into file)
- **Create/Delete directories** (even recursively)
- Create infinite number of files and directories using **one input string**.

It can be done via **Console application** or **Graphical application**, based on your preferences

**IMPORTANT**: Graphical application is not finished yet!!!

**[Note]**
This project is being developed as a side project to improve my coding skills and to ease my work.
# Documentation
### List of exposed namespaces and its content:
- [DirfileExtensions](#dirfileextensions)
- [DirfileContext](#dirfilecontext)
- [DirfileCreator](#dirfilecreator)
- [SlashMode](#slashmode)
- [PathMode](#pathmode)

## DirfileExtensions
**Namespace**: Dirfile_lib.Extensions

**Type**: Class containing enums

**About**: 
- Contains definition of file extensions grouped by enums. You can't create files, that are not defined in this namespace.

There are enums: 
- **Audio** (audio file extensions)
- **Compressed** (Compressed file extension)
- **DiscMedia** (Disc and media file extensions)
- **Data** (Data file extensions)
- **Database** (Database file extensions)
- **Email** (E-mail file extensions)
- **Executable** (Executable  file extensions)
- **Fonts** (Font file extensions)
- **Image** (Image file extensions)
- **Presentation** (Presentation file extensions)
- **Programming** (Programming file extensions)
- **Spreadsheet** (Spreadsheet file extensions)
- **System** (System file extensions)
- **Video** (Video file extensions)
- **Text** (Word and text file extensions)
- **Other** (Other file extensions)

## DirfileContext
**Namespace**: Dirfile_lib.API.Context

**Type**: Class

**About**:
- API to create **multiple** directories with files from **one input string**.
- Allows to create files with initial text.
- First parameter (optional) is **working directory** (Initial: **current working directory**).
- Second parameter (optional) is **slash mode** (Initial: **Backwards**).
- Third parameter (optional) is **PathMode** (Initial: **Absolute**).
- All of these properties can be changed later, via **ChangeCurrentDirector**, **SwitchPathMode**, **SwitchSlashMode**

## DirfileCreator
**Namespace**: Dirfile_lib.API

**Type**: Class

**About**:
- Has defined methods for creating and deleting directories and files.

**Methods**
- **CreateDirector(string, string)**
	- Creates a new directory in a passed directory and with the passed name. 
	- Takes absolute path to directory (in which to create another directory) as a first parameter.
	- The second parameter takes the name of a new directory.
	- Returns **nothing**.

- **CreateFiler(string, string)**
	-	Creates a new file in a passed directory and with the passed name.
	-	Takes absolute path to directory (in which to create a new file) as a first parameter.
	-	The second parameter takes the name of a new file.
	-	Returns **nothing**.

- **DeleteDirector(string, string, bool)**
	- Deletes directory given the parent directory, its name and whether or not to delete it recursively.
	- Takes absolute path to parent directory (where we can find our directory for deletion) as a first parameter.
	- The second parameter takes the name of a directory to delete.
	- Third parameter specifies whether or not to delete it recursively.
	- Returns **nothing**.

- **DeleteFiler(string, string)**
	- Deletes file given the parent directory and file name.
	- Takes absolute path to parent directory (where we can find our file for deletion) as a first parameter.
	- The second parameter takes the name of a file to delete.
	- Returns **nothing**.

## SlashMode
**Namespace**: Dirfile_lib.API.Extraction.Modes

**Type**: Enum

**About**: 
- Holds different types of slash mode used in [DirfileContext](#dirfilecontext).
- Used when creating [DirfileContext](#dirfilecontext) to specify which [SlashMode](#slashmode) to use when creating directories and files.
- Also it can be changed later, for more detail see [DirfileContext](#dirfilecontext).
- Only 2 types: **Forward** ('/') and **Backward** ('\').

## PathMode
**Namespace**: Dirfile_lib.API.Extraction.Modes

**Type**: Enum

**About**: 
- Holds different types of path mode to use in [DirfileContext](#dirfilecontext).
- Used when creating [DirfileContext](#dirfilecontext) to specify which [PathMode](#pathmode) to use when creating directories and files.
- Also it can be changes later, for more details see [DirfileContext](#dirfilecontext).
- Only 2 types: **Relative** and **Absolute**