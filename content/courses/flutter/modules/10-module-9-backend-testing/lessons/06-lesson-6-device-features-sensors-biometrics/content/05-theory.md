---
type: "THEORY"
title: "Part 3: Shake Detection"
---


### Setup

**pubspec.yaml:**

### Shake to Undo Example




```dart
import 'package:flutter/material.dart';
import 'package:shake/shake.dart';

class ShakeToUndoScreen extends StatefulWidget {
  @override
  State<ShakeToUndoScreen> createState() => _ShakeToUndoScreenState();
}

class _ShakeToUndoScreenState extends State<ShakeToUndoScreen> {
  ShakeDetector? _shakeDetector;
  List<String> _actions = [];
  int _counter = 0;

  @override
  void initState() {
    super.initState();

    // Initialize shake detector
    _shakeDetector = ShakeDetector.autoStart(
      onPhoneShake: (ShakeEvent event) {
        // Called when phone is shaken
        _undoLastAction();

        // Optional: Show shake details
        print('Shake detected!');
        print('Direction: ${event.direction}');  // X, Y, or Z axis
        print('Force: ${event.force}');
        print('Time: ${event.timestamp}');
      },
      minimumShakeCount: 1,
      shakeSlopTimeMS: 500,
      shakeCountResetTime: 3000,
      shakeThresholdGravity: 2.7,
    );
  }

  @override
  void dispose() {
    _shakeDetector?.stopListening();
    super.dispose();
  }

  void _addAction() {
    setState(() {
      _counter++;
      _actions.add('Action $_counter');
    });
  }

  void _undoLastAction() {
    if (_actions.isEmpty) return;

    setState(() {
      final lastAction = _actions.removeLast();
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(
          content: Text('Undid: $lastAction'),
          duration: Duration(seconds: 1),
        ),
      );
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Shake to Undo')),
      body: Column(
        children: [
          Padding(
            padding: EdgeInsets.all(16),
            child: Text(
              'Shake your phone to undo!',
              style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
            ),
          ),

          Expanded(
            child: _actions.isEmpty
                ? Center(child: Text('No actions yet'))
                : ListView.builder(
                    itemCount: _actions.length,
                    itemBuilder: (context, index) {
                      return ListTile(
                        leading: CircleAvatar(child: Text('${index + 1}')),
                        title: Text(_actions[index]),
                        trailing: IconButton(
                          icon: Icon(Icons.delete),
                          onPressed: () {
                            setState(() => _actions.removeAt(index));
                          },
                        ),
                      );
                    },
                  ),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton(
        onPressed: _addAction,
        child: Icon(Icons.add),
        tooltip: 'Add Action',
      ),
    );
  }
}
```
