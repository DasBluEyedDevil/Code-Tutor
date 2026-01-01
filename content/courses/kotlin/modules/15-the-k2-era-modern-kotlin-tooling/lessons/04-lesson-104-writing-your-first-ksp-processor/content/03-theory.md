---
type: "THEORY"
title: "KSP Architecture"
---


### How KSP Works

KSP processes Kotlin source files and can generate new source files:

```
Kotlin Source -> KSP Processor -> Generated Kotlin Source
     |                |                    |
  @AutoBuilder    Process symbols    UserBuilder.kt
  data class     Generate code
  User(...)
```

**Key Components**

1. **SymbolProcessor** - Your processor implementation
2. **SymbolProcessorProvider** - Factory for creating processors
3. **Resolver** - Provides access to symbols (classes, functions, etc.)
4. **CodeGenerator** - Creates output files
5. **KSPLogger** - Logs messages and errors

---

