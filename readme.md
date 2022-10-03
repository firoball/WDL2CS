# WDL2CS

WDL to C# transpiler for Acknex3

## What is this?

This is a playground project for some work with context free grammar and compiler building. It takes the WDL syntax of the long abandoned Acknex3 raycaster engine as input. It may in far future be added to [Wm3Util](https://github.com/firoball/Wm3Util) in order to achieve a more complete conversion of Ackex3 levels for Unity. That's a huge step to take, though, so don't expect it to happen in the near future.

## How does it work?

WDL2CS makes use of the freely available [AtoCC](https://atocc.de) toolchain which is based on the [Yacc](https://de.wikipedia.org/wiki/Yacc) compiler.
Workflow is as follows:

![Process](process.jpg)

__Important:__ At this very early stage the transpiler __does not do anything productive__ yet. It can be tested against actual WDL files in order to verify whether parsing works as inteded or not.

## Next steps

High-level road map:
* [ ] define grammar and token regex <-- __HERE__
* [ ] define WDL API for C#
* [ ] add token to script generator logic
* [ ] export C# scripts


## Legal stuff

Please respect [license.txt](license.txt) (Attribution-NonCommercial 4.0 International)

-firoball

[https://firoball.de](https://firoball.de)
