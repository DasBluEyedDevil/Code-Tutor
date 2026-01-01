---
type: "EXAMPLE"
title: "Complete Animation Lifecycle Example"
---


A notification banner that slides in, pauses, then slides out:



```dart
class NotificationBanner extends StatefulWidget {
  final String message;
  final VoidCallback onDismissed;
  
  const NotificationBanner({
    super.key,
    required this.message,
    required this.onDismissed,
  });

  @override
  State<NotificationBanner> createState() => _NotificationBannerState();
}

class _NotificationBannerState extends State<NotificationBanner>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _slideAnimation;
  late Animation<double> _fadeAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      vsync: this,
      duration: const Duration(milliseconds: 400),
    );

    _slideAnimation = Tween<Offset>(
      begin: const Offset(0, -1),
      end: Offset.zero,
    ).animate(CurvedAnimation(
      parent: _controller,
      curve: Curves.easeOut,
    ));

    _fadeAnimation = Tween<double>(
      begin: 0.0,
      end: 1.0,
    ).animate(CurvedAnimation(
      parent: _controller,
      curve: Curves.easeIn,
    ));

    // Listen for animation completion
    _controller.addStatusListener(_onStatusChange);

    // Start entrance animation
    _controller.forward();
  }

  void _onStatusChange(AnimationStatus status) {
    if (status == AnimationStatus.completed) {
      // Pause, then start exit animation
      Future.delayed(const Duration(seconds: 3), () {
        if (mounted) {
          _controller.reverse();
        }
      });
    } else if (status == AnimationStatus.dismissed) {
      // Notify parent when fully dismissed
      widget.onDismissed();
    }
  }

  @override
  void dispose() {
    _controller.removeStatusListener(_onStatusChange);
    _controller.dispose();
    super.dispose();
  }

  void _dismiss() {
    _controller.reverse();
  }

  @override
  Widget build(BuildContext context) {
    return SlideTransition(
      position: _slideAnimation,
      child: FadeTransition(
        opacity: _fadeAnimation,
        child: Material(
          elevation: 4,
          child: Container(
            width: double.infinity,
            padding: const EdgeInsets.all(16),
            color: Colors.blue,
            child: SafeArea(
              bottom: false,
              child: Row(
                children: [
                  const Icon(Icons.info, color: Colors.white),
                  const SizedBox(width: 12),
                  Expanded(
                    child: Text(
                      widget.message,
                      style: const TextStyle(color: Colors.white),
                    ),
                  ),
                  IconButton(
                    icon: const Icon(Icons.close, color: Colors.white),
                    onPressed: _dismiss,
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
```
