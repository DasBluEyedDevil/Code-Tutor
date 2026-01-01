---
type: "EXAMPLE"
title: "Interactive Rive with State Machine Inputs"
---


Control Rive animations with state machine inputs:



```dart
import 'package:flutter/material.dart';
import 'package:rive/rive.dart';

class InteractiveRiveCharacter extends StatefulWidget {
  const InteractiveRiveCharacter({super.key});

  @override
  State<InteractiveRiveCharacter> createState() =>
      _InteractiveRiveCharacterState();
}

class _InteractiveRiveCharacterState extends State<InteractiveRiveCharacter> {
  // State machine controller
  StateMachineController? _controller;
  
  // Input references
  SMITrigger? _jumpTrigger;
  SMIBool? _isWalking;
  SMINumber? _speed;

  void _onRiveInit(Artboard artboard) {
    final controller = StateMachineController.fromArtboard(
      artboard,
      'CharacterStateMachine', // Must match name in Rive file
    );
    
    if (controller != null) {
      artboard.addController(controller);
      _controller = controller;
      
      // Find inputs by name (must match Rive editor names)
      _jumpTrigger = controller.findInput<bool>('jump') as SMITrigger?;
      _isWalking = controller.findInput<bool>('isWalking') as SMIBool?;
      _speed = controller.findInput<double>('speed') as SMINumber?;
    }
  }

  void _jump() {
    _jumpTrigger?.fire(); // Triggers are "fired", not set
  }

  void _toggleWalk() {
    if (_isWalking != null) {
      _isWalking!.value = !_isWalking!.value;
    }
  }

  void _setSpeed(double value) {
    _speed?.value = value;
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // Rive animation
        SizedBox(
          width: 300,
          height: 300,
          child: RiveAnimation.asset(
            'assets/animations/character.riv',
            onInit: _onRiveInit,
            fit: BoxFit.contain,
          ),
        ),
        
        // Controls
        Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            ElevatedButton(
              onPressed: _jump,
              child: const Text('Jump'),
            ),
            const SizedBox(width: 16),
            ElevatedButton(
              onPressed: _toggleWalk,
              child: const Text('Toggle Walk'),
            ),
          ],
        ),
        
        // Speed slider
        Slider(
          value: _speed?.value ?? 0,
          min: 0,
          max: 100,
          onChanged: _setSpeed,
          label: 'Speed: ${_speed?.value.toInt() ?? 0}',
        ),
      ],
    );
  }
}

// Toggle button with Rive animation
class RiveToggleButton extends StatefulWidget {
  final bool initialValue;
  final ValueChanged<bool> onChanged;
  
  const RiveToggleButton({
    super.key,
    this.initialValue = false,
    required this.onChanged,
  });

  @override
  State<RiveToggleButton> createState() => _RiveToggleButtonState();
}

class _RiveToggleButtonState extends State<RiveToggleButton> {
  SMIBool? _isOn;
  late bool _value;

  @override
  void initState() {
    super.initState();
    _value = widget.initialValue;
  }

  void _onRiveInit(Artboard artboard) {
    final controller = StateMachineController.fromArtboard(
      artboard,
      'Toggle',
    );
    
    if (controller != null) {
      artboard.addController(controller);
      _isOn = controller.findInput<bool>('isOn') as SMIBool?;
      _isOn?.value = _value; // Set initial state
    }
  }

  void _toggle() {
    setState(() {
      _value = !_value;
      _isOn?.value = _value;
    });
    widget.onChanged(_value);
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: _toggle,
      child: SizedBox(
        width: 80,
        height: 40,
        child: RiveAnimation.asset(
          'assets/animations/toggle_switch.riv',
          onInit: _onRiveInit,
          fit: BoxFit.contain,
        ),
      ),
    );
  }
}
```
