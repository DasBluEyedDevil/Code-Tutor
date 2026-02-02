---
type: "KEY_POINT"
title: "Summary: The Model Workflow"
---

**The Serverpod Model Workflow:**

1. **Define** - Create a .yaml file in protocol/
   ```yaml
   class: User
   table: users
   fields:
     name: String
     email: String
   ```

2. **Generate** - Run the code generator
   ```bash
   cd my_project_server
   serverpod generate
   ```

3. **Use on Server** - Import and use the model
   ```dart
   import 'package:my_project_server/src/generated/protocol.dart';

   final user = User(name: 'Alice', email: 'a@b.com');
   await User.db.insertRow(session, user);
   ```

4. **Use on Client** - Import the client package
   ```dart
   import 'package:my_project_client/my_project_client.dart';

   final user = await client.user.getUser(42);
   print(user.name);
   ```

**Benefits:**
- Single source of truth (YAML)
- Type safety across the entire stack
- Automatic serialization
- No duplicate model definitions
- Compile-time error checking
- IDE autocomplete for all models

