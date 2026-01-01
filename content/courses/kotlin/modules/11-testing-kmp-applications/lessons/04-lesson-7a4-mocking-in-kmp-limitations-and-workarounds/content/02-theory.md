---
type: "THEORY"
title: "Why Traditional Mocking Doesn't Work"
---

### Platform-Specific Limitations

**Mockito** relies on:
- JVM bytecode manipulation
- Java reflection
- Runtime class generation

These don't exist on iOS (native) or JS platforms.

```kotlin
// ❌ This won't compile in commonTest
import org.mockito.Mockito.mock

@Test
fun test() {
    val mockRepo = mock<NoteRepository>()  // Compiler error!
}
```

### Available Options

| Approach | Works in commonTest | Pros | Cons |
|----------|---------------------|------|------|
| **Fakes (manual)** | ✅ | Simple, reusable, portable | More code to write |
| **MockK** | ❌ (JVM only) | Powerful, Kotlin-native | Platform-specific |
| **Mokkery** | ✅ | KMP-compatible mocking | Newer, less mature |
| **Interfaces + fakes** | ✅ | Standard pattern | Requires discipline |