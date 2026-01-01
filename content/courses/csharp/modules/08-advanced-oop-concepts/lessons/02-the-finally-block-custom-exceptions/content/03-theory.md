---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`finally { cleanup code }`**: Finally block runs AFTER try and catch, ALWAYS! Use for cleanup: closing files, releasing resources. Runs even if exception is thrown.

**`class CustomException : Exception`**: Create custom exception by inheriting from Exception class. Naming convention: end with 'Exception' (InvalidAgeException, not InvalidAge).

**`: base(message)`**: Call base constructor with error message. This passes the message to the Exception class, so ex.Message works.

**`throw new CustomException()`**: 'throw' keyword creates and throws an exception. Program immediately jumps to nearest catch block that handles this exception type.

**`using statement`**: Modern C# alternative to finally for disposable resources: `using (var file = File.OpenRead(path)) { }` automatically closes file when block ends. Even cleaner: `using var file = File.OpenRead(path);` (C# 8+).