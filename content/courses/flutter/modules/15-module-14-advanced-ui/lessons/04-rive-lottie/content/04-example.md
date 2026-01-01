---
type: "EXAMPLE"
title: "Controlling Lottie Playback"
---


Use AnimationController for precise control:



```dart
import 'package:flutter/material.dart';
import 'package:lottie/lottie.dart';

class ControlledLottie extends StatefulWidget {
  const ControlledLottie({super.key});

  @override
  State<ControlledLottie> createState() => _ControlledLottieState();
}

class _ControlledLottieState extends State<ControlledLottie>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;
  bool _isPlaying = false;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Lottie.asset(
          'assets/animations/heart.json',
          controller: _controller,
          width: 200,
          height: 200,
          onLoaded: (composition) {
            // Set duration from the animation file
            _controller.duration = composition.duration;
          },
        ),
        const SizedBox(height: 20),
        
        // Playback controls
        Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            IconButton(
              icon: Icon(_isPlaying ? Icons.pause : Icons.play_arrow),
              onPressed: () {
                setState(() {
                  if (_isPlaying) {
                    _controller.stop();
                  } else {
                    _controller.repeat();
                  }
                  _isPlaying = !_isPlaying;
                });
              },
            ),
            IconButton(
              icon: const Icon(Icons.replay),
              onPressed: () {
                _controller.reset();
                _controller.forward();
                setState(() => _isPlaying = true);
              },
            ),
          ],
        ),
        
        // Scrubbing slider
        Slider(
          value: _controller.value,
          onChanged: (value) {
            _controller.value = value;
          },
        ),
      ],
    );
  }
}

// Interactive Lottie button
class LottieButton extends StatefulWidget {
  final VoidCallback onTap;
  
  const LottieButton({super.key, required this.onTap});

  @override
  State<LottieButton> createState() => _LottieButtonState();
}

class _LottieButtonState extends State<LottieButton>
    with SingleTickerProviderStateMixin {
  late AnimationController _controller;

  @override
  void initState() {
    super.initState();
    _controller = AnimationController(vsync: this);
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        // Play animation on tap
        _controller.forward(from: 0);
        widget.onTap();
      },
      child: Lottie.asset(
        'assets/animations/like_button.json',
        controller: _controller,
        width: 60,
        height: 60,
        onLoaded: (composition) {
          _controller.duration = composition.duration;
        },
      ),
    );
  }
}
```
