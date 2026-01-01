---
type: "KEY_POINT"
title: "Project Setup Checklist"
---


**Before Moving Forward**

Ensure you've completed these setup steps:

**Environment Setup**
- [ ] Dart SDK 3.0+ installed
- [ ] Flutter 3.10+ installed
- [ ] Serverpod CLI installed (`dart pub global activate serverpod_cli`)
- [ ] Docker installed (for local database)
- [ ] IDE configured (VS Code or Android Studio with Dart/Flutter plugins)

**Project Creation**
- [ ] Created Serverpod project with `serverpod create social_chat`
- [ ] Reorganized folders (server/, app/, shared/)
- [ ] Updated pubspec.yaml files with correct paths
- [ ] Added required dependencies

**Configuration Files**
- [ ] Created `app/lib/core/config/app_config.dart` with server URLs
- [ ] Set up `server/config/` with development/production configs
- [ ] Created `.env` files for secrets (never commit these!)

**Initial Run Test**
- [ ] Started Docker containers: `docker compose up -d`
- [ ] Started server: `cd server && dart run bin/main.dart`
- [ ] Started Flutter app: `cd app && flutter run`
- [ ] Verified client can connect to server

**Version Control**
- [ ] Initialized git repository
- [ ] Created `.gitignore` with proper exclusions
- [ ] Made initial commit

