---
type: "EXAMPLE"
title: "Hover and Pointer Interactions"
---


React to hover and pointer events with Rive:



```dart
import 'package:flutter/material.dart';
import 'package:rive/rive.dart';

class HoverableRiveButton extends StatefulWidget {
  final VoidCallback onPressed;
  
  const HoverableRiveButton({super.key, required this.onPressed});

  @override
  State<HoverableRiveButton> createState() => _HoverableRiveButtonState();
}

class _HoverableRiveButtonState extends State<HoverableRiveButton> {
  SMIBool? _isHovered;
  SMITrigger? _pressed;

  void _onRiveInit(Artboard artboard) {
    final controller = StateMachineController.fromArtboard(
      artboard,
      'ButtonState',
    );
    
    if (controller != null) {
      artboard.addController(controller);
      _isHovered = controller.findInput<bool>('hover') as SMIBool?;
      _pressed = controller.findInput<bool>('press') as SMITrigger?;
    }
  }

  @override
  Widget build(BuildContext context) {
    return MouseRegion(
      onEnter: (_) => _isHovered?.value = true,
      onExit: (_) => _isHovered?.value = false,
      child: GestureDetector(
        onTapDown: (_) => _pressed?.fire(),
        onTapUp: (_) => widget.onPressed(),
        child: SizedBox(
          width: 200,
          height: 60,
          child: RiveAnimation.asset(
            'assets/animations/button.riv',
            onInit: _onRiveInit,
            fit: BoxFit.contain,
          ),
        ),
      ),
    );
  }
}

// Progress indicator driven by Rive
class RiveProgressIndicator extends StatefulWidget {
  final double progress; // 0.0 to 1.0
  
  const RiveProgressIndicator({super.key, required this.progress});

  @override
  State<RiveProgressIndicator> createState() => _RiveProgressIndicatorState();
}

class _RiveProgressIndicatorState extends State<RiveProgressIndicator> {
  SMINumber? _progress;

  void _onRiveInit(Artboard artboard) {
    final controller = StateMachineController.fromArtboard(
      artboard,
      'ProgressMachine',
    );
    
    if (controller != null) {
      artboard.addController(controller);
      _progress = controller.findInput<double>('progress') as SMINumber?;
      _progress?.value = widget.progress * 100; // Assuming 0-100 range
    }
  }

  @override
  void didUpdateWidget(RiveProgressIndicator oldWidget) {
    super.didUpdateWidget(oldWidget);
    if (oldWidget.progress != widget.progress) {
      _progress?.value = widget.progress * 100;
    }
  }

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 100,
      height: 100,
      child: RiveAnimation.asset(
        'assets/animations/progress.riv',
        onInit: _onRiveInit,
        fit: BoxFit.contain,
      ),
    );
  }
}
```
