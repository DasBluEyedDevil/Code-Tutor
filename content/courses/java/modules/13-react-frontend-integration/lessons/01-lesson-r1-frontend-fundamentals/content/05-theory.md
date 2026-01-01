---
type: "THEORY"
title: "Full-Stack Architecture"
---

Your complete application will have two parts:

BACKEND (Spring Boot) - Port 8080:
- REST API endpoints
- Business logic
- Database access
- Authentication
- Runs on JVM

FRONTEND (React) - Port 5173:
- User interface
- Forms and buttons
- Displays data from API
- Handles user interactions
- Runs in browser

DATA FLOW:

[User Browser]
     |
     | (1) User clicks 'Login'
     v
[React Frontend :5173]
     |
     | (2) fetch('http://localhost:8080/api/login')
     v
[Spring Boot :8080]
     |
     | (3) Validate credentials
     v
[Database]
     |
     | (4) Return user data
     v
[Spring Boot :8080]
     |
     | (5) Send JSON response
     v
[React Frontend]
     |
     | (6) Update UI with user info
     v
[User sees: 'Welcome, John!']

This is the architecture you'll build in this module.