---
type: "EXAMPLE"
title: "Basic Rive Animation"
---


Load and play Rive animations:



```dart
import 'package:flutter/material.dart';
import 'package:rive/rive.dart';

class RiveExamples extends StatelessWidget {
  const RiveExamples({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // Simple Rive animation from assets
        const SizedBox(
          width: 200,
          height: 200,
          child: RiveAnimation.asset(
            'assets/animations/character.riv',
            fit: BoxFit.contain,
          ),
        ),
        
        // Load from network
        const SizedBox(
          width: 150,
          height: 150,
          child: RiveAnimation.network(
            'https://cdn.rive.app/animations/vehicles.riv',
            fit: BoxFit.contain,
          ),
        ),
        
        // Specify artboard and animation names
        const SizedBox(
          width: 200,
          height: 200,
          child: RiveAnimation.asset(
            'assets/animations/multi_artboard.riv',
            artboard: 'Hero', // Specific artboard
            animations: ['idle', 'walk'], // Multiple animations
            fit: BoxFit.contain,
          ),
        ),
      ],
    );
  }
}

// Controlled Rive animation with callbacks
class ControlledRive extends StatefulWidget {
  const ControlledRive({super.key});

  @override
  State<ControlledRive> createState() => _ControlledRiveState();
}

class _ControlledRiveState extends State<ControlledRive> {
  // Controllers for managing animations
  StateMachineController? _controller;
  
  void _onRiveInit(Artboard artboard) {
    // Find and initialize the state machine
    final controller = StateMachineController.fromArtboard(
      artboard,
      'State Machine 1', // Name from Rive editor
    );
    
    if (controller != null) {
      artboard.addController(controller);
      _controller = controller;
    }
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 300,
      height: 300,
      child: RiveAnimation.asset(
        'assets/animations/interactive.riv',
        onInit: _onRiveInit,
        fit: BoxFit.contain,
      ),
    );
  }
}
```
