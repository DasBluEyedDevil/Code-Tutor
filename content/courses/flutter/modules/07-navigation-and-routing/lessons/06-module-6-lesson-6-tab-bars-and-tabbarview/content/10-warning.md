---
type: "WARNING"
title: "Common Mistakes"
---


❌ **Mistake 1**: Mismatched tab counts

✅ **Fix**: Match counts exactly

❌ **Mistake 2**: Forgetting to dispose TabController

✅ **Fix**:

❌ **Mistake 3**: Not using vsync

✅ **Fix**:



```dart
class _MyState extends State<MyWidget>
    with SingleTickerProviderStateMixin {  // Add mixin!

  late TabController _controller = TabController(
    length: 3,
    vsync: this,  // Pass this
  );
}
```
