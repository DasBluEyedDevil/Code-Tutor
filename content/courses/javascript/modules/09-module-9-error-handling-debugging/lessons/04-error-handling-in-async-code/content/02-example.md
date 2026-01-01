---
type: "EXAMPLE"
title: "The Async Blast Shield"
---

```javascript
// 1. Error handling with async/await (The Gold Standard)
async function fetchSafe() {
    try {
        console.log("Fetching...");
        const response = await fetch('https://invalid-url.xyz');
        const data = await response.json();
    } catch (e) {
        // This catch waits for the promise to fail!
        console.error("Async Error Caught:", e.message);
    }
}

// 2. Error handling with Promises (.catch)
// If you don't use async/await, you MUST use .catch()
function fetchWithCatch() {
    fetch('https://invalid-url.xyz')
        .then(res => res.json())
        .catch(err => {
            console.error("Promise Error Caught:", err.message);
        });
}

// 3. The "Unawaited" Danger
// If you forget 'await', the catch block WON'T work
async function unawaitedMistake() {
    try {
        // DANGER: No await! The function continues immediately.
        fetch('https://invalid-url.xyz'); 
    } catch (e) {
        console.log("This will never run!");
    }
}

// 4. Promise.allSettled (Safe parallel handling)
async function checkAllServices() {
    const results = await Promise.allSettled([
        fetch('https://api.a.com'),
        fetch('https://api.b.com')
    ]);
    
    // results is an array of { status: 'fulfilled' | 'rejected', ... }
    results.forEach(res => {
        if (res.status === 'rejected') {
            console.log("One service failed, but the others are fine.");
        }
    });
}
```