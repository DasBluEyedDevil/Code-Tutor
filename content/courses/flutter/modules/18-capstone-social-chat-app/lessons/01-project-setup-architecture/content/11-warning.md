---
type: "WARNING"
title: "Common Setup Mistakes"
---


**1. Wrong Dart/Flutter Versions**

Serverpod 2.x requires Dart 3.0+. Check your versions:

```bash
dart --version   # Should be 3.0.0 or higher
flutter --version # Should be 3.10.0 or higher
```

**2. Path Dependency Errors**

If you see "Could not find package" errors, check that your path dependencies are correct:

```yaml
# Relative paths must be exact
dependencies:
  social_chat_shared:
    path: ../shared  # Not ./shared or shared/
```

**3. Docker Not Running**

Serverpod needs PostgreSQL and Redis. If the server won't start:

```bash
# Check if containers are running
docker ps

# Start containers if needed
cd server
docker compose up -d
```

**4. Port Conflicts**

Default ports used:
- 8080: Serverpod API
- 8081: Serverpod Insights
- 5432: PostgreSQL
- 6379: Redis

If you have conflicts, update `server/config/development.yaml`.

**5. Forgetting to Generate Code**

After changing protocol files, always regenerate:

```bash
cd server
serverpod generate
```

**6. IDE Cache Issues**

If imports aren't recognized after generation:

```bash
# Restart Dart analysis server
# VS Code: Cmd/Ctrl + Shift + P -> "Dart: Restart Analysis Server"

# Or clear and rebuild
flutter clean
flutter pub get
```

