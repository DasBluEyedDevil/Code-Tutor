import 'package:flutter/material.dart';
import 'package:lottie/lottie.dart';

class LikeButton extends StatefulWidget {
  final bool initiallyLiked;
  final ValueChanged<bool> onLikedChanged;
  
  const LikeButton({
    super.key,
    this.initiallyLiked = false,
    required this.onLikedChanged,
  });

  @override
  State<LikeButton> createState() => _LikeButtonState();
}

class _LikeButtonState extends State<LikeButton>
    with SingleTickerProviderStateMixin {
  // TODO: Create AnimationController
  // TODO: Track liked state
  
  @override
  void initState() {
    super.initState();
    // TODO: Initialize controller
    // TODO: Set initial state based on initiallyLiked
  }

  @override
  void dispose() {
    // TODO: Dispose controller
    super.dispose();
  }

  void _toggleLike() {
    // TODO: Toggle state and play animation
    // - If not liked: play forward
    // - If liked: play reverse
    // - Call onLikedChanged callback
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Return GestureDetector with Lottie.asset
    // Use 'assets/animations/heart.json' as the path
    // Size: 60x60
    return Container();
  }
}