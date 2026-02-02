---
type: "THEORY"
title: "Running the Dev Server"
---


Time to see your API in action!

**Start the Development Server**:

From inside your `my_api` directory:

```bash
dart_frog dev
```

**What You'll See**:
```
[hotreload] Hot reload is enabled.
[server] Running on http://localhost:8080
```

**Visit Your API**:

Open a browser and go to: `http://localhost:8080`

You should see:
```
Welcome to Dart Frog!
```

**Hot Reload in Action**:

With the server still running:

1. Open `routes/index.dart`
2. Change the body text:
   ```dart
   return Response(body: 'Hello from MY first Dart Frog API!');
   ```
3. Save the file
4. Refresh your browser

The change appears instantly! No restart needed. This is **hot reload** for backend development.

**Stopping the Server**:

Press `Ctrl+C` in your terminal to stop the server.

