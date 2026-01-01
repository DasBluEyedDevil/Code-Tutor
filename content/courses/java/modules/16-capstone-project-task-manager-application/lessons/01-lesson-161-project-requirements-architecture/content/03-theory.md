---
type: "THEORY"
title: "Data Model: User, Task, and Category Entities"
---

Before writing any code, we must design our data model. The data model defines what information we store and how entities relate to each other.

User Entity:
The User represents a registered account in our system. Each user has:
- id: Unique identifier (auto-generated)
- email: User's email address (unique, used for login)
- password: Hashed password (never store plain text!)
- name: User's display name
- role: USER or ADMIN (for authorization)
- createdAt: When the account was created
- tasks: List of tasks owned by this user
- categories: List of categories created by this user

Task Entity:
The Task represents a single work item. Each task has:
- id: Unique identifier
- title: Short description of the task (required)
- description: Detailed notes about the task (optional)
- status: PENDING, IN_PROGRESS, or COMPLETED
- priority: LOW, MEDIUM, HIGH, or URGENT
- dueDate: When the task should be completed (optional)
- createdAt: When the task was created
- updatedAt: When the task was last modified
- owner: The User who owns this task
- category: The Category this task belongs to (optional)

Category Entity:
The Category helps organize tasks into groups. Each category has:
- id: Unique identifier
- name: Category name (e.g., "Work", "Personal")
- description: What this category is for (optional)
- color: Hex color code for UI display (e.g., "#3B82F6")
- owner: The User who created this category
- tasks: List of tasks in this category

Relationships:
- One User has many Tasks (one-to-many)
- One User has many Categories (one-to-many)
- One Category has many Tasks (one-to-many)
- One Task belongs to one User (many-to-one)
- One Task optionally belongs to one Category (many-to-one)

This normalized design prevents data duplication and maintains referential integrity. When we delete a User, their Tasks and Categories are also deleted (cascade). When we delete a Category, Tasks in that category have their category set to null (set null).