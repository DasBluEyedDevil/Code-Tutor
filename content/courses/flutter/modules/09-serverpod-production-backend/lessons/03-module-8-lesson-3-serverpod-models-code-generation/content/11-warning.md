---
type: "WARNING"
title: "Never Edit Generated Files"
---

**Critical Rule: NEVER edit files in generated/ folders!**

The following folders are auto-generated:
- `my_project_server/lib/src/generated/`
- `my_project_client/lib/src/protocol/`

**Why?**

Every time you run `serverpod generate`, these folders are **completely overwritten**. Any manual changes you make will be lost.

**What if you need custom logic?**

1. **Extension methods**: Add functionality without modifying the class
   ```dart
   extension UserExtensions on User {
     String get fullName => name; // Custom logic
     bool get isAdult => (age ?? 0) >= 18;
   }
   ```

2. **Wrapper classes**: Create your own class that wraps the generated one

3. **Partial classes**: Serverpod supports custom code in separate files (advanced)

**If you find yourself wanting to edit generated code, you're probably doing something wrong. Ask yourself: "Can I solve this with my YAML definition or an extension?"**

