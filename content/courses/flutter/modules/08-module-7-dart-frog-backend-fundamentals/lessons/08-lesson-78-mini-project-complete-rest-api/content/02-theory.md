---
type: "THEORY"
title: "Project Structure"
---


### Directory Layout

```
my_notes_api/
├── routes/
│   ├── _middleware.dart        # Global middleware (CORS, error handling)
│   ├── auth/
│   │   ├── register.dart       # POST /auth/register
│   │   └── login.dart          # POST /auth/login
│   └── notes/
│       ├── _middleware.dart    # Auth middleware (JWT validation)
│       ├── index.dart          # GET, POST /notes
│       └── [id].dart           # GET, PUT, DELETE /notes/:id
├── lib/
│   ├── models/
│   │   ├── user.dart           # User model class
│   │   └── note.dart           # Note model class
│   ├── services/
│   │   ├── auth_service.dart   # User registration, login logic
│   │   └── note_service.dart   # Note CRUD operations
│   └── utils/
│       └── jwt_helper.dart     # Token creation and verification
└── pubspec.yaml                # Dependencies
```

### Key Architecture Decisions

**1. Separation of Concerns**
- **Routes**: Handle HTTP requests/responses only
- **Services**: Contain business logic (validation, data manipulation)
- **Models**: Define data structures
- **Utils**: Helper functions (JWT, password hashing)

**2. Middleware Hierarchy**
```
routes/
├── _middleware.dart          # Runs for ALL routes (CORS, logging)
├── auth/                     # Public routes - no auth needed
└── notes/
    └── _middleware.dart      # Runs for /notes/* - requires JWT
```

**3. Route Naming Convention**
- `index.dart` = handles the parent path (`/notes`)
- `[id].dart` = dynamic segment (`/notes/abc123`)

