---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Annotations add metadata to code** that frameworks can process at compile-time or runtime. They enable declarative programming patterns like dependency injection, serialization, and validation without explicit boilerplate.

**Reflection provides runtime introspection** of classes, properties, and functions. Use it for frameworks and tooling, but avoid it in application logic—reflection is slow and circumvents type safety.

**Kotlin reflection (`kotlin-reflect`) is more powerful than Java reflection**, offering access to nullability, extension functions, and other Kotlin-specific features. However, it adds ~2MB to your binary, so use judiciously in mobile apps.
