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
  late AnimationController _controller;
  late bool _isLiked;
  
  @override
  void initState() {
    super.initState();
    _isLiked = widget.initiallyLiked;
    _controller = AnimationController(vsync: this);
    
    // Set initial state without animation
    if (_isLiked) {
      _controller.value = 1.0;
    }
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }

  void _toggleLike() {
    setState(() {
      _isLiked = !_isLiked;
      
      if (_isLiked) {
        _controller.forward();
      } else {
        _controller.reverse();
      }
    });
    
    widget.onLikedChanged(_isLiked);
  }

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: _toggleLike,
      child: Lottie.asset(
        'assets/animations/heart.json',
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