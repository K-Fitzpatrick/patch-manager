[![GUI](https://img.shields.io/github/v/release/K-Fitzpatrick/patch-manager)](https://github.com/K-Fitzpatrick/patch-manager/releases)
[![Nuget](https://img.shields.io/nuget/v/patch-applier)](https://www.nuget.org/packages/patch-applier/)

# Patch Manager
1. A program to apply IPS and Assembly Code patches to a base file in a specific order.
1. A C# library/NuGet package to do the above on the fly from within other C# projects

The primary purpose is Super Metroid Romhacking, where knowing the exact composition of your ROM and being able to recreate it is very useful.

## NuGet Package
The patch-applier NuGet package provides methods for:

1. Downloading patches from arbitrary URLs and locations on your local computer.
    - Unzipping specific patches from the downloaded/retreived files is supported
1. Applying IPS and ASM patches in a specific order, given a list of URLs

Download here: [![Nuget](https://img.shields.io/nuget/v/patch-applier)](https://www.nuget.org/packages/patch-applier/)

## GUI Tool
This application will read the given JSON file, and download/apply patches as specified to the given base ROM.

[See here for an example](patchfile.json).

If you use relative paths, make them relative to the patchfile itself. Example: If you want the output in the same directory as the patchfile, you would say "./[file-name].smc".

Released right here on GitHub: [![Release](https://img.shields.io/github/v/release/K-Fitzpatrick/patch-manager)](https://github.com/K-Fitzpatrick/patch-manager/releases)
