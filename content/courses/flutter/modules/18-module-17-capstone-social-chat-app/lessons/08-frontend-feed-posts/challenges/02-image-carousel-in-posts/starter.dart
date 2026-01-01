// lib/features/feed/presentation/widgets/post_image_carousel.dart
import 'package:flutter/material.dart';
import 'package:cached_network_image/cached_network_image.dart';

class PostImageCarousel extends StatefulWidget {
  final List<String> imageUrls;
  final double height;
  final Function(int)? onPageChanged;
  final Function(int)? onImageTap;

  const PostImageCarousel({
    super.key,
    required this.imageUrls,
    this.height = 300,
    this.onPageChanged,
    this.onImageTap,
  });

  @override
  State<PostImageCarousel> createState() => _PostImageCarouselState();
}

class _PostImageCarouselState extends State<PostImageCarousel> {
  late PageController _pageController;
  int _currentPage = 0;

  @override
  void initState() {
    super.initState();
    _pageController = PageController();
  }

  @override
  void dispose() {
    _pageController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Implement image carousel
    // 1. PageView.builder for images
    // 2. Dots indicator at bottom
    // 3. Image counter overlay
    // 4. Pinch-to-zoom support
    // 5. Preload adjacent images
    throw UnimplementedError();
  }
}

// lib/features/feed/presentation/widgets/dots_indicator.dart
class DotsIndicator extends StatelessWidget {
  final int count;
  final int currentIndex;
  final Color activeColor;
  final Color inactiveColor;

  const DotsIndicator({
    super.key,
    required this.count,
    required this.currentIndex,
    this.activeColor = Colors.white,
    this.inactiveColor = Colors.white54,
  });

  @override
  Widget build(BuildContext context) {
    // TODO: Implement dots indicator
    // 1. Row of dots
    // 2. Active dot is larger/highlighted
    // 3. Animate size changes
    // 4. Handle many dots (5+) with scrolling or condensing
    throw UnimplementedError();
  }
}