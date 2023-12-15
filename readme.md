# RoNSaveViewer

This is a .NET WPF Application for reading and writing standard Unreal Engine 4 save game files. This library should work with most games which do not use any custom serialization, but may require updating for games it hasn't seen before.

## Games Tested

The following games have been tested using this library. Testing consists of loading a file, saving it, and checking that the output is binary equal to the input.

### Working
* Ready or Not
* Ghostrunner 2 (partially)
