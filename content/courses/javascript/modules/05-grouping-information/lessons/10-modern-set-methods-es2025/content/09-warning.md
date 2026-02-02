---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. **Forgetting Sets use reference equality for objects:**
   ```javascript
   let set = new Set();
   set.add({ name: 'Alice' });
   set.add({ name: 'Alice' });  // Added! Different object reference
   console.log(set.size);  // 2 - both objects added!
   ```

2. **difference() order matters:**
   ```javascript
   let a = new Set([1, 2, 3]);
   let b = new Set([2, 3, 4]);
   a.difference(b);  // Set { 1 } - what's in A but not B
   b.difference(a);  // Set { 4 } - what's in B but not A
   // These are different!
   ```

3. **Runtime requirements (ES2025):**
   - **Node.js 22+** required for Set methods
   - **Chrome 122+**, **Firefox 127+**, **Safari 17+** for browser support
   - Older environments will throw: `TypeError: set.union is not a function`
   - Consider polyfills (core-js) or transpilation for older environments
   - Code Tutor uses your local Node.js - update Node if methods don't work

4. **Sets don't have array methods:**
   ```javascript
   let set = new Set([1, 2, 3]);
   set.map(x => x * 2);  // ERROR! Sets don't have map()
   [...set].map(x => x * 2);  // [2, 4, 6] - convert first!
   ```

5. **Modifying set while iterating:**
   ```javascript
   let set = new Set([1, 2, 3]);
   for (let item of set) {
     set.delete(item);  // Works but can be confusing
   }
   // Better: collect items to delete, then delete after loop
   ```