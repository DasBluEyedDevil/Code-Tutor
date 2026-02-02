---
type: "KEY_POINT"
title: "Fetching and Activating Config Values"
---


**The Fetch-Activate Pattern**

Remote Config uses a two-step process to prevent jarring UI changes:

1. **Fetch** - Download new values from server (stored locally)
2. **Activate** - Apply the fetched values to the app

```dart
// Fetch latest values
await remoteConfig.fetch();

// Activate them (makes them available via get methods)
await remoteConfig.activate();

// Or do both in one call
await remoteConfig.fetchAndActivate();
```

**When to Fetch**

- **App Launch** - Get fresh values at startup
- **Foreground Resume** - Refresh when user returns
- **Pull to Refresh** - Manual refresh option
- **Periodic** - Background refresh on a schedule

**Lifecycle-Aware Fetching**

```dart
class _MyAppState extends State<MyApp> with WidgetsBindingObserver {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addObserver(this);
  }
  
  @override
  void dispose() {
    WidgetsBinding.instance.removeObserver(this);
    super.dispose();
  }
  
  @override
  void didChangeAppLifecycleState(AppLifecycleState state) {
    if (state == AppLifecycleState.resumed) {
      // Refresh feature flags when app comes to foreground
      FeatureFlagService().refresh();
    }
  }
}
```

**Real-Time Updates (Firebase Real-Time Remote Config)**

For immediate updates without fetch delays:

```dart
// Enable real-time listeners
remoteConfig.onConfigUpdated.listen((event) async {
  // New config available
  await remoteConfig.activate();
  print('Config updated in real-time');
});
```

**Fetch Throttling**

Firebase throttles fetches to prevent abuse:

- Default minimum interval: 12 hours
- Can reduce for development
- Production apps should respect throttling

```dart
// Development: Allow frequent fetches
if (kDebugMode) {
  await remoteConfig.setConfigSettings(RemoteConfigSettings(
    fetchTimeout: const Duration(minutes: 1),
    minimumFetchInterval: Duration.zero, // No throttling in debug
  ));
}
```

