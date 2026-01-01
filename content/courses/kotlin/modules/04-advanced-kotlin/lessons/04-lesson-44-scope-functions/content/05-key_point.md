---
type: "KEY_POINT"
title: "📊 SCOPE FUNCTIONS CHEAT SHEET - Print This!"
---


**THE DECISION FLOWCHART:**

```
                    What do you want to do?
                           │
           ┌───────────────┼───────────────┐
           ▼               ▼               ▼
      Transform        Configure       Side Effect
      (compute)        (mutate)        (log/debug)
           │               │               │
    ┌──────┴──────┐   ┌───┴───┐       ┌───┴───┐
    ▼             ▼   ▼       ▼       ▼       ▼
  Null?      Non-null this   it    this     it
    │            │    │       │      │       │
    ▼            ▼    ▼       ▼      ▼       ▼
   LET          RUN  APPLY  N/A   N/A     ALSO
              or WITH
```

**QUICK COMPARISON TABLE:**

| Function | Access Object | Returns | Use When |
|----------|--------------|---------|----------|
| **let**  | `it.name`    | result  | Null checks, transform |
| **run**  | `name`       | result  | Compute something |
| **with** | `name`       | result  | Many operations, have object |
| **apply**| `name`       | object  | Configure, build |
| **also** | `it.name`    | object  | Log, validate, debug |

**MEMORY TRICK:**
- **L**et = **L**ambda result, **L**et me transform it
- **R**un = **R**esult focus, **R**un some calculations
- **A**pply = **A**pply configuration, **A**nd return object
- **A**lso = **A**lso do this (side effect), **A**nd return object
- **W**ith = **W**ork with existing object, **W**rap operations

**CODE COMPARISON:**
```kotlin
val user: User? = getUser()

// LET: Null-safe transformation
user?.let { println(it.name) }       // Uses 'it'

// RUN: Compute result
user?.run { "$name: $email" }        // Uses 'this' (omitted)

// APPLY: Configure and return object
val newUser = User().apply {
    name = "Alice"                    // Uses 'this' (omitted)
    email = "alice@email.com"
}

// ALSO: Side effect, returns object
val logged = user.also {
    println("Processing: ${it.name}") // Uses 'it'
}

// WITH: Multiple operations on existing object
val info = with(user) {
    println(name)                     // Uses 'this' (omitted)
    "$name ($email)"
}
```

**RULE OF THUMB:**
- Need the result of a calculation? → `let`, `run`, `with`
- Need the object back for chaining? → `apply`, `also`
- Working with nullable? → `let` (or `run` with `?.`)
- Configuring an object? → `apply`
- Adding logging/debugging? → `also`

---

