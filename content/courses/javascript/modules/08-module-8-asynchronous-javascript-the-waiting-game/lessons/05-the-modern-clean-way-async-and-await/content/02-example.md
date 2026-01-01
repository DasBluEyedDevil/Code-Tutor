---
type: "EXAMPLE"
title: "Synchronous Style Async Code"
---

```javascript
// 1. Basic Async Function
// The 'async' keyword makes a function always return a Promise
async function greet() {
    return "Hello!";
}

// 2. The 'await' Keyword
// 'await' can only be used inside an 'async' function
async function processData() {
    console.log("Starting...");
    
    // Imagine this is a slow database call
    const data = await fetchUserData(); 
    
    console.log(`Finished processing ${data.name}`);
}

// 3. Proper Error Handling (try/catch)
async function safeFetch() {
    try {
        const response = await someSlowTask();
        console.log("Success:", response);
    } catch (error) {
        console.log("Something went wrong:", error);
    }
}

// 4. Sequential vs Parallel
async function getFullReport() {
    // SEQUENTIAL (Slow: one after the other)
    const user = await fetchUser();
    const posts = await fetchPosts(); // Starts ONLY after user is done
    
    // PARALLEL (Fast: both at the same time)
    const [user2, posts2] = await Promise.all([
        fetchUser(),
        fetchPosts()
    ]);
}
```