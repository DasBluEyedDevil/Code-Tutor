---
type: "THEORY"
title: "Common KMP Architecture Patterns"
---

### Pattern Overview

| Pattern | Description | Best For |
|---------|-------------|----------|
| **MVVM** | Model-View-ViewModel | Most apps, familiar to Android devs |
| **MVI** | Model-View-Intent | Complex state, predictable updates |
| **Clean** | Layered with use cases | Large teams, complex domains |
| **Redux-like** | Single state store | Apps needing time-travel debugging |

### MVVM (Most Common)
```
View (Compose) → ViewModel → Repository → Data Sources
     ↑              |
     |______________|  (StateFlow)
```

### MVI (Most Predictable)
```
View → Intent → Reducer → State → View
                  ↓
            Side Effects
```

### What We'll Cover

1. **Lesson 6B.2**: Clean Architecture layers
2. **Lesson 6B.3**: MVVM implementation in KMP
3. **Lesson 6B.4**: MVI implementation in KMP
4. **Lesson 6B.5**: Sharing ViewModels across platforms
5. **Lesson 6B.6**: Navigation patterns
6. **Lesson 6B.7**: Putting it all together