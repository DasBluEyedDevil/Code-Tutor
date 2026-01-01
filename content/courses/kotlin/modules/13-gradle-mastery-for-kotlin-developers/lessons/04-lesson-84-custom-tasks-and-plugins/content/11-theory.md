---
type: "THEORY"
title: "Exercise: Create Build Info Task"
---


**Goal**: Create a task that generates a `BuildInfo.kt` file.

**Requirements**:
1. Generate file with version, build time, and git hash
2. Output to `build/generated/BuildInfo.kt`
3. Make compileKotlin depend on it
4. Declare proper inputs/outputs for caching
5. Only regenerate when version changes

---

