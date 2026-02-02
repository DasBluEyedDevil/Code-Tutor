---
type: "THEORY"
title: "Complete Platform Channel Example: Get Battery Level"
---


### Step 1: Flutter Side (Dart)

**lib/services/battery_service.dart:**

```dart
import 'package:flutter/services.dart';

class BatteryService {
  // Create a channel with a unique name
  static const platform = MethodChannel('com.myapp/battery');
  
  // Call native code to get battery level
  static Future<int> getBatteryLevel() async {
    try {
      // invokeMethod sends a message to native code
      final int result = await platform.invokeMethod('getBatteryLevel');
      return result;
    } on PlatformException catch (e) {
      print('Failed to get battery level: ${e.message}');
      return -1;
    }
  }
  
  // Call native code with arguments
  static Future<bool> setBatteryAlarm(int threshold) async {
    try {
      final result = await platform.invokeMethod(
        'setBatteryAlarm',
        {'threshold': threshold},  // Pass data to native
      );
      return result as bool;
    } on PlatformException {
      return false;
    }
  }
}
```

### Step 2: Android Side (Kotlin)

**android/app/src/main/kotlin/.../MainActivity.kt:**

```kotlin
package com.example.myapp

import android.content.Context
import android.content.ContextWrapper
import android.content.Intent
import android.content.IntentFilter
import android.os.BatteryManager
import android.os.Build
import io.flutter.embedding.android.FlutterActivity
import io.flutter.embedding.engine.FlutterEngine
import io.flutter.plugin.common.MethodChannel

class MainActivity : FlutterActivity() {
    private val CHANNEL = "com.myapp/battery"  // Must match Dart!

    override fun configureFlutterEngine(flutterEngine: FlutterEngine) {
        super.configureFlutterEngine(flutterEngine)

        MethodChannel(flutterEngine.dartExecutor.binaryMessenger, CHANNEL)
            .setMethodCallHandler { call, result ->
                when (call.method) {
                    "getBatteryLevel" -> {
                        val batteryLevel = getBatteryLevel()
                        if (batteryLevel != -1) {
                            result.success(batteryLevel)
                        } else {
                            result.error("UNAVAILABLE", "Battery level not available", null)
                        }
                    }
                    "setBatteryAlarm" -> {
                        val threshold = call.argument<Int>("threshold") ?: 20
                        // Implement your alarm logic here
                        result.success(true)
                    }
                    else -> result.notImplemented()
                }
            }
    }

    private fun getBatteryLevel(): Int {
        return if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.LOLLIPOP) {
            val batteryManager = getSystemService(Context.BATTERY_SERVICE) as BatteryManager
            batteryManager.getIntProperty(BatteryManager.BATTERY_PROPERTY_CAPACITY)
        } else {
            val intent = ContextWrapper(applicationContext)
                .registerReceiver(null, IntentFilter(Intent.ACTION_BATTERY_CHANGED))
            (intent?.getIntExtra(BatteryManager.EXTRA_LEVEL, -1) ?: -1) * 100 /
                (intent?.getIntExtra(BatteryManager.EXTRA_SCALE, -1) ?: 1)
        }
    }
}
```

### Step 3: iOS Side (Swift)

**ios/Runner/AppDelegate.swift:**

```swift
import UIKit
import Flutter

@UIApplicationMain
@objc class AppDelegate: FlutterAppDelegate {
    override func application(
        _ application: UIApplication,
        didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?
    ) -> Bool {
        let controller = window?.rootViewController as! FlutterViewController
        
        let batteryChannel = FlutterMethodChannel(
            name: "com.myapp/battery",  // Must match Dart!
            binaryMessenger: controller.binaryMessenger
        )
        
        batteryChannel.setMethodCallHandler { [weak self] (call, result) in
            switch call.method {
            case "getBatteryLevel":
                self?.receiveBatteryLevel(result: result)
            case "setBatteryAlarm":
                if let args = call.arguments as? [String: Any],
                   let threshold = args["threshold"] as? Int {
                    // Implement alarm logic
                    result(true)
                } else {
                    result(FlutterError(code: "INVALID_ARGS", message: nil, details: nil))
                }
            default:
                result(FlutterMethodNotImplemented)
            }
        }
        
        GeneratedPluginRegistrant.register(with: self)
        return super.application(application, didFinishLaunchingWithOptions: launchOptions)
    }
    
    private func receiveBatteryLevel(result: FlutterResult) {
        UIDevice.current.isBatteryMonitoringEnabled = true
        let batteryLevel = Int(UIDevice.current.batteryLevel * 100)
        
        if batteryLevel == -100 {
            result(FlutterError(
                code: "UNAVAILABLE",
                message: "Battery info unavailable",
                details: nil
            ))
        } else {
            result(batteryLevel)
        }
    }
}
```

### Step 4: Use It in Flutter

```dart
class BatteryScreen extends StatefulWidget {
  @override
  State<BatteryScreen> createState() => _BatteryScreenState();
}

class _BatteryScreenState extends State<BatteryScreen> {
  int _batteryLevel = -1;
  
  @override
  void initState() {
    super.initState();
    _loadBatteryLevel();
  }
  
  Future<void> _loadBatteryLevel() async {
    final level = await BatteryService.getBatteryLevel();
    setState(() => _batteryLevel = level);
  }
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Battery Level')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              _batteryLevel > 50 ? Icons.battery_full : Icons.battery_alert,
              size: 100,
              color: _batteryLevel > 20 ? Colors.green : Colors.red,
            ),
            SizedBox(height: 20),
            Text(
              '$_batteryLevel%',
              style: TextStyle(fontSize: 48, fontWeight: FontWeight.bold),
            ),
            SizedBox(height: 20),
            ElevatedButton(
              onPressed: _loadBatteryLevel,
              child: Text('Refresh'),
            ),
          ],
        ),
      ),
    );
  }
}
```

