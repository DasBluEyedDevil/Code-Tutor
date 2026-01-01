// Image Gallery Challenge
// Create an app with multiple network images

import 'package:flutter/material.dart';

void main() {
  runApp(const ImageGalleryApp());
}

class ImageGalleryApp extends StatelessWidget {
  const ImageGalleryApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Image Gallery')),
        body: SingleChildScrollView(
          padding: const EdgeInsets.all(16),
          child: Column(
            children: [
              // TODO: Add Image.network for a large landscape image
              // Use: https://picsum.photos/400/200
              
              const SizedBox(height: 16),
              
              // TODO: Add a Row with two images side by side
              // One square image, one CircleAvatar
              
              const SizedBox(height: 16),
              
              // TODO: Add text labels under each image
            ],
          ),
        ),
      ),
    );
  }
}