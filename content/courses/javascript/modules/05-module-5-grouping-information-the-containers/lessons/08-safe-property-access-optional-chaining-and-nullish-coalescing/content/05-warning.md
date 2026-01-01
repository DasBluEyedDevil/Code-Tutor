---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. **Using ?. when not needed:**
   ```javascript
   let name = user?.name;  // If you KNOW user exists, just use user.name
   ```
   Only use ?. when the value might be null/undefined.

2. **Confusing ?? with ||:**
   ```javascript
   let port = config.port || 3000;  // WRONG if port could be 0
   let port = config.port ?? 3000;  // CORRECT
   ```

3. **Optional chaining doesn't help with undefined properties:**
   ```javascript
   let user = { name: undefined };
   console.log(user?.name);  // undefined (as expected)
   // ?. checks if user exists, not if user.name has a value
   ```

4. **Cannot use ?. for assignment:**
   ```javascript
   user?.address = 'NYC';  // SYNTAX ERROR!
   // Optional chaining is for reading, not writing
   ```

5. **Mixing ?? with || or && without parentheses:**
   ```javascript
   let a = null || undefined ?? 'default';  // SYNTAX ERROR!
   let a = (null || undefined) ?? 'default';  // OK: 'default'
   ```
   JavaScript requires parentheses when mixing ?? with || or &&.

6. **Overusing optional chaining (code smell):**
   ```javascript
   data?.users?.[0]?.profile?.settings?.theme?.color?.hex
   // If you need this many ?., your data structure might need rethinking
   ```