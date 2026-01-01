---
type: "ANALOGY"
title: "Think of Navigation as a Stack of Cards"
---


Imagine a deck of cards:
- **Push**: Add a card on top (new screen covers current)
- **Pop**: Remove top card (go back to previous screen)


This is called a **navigation stack**!



```dart
[Home Screen]
[Home Screen] → Push → [Home Screen, Detail Screen]
[Home Screen, Detail Screen] → Pop → [Home Screen]
```
