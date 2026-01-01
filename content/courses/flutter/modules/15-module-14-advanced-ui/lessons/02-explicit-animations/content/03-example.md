---
type: "EXAMPLE"
title: "Basic AnimationController Setup"
---


A spinning loader that continuously rotates:



```dart
class SpinningLoader extends StatefulWidget {
  const SpinningLoader({super.key});

  @override
  State<SpinningLoader> createState() => _SpinningLoaderState();
}

class _SpinningLoaderState extends State<SpinningLoader>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this, // Required for performance
      duration: const Duration(seconds: 2),
    );
    
    // Start looping animation
    _controller.repeat();
  }

  @override
  void dispose() {
    _controller.dispose(); // Always dispose!
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return AnimatedBuilder(
      animation: _controller,
      builder: (context, child) {
        return Transform.rotate(
          angle: _controller.value * 2 * 3.14159, // Full rotation
          child: child,
        );
      },
      child: const Icon(
        Icons.refresh,
        size: 48,
        color: Colors.blue,
      ),
    );
  }
}
```
