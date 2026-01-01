---
type: "ANALOGY"
title: "The Concept: What Are DSLs?"
---


### Internal vs External DSLs

**External DSL**: A separate language with its own parser (like SQL, regex)


**Internal DSL**: Built within the host language (Kotlin)


### Why DSLs in Kotlin?

Kotlin DSLs are readable, type-safe, and have IDE support:


---



```kotlin
// Without DSL
val table = Table()
table.setWidth("100%")
val row = Row()
val cell = Cell()
cell.setText("Hello")
row.addCell(cell)
table.addRow(row)

// With DSL
table {
    width = "100%"
    row {
        cell { text = "Hello" }
    }
}
```
