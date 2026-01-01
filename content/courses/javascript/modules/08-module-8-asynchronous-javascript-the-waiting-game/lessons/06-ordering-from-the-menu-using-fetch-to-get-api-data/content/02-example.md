---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Basic fetch - GET request
async function getUsers() {
  try {
    let response = await fetch('https://jsonplaceholder.typicode.com/users');
    
    // Check if request was successful
    if (!response.ok) {
      throw new Error('Request failed: ' + response.status);
    }
    
    // Parse JSON response
    let users = await response.json();
    console.log(users);
    
    return users;
  } catch (error) {
    console.log('Error:', error);
  }
}

// Fetch with options - POST request
async function createUser(userData) {
  try {
    let response = await fetch('https://api.example.com/users', {
      method: 'POST',  // HTTP method
      headers: {
        'Content-Type': 'application/json'  // Sending JSON
      },
      body: JSON.stringify(userData)  // Convert object to JSON string
    });
    
    let newUser = await response.json();
    console.log('Created:', newUser);
    return newUser;
  } catch (error) {
    console.log('Error:', error);
  }
}

// Example: Create user
createUser({
  name: 'Alice',
  email: 'alice@example.com',
  age: 25
});

// UPDATE - PUT/PATCH request
async function updateUser(userId, updates) {
  let response = await fetch(`https://api.example.com/users/${userId}`, {
    method: 'PATCH',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(updates)
  });
  
  return await response.json();
}

// DELETE request
async function deleteUser(userId) {
  let response = await fetch(`https://api.example.com/users/${userId}`, {
    method: 'DELETE'
  });
  
  if (response.ok) {
    console.log('User deleted');
  }
}

// Practical example: Display users on page
async function displayUsers() {
  try {
    let response = await fetch('https://jsonplaceholder.typicode.com/users');
    let users = await response.json();
    
    let userList = document.querySelector('#userList');
    
    users.forEach(user => {
      let li = document.createElement('li');
      li.textContent = user.name;
      userList.appendChild(li);
    });
  } catch (error) {
    console.log('Failed to load users:', error);
  }
}

// With loading state
async function fetchWithLoading() {
  let loadingEl = document.querySelector('#loading');
  let contentEl = document.querySelector('#content');
  
  try {
    loadingEl.style.display = 'block';  // Show loading
    
    let response = await fetch('/api/data');
    let data = await response.json();
    
    contentEl.textContent = JSON.stringify(data);
  } catch (error) {
    contentEl.textContent = 'Error loading data';
  } finally {
    loadingEl.style.display = 'none';  // Hide loading
  }
}
```
