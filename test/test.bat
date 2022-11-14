@echo off
echo %TIME% Running Parser...
echo.
for %%f in (*.wdl) do ..\code\parser.exe %%f || echo. %%f
echo.
echo %TIME% Finished.