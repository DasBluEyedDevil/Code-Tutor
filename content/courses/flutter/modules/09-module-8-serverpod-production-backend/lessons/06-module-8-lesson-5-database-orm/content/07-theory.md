---
type: "THEORY"
title: "Defining Models with Protocol YAML"
---

In Serverpod, you define your data models using YAML files in the `lib/src/protocol/` directory. These YAML definitions are converted to Dart classes and database tables by the code generator.

**Why YAML Instead of Dart?**

1. YAML is declarative - you describe WHAT you want, not HOW to build it
2. Serverpod generates both server and client code from the same source
3. Database migrations are automatically created from YAML changes
4. Less boilerplate - one YAML file generates hundreds of lines of Dart

**Protocol File Structure:**

```yaml
class: ClassName          # Required: The Dart class name
table: table_name         # Optional: Creates a database table
fields:                   # Required: The properties of the class
  fieldName: Type         # Field name and its type
```

**Supported Field Types:**

- Primitives: `int`, `double`, `bool`, `String`, `DateTime`, `Duration`
- Nullable: Add `?` suffix (e.g., `String?`)
- Lists: `List<Type>` (e.g., `List<String>`)
- Maps: `Map<String, Type>` (e.g., `Map<String, int>`)
- Custom classes: Reference other protocol classes by name
- ByteData: For binary data storage
- UuidValue: For UUID fields

