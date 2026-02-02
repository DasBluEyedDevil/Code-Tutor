---
type: "EXAMPLE"
title: "Recommended Directory Structure"
---

A well-organized backend project follows a consistent directory structure that makes it immediately clear where any piece of code belongs. This structure scales from small APIs to large applications with dozens of developers. Each directory has a specific purpose, and files within follow predictable naming conventions. The key insight is that the structure reflects the architecture: routes separate from logic, logic separate from data access.

```plaintext
my-api/
├── src/
│   ├── index.ts              # Application entry point
│   │
│   ├── routes/               # Route definitions (thin layer)
│   │   ├── index.ts          # Main router combining all routes
│   │   ├── users.routes.ts   # User-related routes
│   │   ├── posts.routes.ts   # Post-related routes
│   │   └── auth.routes.ts    # Authentication routes
│   │
│   ├── handlers/             # Request handlers (controllers)
│   │   ├── users.handler.ts  # Handles user requests
│   │   ├── posts.handler.ts  # Handles post requests
│   │   └── auth.handler.ts   # Handles auth requests
│   │
│   ├── services/             # Business logic layer
│   │   ├── user.service.ts   # User business logic
│   │   ├── post.service.ts   # Post business logic
│   │   ├── auth.service.ts   # Authentication logic
│   │   └── email.service.ts  # Email sending logic
│   │
│   ├── db/                   # Database access layer
│   │   ├── index.ts          # Database connection setup
│   │   ├── users.db.ts       # User database operations
│   │   ├── posts.db.ts       # Post database operations
│   │   └── migrations/       # Database migrations
│   │
│   ├── middleware/           # Custom middleware
│   │   ├── auth.ts           # Authentication middleware
│   │   ├── validate.ts       # Validation middleware
│   │   ├── rateLimit.ts      # Rate limiting middleware
│   │   └── errorHandler.ts   # Global error handler
│   │
│   ├── utils/                # Helper functions
│   │   ├── logger.ts         # Logging utilities
│   │   ├── hash.ts           # Password hashing
│   │   ├── jwt.ts            # JWT utilities
│   │   └── dates.ts          # Date formatting
│   │
│   ├── types/                # TypeScript types/interfaces
│   │   ├── user.types.ts     # User-related types
│   │   ├── post.types.ts     # Post-related types
│   │   ├── api.types.ts      # API request/response types
│   │   └── env.d.ts          # Environment variable types
│   │
│   └── config/               # Configuration
│       ├── index.ts          # Main config export
│       ├── database.ts       # Database config
│       └── cors.ts           # CORS config
│
├── tests/                    # Test files (mirrors src/ structure)
│   ├── services/
│   ├── handlers/
│   └── integration/
│
├── .env                      # Environment variables (never commit!)
├── .env.example              # Example env file (commit this)
├── package.json
├── tsconfig.json
└── README.md

# What goes where:
#
# routes/      - Define URL patterns and HTTP methods. Connect paths to handlers.
#                Keep these thin - no business logic here!
#
# handlers/    - Process HTTP requests. Extract data from requests, call services,
#                format responses. Also called "controllers" in some frameworks.
#
# services/    - Core business logic. Database-agnostic rules and operations.
#                This is where most of your application logic lives.
#
# db/          - Database operations. SQL queries, ORM calls, connection pooling.
#                Isolates data access so you can swap databases later.
#
# middleware/  - Request/response processing. Authentication, logging, validation.
#                Runs before or after handlers.
#
# utils/       - Pure helper functions. No side effects, highly reusable.
#                Things like formatting, hashing, date manipulation.
#
# types/       - TypeScript interfaces and type definitions.
#                Shared across the codebase for type safety.
#
# config/      - Application configuration. Environment-based settings.
#                Centralizes all config in one place.
```
