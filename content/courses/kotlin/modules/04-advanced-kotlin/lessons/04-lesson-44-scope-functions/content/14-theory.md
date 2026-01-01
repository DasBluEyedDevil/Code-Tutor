---
type: "THEORY"
title: "Decision Matrix: Which Scope Function to Use?"
---


### Flowchart


### Quick Reference

| Want to... | Use | Example |
|------------|-----|---------|
| Transform nullable value | `let` | `name?.let { it.uppercase() }` |
| Configure object | `apply` | `Person().apply { name = "Alice" }` |
| Log/debug without breaking chain | `also` | `.also { println(it) }` |
| Group operations, compute result | `run` / `with` | `person.run { age + 1 }` |
| Multiple calls on existing object | `with` | `with(config) { ... }` |

---



```kotlin
Need to transform/compute result?
├─ Yes → Returns lambda result
│  ├─ Have object already? → with
│  ├─ Need null safety? → let
│  └─ Creating/chaining? → run
│
└─ No → Returns object (chainable)
   ├─ Need configuration? → apply (this)
   └─ Need side effect? → also (it)
```
