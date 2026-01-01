---
type: "THEORY"
title: "Step 4: Frontend JavaScript"
---

const API_URL = 'http://localhost:8080/api/users';

// Load users when page loads
window.onload = function() {
    loadUsers();
};

// Fetch and display users
function loadUsers() {
    fetch(API_URL)
        .then(response => response.json())
        .then(users => {
            const userList = document.getElementById('userList');
            userList.innerHTML = '';
            
            users.forEach(user => {
                const div = document.createElement('div');
                div.textContent = `${user.name} (${user.email})`;
                userList.appendChild(div);
            });
        })
        .catch(error => console.error('Error:', error));
}

// Add new user
document.getElementById('addUserForm').onsubmit = function(e) {
    e.preventDefault();
    
    const user = {
        name: document.getElementById('name').value,
        email: document.getElementById('email').value
    };
    
    fetch(API_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    })
    .then(response => response.json())
    .then(() => {
        loadUsers();  // Refresh list
        e.target.reset();  // Clear form
    });
};