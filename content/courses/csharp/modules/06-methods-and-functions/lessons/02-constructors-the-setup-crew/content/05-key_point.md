---
type: "KEY_POINT"
title: "Constructor Patterns"
---

## Key Takeaways

- **Constructors have the class name and no return type** -- `public Player(string name, int hp)` runs automatically when you call `new Player("Alice", 100)`. Use them to enforce required initialization.

- **Primary constructors (C# 12+) reduce boilerplate** -- `class Enemy(string name, int damage)` puts parameters right after the class name. They are available throughout the class body.

- **Validate in constructors** -- if `hp` must be positive, throw `ArgumentException` in the constructor. An object should never exist in an invalid state.
