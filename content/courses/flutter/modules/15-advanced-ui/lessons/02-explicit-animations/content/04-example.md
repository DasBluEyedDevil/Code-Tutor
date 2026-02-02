---
type: "EXAMPLE"
title: "Forward and Reverse Control"
---


A heart that grows when tapped and shrinks when tapped again:



```dart
class PulsatingHeart extends StatefulWidget {
  const PulsatingHeart({super.key});

  @override
  State<PulsatingHeart> createState() => _PulsatingHeartState();
}

class _PulsatingHeartState extends State<PulsatingHeart>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  bool _isExpanded = false;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 300),
    );
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  void _toggleAnimation() {
    if (_isExpanded) {
      _controller.reverse();
    } else {
      _controller.forward();
    }
    _isExpanded = !_isExpanded;
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: _toggleAnimation,
      child: AnimatedBuilder(
        animation: _controller,
        builder: (context, child) {
          // Scale from 1.0 to 1.5
          final scale = 1.0 + (_controller.value * 0.5);
          return Transform.scale(
            scale: scale,
            child: child,
          );
        },
        child: const Icon(
          Icons.favorite,
          size: 64,
          color: Colors.red,
        ),
      ),
    );
  }
}
```
