---
type: "KEY_POINT"
title: "LinkedList is Like a Chain of People Holding Hands"
---

ARRAYLIST:
= People standing in numbered spots
[Person 0] [Person 1] [Person 2] [Person 3]
- To add someone in middle: everyone shifts spots
- Access by position: very fast (just go to spot 2)

LINKEDLIST:
= Chain of people holding hands
(Alice) → (Bob) → (Carol) → (Dave)
- Each person holds hand of next person
- To insert: break one handhold, insert new person, reconnect
- Access by position: must walk from start (slower)

LinkedList: Fast insertion, slower access
ArrayList: Slower insertion, fast access