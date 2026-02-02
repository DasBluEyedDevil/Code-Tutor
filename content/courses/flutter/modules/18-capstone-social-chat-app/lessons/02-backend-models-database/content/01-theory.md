---
type: "THEORY"
title: "Data Model Overview"
---


**Designing the Data Foundation**

Before writing any code, we need to carefully design our data models. A social chat application has several interconnected entities that must work together seamlessly.

**Core Entities**

Our application needs these primary data models:

| Entity | Purpose | Key Relationships |
|--------|---------|------------------|
| **User** | User accounts and profiles | Has many posts, messages, conversations |
| **Post** | Social feed content | Belongs to user, has many comments |
| **Comment** | Responses to posts | Belongs to user and post |
| **Conversation** | Chat threads | Has many participants and messages |
| **Message** | Individual chat messages | Belongs to conversation and sender |
| **Participant** | Conversation membership | Links users to conversations |

**Entity Relationship Diagram**

```
┌─────────────┐       ┌─────────────┐       ┌─────────────┐
│    User     │──────<│    Post     │──────<│   Comment   │
│             │ 1:N   │             │ 1:N   │             │
└─────────────┘       └─────────────┘       └─────────────┘
       │                                           │
       │ 1:N                                       │ N:1
       ▼                                           │
┌─────────────┐       ┌─────────────┐              │
│ Participant │>──────│Conversation │              │
│             │ N:1   │             │              │
└─────────────┘       └─────────────┘              │
       │                     │                     │
       │ N:1                 │ 1:N                 │
       ▼                     ▼                     │
┌─────────────┐       ┌─────────────┐              │
│    User     │<──────│   Message   │<─────────────┘
│  (sender)   │ 1:N   │             │     (author)
└─────────────┘       └─────────────┘
```

**Serverpod Model Philosophy**

Serverpod uses YAML protocol files to define models. These files:

1. **Define the schema** - Fields, types, and relationships
2. **Generate Dart classes** - Immutable data classes with serialization
3. **Create database tables** - PostgreSQL migrations are generated automatically
4. **Enable type-safe APIs** - Client and server share the same types

**Key Design Decisions**

1. **Soft deletes**: Use `isDeleted` flag instead of removing records
2. **Timestamps**: All entities have `createdAt` and `updatedAt`
3. **Nullable relations**: Optional fields for flexibility
4. **Indexed fields**: Optimize common query patterns

