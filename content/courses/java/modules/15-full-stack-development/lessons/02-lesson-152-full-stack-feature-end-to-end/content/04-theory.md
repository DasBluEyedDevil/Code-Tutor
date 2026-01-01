---
type: "THEORY"
title: "Step 3: Frontend HTML"
---

<!DOCTYPE html>
<html>
<head>
    <title>User Manager</title>
</head>
<body>
    <h1>Users</h1>
    
    <!-- Display users -->
    <div id="userList"></div>
    
    <!-- Add user form -->
    <h2>Add User</h2>
    <form id="addUserForm">
        <input type="text" id="name" placeholder="Name" required>
        <input type="email" id="email" placeholder="Email" required>
        <button type="submit">Add User</button>
    </form>
    
    <script src="app.js"></script>
</body>
</html>