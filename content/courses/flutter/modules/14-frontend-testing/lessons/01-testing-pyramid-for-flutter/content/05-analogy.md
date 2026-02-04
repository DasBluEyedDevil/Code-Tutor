---
type: "ANALOGY"
title: "Building Inspection Levels"
---

Testing a Flutter app is like inspecting a building at different levels. **Unit tests** are like checking individual bricks -- is this brick the right size? Does this function return the correct value? You can test thousands of bricks in seconds because each check is tiny and isolated. **Widget tests** are like inspecting a wall -- do the bricks fit together properly? Does this login form display the right fields and respond to taps? You test components in combination, but still in a controlled environment. **Integration tests** are like a building walkthrough -- does the front door open, can you walk to the elevator, and does the elevator reach every floor? You test the entire app running on a real device.

The pyramid shape matters: you want many unit tests (fast, cheap, precise), fewer widget tests (moderate speed, test component interactions), and just a handful of integration tests (slow, expensive, but cover real user journeys). If you invert the pyramid and rely mostly on integration tests, your test suite will be slow, flaky, and expensive to maintain.

**A failing unit test tells you exactly which brick is broken. A failing integration test tells you the building has a problem somewhere -- but finding the broken brick takes detective work.**
