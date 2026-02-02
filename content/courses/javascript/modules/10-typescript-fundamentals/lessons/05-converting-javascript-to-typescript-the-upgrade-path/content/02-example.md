---
type: "EXAMPLE"
title: "The Upgrade Path"
---

```javascript
// STEP 1: The Original JavaScript (user.js)
function displayUser(user) {
    console.log(user.name.toUpperCase());
}

// STEP 2: JSDoc Migration (No file rename yet!)
// This tells VS Code about the types without using TypeScript syntax
/**
 * @param {{ name: string }} user 
 */
function displayUserJSDoc(user) {
    console.log(user.name.toUpperCase());
}

// STEP 3: Rename to .ts and add basic types
interface UserProfile {
    name: string;
    email?: string;
}

function displayUserTS(user: UserProfile) {
    // TypeScript will warn us if 'name' might be missing!
    console.log(user.name.toUpperCase());
}

// STEP 4: Refining types (Adding safety)
function safeDisplay(user: UserProfile | null) {
    if (user) {
        console.log(user.name.toUpperCase());
    } else {
        console.log("No user found.");
    }
}
```