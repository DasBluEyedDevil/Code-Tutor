---
type: "THEORY"
title: "The Testing Pyramid"
---


Flutter testing follows the classic testing pyramid:

**Unit Tests (Base - Most Tests)**
- Test individual functions, classes, methods
- Fast execution (milliseconds)
- No Flutter framework needed
- Example: Testing a Notifier's business logic

**Widget Tests (Middle)**
- Test individual widgets in isolation
- Medium speed (seconds)
- Uses Flutter test framework
- Example: Testing a LoginForm widget

**Integration Tests (Top - Fewest Tests)**
- Test complete user flows
- Slowest (minutes)
- Runs on real device/emulator
- Example: Testing entire checkout flow

