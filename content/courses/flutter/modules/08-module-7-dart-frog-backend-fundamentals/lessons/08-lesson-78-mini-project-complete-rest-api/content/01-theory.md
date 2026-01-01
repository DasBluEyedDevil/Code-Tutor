---
type: "THEORY"
title: "Project Overview: Notes API"
---


### What You'll Build

A complete REST API for a Notes application with authentication:

**Public Endpoints** (no auth required):
- `POST /auth/register` - Create a new user account
- `POST /auth/login` - Login and receive JWT token

**Protected Endpoints** (require valid JWT):
- `GET /notes` - List all notes for the authenticated user
- `POST /notes` - Create a new note
- `GET /notes/:id` - Get a specific note by ID
- `PUT /notes/:id` - Update an existing note
- `DELETE /notes/:id` - Delete a note

### API Specification

**Register**
```bash
POST /auth/register
{"email": "user@example.com", "password": "secret123"}

# Response 201:
{"message": "User created", "userId": "usr_abc123"}

# Response 400 (if email exists):
{"error": "Email already registered"}
```

**Login**
```bash
POST /auth/login
{"email": "user@example.com", "password": "secret123"}

# Response 200:
{"token": "eyJhbGciOiI...", "userId": "usr_abc123"}

# Response 401:
{"error": "Invalid credentials"}
```

**Notes CRUD**
```bash
# List notes (protected)
GET /notes
Authorization: Bearer <token>
# Response 200:
{"notes": [{"id": "...", "title": "...", "content": "..."}]}

# Create note (protected)
POST /notes
Authorization: Bearer <token>
{"title": "My Note", "content": "Note content here"}
# Response 201:
{"id": "note_xyz", "title": "My Note", ...}

# Update note (protected)
PUT /notes/note_xyz
Authorization: Bearer <token>
{"title": "Updated Title"}
# Response 200:
{"id": "note_xyz", "title": "Updated Title", ...}

# Delete note (protected)
DELETE /notes/note_xyz
Authorization: Bearer <token>
# Response 200:
{"message": "Note deleted"}
```

