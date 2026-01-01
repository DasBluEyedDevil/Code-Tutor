---
type: "KEY_POINT"
title: "When to Use Each Collection"
---

USE ARRAYLIST WHEN:
✓ You access elements by index frequently
✓ You mostly add to the end
✓ You iterate through all elements
✓ MOST COMMON USE CASE (default choice)

Example: Displaying a list of products

USE LINKEDLIST WHEN:
✓ You frequently insert/remove at the beginning
✓ You implement a Queue (FIFO)
✓ You implement a Stack (LIFO)
✓ You rarely access by index

Example: Task queue where tasks are added/removed from front

USE HASHMAP WHEN:
✓ You need key-value pairs
✓ You need fast lookup by key
✓ Order doesn't matter

Example: User profiles by username

RULE OF THUMB: Start with ArrayList unless you have a reason not to!