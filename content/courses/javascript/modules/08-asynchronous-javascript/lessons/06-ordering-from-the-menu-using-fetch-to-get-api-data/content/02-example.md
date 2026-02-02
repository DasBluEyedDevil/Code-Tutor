---
type: "EXAMPLE"
title: "Fetching Live Data"
---

```javascript
// 1. Basic GET Request
async function getUser() {
    try {
        // Step 1: Wait for the network response
        const response = await fetch('https://jsonplaceholder.typicode.com/users/1');
        
        // Step 2: Extract the JSON data
        const data = await response.json();
        
        console.log(`User Name: ${data.name}`);
    } catch (error) {
        console.error("Network Error:", error);
    }
}

// 2. Handling HTTP Errors (Important!)
async function safeFetch() {
    const response = await fetch('https://api.example.com/missing');
    
    // fetch only throws an error on network failure (like no internet)
    // For 404 (Not Found) or 500 (Server Error), we must check manually:
    if (!response.ok) {
        console.log(`API Error: ${response.status}`);
        return;
    }
    
    const data = await response.json();
    console.log(data);
}

// 3. Sending Data (POST Request)
async function createUser() {
    const newUser = { name: "Alice", email: "alice@test.com" };
    
    const response = await fetch('https://jsonplaceholder.typicode.com/posts', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(newUser) // Convert object to text for the network
    });
    
    const result = await response.json();
    console.log("Created user with ID:", result.id);
}
```