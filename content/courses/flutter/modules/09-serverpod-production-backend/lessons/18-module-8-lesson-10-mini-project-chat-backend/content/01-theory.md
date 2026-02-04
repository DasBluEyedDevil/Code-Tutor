---
type: "THEORY"
title: "What You Will Build"
---

Welcome to the capstone project for Module 8! In this lesson, you will build a complete, production-ready chat backend using Serverpod. This project combines every concept you have learned throughout this module into a cohesive, real-world application.

**The ChatPod Backend:**

You will build a full-featured chat server that supports:

- User authentication with email/password
- Real-time messaging with WebSocket streams
- Message persistence in PostgreSQL
- File attachments for sharing images and documents
- Typing indicators to show when users are composing messages
- Online presence detection
- Chat room management (create, join, leave)
- Message history with pagination

**Learning Objectives:**

By the end of this project, you will have:
- Integrated multiple Serverpod features into a cohesive system
- Understood how authentication, streaming, and database operations work together
- Built a scalable architecture for real-time applications
- Created a backend you can use as a foundation for your own apps

**Prerequisites:**

You should have completed all previous lessons in Module 8:
- Serverpod setup and project structure (8.2)
- Models and ORM (8.3)
- Endpoints and CRUD (8.4)
- Authentication (8.5 and 8.6)
- Real-time streams (8.7)

> **Version Note:** This project uses Serverpod 2.x patterns. Ensure your CLI is pinned: `dart pub global activate serverpod_cli ^2.0.0`. Serverpod 3.x has breaking changes in authentication and streaming APIs.

Let us begin building ChatPod!

