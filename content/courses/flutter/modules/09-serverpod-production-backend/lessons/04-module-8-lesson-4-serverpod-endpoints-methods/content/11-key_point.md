---
type: "KEY_POINT"
title: "Endpoint Organization Best Practices"
---

**Organize endpoints by domain, not by operation type.**

**Good Structure:**
```
lib/src/endpoints/
├── user_endpoint.dart      # All user operations
├── product_endpoint.dart   # All product operations
├── order_endpoint.dart     # All order operations
├── cart_endpoint.dart      # All cart operations
└── auth_endpoint.dart      # Authentication operations
```

**Bad Structure:**
```
lib/src/endpoints/
├── get_endpoints.dart      # All GET operations (mixed domains)
├── post_endpoints.dart     # All POST operations (mixed domains)
├── delete_endpoints.dart   # All DELETE operations (mixed domains)
```

**Naming Conventions:**

1. **Endpoint Class**: `{Domain}Endpoint` (PascalCase)
   - UserEndpoint, ProductEndpoint, OrderEndpoint

2. **Methods**: Use verb-noun format (camelCase)
   - getUser, createUser, updateUser, deleteUser
   - listProducts, findProductByName, countProducts
   - placeOrder, cancelOrder, getOrderHistory

3. **File Names**: `{domain}_endpoint.dart` (snake_case)
   - user_endpoint.dart, product_endpoint.dart

**Method Grouping Within an Endpoint:**

```dart
class UserEndpoint extends Endpoint {
  // === CRUD Operations ===
  Future<User> createUser(...) async { }
  Future<User?> getUser(...) async { }
  Future<User> updateUser(...) async { }
  Future<bool> deleteUser(...) async { }

  // === Query Operations ===
  Future<List<User>> getAllUsers(...) async { }
  Future<List<User>> searchUsers(...) async { }
  Future<int> countUsers(...) async { }

  // === Authentication Related ===
  Future<User?> getCurrentUser(...) async { }
  Future<void> updatePassword(...) async { }
}
```

