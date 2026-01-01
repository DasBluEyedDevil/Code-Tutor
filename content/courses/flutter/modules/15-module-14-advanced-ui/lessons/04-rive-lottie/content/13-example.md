---
type: "EXAMPLE"
title: "Real-World Use Cases"
---


Practical implementations combining both libraries:



```dart
import 'package:flutter/material.dart';
import 'package:lottie/lottie.dart';
import 'package:rive/rive.dart';

// Loading screen with Lottie
class LoadingScreen extends StatelessWidget {
  const LoadingScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // Lottie - perfect for loading animations
            Lottie.asset(
              'assets/animations/loading.json',
              width: 200,
              height: 200,
            ),
            const SizedBox(height: 24),
            const Text('Loading...'),
          ],
        ),
      ),
    );
  }
}

// Empty state with Lottie
class EmptyStateWidget extends StatelessWidget {
  final String message;
  final VoidCallback onAction;
  
  const EmptyStateWidget({
    super.key,
    required this.message,
    required this.onAction,
  });

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Column(
        mainAxisAlignment: MainAxisAlignment.center,
        children: [
          Lottie.asset(
            'assets/animations/empty_box.json',
            width: 200,
            height: 200,
            repeat: true,
          ),
          const SizedBox(height: 16),
          Text(
            message,
            style: Theme.of(context).textTheme.titleMedium,
          ),
          const SizedBox(height: 16),
          ElevatedButton(
            onPressed: onAction,
            child: const Text('Get Started'),
          ),
        ],
      ),
    );
  }
}

// Interactive rating with Rive
class RiveRating extends StatefulWidget {
  final int maxRating;
  final ValueChanged<int> onRatingChanged;
  
  const RiveRating({
    super.key,
    this.maxRating = 5,
    required this.onRatingChanged,
  });

  @override
  State<RiveRating> createState() => _RiveRatingState();
}

class _RiveRatingState extends State<RiveRating> {
  final List<SMIBool?> _starInputs = [];
  int _currentRating = 0;

  void _setRating(int rating) {
    setState(() {
      _currentRating = rating;
      for (int i = 0; i < _starInputs.length; i++) {
        _starInputs[i]?.value = i < rating;
      }
    });
    widget.onRatingChanged(rating);
  }

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisSize: MainAxisSize.min,
      children: List.generate(widget.maxRating, (index) {
        return GestureDetector(
          onTap: () => _setRating(index + 1),
          child: SizedBox(
            width: 40,
            height: 40,
            child: RiveAnimation.asset(
              'assets/animations/star.riv',
              onInit: (artboard) {
                final controller = StateMachineController.fromArtboard(
                  artboard,
                  'StarState',
                );
                if (controller != null) {
                  artboard.addController(controller);
                  final input = controller.findInput<bool>('isFilled') as SMIBool?;
                  if (_starInputs.length <= index) {
                    _starInputs.add(input);
                  }
                  input?.value = index < _currentRating;
                }
              },
            ),
          ),
        );
      }),
    );
  }
}

// Animated success checkmark (Lottie for one-shot animation)
class SuccessAnimation extends StatelessWidget {
  final VoidCallback onComplete;
  
  const SuccessAnimation({super.key, required this.onComplete});

  @override
  Widget build(BuildContext context) {
    return Lottie.asset(
      'assets/animations/success_check.json',
      width: 150,
      height: 150,
      repeat: false,
      onLoaded: (composition) {
        // Trigger callback after animation completes
        Future.delayed(composition.duration, onComplete);
      },
    );
  }
}
```
