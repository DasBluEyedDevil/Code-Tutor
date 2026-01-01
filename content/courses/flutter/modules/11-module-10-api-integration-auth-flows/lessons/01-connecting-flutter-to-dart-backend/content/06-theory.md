---
type: "THEORY"
title: "Making Your First API Call"
---


With the client initialized, calling your backend endpoints becomes straightforward. Let us examine the anatomy of an API call in Serverpod.

**Understanding the Client Structure:**

The generated `Client` class has properties for each endpoint class you define on the server. If your server has:

```dart
// Server: lib/src/endpoints/user_endpoint.dart
class UserEndpoint extends Endpoint {
  Future<User?> getUser(Session session, int id) async { ... }
  Future<User> createUser(Session session, User user) async { ... }
}

// Server: lib/src/endpoints/product_endpoint.dart
class ProductEndpoint extends Endpoint {
  Future<List<Product>> listProducts(Session session) async { ... }
}
```

Then your generated client has:

```dart
// Generated client provides typed access to all endpoints
client.user.getUser(id);        // Calls UserEndpoint.getUser
client.user.createUser(user);   // Calls UserEndpoint.createUser
client.product.listProducts();  // Calls ProductEndpoint.listProducts
```

**Notice What is Missing:**

- No HTTP method specification (GET, POST, etc.)
- No URL paths
- No request body construction
- No response parsing

Serverpod handles all of this. You just call methods with typed parameters and get typed results.

**The Session Parameter:**

You may have noticed that server endpoints take a `Session session` parameter, but client calls do not include it. The session is automatically created and managed by Serverpod. It handles:

- Request tracking and logging
- Database connections
- Authentication state
- Transaction management

The client sends authentication tokens automatically when available, and the session on the server reflects the authenticated user.

