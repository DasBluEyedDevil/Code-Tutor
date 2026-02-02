---
type: "THEORY"
title: "Architecture in KMP Context"
---

### The Unique Challenge of Multiplatform

KMP adds complexity because:
1. **Multiple UI frameworks** - Compose on Android, SwiftUI/UIKit on iOS
2. **Platform-specific APIs** - Different storage, networking, threading
3. **Code sharing decisions** - What goes in commonMain vs platform code?

### The KMP Architecture Goal

```
┌─────────────────────────────────────────────────────┐
│                  Shared (95%)                        │
│  ┌─────────────────────────────────────────────────┐ │
│  │           UI (Compose Multiplatform)            │ │
│  ├─────────────────────────────────────────────────┤ │
│  │           Presentation (ViewModels)            │ │
│  ├─────────────────────────────────────────────────┤ │
│  │           Domain (Use Cases, Entities)         │ │
│  ├─────────────────────────────────────────────────┤ │
│  │           Data (Repositories, DTOs)            │ │
│  └─────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────┘
┌────────────────┐           ┌────────────────┐
│  Android (5%)  │           │    iOS (5%)    │
│  - Drivers     │           │  - Drivers     │
│  - Permissions │           │  - Permissions │
└────────────────┘           └────────────────┘
```

### What Goes Where

| Layer | Location | Examples |
|-------|----------|----------|
| **UI** | commonMain | Compose screens, components |
| **ViewModel** | commonMain | State management, UI logic |
| **Use Cases** | commonMain | Business operations |
| **Repository** | commonMain | Data access abstraction |
| **Data Sources** | mostly commonMain | API clients, database queries |
| **Drivers** | platform-specific | SQLite drivers, HTTP engines |