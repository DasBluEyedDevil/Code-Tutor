---
type: "THEORY"
title: "The Three Layers of MVVM"
---

MVVM stands for **Model-View-ViewModel**. These are the three layers that make up the architecture. Let us understand each one using a restaurant analogy.

### The Restaurant Analogy

Imagine a restaurant:
- **Model = Ingredients in the kitchen** - Raw data. The chicken, vegetables, spices. They do not know how to cook themselves or how to be served. They just exist.
- **ViewModel = The Chef** - Takes ingredients (Model), prepares them according to recipes (business logic), and decides what goes on each plate. The chef never serves customers directly.
- **View = The Presentation** - The plate, the garnish, the table setting. How food looks when served. The waiter brings it to you. This is what customers (users) see.

### In Code Terms

**Model**: Pure data classes. No logic, no Flutter imports, just data structure.

**ViewModel**: Contains business logic and state. Transforms data from Model into something the View can display. Handles user actions and updates state.

**View**: Flutter widgets that display data and capture user input. Contains ONLY UI code, no business logic.

### Why Three Layers?

Each layer has exactly ONE job:
- Model answers: "What does the data look like?"
- ViewModel answers: "What should happen with the data?"
- View answers: "How should the data appear on screen?"

```dart
// VISUAL REPRESENTATION OF MVVM
//
//  +------------------+
//  |      VIEW        |  <- Flutter Widgets (UI only)
//  |   (Presentation) |     Displays data, captures user input
//  +--------+---------+
//           |
//           | watches state, sends user actions
//           v
//  +--------+---------+
//  |    VIEWMODEL     |  <- Riverpod Notifier (Logic + State)
//  | (Business Logic) |     Processes actions, updates state
//  +--------+---------+
//           |
//           | reads/writes data
//           v
//  +--------+---------+
//  |      MODEL       |  <- Pure Dart classes (Data only)
//  |      (Data)      |     Defines data structure
//  +------------------+
```
