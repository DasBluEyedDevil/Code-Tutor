---
type: "THEORY"
title: "Choosing the Right Approach"
---


When should you use each style?

### Decision Matrix

| Scenario | Best Choice | Example |
|----------|-------------|---------|
| Simple operation on single parameter | Lambda with `it` | `numbers.map { it * 2 }` |
| Complex operation or nested lambdas | Lambda with named parameter | `orders.map { order -> order.calculate() }` |
| Existing function matches signature | Function reference | `numbers.filter(::isEven)` |
| Need explicit returns | Anonymous function | `fun(x) { if(x < 0) return false; return true }` |
| Calling method on each element | Member reference | `people.map(Person::name)` |

### Examples of Each


---



```kotlin
// Lambda with 'it': simple operations
val doubled = numbers.map { it * 2 }
val filtered = numbers.filter { it > 10 }

// Lambda with named parameter: complex or nested
val processed = orders.map { order ->
    order.items.filter { item -> item.price > 100 }
}

// Function reference: existing function
fun isValid(s: String) = s.isNotEmpty() && s.length > 3
val valid = strings.filter(::isValid)

// Member reference: calling methods/properties
val names = people.map(Person::name)
val adults = people.filter(Person::isAdult)

// Anonymous function: explicit returns
val result = numbers.firstOrNull(fun(n): Boolean {
    if (n < 0) return false
    return n % 2 == 0
})
```
