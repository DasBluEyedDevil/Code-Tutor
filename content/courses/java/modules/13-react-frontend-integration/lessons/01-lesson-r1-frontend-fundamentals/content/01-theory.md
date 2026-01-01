---
type: "THEORY"
title: "The Problem: Your API Needs a Face"
---

You've built a powerful Spring Boot API. It can authenticate users, store data, and process requests. But there's a problem:

CURL COMMAND:
curl -X POST http://localhost:8080/api/login \
  -H "Content-Type: application/json" \
  -d '{"username":"john","password":"secret"}'

Is this how your users will interact with your app? Through terminal commands?

Of course not. Users need:
- Buttons to click
- Forms to fill out
- Visual feedback
- Intuitive navigation

This is where the FRONTEND comes in. The frontend is the user interface - what people see and interact with. Your Spring Boot backend is the engine; the frontend is the dashboard and steering wheel.