---
type: "EXAMPLE"
title: "The Blast Shield"
---

```javascript
// 1. Basic try/catch
try {
    const data = JSON.parse("Invalid JSON!"); // This will fail
    console.log(data); // This line will NEVER run
} catch (error) {
    console.log("Caught an error!");
    console.error(`Message: ${error.message}`);
}

// 2. The 'throw' keyword
// You can create your own errors manually
function validateAge(age) {
    if (age < 0) {
        throw new Error("Age cannot be negative!");
    }
    return true;
}

try {
    validateAge(-5);
} catch (e) {
    console.log(`Validation failed: ${e.message}`);
}

// 3. The 'finally' block
// Use this for cleanup (closing files, hiding loading spinners)
let isLoading = true;
try {
    console.log("Fetching data...");
    // doWork();
} catch (err) {
    console.log("Error during fetch");
} finally {
    isLoading = false;
    console.log("Cleanup: Loader hidden.");
}

// 4. Omission of 'catch' binding (Modern JS)
// If you don't care about the error object, you can skip it
try {
    someTask();
} catch {
    console.log("Something went wrong, but I don't care why.");
}
```