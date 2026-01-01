---
type: "WARNING"
title: "Logical Landmines"
---

### 1. Complex Conditions = Unreadable Code
If your `if` statement has 5 different `&&` and `||` operators, it’s hard for humans to understand.
*   **Fix:** Break complex logic into smaller variables with descriptive names.
```javascript
// Hard to read:
if (age > 18 && hasTicket && !isBanned && (isMember || hasGuestPass))

// Easier to read:
const isAdultWithTicket = age > 18 && hasTicket;
const hasValidEntry = isMember || hasGuestPass;
if (isAdultWithTicket && hasValidEntry && !isBanned)
```

### 2. The `!` with Non-Booleans
Remember "Truthy" and "Falsy"? Using `!` on a number or string will convert it to a boolean and then flip it.
*   `!0` becomes `true` (because 0 is falsy).
*   `!"Hello"` becomes `false` (because "Hello" is truthy).
Be careful using `!` on variables unless you are sure they contain booleans.

### 3. Confusing `&&` and `||`
It sounds simple, but it’s a very common logic bug. For example: "I want to block users if they aren't an Admin AND they aren't a Moderator."
If you write `if (role !== 'Admin' || role !== 'Moderator')`, it will **always** be true, because a user can't be both at the exact same time! 
*   **Think carefully:** Do you need BOTH to be true, or just ONE?
