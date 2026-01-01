---
type: "THEORY"
title: "Breaking Down the Syntax"
---

fetch() syntax:

**Basic GET request:**

let response = await fetch(url);
let data = await response.json();

**With options:**

let response = await fetch(url, {
  method: 'POST',  // GET, POST, PUT, PATCH, DELETE
  headers: {       // Request headers
    'Content-Type': 'application/json',
    'Authorization': 'Bearer token123'
  },
  body: JSON.stringify(data)  // Request body (POST/PUT/PATCH)
});

**Response object properties:**

response.ok          // true if status 200-299
response.status      // HTTP status code (200, 404, 500, etc.)
response.statusText  // Status message ('OK', 'Not Found', etc.)
response.headers     // Response headers
response.json()      // Parse as JSON (returns Promise)
response.text()      // Get as text (returns Promise)
response.blob()      // Get as binary (for images, files)

**HTTP Methods (CRUD):**

GET    - Read data (default)
POST   - Create new resource
PUT    - Replace entire resource
PATCH  - Update part of resource
DELETE - Delete resource

**Complete pattern:**

async function apiCall() {
  try {
    // 1. Make request
    let response = await fetch(url, options);
    
    // 2. Check if successful
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    
    // 3. Parse response
    let data = await response.json();
    
    // 4. Use data
    return data;
    
  } catch (error) {
    // 5. Handle errors
    console.error('Fetch error:', error);
    throw error;  // Re-throw or handle
  }
}

**Common Headers:**

'Content-Type': 'application/json'  // Sending JSON
'Authorization': 'Bearer token'      // Authentication
'Accept': 'application/json'         // Expecting JSON

**Sending Data:**

// Object to JSON string
let user = { name: 'Alice', age: 25 };
let jsonString = JSON.stringify(user);

// Send in fetch
body: JSON.stringify(user)

// Parsing response
let data = await response.json();  // JSON string to object

**Error Handling:**

// Network errors (no connection)
try {
  let response = await fetch(url);
} catch (error) {
  console.log('Network error:', error);
}

// HTTP errors (404, 500, etc.)
if (!response.ok) {
  throw new Error('HTTP ' + response.status);
}

// JSON parsing errors
try {
  let data = await response.json();
} catch (error) {
  console.log('Invalid JSON:', error);
}