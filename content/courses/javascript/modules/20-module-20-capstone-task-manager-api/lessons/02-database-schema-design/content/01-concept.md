---
type: "THEORY"
title: "Data Modeling for Task Manager"
---

Good database design is the foundation of any application. Let's think through our data model:

**Entities:**
1. **User** - The person using the app
   - Has many tasks
   - Has many categories
   - Stores hashed password (never plain text!)

2. **Task** - A todo item
   - Belongs to one user (owner)
   - Optionally belongs to one category
   - Has status, priority, due date

3. **Category** - For organizing tasks
   - Belongs to one user (owner)
   - Has many tasks
   - Simple: just name and color

**Key Relationships:**
- User 1:N Tasks (one user has many tasks)
- User 1:N Categories (one user has many categories)
- Category 1:N Tasks (one category has many tasks)

**Access Control:**
- Users can only see/modify their own tasks and categories
- No sharing between users in this version