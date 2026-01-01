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
      'ToggleState',
    );
    
    if (controller != null) {
      artboard.addController(controller);
      _isOn = controller.findInput<bool>('isOn') as SMIBool?;
      _isOn?.value = _value;
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
          'assets/animations/toggle.riv',
          onInit: _onRiveInit,
          fit: BoxFit.contain,
        ),
      ),
    );
  }
}