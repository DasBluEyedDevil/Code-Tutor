---
type: "THEORY"
title: "The Clean Architecture Concept"
---

### Origin

Clean Architecture was popularized by Robert C. Martin (Uncle Bob). The core idea is:

> **Dependencies point inward. Inner layers know nothing about outer layers.**

### The Layers

```
┌─────────────────────────────────────────────────────────┐
│                 Frameworks & Drivers                     │
│    (UI, Database Drivers, Network Clients, OS APIs)     │
│  ┌─────────────────────────────────────────────────────┐│
│  │              Interface Adapters                     ││
│  │     (Controllers, Presenters, Gateways)             ││
│  │  ┌─────────────────────────────────────────────────┐││
│  │  │            Application Business Rules           │││
│  │  │                  (Use Cases)                    │││
│  │  │  ┌─────────────────────────────────────────────┐│││
│  │  │  │       Enterprise Business Rules             ││││
│  │  │  │              (Entities)                     ││││
│  │  │  └─────────────────────────────────────────────┘│││
│  │  └─────────────────────────────────────────────────┘││
│  └─────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────┘
```

### Key Rules

1. **Dependency Rule**: Source code dependencies only point inward
2. **Entities**: Core business objects, no framework dependencies
3. **Use Cases**: Application-specific business rules
4. **Interface Adapters**: Convert data between layers
5. **Frameworks**: External tools and drivers