// Dynamic Routes Challenge
// Handle /post/:id and /category/:slug with onGenerateRoute

import 'package:flutter/material.dart';

void main() {
  runApp(const DynamicRoutesApp());
}

class DynamicRoutesApp extends StatelessWidget {
  const DynamicRoutesApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      initialRoute: '/',
      // TODO 1: Implement onGenerateRoute
      onGenerateRoute: (settings) {
        final uri = Uri.parse(settings.name ?? '/');
        final pathSegments = uri.pathSegments;

        // Home route
        if (pathSegments.isEmpty) {
          return MaterialPageRoute(builder: (_) => const HomeScreen());
        }

        // TODO 2: Handle /post/:id pattern
        // Check if pathSegments.length == 2 && pathSegments[0] == 'post'
        // Extract postId = pathSegments[1]
        // Return MaterialPageRoute with PostScreen(postId: postId)

        // TODO 3: Handle /category/:slug pattern
        // Similar to above but for CategoryScreen

        // TODO 4: Return NotFoundScreen for unknown routes
        return MaterialPageRoute(
          builder: (_) => const NotFoundScreen(),
        );
      },
    );
  }
}

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Home')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            ElevatedButton(
              onPressed: () => Navigator.pushNamed(context, '/post/123'),
              child: const Text('View Post 123'),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => Navigator.pushNamed(context, '/category/flutter'),
              child: const Text('Flutter Category'),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => Navigator.pushNamed(context, '/unknown'),
              child: const Text('Unknown Route (404)'),
            ),
          ],
        ),
      ),
    );
  }
}

class PostScreen extends StatelessWidget {
  final String postId;
  const PostScreen({super.key, required this.postId});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Post \$postId')),
      body: Center(
        child: Text('Viewing post with ID: \$postId', style: const TextStyle(fontSize: 24)),
      ),
    );
  }
}

class CategoryScreen extends StatelessWidget {
  final String slug;
  const CategoryScreen({super.key, required this.slug});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Category: \$slug')),
      body: Center(
        child: Text('Viewing category: \$slug', style: const TextStyle(fontSize: 24)),
      ),
    );
  }
}

class NotFoundScreen extends StatelessWidget {
  const NotFoundScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('404')),
      body: const Center(
        child: Text('Page Not Found', style: TextStyle(fontSize: 24)),
      ),
    );
  }
}