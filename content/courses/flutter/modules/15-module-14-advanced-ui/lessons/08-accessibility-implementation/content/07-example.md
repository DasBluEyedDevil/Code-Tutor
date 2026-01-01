---
type: "EXAMPLE"
title: "Implementing Reduced Motion Support"
---


Building animations that respect user preferences:



```dart
import 'package:flutter/material.dart';

/// Extension to easily check motion preferences
extension MotionPreferenceExtension on BuildContext {
  bool get prefersReducedMotion => MediaQuery.disableAnimationsOf(this);
  
  Duration get animationDuration => prefersReducedMotion
      ? Duration.zero
      : const Duration(milliseconds: 300);
  
  Curve get animationCurve => prefersReducedMotion
      ? Curves.linear
      : Curves.easeInOut;
}

/// Animated container that respects reduced motion
class MotionAwareContainer extends StatelessWidget {
  final Widget child;
  final Duration duration;
  final Curve curve;
  final Decoration? decoration;
  final EdgeInsets? padding;
  final double? width;
  final double? height;

  const MotionAwareContainer({
    super.key,
    required this.child,
    this.duration = const Duration(milliseconds: 300),
    this.curve = Curves.easeInOut,
    this.decoration,
    this.padding,
    this.width,
    this.height,
  });

  @override
  Widget build(BuildContext context) {
    final prefersReduced = context.prefersReducedMotion;

    if (prefersReduced) {
      // No animation - instant changes
      return Container(
        width: width,
        height: height,
        padding: padding,
        decoration: decoration,
        child: child,
      );
    }

    return AnimatedContainer(
      duration: duration,
      curve: curve,
      width: width,
      height: height,
      padding: padding,
      decoration: decoration,
      child: child,
    );
  }
}

/// Page transition that adapts to motion preferences
class MotionAwarePageRoute<T> extends PageRouteBuilder<T> {
  final Widget page;
  final bool prefersReducedMotion;

  MotionAwarePageRoute({
    required this.page,
    required this.prefersReducedMotion,
  }) : super(
          pageBuilder: (context, animation, secondaryAnimation) => page,
          transitionDuration: prefersReducedMotion
              ? Duration.zero
              : const Duration(milliseconds: 300),
          reverseTransitionDuration: prefersReducedMotion
              ? Duration.zero
              : const Duration(milliseconds: 300),
          transitionsBuilder: (context, animation, secondaryAnimation, child) {
            if (prefersReducedMotion) {
              // Fade only for reduced motion
              return FadeTransition(
                opacity: animation,
                child: child,
              );
            }

            // Slide + fade for normal motion
            return SlideTransition(
              position: Tween<Offset>(
                begin: const Offset(1.0, 0.0),
                end: Offset.zero,
              ).animate(CurvedAnimation(
                parent: animation,
                curve: Curves.easeOutCubic,
              )),
              child: FadeTransition(
                opacity: animation,
                child: child,
              ),
            );
          },
        );
}

/// Helper to navigate with motion-aware transitions
void navigateWithMotion(BuildContext context, Widget page) {
  Navigator.of(context).push(
    MotionAwarePageRoute(
      page: page,
      prefersReducedMotion: context.prefersReducedMotion,
    ),
  );
}

/// Loading indicator that adapts to motion preferences
class MotionAwareLoadingIndicator extends StatelessWidget {
  final String? message;
  final double size;

  const MotionAwareLoadingIndicator({
    super.key,
    this.message,
    this.size = 48,
  });

  @override
  Widget build(BuildContext context) {
    final prefersReduced = context.prefersReducedMotion;

    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        if (prefersReduced)
          // Static indicator for reduced motion
          _StaticLoadingIndicator(size: size)
        else
          // Animated spinner for normal motion
          SizedBox(
            width: size,
            height: size,
            child: const CircularProgressIndicator(),
          ),
        if (message != null) ...[
          const SizedBox(height: 16),
          Text(message!),
        ],
      ],
    );
  }
}

class _StaticLoadingIndicator extends StatelessWidget {
  final double size;

  const _StaticLoadingIndicator({required this.size});

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: size,
      height: size,
      child: Stack(
        alignment: Alignment.center,
        children: [
          Container(
            width: size,
            height: size,
            decoration: BoxDecoration(
              shape: BoxShape.circle,
              border: Border.all(
                color: Theme.of(context).colorScheme.primary.withValues(alpha: 0.3),
                width: 4,
              ),
            ),
          ),
          Container(
            width: size * 0.3,
            height: size * 0.3,
            decoration: BoxDecoration(
              shape: BoxShape.circle,
              color: Theme.of(context).colorScheme.primary,
            ),
          ),
        ],
      ),
    );
  }
}

/// Card with expandable content that respects motion preferences
class MotionAwareExpandableCard extends StatefulWidget {
  final String title;
  final Widget content;

  const MotionAwareExpandableCard({
    super.key,
    required this.title,
    required this.content,
  });

  @override
  State<MotionAwareExpandableCard> createState() => _MotionAwareExpandableCardState();
}

class _MotionAwareExpandableCardState extends State<MotionAwareExpandableCard>
    with SingleTickerProviderStateMixin {
  bool _isExpanded = false;
  late AnimationController _controller;
  late Animation<double> _heightFactor;
  late Animation<double> _iconRotation;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(
      duration: const Duration(milliseconds: 300),
      vsync: this,
    );
    _heightFactor = _controller.drive(CurveTween(curve: Curves.easeInOut));
    _iconRotation = _controller.drive(
      Tween<double>(begin: 0.0, end: 0.5),
    );
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  void _toggleExpanded() {
    setState(() {
      _isExpanded = !_isExpanded;
      if (context.prefersReducedMotion) {
        // Instant change for reduced motion
        if (_isExpanded) {
          _controller.value = 1.0;
        } else {
          _controller.value = 0.0;
        }
      } else {
        // Animate for normal motion
        if (_isExpanded) {
          _controller.forward();
        } else {
          _controller.reverse();
        }
      }
    });
  }

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Column(
        children: [
          InkWell(
            onTap: _toggleExpanded,
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Row(
                children: [
                  Expanded(
                    child: Text(
                      widget.title,
                      style: Theme.of(context).textTheme.titleMedium,
                    ),
                  ),
                  RotationTransition(
                    turns: _iconRotation,
                    child: const Icon(Icons.keyboard_arrow_down),
                  ),
                ],
              ),
            ),
          ),
          ClipRect(
            child: AnimatedBuilder(
              animation: _controller,
              builder: (context, child) {
                return Align(
                  alignment: Alignment.topCenter,
                  heightFactor: _heightFactor.value,
                  child: child,
                );
              },
              child: Padding(
                padding: const EdgeInsets.fromLTRB(16, 0, 16, 16),
                child: widget.content,
              ),
            ),
          ),
        ],
      ),
    );
  }
}
```
