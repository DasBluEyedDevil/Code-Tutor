---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Database fundamentals:

1. **Database Types**:
   - **SQL (Relational)**: Structured tables, relationships, strict schema
     * PostgreSQL (powerful, feature-rich)
     * MySQL (popular, web apps)
     * SQLite (simple, file-based)
   
   - **NoSQL**: Flexible structure, JSON-like
     * MongoDB (document-based)
     * Redis (key-value, caching)

2. **Key Database Concepts**:
   - **Table**: Collection of similar data (like a spreadsheet)
   - **Row**: Individual record (one user, one post)
   - **Column**: Property/field (name, email, age)
   - **Primary Key**: Unique identifier (usually `id`)
   - **Foreign Key**: Reference to another table (userId references users table)

3. **SQL Basics** (Structured Query Language):
   ```sql
   -- Create table
   CREATE TABLE users (
     id INTEGER PRIMARY KEY,
     name TEXT NOT NULL,
     email TEXT UNIQUE
   );
   
   -- Insert data
   INSERT INTO users (name, email) VALUES ('Alice', 'alice@example.com');
   
   -- Read data
   SELECT * FROM users WHERE name = 'Alice';
   
   -- Update data
   UPDATE users SET email = 'newemail@example.com' WHERE id = 1;
   
   -- Delete data
   DELETE FROM users WHERE id = 1;
   ```

4. **CRUD Operations**:
   - **C**reate: INSERT
   - **R**ead: SELECT
   - **U**pdate: UPDATE
   - **D**elete: DELETE

5. **Relationships**:
   - **One-to-Many**: One user has many posts
   - **Many-to-Many**: Users can follow many users, users have many followers
   - **One-to-One**: User has one profile

6. **Choosing a Database**:
   - **PostgreSQL**: Production apps, complex queries, reliability
   - **SQLite**: Development, simple apps, mobile apps
   - **MongoDB**: Flexible schemas, rapid prototyping
   - **Redis**: Caching, sessions, real-time features