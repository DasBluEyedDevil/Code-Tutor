---
type: "THEORY"
title: "SQLDelight vs Other Options"
---

### Comparison Table

| Feature | SQLDelight | Room | Realm | Core Data |
|---------|------------|------|-------|------------|
| **KMP Support** | ✅ Full | ❌ Android only | ✅ Partial | ❌ Apple only |
| **Type Safety** | ✅ Compile-time | ✅ Compile-time | ⚠️ Runtime | ⚠️ Runtime |
| **SQL Knowledge** | Required | Required | Not needed | Not needed |
| **Reactive** | ✅ Flow | ✅ Flow/LiveData | ✅ | ✅ |
| **Migration** | ✅ Built-in | ✅ Built-in | ✅ | ✅ |
| **Sync** | ❌ Manual | ❌ Manual | ✅ Built-in | ✅ CloudKit |

### When to Choose SQLDelight

**Choose SQLDelight when:**
- Building a KMP application
- You're comfortable with SQL
- You want compile-time query verification
- You need fine-grained control over database operations

**Consider alternatives when:**
- Building single-platform app (Room for Android is excellent)
- Need cloud sync out of the box (Realm might be simpler)
- Team has no SQL experience