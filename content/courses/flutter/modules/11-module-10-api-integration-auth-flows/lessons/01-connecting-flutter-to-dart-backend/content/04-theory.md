---
type: "THEORY"
title: "Setting Up the Serverpod Client"
---


Let us walk through setting up the Serverpod client in your Flutter app from scratch. If you created your project with `serverpod create`, much of this is already done, but understanding the setup helps you troubleshoot issues and work with existing projects.

**Step 1: Project Structure**

A Serverpod project has this structure after creation:

```
my_project/
|-- my_project_server/       # Backend code
|   |-- lib/
|   |   |-- src/
|   |       |-- endpoints/   # Your API endpoints
|   |       |-- protocol/    # Model definitions (YAML)
|   |-- pubspec.yaml
|
|-- my_project_client/       # Generated client (DO NOT EDIT)
|   |-- lib/
|   |   |-- src/
|   |       |-- protocol/    # Generated Dart classes
|   |-- pubspec.yaml
|
|-- my_project_flutter/      # Your Flutter app
|   |-- lib/
|   |   |-- main.dart
|   |-- pubspec.yaml         # References my_project_client
```

**Step 2: Add Client Dependency**

In your Flutter app's `pubspec.yaml`, add the client package:

```yaml
# my_project_flutter/pubspec.yaml
dependencies:
  flutter:
    sdk: flutter
  
  # Reference the generated client package
  my_project_client:
    path: ../my_project_client
  
  # Serverpod Flutter integration
  serverpod_flutter: ^2.0.0
```

Run `flutter pub get` to install dependencies.

**Step 3: Generate Client Code**

Whenever you change your server endpoints or models, regenerate the client:

```bash
# In the server directory
cd my_project_server
serverpod generate
```

This reads your endpoint definitions and YAML models, then generates corresponding Dart code in the client package.

