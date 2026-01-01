---
type: "WARNING"
title: "Migration Pitfalls"
---

### 1. Blindly trusting JavaScript
Just because your JavaScript code "worked" doesn't mean it's safe. TypeScript will often find bugs you didn't even know you had—like a variable that *usually* contains a string but *sometimes* contains `null`. 
*   **Fix:** Don't ignore these warnings by using `any`. Take the time to understand why TypeScript is worried!

### 2. The `any` Addiction
It's very easy to use `any` to make a deadline, but then never come back to fix it. 
*   **Result:** You have all the complexity of a TypeScript build process with none of the safety. 
*   **Rule:** Every time you use `any`, add a `// TODO: Define type` comment above it.

### 3. Missing Third-Party Types
If your JavaScript code uses a library like `jQuery` or `Express`, you'll need to install the types for those libraries as soon as you rename your files to `.ts`.

### 4. Overcomplicating Types
During migration, keep your types simple. You don't need complex generics or mapped types yet. Use basic interfaces that match your current data shapes.
