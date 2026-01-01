---
type: "THEORY"
title: "The DOM: Your Page as a Tree"
---

When a browser loads HTML, it creates the DOM (Document Object Model) - a tree structure representing your page:

HTML:
<html>
  <body>
    <div id="app">
      <h1>Welcome</h1>
      <button>Click me</button>
    </div>
  </body>
</html>

DOM TREE:
html
└── body
    └── div#app
        ├── h1 ("Welcome")
        └── button ("Click me")

JavaScript can:
- READ the DOM: document.getElementById('app')
- MODIFY the DOM: element.textContent = 'New text'
- ADD to the DOM: parent.appendChild(newElement)
- REMOVE from DOM: element.remove()

THE PROBLEM WITH VANILLA JS:

// Updating a user list manually
function updateUsers(users) {
    const list = document.getElementById('userList');
    list.innerHTML = '';  // Clear existing
    users.forEach(user => {
        const li = document.createElement('li');
        li.textContent = user.name;
        li.addEventListener('click', () => selectUser(user.id));
        list.appendChild(li);
    });
}

This is tedious, error-prone, and doesn't scale. React solves this.