---
type: "THEORY"
title: "Project Overview"
---

Time to put everything together! In this mini-project, you will refactor a messy Notes app into clean MVVM architecture.

### What We're Starting With

A Notes app where everything is crammed into one StatefulWidget:
- Notes stored directly in widget state
- CRUD methods mixed with UI code
- No separation of concerns
- Hard to test, hard to maintain

### What We're Building

A properly architected Notes app with:
- **Model**: Clean `Note` class with immutable data
- **ViewModel**: `NotesViewModel` with Riverpod handling all logic
- **View**: Clean `ConsumerWidget` that only handles UI

### Why This Matters

The messy version works, but it has problems:
- Cannot test business logic without testing UI
- Cannot share notes state with other screens
- Hard to add features without breaking things
- Difficult for teams to work on

The refactored version solves all of these issues.

### The Refactoring Process

We will follow three simple steps:
1. **Extract the Model**: Create a proper `Note` class
2. **Extract the ViewModel**: Move all logic to a Riverpod provider
3. **Clean up the View**: Make the widget only handle UI