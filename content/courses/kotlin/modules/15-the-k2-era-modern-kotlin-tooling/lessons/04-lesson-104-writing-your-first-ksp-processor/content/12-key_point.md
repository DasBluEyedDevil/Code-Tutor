---
type: "KEY_POINT"
title: "Key Takeaways"
---

**KSP processors implement `SymbolProcessor` interface**, processing annotated declarations during compilation. Generate code by visiting symbols (classes, functions, properties) and writing files via `CodeGenerator`.

**KSP provides a visitor-based API** for traversing Kotlin code structure. Override `visitClassDeclaration`, `visitFunctionDeclaration`, etc., to inspect annotated elements and generate corresponding code.

**KSP processors run during compilation**, so generated code is immediately available to the rest of your build. Use KSP for dependency injection, serialization, database DAOs, and other boilerplate generation.
