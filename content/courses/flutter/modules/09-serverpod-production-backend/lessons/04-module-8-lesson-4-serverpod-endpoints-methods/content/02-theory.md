---
type: "THEORY"
title: "What are Endpoints?"
---

**Endpoints are the API of your Serverpod application.**

In traditional REST APIs, you define routes like:
- GET /api/users/123
- POST /api/users
- PUT /api/users/123
- DELETE /api/users/123

In Serverpod, you define **methods on endpoint classes**:

```dart
class UserEndpoint extends Endpoint {
  Future<User?> getUser(Session session, int userId) async { ... }
  Future<User> createUser(Session session, User user) async { ... }
  Future<User> updateUser(Session session, User user) async { ... }
  Future<bool> deleteUser(Session session, int userId) async { ... }
}
```

**Key Differences from REST:**

| REST API | Serverpod Endpoint |
|----------|-------------------|
| URL routes (/api/users) | Class methods (user.getUser) |
| JSON strings | Typed Dart objects |
| Manual parsing | Automatic serialization |
| HTTP verbs (GET, POST) | Method names |
| Separate client SDK | Generated client |

**Benefits of Serverpod Endpoints:**

1. **Type Safety**: Parameters and return types are checked at compile time
2. **No Boilerplate**: No JSON parsing, no URL routing configuration
3. **Auto-Generated Client**: Flutter client code is generated automatically
4. **IDE Support**: Autocomplete for all endpoint methods

