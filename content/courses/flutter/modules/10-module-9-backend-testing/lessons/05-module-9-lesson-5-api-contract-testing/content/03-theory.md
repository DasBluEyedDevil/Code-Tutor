---
type: "THEORY"
title: "Serverpod's Type-Safe Advantage"
---


**Serverpod** provides a unique solution to contract testing: it eliminates the problem entirely through **code generation** and **shared types**.

**How Serverpod Works:**

When you define a model in Serverpod:

```yaml
# lib/src/protocol/user.yaml
class: User
fields:
  id: int
  name: String
  email: String
  createdAt: DateTime
```

Serverpod generates:

1. **Server-side Dart class**: Used in your backend
2. **Client-side Dart class**: Used in your Flutter app
3. **Serialization code**: JSON encoding/decoding
4. **Type-safe client methods**: API calls with proper types

**The Generated Client:**

```dart
// Auto-generated in your Flutter app
class UserEndpoint {
  Future<User> getUser(int id) async {
    return await caller.callServerEndpoint(
      'user',
      'getUser',
      {'id': id},
    );
  }
}
```

**Why This Eliminates Contract Issues:**

1. **Single Source of Truth**: Model defined once, used everywhere
2. **Compile-Time Safety**: Type mismatches caught during build
3. **Automatic Sync**: Regenerate client when server changes
4. **No JSON Parsing**: Serialization handled by generated code

**Serverpod Contract Testing Strategy:**

Even with Serverpod, you should still test:

1. **Model Validation**: Ensure generated models serialize correctly
2. **Endpoint Behavior**: Verify endpoints return expected data
3. **Version Compatibility**: Test client/server version combinations

