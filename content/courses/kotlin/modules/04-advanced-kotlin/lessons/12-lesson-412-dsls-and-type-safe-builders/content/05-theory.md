---
type: "THEORY"
title: "Type-Safe Builders"
---


Type-safe builders use lambdas with receiver to create hierarchical structures.

### Simple Example: List Builder


### Nested Builders


---



```kotlin
class Item(val name: String)

class ItemList {
    private val items = mutableListOf<Item>()

    fun item(name: String) {
        items.add(Item(name))
    }

    fun getItems(): List<Item> = items
}

class ShoppingList {
    private val lists = mutableListOf<ItemList>()

    fun category(name: String, action: ItemList.() -> Unit) {
        println("Category: $name")
        val list = ItemList()
        list.action()
        lists.add(list)
    }

    fun getAllItems(): List<Item> = lists.flatMap { it.getItems() }
}

fun shoppingList(action: ShoppingList.() -> Unit): ShoppingList {
    val list = ShoppingList()
    list.action()
    return list
}

fun main() {
    val list = shoppingList {
        category("Fruits") {
            item("Apple")
            item("Banana")
            item("Orange")
        }

        category("Vegetables") {
            item("Carrot")
            item("Broccoli")
        }
    }

    println("\nAll items:")
    list.getAllItems().forEach { println("  - ${it.name}") }
}
```
