# TheDfromSOLID
Proof of Concept - Dependency Injection

The idea is to show how this works on C#, decoupling all dependencies from a class, and injecting the dependencies as needed.

InputHubReader reads content from a Hub every X time and dumps that content to a DumpSystem.
Reading interval is defined in the Configuration, as well as a general purpose Temporal Folder

InputHubReader
  Dependencies: Hub, Configuration, DumpSystem

On program start, pass to InputHubReader the implementations it needs.

