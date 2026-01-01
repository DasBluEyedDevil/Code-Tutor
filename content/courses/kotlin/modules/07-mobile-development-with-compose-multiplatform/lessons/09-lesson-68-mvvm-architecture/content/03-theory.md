---
type: "THEORY"
title: "MVVM Pattern"
---


### Architecture Overview


### Responsibilities

**View** (Composables):
- Display UI
- Capture user input
- Observe ViewModel state
- **No business logic**

**ViewModel**:
- Hold UI state
- Handle user events
- Call repository methods
- Transform data for UI
- **No Android framework dependencies** (except AndroidX)

**Repository**:
- Abstract data sources
- Combine local + remote data
- Caching strategy
- **Single source of truth**

**Model** (Data Classes):
- Plain data structures
- No logic

---



```kotlin
┌──────────────────────────────────────┐
│  View (Composables)                  │  UI Layer
│  - Displays data                     │
│  - Handles user input                │
└─────────────┬────────────────────────┘
              │ observes
              ↓
┌──────────────────────────────────────┐
│  ViewModel                           │  Presentation Layer
│  - Holds UI state                    │
│  - Business logic                    │
│  - Survives config changes           │
└─────────────┬────────────────────────┘
              │ calls
              ↓
┌──────────────────────────────────────┐
│  Repository                          │  Data Layer
│  - Single source of truth            │
│  - Manages data sources              │
└─────────────┬────────────────────────┘
              │
       ┌──────┴──────┐
       ↓             ↓
┌─────────────┐ ┌─────────────┐
│  Remote     │ │  Local      │
│  (API)      │ │  (Room)     │
└─────────────┘ └─────────────┘
```
