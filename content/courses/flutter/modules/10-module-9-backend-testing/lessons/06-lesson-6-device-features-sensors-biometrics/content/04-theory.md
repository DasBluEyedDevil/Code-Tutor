---
type: "THEORY"
title: "Part 2: Motion Sensors"
---


### Setup

**pubspec.yaml:**

**iOS Configuration (`ios/Runner/Info.plist`):**

### Accelerometer (Detects Device Motion)


### Gyroscope (Detects Rotation)




```dart
class GyroscopeScreen extends StatefulWidget {
  @override
  State<GyroscopeScreen> createState() => _GyroscopeScreenState();
}

class _GyroscopeScreenState extends State<GyroscopeScreen> {
  double _rotationX = 0.0, _rotationY = 0.0, _rotationZ = 0.0;
  StreamSubscription? _gyroscopeSubscription;

  @override
  void initState() {
    super.initState();

    _gyroscopeSubscription = gyroscopeEventStream().listen((GyroscopeEvent event) {
      setState(() {
        _rotationX = event.x;  // Pitch (nose up/down)
        _rotationY = event.y;  // Roll (wing up/down)
        _rotationZ = event.z;  // Yaw (turn left/right)
      });
    });
  }

  @override
  void dispose() {
    _gyroscopeSubscription?.cancel();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Gyroscope')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Text('Rotation Rate (radians/second)', style: TextStyle(fontSize: 20)),
            SizedBox(height: 40),

            _buildRotationIndicator('Pitch (X)', _rotationX, Colors.red),
            SizedBox(height: 20),
            _buildRotationIndicator('Roll (Y)', _rotationY, Colors.green),
            SizedBox(height: 20),
            _buildRotationIndicator('Yaw (Z)', _rotationZ, Colors.blue),

            SizedBox(height: 40),

            Text(
              'Tilt your phone to see rotation values',
              style: TextStyle(color: Colors.grey),
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildRotationIndicator(String label, double value, Color color) {
    return Column(
      children: [
        Text(label, style: TextStyle(fontSize: 16)),
        SizedBox(height: 8),
        Container(
          width: 300,
          height: 40,
          decoration: BoxDecoration(
            border: Border.all(color: Colors.grey),
            borderRadius: BorderRadius.circular(8),
          ),
          child: Stack(
            children: [
              // Center line
              Center(
                child: Container(
                  width: 2,
                  height: 40,
                  color: Colors.grey,
                ),
              ),
              // Indicator
              Align(
                alignment: Alignment(value.clamp(-1.0, 1.0), 0),
                child: Container(
                  width: 20,
                  height: 40,
                  color: color,
                ),
              ),
            ],
          ),
        ),
        SizedBox(height: 4),
        Text('${value.toStringAsFixed(2)} rad/s'),
      ],
    );
  }
}
```
