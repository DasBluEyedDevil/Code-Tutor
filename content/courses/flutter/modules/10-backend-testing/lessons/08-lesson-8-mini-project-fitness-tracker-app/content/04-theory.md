---
type: "THEORY"
title: "Step 2: Models"
---


### User Profile Model

**lib/models/user_profile.dart:**

### Workout Model

**lib/models/workout.dart:**



```dart
class Workout {
  final int? id;
  final String type;  // 'running', 'cycling', 'gym', 'walking'
  final DateTime startTime;
  final DateTime endTime;
  final double? distance;  // in km (null for gym workouts)
  final int calories;
  final String? notes;
  final String? routeJson;  // JSON string of LatLng points

  Workout({
    this.id,
    required this.type,
    required this.startTime,
    required this.endTime,
    this.distance,
    required this.calories,
    this.notes,
    this.routeJson,
  });

  Duration get duration => endTime.difference(startTime);

  double? get avgSpeed {
    if (distance == null || distance == 0) return null;
    final hours = duration.inMinutes / 60;
    return distance! / hours;  // km/h
  }

  Map<String, dynamic> toMap() {
    return {
      'id': id,
      'type': type,
      'start_time': startTime.millisecondsSinceEpoch,
      'end_time': endTime.millisecondsSinceEpoch,
      'distance': distance,
      'calories': calories,
      'notes': notes,
      'route_json': routeJson,
    };
  }

  factory Workout.fromMap(Map<String, dynamic> map) {
    return Workout(
      id: map['id'],
      type: map['type'],
      startTime: DateTime.fromMillisecondsSinceEpoch(map['start_time']),
      endTime: DateTime.fromMillisecondsSinceEpoch(map['end_time']),
      distance: map['distance'],
      calories: map['calories'],
      notes: map['notes'],
      routeJson: map['route_json'],
    );
  }
}
```
