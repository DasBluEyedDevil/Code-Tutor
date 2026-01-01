---
type: "THEORY"
title: "Serverpod Model Syntax Deep Dive"
---

Let's understand every part of the YAML model syntax.

**1. The class Keyword**

```yaml
class: User
```

This names your model. It becomes a Dart class named `User`. Use PascalCase for class names.

**2. The table Keyword (Optional)**

```yaml
table: users
```

If present, Serverpod creates a database table with this name. Use snake_case for table names. If omitted, the model exists only in memory (useful for DTOs).

**3. The fields Section**

```yaml
fields:
  fieldName: Type
```

This defines all the properties of your model.

**Field Types Supported:**

| Type | Description | Example |
|------|-------------|---------|
| String | Text data | `name: String` |
| int | Integer numbers | `age: int` |
| double | Decimal numbers | `price: double` |
| bool | True/false | `isActive: bool` |
| DateTime | Date and time | `createdAt: DateTime` |
| ByteData | Binary data | `imageData: ByteData` |
| Duration | Time duration | `timeout: Duration` |
| UuidValue | UUID identifiers | `uuid: UuidValue` |
| List<T> | List of items | `tags: List<String>` |
| Map<K,V> | Key-value pairs | `metadata: Map<String, String>` |
| CustomType | Other models | `author: User` |

**4. Making Fields Optional**

Add `?` after the type to make it nullable:

```yaml
fields:
  requiredField: String     # Must have a value
  optionalField: String?    # Can be null
```

**5. Default Values**

```yaml
fields:
  status: String, default="'pending'"
  count: int, default='0'
  isPublic: bool, default='true'
```

Note: String defaults need nested quotes: `"'value'"`. Other types use single quotes: `'0'`, `'true'`.

