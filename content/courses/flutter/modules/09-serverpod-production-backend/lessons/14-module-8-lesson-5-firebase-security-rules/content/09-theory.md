---
type: "THEORY"
title: "Common Security Mistakes"
---


### ❌ Mistake 1: Test Mode in Production


**Problem**: Anyone can read/write your entire database!

### ❌ Mistake 2: Relying on Client-Side Checks


**Problem**: Hackers can modify your app code and bypass this check.

**Solution**: Enforce in security rules!


### ❌ Mistake 3: Not Validating Data


**Problem**: Users can write invalid data (empty titles, negative numbers, etc.)

**Solution**: Validate everything!




```javascript
// ✅ GOOD: Strict validation
match /posts/{postId} {
  allow write: if request.auth != null
               && request.resource.data.title is string
               && request.resource.data.title.size() > 0;
}
```
