---
type: "THEORY"
title: "Platform Channel Types"
---


| Channel Type | Description | Use Case |
|--------------|-------------|----------|
| **MethodChannel** | Two-way async method calls | Most common - API calls, getting data |
| **EventChannel** | One-way stream from native | Continuous data (sensors, location) |
| **BasicMessageChannel** | Raw message passing | Custom encoding, simple messages |

### EventChannel Example (Continuous Data)

```dart
// Dart side - receive continuous updates
class SensorStream {
  static const eventChannel = EventChannel('com.myapp/sensor');
  
  static Stream<double> get sensorStream {
    return eventChannel.receiveBroadcastStream().map((value) => value as double);
  }
}

// Usage
SensorStream.sensorStream.listen((value) {
  print('Sensor value: $value');
});
```

### Best Practices

1. **Use unique channel names** - reverse domain format (`com.myapp/feature`)
2. **Handle errors gracefully** - wrap in try-catch, provide fallbacks
3. **Check platform first** - use `Platform.isAndroid` / `Platform.isIOS`
4. **Test on both platforms** - native code differs between Android/iOS
5. **Prefer plugins** - only use channels when no plugin exists

**Pro Tip:** Before writing platform channels, check [pub.dev](https://pub.dev) - there's probably already a plugin for what you need!

