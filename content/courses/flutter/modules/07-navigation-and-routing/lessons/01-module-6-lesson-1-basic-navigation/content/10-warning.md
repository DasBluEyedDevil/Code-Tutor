---
type: "WARNING"
title: "Common Mistakes"
---


❌ **Mistake 1**: Forgetting `context`

✅ **Fix**: Always pass context

❌ **Mistake 2**: Not using `await` when expecting result

✅ **Fix**: Use await



```dart
final result = await Navigator.push(context, MaterialPageRoute(...));
```
