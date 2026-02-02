---
type: "THEORY"
title: "MVI Concept"
---

### What is MVI?

**Model-View-Intent** enforces unidirectional data flow:

```
┌──────────────────────────────────────────────────────┐
│                        VIEW                           │
│                                                       │
│   ┌─────────────┐              ┌─────────────┐       │
│   │ Render State│ ◄─────────── │ Send Intent │       │
│   └─────────────┘              └─────────────┘       │
│          ▲                            │              │
└──────────┼────────────────────────────┼──────────────┘
           │                            │
           │ State                      │ Intent
           │                            ▼
┌──────────┼──────────────────────────────────────────┐
│          │           VIEWMODEL                       │
│          │                                           │
│   ┌──────┴───────┐    ┌────────────┐                │
│   │    State     │◄───│   Reducer  │◄───┐           │
│   └──────────────┘    └────────────┘    │           │
│                              ▲          │           │
│                              │          │           │
│                       ┌──────┴─────┐    │           │
│                       │ Process    │────┘           │
│                       │ Intent     │                │
│                       └────────────┘                │
│                              │                      │
│                              │ Side Effects         │
│                              ▼                      │
│                       ┌────────────┐                │
│                       │ Repository │                │
│                       └────────────┘                │
└─────────────────────────────────────────────────────┘
```

### Key Concepts

| Concept | Description |
|---------|-------------|
| **Intent** | User actions (clicks, text input, etc.) |
| **State** | Single immutable representation of UI |
| **Reducer** | Pure function: (State, Intent) → State |
| **Side Effects** | Async operations (API calls, DB writes) |