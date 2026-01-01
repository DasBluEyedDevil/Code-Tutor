---
type: "EXAMPLE"
title: "The Standard Errors"
---

```javascript
// 1. ReferenceError
// Occurs when you use a variable that doesn't exist
try {
    console.log(unknownVariable);
} catch (e) {
    console.log(`Type: ${e.name}`); // ReferenceError
}

// 2. TypeError
// Occurs when a value is not the expected type
try {
    const num = 5;
    num.toUpperCase(); // Numbers don't have toUpperCase!
} catch (e) {
    console.log(`Type: ${e.name}`); // TypeError
}

// 3. SyntaxError
// Occurs when the code is written incorrectly
// (Note: This usually prevents code from running at all,
// but eval() can trigger it at runtime)
try {
    eval('const x = ;'); 
} catch (e) {
    console.log(`Type: ${e.name}`); // SyntaxError
}

// 4. RangeError
// Occurs when a number is outside of its legal range
try {
    const arr = new Array(-1); // Can't have negative length
} catch (e) {
    console.log(`Type: ${e.name}`); // RangeError
}

// 5. Checking for specific types
try {
    // ... some code ...
} catch (e) {
    if (e instanceof TypeError) {
        console.log("Input was the wrong type.");
    } else if (e instanceof ReferenceError) {
        console.log("A variable is missing.");
    } else {
        console.log("An unknown error occurred.");
    }
}
```