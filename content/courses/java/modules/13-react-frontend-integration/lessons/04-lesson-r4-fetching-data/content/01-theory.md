---
type: "THEORY"
title: "Connecting to Your API"
---

Your React frontend needs to fetch data from your Spring Boot backend:

// Fetch users from Spring Boot API
async function getUsers() {
    const response = await fetch('http://localhost:8080/api/users');
    const users = await response.json();
    return users;
}

THE FETCH API:
- Native browser API (no library needed)
- Returns a Promise
- Response must be parsed (.json(), .text(), etc.)
- Doesn't throw on HTTP errors (4xx, 5xx)

BASIC FETCH PATTERN:

try {
    const response = await fetch(url);
    
    if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
    }
    
    const data = await response.json();
    return data;
} catch (error) {
    console.error('Fetch failed:', error);
    throw error;
}

FETCH OPTIONS:

fetch(url, {
    method: 'POST',              // GET, POST, PUT, DELETE
    headers: {
        'Content-Type': 'application/json',
        'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify(data),  // For POST/PUT
    credentials: 'include'       // Include cookies (for sessions)
});