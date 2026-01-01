---
type: "THEORY"
title: "The Full-Stack Feature Journey"
---

Let's build a complete feature: USER TASK MANAGEMENT

Feature: Users can create, view, update, and delete tasks

THE COMPLETE STACK:

1. DATABASE (PostgreSQL):
   - tasks table (id, title, description, completed, user_id)

2. BACKEND (Spring Boot):
   - Task entity (Java class mapped to database)
   - TaskRepository (database access)
   - TaskService (business logic)
   - TaskController (REST API endpoints)

3. API LAYER (REST):
   - GET /api/tasks - List all tasks
   - POST /api/tasks - Create task
   - PUT /api/tasks/{id} - Update task
   - DELETE /api/tasks/{id} - Delete task

4. FRONTEND (HTML + JavaScript):
   - Task list display
   - Create task form
   - Mark complete button
   - Delete button

We'll build this step-by-step, bottom-up!