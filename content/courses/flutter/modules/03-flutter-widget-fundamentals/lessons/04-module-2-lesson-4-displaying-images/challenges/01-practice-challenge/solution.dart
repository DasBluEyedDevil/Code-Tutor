// Solution: Image Gallery with Labels
// Shows network images with different sizes and shapes

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
              // Image 1: Large rectangular image
              _buildImageCard(
                imageUrl: 'https://picsum.photos/400/200',
                label: 'Landscape Photo',
                width: double.infinity,
                height: 200,
              ),
              const SizedBox(height: 16),
              
              // Row with two smaller images
              Row(
                children: [
                  // Image 2: Medium square image
                  Expanded(
                    child: _buildImageCard(
                      imageUrl: 'https://picsum.photos/200/200',
                      label: 'Square Photo',
                      height: 150,
                    ),
                  ),
                  const SizedBox(width: 16),
                  
                  // Image 3: Circular profile image
                  Column(
                    children: [
                      const CircleAvatar(
                        radius: 60,
                        backgroundImage: NetworkImage(
                          'https://picsum.photos/150/150',
                        ),
                      ),
                      const SizedBox(height: 8),
                      const Text(
                        'Profile Photo',
                        style: TextStyle(fontWeight: FontWeight.w500),
                      ),
                    ],
                  ),
                ],
              ),
              const SizedBox(height: 16),
              
              // Image 4: Small thumbnail
              _buildImageCard(
                imageUrl: 'https://picsum.photos/300/150',
                label: 'Thumbnail',
                width: 200,
                height: 100,
              ),
            ],
          ),
        ),
      ),
    );
  }

  Widget _buildImageCard({
    required String imageUrl,
    required String label,
    double? width,
    double? height,
  }) {
    return Column(
      children: [
        ClipRRect(
          borderRadius: BorderRadius.circular(8),
          child: Image.network(
            imageUrl,
            width: width,
            height: height,
            fit: BoxFit.cover,
            loadingBuilder: (context, child, loadingProgress) {
              if (loadingProgress == null) return child;
              return SizedBox(
                width: width,
                height: height,
                child: const Center(child: CircularProgressIndicator()),
              );
            },
            errorBuilder: (context, error, stackTrace) {
              return SizedBox(
                width: width,
                height: height,
                child: const Center(child: Icon(Icons.error)),
              );
            },
          ),
        ),
        const SizedBox(height: 8),
        Text(label, style: const TextStyle(fontWeight: FontWeight.w500)),
      ],
    );
  }
}

// Key concepts:
// - Image.network() loads images from URLs
// - CircleAvatar for circular profile images
// - ClipRRect for rounded corners
// - loadingBuilder shows progress
// - errorBuilder handles failed loads