---
type: "ANALOGY"
title: "The Architectural Blueprint"
---

Starting a capstone project without proper architecture is like building a house without blueprints. You might get the foundation poured and the walls up, but when you try to add plumbing, you realize you forgot to leave space for pipes. When you add the electrical system, the wires conflict with the plumbing. Each new system makes the previous ones harder to maintain.

Project setup and architecture planning is your blueprint phase. You decide where the load-bearing walls go (core modules), where the plumbing runs (data layer), where the electrical wiring routes (state management), and where the doors connect rooms (navigation). Changing a blueprint is cheap. Changing a half-built house is expensive.

**The feature-first folder structure is your room layout.** Auth gets its own wing, chat gets its own wing, and shared utilities go in the basement (core/). When you need to add a new room later, you add it without tearing down existing walls. This is why the first lesson of any capstone is always architecture -- not because it is the most exciting work, but because it makes all the exciting work possible.
