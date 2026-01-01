---
type: "THEORY"
title: "Fetching Data from JavaScript"
---

Your API endpoint:
GET /api/users/1 → {"name": "Alice", "age": 20}

JavaScript code to call it:

fetch('http://localhost:8080/api/users/1')
  .then(response => response.json())
  .then(user => {
    console.log(user.name);  // "Alice"
    document.getElementById('username').textContent = user.name;
  });

This:
1. Sends HTTP GET request to your API
2. Receives JSON response
3. Displays it on the webpage

Full-stack = Backend (Java) + Frontend (HTML/JS) working together!