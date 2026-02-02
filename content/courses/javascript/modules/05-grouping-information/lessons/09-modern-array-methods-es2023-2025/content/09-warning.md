---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. **Confusing at() with negative indexing on brackets:**
   ```javascript
   array[-1]    // undefined - doesn't work!
   array.at(-1) // Last element - works!
   ```

2. **Forgetting toSorted/toReversed/with return new arrays:**
   ```javascript
   let nums = [3, 1, 2];
   nums.toSorted();  // Returns [1, 2, 3] but you didn't save it!
   nums.with(0, 99); // Returns [99, 1, 2] but you didn't save it!
   console.log(nums);  // Still [3, 1, 2]
   
   // Correct:
   let sorted = nums.toSorted();
   let updated = nums.with(0, 99);
   ```

3. **Using toSorted() without comparison for numbers:**
   ```javascript
   [10, 2, 1].toSorted()  // [1, 10, 2] - sorted as strings!
   [10, 2, 1].toSorted((a, b) => a - b)  // [1, 2, 10] - correct!
   ```

4. **Object.groupBy returns null prototype object:**
   ```javascript
   let grouped = Object.groupBy(items, fn);
   // grouped doesn't have standard Object methods like hasOwnProperty
   // Use Object.keys(grouped) or 'key' in grouped instead
   ```

5. **Using Object.groupBy when you need non-string keys:**
   ```javascript
   // Object.groupBy converts keys to strings
   Object.groupBy(items, x => x.active);  // Keys: 'true', 'false' (strings)
   
   // Map.groupBy preserves key types
   Map.groupBy(items, x => x.active);     // Keys: true, false (booleans)
   ```

6. **Browser compatibility:**
   - at(): Supported in all modern browsers (2022+)
   - toSorted/toReversed/toSpliced/with: ES2023 (mid-2023+)
   - Object.groupBy/Map.groupBy: ES2024 (late 2023+)
   - Check compatibility if supporting older browsers!

7. **Mixing up splice and toSpliced return values:**
   ```javascript
   // splice returns REMOVED elements
   // toSpliced returns the NEW array
   let arr = [1, 2, 3];
   arr.splice(1, 1);      // Returns [2] (removed)
   arr.toSpliced(1, 1);   // Returns [1, 3] (new array)
   ```

8. **with() throws RangeError for invalid index:**
   ```javascript
   let arr = [1, 2, 3];
   arr.with(10, 'x');  // RangeError! Index out of bounds
   arr.with(-10, 'x'); // RangeError! Index out of bounds
   ```