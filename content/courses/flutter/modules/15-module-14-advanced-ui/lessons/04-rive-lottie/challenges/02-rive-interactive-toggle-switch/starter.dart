import 'package:flutter/material.dart';
import 'package:rive/rive.dart';

class RiveToggle extends StatefulWidget {
  final bool initialValue;
  final ValueChanged<bool> onChanged;
  
  const RiveToggle({
    super.key,
    this.initialValue = false,
    required this.onChanged,
  });

  @override
  State<RiveToggle> createState() => _RiveToggleState();
}

class _RiveToggleState extends State<RiveToggle> {
  // TODO: Store SMIBool reference for 'isOn' input
  // TODO: Track current value

  void _onRiveInit(Artboard artboard) {
    // TODO: Get StateMachineController from artboard
    // State machine name: 'ToggleState'
    // TODO: Find 'isOn' input and store reference
    // TODO: Set initial value
  }

  void _toggle() {
    // TODO: Toggle value
    // TODO: Update Rive input
    // TODO: Call onChanged callback
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Return GestureDetector with RiveAnimation.asset
    // Asset path: 'assets/animations/toggle.riv'
    // Size: 80x40
    return Container();
  }
}