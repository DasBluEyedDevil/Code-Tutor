---
type: "THEORY"
title: "Project Setup"
---


**Build an Offline-First Notes App**

In this project, you'll combine everything learned to build a notes app that:
- Works completely offline
- Syncs when connection is available
- Shows sync status on each note
- Handles conflicts gracefully

**Project Structure:**

```
lib/
  main.dart
  database/
    database.dart
    database.g.dart
  models/
    note.dart
    sync_operation.dart
  services/
    notes_service.dart
    sync_service.dart
    connectivity_service.dart
  providers/
    notes_provider.dart
  screens/
    notes_list_screen.dart
    note_editor_screen.dart
  widgets/
    sync_status_indicator.dart
    offline_banner.dart
```

