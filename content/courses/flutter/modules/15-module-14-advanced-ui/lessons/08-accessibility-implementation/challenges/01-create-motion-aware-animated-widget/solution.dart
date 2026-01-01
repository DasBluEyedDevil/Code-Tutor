import 'package:flutter/material.dart';
import 'package:flutter/semantics.dart';

class NotificationCard extends StatefulWidget {
  final String title;
  final String message;
  final VoidCallback onDismiss;
  final bool isVisible;

  const NotificationCard({
    super.key,
    required this.title,
    required this.message,
    required this.onDismiss,
    required this.isVisible,
  });

  @override
  State<NotificationCard> createState() => _NotificationCardState();
}

class _NotificationCardState extends State<NotificationCard>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  late Animation<Offset> _slideAnimation;
  late Animation<double> _fadeAnimation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(milliseconds: 300),
      vsync: this,
    );
    
    _slideAnimation = Tween<Offset>(
      begin: const Offset(1.0, 0.0),
      end: Offset.zero,
    ).animate(CurvedAnimation(
      parent: _controller,
      curve: Curves.easeOutCubic,
    ));
    
    _fadeAnimation = Tween<double>(
      begin: 0.0,
      end: 1.0,
    ).animate(CurvedAnimation(
      parent: _controller,
      curve: Curves.easeIn,
    ));

    if (widget.isVisible) {
      _controller.value = 1.0;
    }
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  void didUpdateWidget(NotificationCard oldWidget) {
    super.didUpdateWidget(oldWidget);
    
    if (widget.isVisible != oldWidget.isVisible) {
      final prefersReducedMotion = MediaQuery.disableAnimationsOf(context);
      
      if (prefersReducedMotion) {
        // Instant change for reduced motion
        _controller.value = widget.isVisible ? 1.0 : 0.0;
      } else {
        // Animate for normal motion
        if (widget.isVisible) {
          _controller.forward();
          // Announce to screen readers
          SemanticsService.announce(
            'Notification: ${widget.title}',
            TextDirection.ltr,
          );
        } else {
          _controller.reverse();
        }
      }
    }
  }

  @override
  Widget build(BuildContext context) {
    final prefersReducedMotion = MediaQuery.disableAnimationsOf(context);
    final theme = Theme.of(context);

    Widget card = Card(
      margin: const EdgeInsets.all(16),
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Row(
          children: [
            Icon(
              Icons.notifications,
              color: theme.colorScheme.primary,
            ),
            const SizedBox(width: 16),
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisSize: MainAxisSize.min,
                children: [
                  Text(
                    widget.title,
                    style: theme.textTheme.titleMedium,
                  ),
                  const SizedBox(height: 4),
                  Text(
                    widget.message,
                    style: theme.textTheme.bodyMedium,
                  ),
                ],
              ),
            ),
            Semantics(
              button: true,
              label: 'Dismiss notification',
              child: IconButton(
                icon: const Icon(Icons.close),
                onPressed: widget.onDismiss,
                tooltip: 'Dismiss',
              ),
            ),
          ],
        ),
      ),
    );

    if (prefersReducedMotion) {
      // Simple fade only for reduced motion
      return FadeTransition(
        opacity: _fadeAnimation,
        child: card,
      );
    }

    // Full slide + fade animation for normal motion
    return SlideTransition(
      position: _slideAnimation,
      child: FadeTransition(
        opacity: _fadeAnimation,
        child: card,
      ),
    );
  }
}