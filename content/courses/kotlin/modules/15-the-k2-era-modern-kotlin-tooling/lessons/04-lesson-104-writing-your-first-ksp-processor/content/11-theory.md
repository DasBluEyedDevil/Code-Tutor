---
type: "THEORY"
title: "Lesson Summary"
---


### KSP Processor Development Summary

1. **Three-module structure**: annotations, processor, and app
2. **SymbolProcessor** processes annotated symbols
3. **KotlinPoet** generates clean Kotlin code
4. **Register via META-INF/services** or AutoService
5. **Test with kotlin-compile-testing-ksp**

### Key Classes

- `SymbolProcessor` - Your processor logic
- `SymbolProcessorProvider` - Factory for processor
- `Resolver` - Access to symbols
- `CodeGenerator` - Write output files
- `KSClassDeclaration` - Represents a class
- `KSPropertyDeclaration` - Represents a property

### Best Practices

- Return unvalidated symbols for reprocessing
- Log errors with `logger.error()` for clear messages
- Use `Dependencies` to track file relationships
- Test with compile-testing library

---

