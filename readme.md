# WDL2CS

WDL to C# transpiler for Acknex3 Engine

## What is this?

This is a playground project for some work with context free grammar and compiler building. It takes the WDL syntax of the long abandoned Acknex3 raycaster engine as input. It may in far future be added to [Wm3Util](https://github.com/firoball/Wm3Util) in order to achieve a more complete conversion of Ackex3 levels for Unity. That's a huge step to take, though, so don't expect it to happen in the near future.

## How does it work?

WDL2CS makes use of the freely available [AtoCC](https://atocc.de) toolchain which is based on the [Yacc](https://de.wikipedia.org/wiki/Yacc) compiler.
Workflow is as follows:

![Process](process.jpg)

__Important:__ At this very early stage the transpiler __does not do anything productive__ yet. It can be tested against actual WDL files in order to verify whether parsing works as intended or not.

## Step by step instruction

### Preparation

* Download and install [AtoCC](https://atocc.de) tool chain. The included tools **kfgEdit** and **VCC** will be used for generation.
* Copy `vcc\yacc.ctpl` or `vcc\yacc_opt.ctpl` (rename!) to same folder where your **vcc.exe** is located. Make sure to keep a backup of the original file.
* The optimized parser requires some manual patching and recompilation. For easier patching you can add the Notepad++ macro `meta\regex_helpers\regex_npp_macro.xml` to your `shortcuts.xml` (requires **Notepad++**). 

### Workflow

The AtoCC tool chain is pretty limited. With this transpiler getting more and more complex, several manual patches were required to be introduced in the somewhat tedious build process.
Below, a step by step list is provided in order to get a successful compile done.
The transpiler turned out to be very slow, which was a major roadblock. It showed, that the code generator produces very inefficient code. Unfortunately, that part is hardcoded into **VCC**, therefore some more manual patching had to be introduced. The optimized version processes large chunks of `.wdl` files in several seconds, whereas the non-optimized version takes minutes.

#### Grammar

* Define grammar in `kfg\parser.txt`
* Open **kfgEdit** and load `kfg\parser.txt`.
* Hit **Export Compiler** and save `vcc\parser_vcc.xml`. **VCC** will open automatically.

#### Tokenizer and Generator

* If not open, start **VCC** and load `vcc\parser_vcc.xml`. Make sure compile option for **C#** is set.
* Hit **Create Compiler** and check for any reported conflicts. Don't save any code yet.
* Save `vcc\parser_vcc.xml`.
* Open `vcc\parser_vcc.xml` in text editor of choice.
* Apply any new regular expressions to `vcc\regex.txt` or `vcc\regex_opt.txt` (optimized, more complex regex).
* Copy contents of `vcc\regex.txt` or `vcc\regex_opt.txt` to `vcc\parser_vcc.xml` between `<scanner>..</scanner>` tags.
* Save `vcc\parser_vcc.xml` and reload file in **VCC**.
* Hit **Create Compiler** and save generated code to `code\parser.cs`. **VCC** will automatically compile `code\parser.exe`

#### Patch and compile optimized transpiler

Following steps are only required when `vcc\yacc_opt.ctpl` is used for building an optimized executable!

* Locate _FindToken_ function in `code\parser.cs`.
* Copy all `if (Regex.IsMatch(..){..}` statements into a new file in **Notepad++**. Make sure order remains as generated.
* Run the **WDL2CS** macro (see _preparation_ chapter) and copy the result to clipboard.
* Locate line `#warning Place tList and rList init in MyCompiler(){} (...)` in `code\parser.cs`.
* Replace with previously copied content and save.
* Run `csc parser.cs` from `code`folder. `code\parser.exe` should be compiled without error.

#### Run transpiler

* Run `parser <file>` or `parser -t <file>` (for listing all identified tokens).
* An example for parsing through all files in a specific folder is provided: `test\test.bat`

## Current status

The transpiler successfully parses and accepts WDL files of ten different games. After hitting very long execution times due to intense regex usage, the transpiler code generated by **VCC** was reviewed and optimized.
Due to the limits of [AtoCC](https://atocc.de), manual patches are required in several places of the build process.

## Next steps

High-level road map:
* [x] define grammar and token regex
* [x] define WDL API for C# (separate project, in progress)
* [ ] add token to script generator logic <-- __HERE__
* [ ] export C# scripts <-- __HERE__


## Legal stuff

Please respect [license.txt](license.txt) (Attribution-NonCommercial 4.0 International)

-firoball

[https://firoball.de](https://firoball.de)
