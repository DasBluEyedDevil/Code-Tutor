---
type: "THEORY"
title: "Clean Architecture in KMP"
---

### Simplified for Mobile

For KMP apps, we adapt Clean Architecture into practical layers:

```
shared/src/commonMain/kotlin/com/example/app/
├── domain/          # Enterprise + Application Rules
│   ├── model/       # Entities (pure data classes)
│   ├── repository/  # Repository interfaces
│   └── usecase/     # Use cases (optional for simple apps)
├── data/            # Interface Adapters + Frameworks
│   ├── repository/  # Repository implementations
│   ├── remote/      # API clients, DTOs
│   └── local/       # Database, preferences
├── presentation/    # UI Layer
│   ├── viewmodel/   # ViewModels
│   └── mapper/      # UI model mappers
└── di/              # Dependency injection setup
```

### Layer Responsibilities

| Layer | Knows About | Doesn't Know About |
|-------|-------------|--------------------|
| **Domain** | Nothing external | Data sources, UI, frameworks |
| **Data** | Domain interfaces | UI, specific ViewModels |
| **Presentation** | Domain models | Data implementation details |