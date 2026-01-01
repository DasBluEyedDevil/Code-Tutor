---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine your shopping list on your phone:

<pre style='background-color: #f0f0f0; padding: 10px;'>Shopping List:
1. Milk
2. Eggs
3. Bread
4. Apples
5. Chicken
</pre>This is a **list** - an ordered collection of items. Key properties:

- **Ordered**: Items have a specific sequence (Milk is first, Chicken is last)
- **Numbered**: Each item has a position (1st, 2nd, 3rd...)
- **Flexible**: You can add items, remove items, change items
- **Mixed types allowed**: Could include quantities ("2 dozen eggs")

### Python Lists Work the Same Way!
In Python, a **list** is an ordered collection that can store multiple values:

```
shopping_list = ["Milk", "Eggs", "Bread", "Apples", "Chicken"]

```
### Why Use Lists?
Instead of this mess:

```
item1 = "Milk"
item2 = "Eggs"
item3 = "Bread"
item4 = "Apples"
item5 = "Chicken"

```
You get this elegance:

```
shopping = ["Milk", "Eggs", "Bread", "Apples", "Chicken"]

```
### Real-World Examples:

- **Playlist**: ["Song 1", "Song 2", "Song 3"] - order matters!
- **Test scores**: [85, 92, 78, 95, 88] - multiple numbers
- **Todo list**: ["Study", "Exercise", "Call mom"] - tasks in order
- **Game inventory**: ["Sword", "Shield", "Potion", "Map"] - items collected
- **Temperature readings**: [72, 75, 73, 71, 70] - hourly data

### Key Concepts:
**1. Lists use square brackets: [ ]**

```
numbers = [1, 2, 3, 4, 5]
names = ["Alice", "Bob", "Charlie"]
mixed = ["Alice", 25, True, 3.14]  # Different types OK!
empty = []  # Empty list

```
**2. Lists are indexed (numbered) starting at 0**

```
fruits = ["Apple", "Banana", "Cherry"]

# Position:  0        1         2
#           Apple   Banana   Cherry

```
Python uses **zero-based indexing** - the first item is at position 0, not 1!

**3. You can access items by their index**

```
print(fruits[0])  # Apple (first item)
print(fruits[1])  # Banana (second item)
print(fruits[2])  # Cherry (third item)

```
**4. Negative indexing counts from the end**

```
print(fruits[-1])  # Cherry (last item)
print(fruits[-2])  # Banana (second to last)
print(fruits[-3])  # Apple (third from end)

```
### Why Zero-Based Indexing?
Think of indexing as "how many steps from the start":

- Index 0: Zero steps from start (first item)
- Index 1: One step from start (second item)
- Index 2: Two steps from start (third item)

Most programming languages use this system!