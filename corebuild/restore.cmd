@if not defined _echo @echo off

REM restore.cmd will bootstrap the cli and ultimately call "dotnet
REM restore". The configuration specified here is just a workaround to
REM set the correct conditional properties for the netcore/netstandard
REM restore in the .csproj files. It doesn't matter whether we choose
REM Debug or Release here, as the same assets will be restored in either
REM case.

REM Normally, "dotnet restore" will also restore referenced
REM projects. However, because we are using the old .csproj format,
REM restore will not correctly restore project references, and so we
REM need to explicitly restore Mono.Cecil as well as Mono.Linker.

@call dotnet.cmd restore ..\linker\Mono.Linker.csproj /p:Configuration=netcore_Debug %*
@call dotnet.cmd restore ..\cecil\Mono.Cecil.csproj /p:Configuration=netstandard_Debug %*
@call dotnet.cmd restore ..\cecil\symbols\pdb\Mono.Cecil.Pdb.csproj /p:Configuration=netstandard_Debug %*
@exit /b %ERRORLEVEL%