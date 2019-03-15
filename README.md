# TheDfromSOLID
Proof of Concept - Dependency Injection

The idea is to show how this works on C#, decoupling all dependencies from a class, and injecting the dependencies as needed.

InputHubReader
  Dependencies: Hub, Configuration, DumpSystem

On program start, pass to InputHubReader the implementations it needs.

