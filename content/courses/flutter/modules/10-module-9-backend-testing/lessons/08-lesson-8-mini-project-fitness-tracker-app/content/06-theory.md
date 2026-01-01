---
type: "THEORY"
title: "Step 4: Main App Setup"
---


**lib/main.dart:**



```dart
import 'package:flutter/material.dart';
import 'package:hive_flutter/hive_flutter.dart';
import 'package:workmanager/workmanager.dart';
import 'models/user_profile.dart';
import 'screens/home_screen.dart';
import 'screens/profile_screen.dart';
import 'services/database_service.dart';

// Background task callback
@pragma('vm:entry-point')
void callbackDispatcher() {
  Workmanager().executeTask((task, inputData) async {
    switch (task) {
      case 'dailyReminder':
        // In real app: trigger local notification
        print('⏰ Daily workout reminder!');
        break;
      case 'syncData':
        // In real app: sync to cloud
        print('☁️ Syncing workout data...');
        break;
    }

    return Future.value(true);
  });
}

void main() async {
  WidgetsFlutterBinding.ensureInitialized();

  // Initialize Hive
  await Hive.initFlutter();
  Hive.registerAdapter(UserProfileAdapter());
  await Hive.openBox<UserProfile>('profile');
  await Hive.openBox('settings');

  // Initialize SQLite
  await DatabaseService().database;

  // Initialize Workmanager
  await Workmanager().initialize(callbackDispatcher, isInDebugMode: true);

  // Register daily reminder (8 AM every day)
  await Workmanager().registerPeriodicTask(
    'daily-reminder',
    'dailyReminder',
    frequency: Duration(hours: 24),
    initialDelay: _calculateDelayUntil8AM(),
  );

  runApp(FitnessTrackerApp());
}

Duration _calculateDelayUntil8AM() {
  final now = DateTime.now();
  var next8AM = DateTime(now.year, now.month, now.day, 8, 0);

  if (now.isAfter(next8AM)) {
    next8AM = next8AM.add(Duration(days: 1));
  }

  return next8AM.difference(now);
}

class FitnessTrackerApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Fitness Tracker',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.blue),
        useMaterial3: true,
      ),
      darkTheme: ThemeData.dark(useMaterial3: true),
      home: HomeScreen(),
    );
  }
}
```
