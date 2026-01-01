---
type: "EXAMPLE"
title: "How Kotlin Code Becomes a Running Program"
---


This is what happens when you click "Run":


**Step-by-Step**:

1. **You write code** in a `.kt` file (Kotlin source file)
2. **Kotlin Compiler** translates your code into **bytecode**
3. **Bytecode** is a language the JVM understands
4. **JVM** (Java Virtual Machine) runs the bytecode
5. **Output** appears on your screen

**Why JVM?**
- JVM is incredibly mature and optimized (30+ years old)
- Works on Windows, macOS, Linux, and more
- Kotlin leverages all of Java's ecosystem

---



```kotlin
Your Code (Main.kt)
        ↓
   [Kotlin Compiler]
        ↓
   Bytecode (.class files)
        ↓
   [Java Virtual Machine (JVM)]
        ↓
   Running Program (Output)
```
