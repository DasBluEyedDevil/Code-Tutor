---
type: "THEORY"
title: "MVVM Overview"
---

### What is MVVM?

**Model-View-ViewModel** separates:
- **Model**: Data and business logic (domain + data layers)
- **View**: UI that displays data and captures user input
- **ViewModel**: Prepares data for the View, handles UI logic

### Data Flow

```
┌─────────────────────────────────────────────────────┐
│                      VIEW                            │
│              (Compose UI / SwiftUI)                  │
│                                                      │
│    ┌────────────────┐    ┌────────────────┐         │
│    │  Observe State │    │  Send Actions  │         │
│    │    (collect)   │    │   (onClick)    │         │
│    └───────▲────────┘    └───────┬────────┘         │
└────────────┼─────────────────────┼──────────────────┘
             │                     │
             │  StateFlow          │  Function Calls
             │                     │
┌────────────┼─────────────────────┼──────────────────┐
│            │    VIEWMODEL        ▼                  │
│    ┌───────┴────────────────────────────┐           │
│    │          _state: MutableStateFlow   │           │
│    │           state: StateFlow          │           │
│    │                                     │           │
│    │   fun onAction(action: Action)      │           │
│    └─────────────────┬───────────────────┘           │
└──────────────────────┼───────────────────────────────┘
                       │
                       │  suspend calls
                       ▼
┌──────────────────────────────────────────────────────┐
│                     MODEL                             │
│           (Repository, Use Cases)                     │
└──────────────────────────────────────────────────────┘
```