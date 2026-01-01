---
type: "THEORY"
title: "Organizing API Calls with the Repository Pattern"
---


As your app grows, you need a clean architecture to organize API calls. The **Repository Pattern** provides a layer of abstraction between your UI code and the data sources.

**Benefits of the Repository Pattern:**

1. **Separation of Concerns**: UI code does not know about API details
2. **Testability**: Mock repositories for widget and integration tests
3. **Flexibility**: Switch data sources (API, cache, local DB) without changing UI
4. **Caching**: Add caching logic in one place
5. **Offline Support**: Combine local and remote data seamlessly

**Architecture Overview:**

```
+------------------+
|    UI Widgets    |  <- Displays data, handles user input
+------------------+
        |
        v
+------------------+
|    Providers     |  <- State management (Riverpod/Bloc)
+------------------+
        |
        v
+------------------+
|   Repositories   |  <- Abstracts data access, caching
+------------------+
        |
        v
+------------------+
| Serverpod Client |  <- Generated API client
+------------------+
        |
        v
+------------------+
| Serverpod Server |  <- Your Dart backend
+------------------+
```

**When to Use Repositories:**

- Always use repositories in production apps
- For simple demos or prototypes, calling the client directly is acceptable
- Repositories become essential when you need caching or offline support

