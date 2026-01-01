---
type: "WARNING"
title: "Common Mistakes"
---


❌ **Mistake 1**: No visual feedback

✅ **Fix**: Use InkWell or change colors on tap

❌ **Mistake 2**: Forgetting setState in gesture handlers

✅ **Fix**: Always use setState



```dart
onTap: () {
  setState(() {
    isLiked = !isLiked;
  });
}
```
