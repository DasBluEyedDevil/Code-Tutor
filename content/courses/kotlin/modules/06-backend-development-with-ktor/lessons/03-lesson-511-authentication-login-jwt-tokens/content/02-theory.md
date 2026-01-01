---
type: "THEORY"
title: "Topic Introduction"
---


You've built secure user registration with bcrypt-hashed passwords. Now users can sign up—but how do they prove their identity on subsequent requests?

Traditional web applications use server-side sessions (cookies stored in server memory). But modern APIs need something more scalable and stateless: **JSON Web Tokens (JWT)**.

In this lesson, you'll implement a complete login system that verifies passwords and issues JWTs, allowing users to authenticate with your API without storing session state on the server.

---

