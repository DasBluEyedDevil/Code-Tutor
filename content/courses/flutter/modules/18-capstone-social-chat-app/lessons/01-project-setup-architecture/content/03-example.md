---
type: "EXAMPLE"
title: "Serverpod Project Creation"
---


**Step 1: Install Serverpod CLI**

First, ensure you have Serverpod CLI installed:

```bash
# Install Serverpod CLI globally
dart pub global activate serverpod_cli

# Verify installation
serverpod version
```

**Step 2: Create the Project**

Serverpod's CLI creates the monorepo structure automatically:

```bash
# Create new Serverpod project
serverpod create social_chat

# This creates:
# social_chat/
# ├── social_chat_server/     # Backend
# ├── social_chat_client/     # Generated client code
# └── social_chat_flutter/    # Flutter app
```

**Step 3: Reorganize for Cleaner Structure**

Serverpod's default names are verbose. Let's rename for clarity:

```bash
cd social_chat

# Rename directories
mv social_chat_server server
mv social_chat_flutter app
mv social_chat_client shared
```

**Step 4: Update Package References**

After renaming, update pubspec.yaml files:



```yaml
# app/pubspec.yaml
name: social_chat_app
description: Social Chat Flutter Application

environment:
  sdk: '>=3.10.0 <4.0.0'
  flutter: '>=3.10.0'

dependencies:
  flutter:
    sdk: flutter
  
  # Serverpod client
  serverpod_client: ^2.0.0
  serverpod_auth_client: ^2.0.0
  serverpod_flutter: ^2.0.0
  
  # Our shared models
  social_chat_shared:
    path: ../shared
  
  # State management
  flutter_riverpod: ^2.6.1
  riverpod_annotation: ^2.6.1
  
  # Navigation
  go_router: ^17.0.0
  
  # UI
  cached_network_image: ^3.3.0
  flutter_animate: ^4.5.0
  
  # Storage
  shared_preferences: ^2.2.0
  flutter_secure_storage: ^9.0.0

dev_dependencies:
  flutter_test:
    sdk: flutter
  flutter_lints: ^4.0.0
  riverpod_generator: ^2.6.1
  build_runner: ^2.4.0
  mockito: ^5.4.0
```
