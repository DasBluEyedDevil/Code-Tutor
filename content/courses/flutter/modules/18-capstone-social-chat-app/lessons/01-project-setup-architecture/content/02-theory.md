---
type: "THEORY"
title: "Monorepo Structure"
---


**Why a Monorepo?**

For full-stack Flutter/Serverpod development, a monorepo (single repository containing multiple projects) offers significant advantages:

- **Shared Models**: Define data classes once, use everywhere
- **Atomic Changes**: Update client and server together
- **Simplified Dependencies**: One place for version management
- **Easier Refactoring**: IDE support across the entire codebase

**Our Project Structure**

```
social_chat/
├── app/                          # Flutter mobile/web app
│   ├── lib/
│   │   ├── main.dart
│   │   ├── core/                 # App-wide utilities
│   │   │   ├── config/
│   │   │   ├── router/
│   │   │   └── theme/
│   │   ├── features/             # Feature modules
│   │   │   ├── auth/
│   │   │   ├── chat/
│   │   │   ├── contacts/
│   │   │   └── profile/
│   │   └── shared/               # Shared widgets
│   ├── pubspec.yaml
│   └── test/
│
├── server/                       # Serverpod backend
│   ├── lib/
│   │   ├── server.dart
│   │   └── src/
│   │       ├── endpoints/        # API endpoints
│   │       ├── services/         # Business logic
│   │       └── util/             # Utilities
│   ├── config/
│   └── pubspec.yaml
│
├── shared/                       # Shared Dart code
│   ├── lib/
│   │   ├── models/               # Data models
│   │   ├── protocols/            # Serverpod protocols
│   │   └── constants/            # Shared constants
│   └── pubspec.yaml
│
├── docs/                         # Documentation
│   ├── api/
│   └── architecture/
│
└── scripts/                      # Build/deploy scripts
    ├── setup.sh
    └── deploy.sh
```

**Key Directories Explained**

**app/**: The Flutter client application. This is what users download and run on their devices.

**server/**: The Serverpod backend. Handles API requests, WebSocket connections, database operations, and business logic.

**shared/**: Code shared between app and server. Contains:
- **models/**: Data classes used by both client and server
- **protocols/**: Serverpod protocol definitions (auto-generates serialization)
- **constants/**: Shared constants like API versions, error codes

