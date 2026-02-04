---
type: KEY_POINT
---

- SharedPreferences stores simple key-value pairs (strings, ints, bools) -- use it for settings and flags, not structured data
- Hive is a fast NoSQL database in pure Dart -- ideal for structured data without complex relationships or SQL queries
- Drift provides type-safe SQL with compile-time query verification and reactive streams -- best for relational data
- Choose storage based on data complexity: SharedPreferences for flags, Hive for documents, Drift for relational data with queries
- Never store sensitive data (tokens, passwords) in SharedPreferences or Hive -- use `flutter_secure_storage` for secrets
