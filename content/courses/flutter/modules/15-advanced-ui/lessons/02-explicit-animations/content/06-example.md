---
type: "EXAMPLE"
title: "Tweens with Curves Example"
---


A button that changes color and size with a bouncy effect:



```dart
class BouncyColorButton extends StatefulWidget {
  const BouncyColorButton({super.key});

  @override
  State<BouncyColorButton> createState() => _BouncyColorButtonState();
}

class _BouncyColorButtonState extends State<BouncyColorButton>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<double> _sizeAnimation;
  late Animation<Color?> _colorAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 600),
    );

    // Size animation with bounce curve
    _sizeAnimation = Tween<double>(
      begin: 100.0,
      end: 150.0,
    ).animate(CurvedAnimation(
      parent: _controller,
      curve: Curves.elasticOut,
    ));

    // Color animation with ease curve
    _colorAnimation = ColorTween(
      begin: Colors.blue,
      end: Colors.purple,
    ).animate(CurvedAnimation(
      parent: _controller,
      curve: Curves.easeInOut,
    ));
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  void _onTapDown(TapDownDetails details) {
    _controller.forward();
  }

  void _onTapUp(TapUpDetails details) {
    _controller.reverse();
  }

  void _onTapCancel() {
    _controller.reverse();
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTapDown: _onTapDown,
      onTapUp: _onTapUp,
      onTapCancel: _onTapCancel,
      child: AnimatedBuilder(
        animation: _controller,
        builder: (context, child) {
          return Container(
            width: _sizeAnimation.value,
            height: _sizeAnimation.value,
            decoration: BoxDecoration(
              color: _colorAnimation.value,
              borderRadius: BorderRadius.circular(16),
              boxShadow: [
                BoxShadow(
                  color: (_colorAnimation.value ?? Colors.blue).withOpacity(0.4),
                  blurRadius: 8 + (_controller.value * 12),
                  offset: Offset(0, 4 + (_controller.value * 4)),
                ),
              ],
            ),
            child: const Center(
              child: Icon(
                Icons.touch_app,
                color: Colors.white,
                size: 32,
              ),
            ),
          );
        },
      ),
    );
  }
}
```
