---
type: "THEORY"
title: "Use-Site Variance: Type Projections"
---


You can specify variance at the use site instead of the declaration site:


---



```kotlin
class Box<T>(var item: T)

fun copyFrom(from: Box<out Animal>, to: Box<Animal>) {
    to.item = from.item  // ✅ Can read from 'from'
}

fun copyTo(from: Box<Animal>, to: Box<in Animal>) {
    to.item = from.item  // ✅ Can write to 'to'
}

fun main() {
    val dogBox = Box(Dog())
    val animalBox = Box<Animal>(Cat())

    copyFrom(dogBox, animalBox)  // ✅ Works with out projection
}
```
