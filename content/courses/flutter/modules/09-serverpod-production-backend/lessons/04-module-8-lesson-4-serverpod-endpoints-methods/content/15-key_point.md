---
type: "KEY_POINT"
title: "Summary: The Endpoint Workflow"
---

**Creating and Using Endpoints:**

1. **Create Endpoint Class**
   ```dart
   // lib/src/endpoints/my_endpoint.dart
   class MyEndpoint extends Endpoint {
     Future<Result> myMethod(Session session, ...) async {
       // Implementation
     }
   }
   ```

2. **Generate Client Code**
   ```bash
   cd my_project_server
   serverpod generate
   ```

3. **Call from Flutter**
   ```dart
   final result = await client.my.myMethod(...);
   ```

**Key Points:**
- Endpoints are classes that extend `Endpoint`
- Session is always the first parameter
- Methods are automatically exposed to the client
- Return types are automatically serialized
- Exceptions propagate to the client
- Use `requireLogin` for authenticated endpoints
- Private methods (starting with _) are not exposed

**Best Practices:**
- One endpoint per domain (UserEndpoint, PostEndpoint)
- Clear method names (getUser, createPost, deleteOrder)
- Proper error handling with typed exceptions
- Authentication checks where needed
- Logging for important operations
- Input validation before database operations

