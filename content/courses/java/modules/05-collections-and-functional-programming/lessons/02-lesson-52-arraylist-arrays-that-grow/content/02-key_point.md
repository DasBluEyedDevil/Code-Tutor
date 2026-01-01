---
type: "KEY_POINT"
title: "ArrayList is Like an Accordion Folder"
---

FIXED ARRAY:
= Box with 5 compartments
- Can hold exactly 5 items
- Want to add a 6th? Too bad!
- Want to remove one? Empty slot remains

ARRAYLIST:
= Accordion folder
- Starts with some capacity
- Add items → expands automatically
- Remove items → shrinks, no gaps
- Has methods: add(), remove(), get(), size()

ArrayList handles resizing for you!