# WDL2CS

WDL to C# transpiler for Acknex3 Engine

## What is this?

This is a playground project for some work with context free grammar and compiler building. It takes the WDL syntax of the long abandoned Acknex3 raycaster engine as input. It may in far future be added to [Wm3Util](https://github.com/firoball/Wm3Util) in order to achieve a more complete conversion of Ackex3 levels for Unity. That's a huge step to take, though, so don't expect it to happen in the near future.

## How does it work?

WDL2CS makes use of the freely available [AtoCC](https://atocc.de) toolchain which is based on the [Yacc](https://de.wikipedia.org/wiki/Yacc) compiler.
Workflow is as follows:

![Process](process.jpg)

__Important:__ At this very early stage the transpiler __does not do anything productive__ yet. It can be tested against actual WDL files in order to verify whether parsing works as intended or not.
The generated code can be hooked into my [Acknex3 C# Api](https://github.com/firoball/AcknexCSApi) project and reviewed for syntax errors.


## Step by step instruction

### Preparation

* Download and install [AtoCC](https://atocc.de) tool chain. The included tools **kfgEdit** and **VCC** will be used for generation.
* Copy `vcc\yacc.ctpl` to same folder where your **vcc.exe** is located. Make sure to keep a backup of the original file.
* The optimized parser requires some manual patching and recompilation. For easier patching you can add the Notepad++ macro `meta\regex_helpers\regex_npp_macro.xml` to your `shortcuts.xml` (requires **Notepad++**). 
* Install a recent version of [Microsoft Visual Studio](https://visualstudio.microsoft.com/de/downloads/) (Community edition for C#).

### Workflow

The AtoCC tool chain is pretty limited. With this transpiler getting more and more complex, several manual patches were required to be introduced in the somewhat tedious build process.
Below, a step by step list is provided in order to get a successful compile done.
The transpiler turned out to be very slow, which was a major roadblock. It showed, that the code generator produces very inefficient code. Unfortunately, that part is hardcoded into **VCC**, therefore some more manual patching was introduced. The optimized version processes large chunks of `.wdl` files in several seconds, whereas the non-optimized version takes minutes.

#### Grammar

* Define grammar in `kfg\parser.txt`
* Open **kfgEdit** and load `kfg\parser.txt`.
* Hit **Export Compiler** and save `vcc\parser_vcc.xml`. **VCC** will open automatically.

#### Tokenizer and Generator

* If not open, start **VCC** and load `vcc\parser_vcc.xml`. Make sure compile option for **C#** is set.
* Hit **Create Compiler** and check for any reported conflicts. Don't save any code yet.
* Save `vcc\parser_vcc.xml`.
* Open `vcc\parser_vcc.xml` in text editor of choice.
* Apply any new regular expressions to `vcc\regex.txt`.
* Copy contents of `vcc\regex.txt` to `vcc\parser_vcc.xml` between `<scanner>..</scanner>` tags.
* Save `vcc\parser_vcc.xml` and reload file in **VCC**.
* Hit **Create Compiler** and save generated code to `code\parser.cs`. **VCC** will abort compilation due to a `#warning` pragma. This is intended.

#### Patch optimized transpiler

* Locate _FindToken_ function in `code\parser.cs`.
* Copy all `if (Regex.IsMatch(..){..}` statements into a new file in **Notepad++**. Make sure order remains as generated.
* Run the **WDL2CS** macro (see _preparation_ chapter) and copy the result to clipboard.
* Locate line `#warning Place tList and rList init in MyCompiler(){} (...)` in `code\parser.cs`.
* Replace with previously copied content and save.

#### Compile transpiler

* Open `code\WDL2CS Transpiler.sln` in **Visual Studio** and build project.

#### Run transpiler

* Run `parser <file>` or `parser -t <file>` (for listing all identified tokens) from command line.
* An example for parsing through all files in a specific folder is provided: `test\test.bat`
* If preferred, output can be redirected to file (append `> out.cs` to command line) or output file name can be provided as additional parameter.

## Architecture and Workflow 

![Architecture](architecture.jpg)

The parser code is generated through **VCC** tool (and manually patched afterwards). Any function hooked in the parser configuration file is strictly provided by a static interface class. Behind the static layer the actual core of the transpiler is abstracted. These parts are maintained manually and unrelated to the workflow of the **VCC** tool.

![Workflow](workflow.jpg)

Since the parser operates string-based, any object represented by an internal data structure is serialized to a string-based stream. Once all incoming tokens have been processed, the whole stream is deserialized again into internal data structures. Based on certain criteria like required initialization or preprocessor conditions, all data is sorted.
As last step every object is formatted, in the sense of the corresponding C# code is generated.

## Current status

The transpiler successfully parses and accepts WDL files of ten different games. After hitting very long execution times due to intense regex usage, the transpiler code generated by **VCC** was reviewed and optimized.
Due to the limits of [AtoCC](https://atocc.de), manual patches are required in several places of the build process.

## Next steps

High-level road map:
* [x] define grammar and token regex
* [x] define WDL API for C# (separate project, in progress)
* [x] add token to script generator logic
* [ ] export C# scripts <-- __HERE__
* [ ] test exported code against AckexCSApi <-- __HERE__

## Compatibility

Following Acknex3 games have been transpiled successfully:

* Abiventure 2 (Crew99)
* Adeptus (Conitec)
* Angst (ManMachine Games)
* Deathman's Island (Lutz Hüls)
* Floriansdorf (BigByte Software)
* Lord of Lightning (Alex Seifriz)
* Nightfreeze (Dennis Lenz)
* oPDemo3 (oP Group Germany)
* Pibbgame
* Skaphander Demo (oP Group Germany)
* Streetlife (Madhouse Games)
* Tasty Temple Challenge
* Tyrannizer Demo (Viper Byte Software)
* Vampira (CWR-Spiele)
* Varghina Incident / Alien Anarchy (except SDROME map) (Perceptum Informática)
* VRDemo (oP Group Germany)
* VVL (CWR-Spiele) 
* World of Kandoria - Contest version (mine)

Source code is not freely available for all of the listed games, therefore transpiled code is not uploaded to GitHub.

Following Acknex3 games currently are **not** supported by the transpiler:

* Der Name des Bruders (huge WDL files lead to breakdown of parser)
* VR Messe (Name clashes between different object types and actions)

## Legal stuff

Please respect [license.txt](license.txt) (Attribution-NonCommercial 4.0 International)

-firoball

[https://firoball.de](https://firoball.de)
