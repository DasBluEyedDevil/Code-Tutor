// Solution: Dynamic Routes with onGenerateRoute
// Handles /post/:id and /category/:slug patterns

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
      onGenerateRoute: (settings) {
        final uri = Uri.parse(settings.name ?? '/');
        final pathSegments = uri.pathSegments;

        // Home route
        if (pathSegments.isEmpty) {
          return MaterialPageRoute(builder: (_) => const HomeScreen());
        }

        // /post/:id route
        if (pathSegments.length == 2 && pathSegments[0] == 'post') {
          final postId = pathSegments[1];
          return MaterialPageRoute(
            builder: (_) => PostScreen(postId: postId),
            settings: settings,
          );
        }

        // /category/:slug route
        if (pathSegments.length == 2 && pathSegments[0] == 'category') {
          final slug = pathSegments[1];
          return MaterialPageRoute(
            builder: (_) => CategoryScreen(slug: slug),
            settings: settings,
          );
        }

        // 404 - Not Found
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
              onPressed: () => Navigator.pushNamed(context, '/post/456'),
              child: const Text('View Post 456'),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => Navigator.pushNamed(context, '/category/flutter'),
              child: const Text('Flutter Category'),
            ),
            const SizedBox(height: 16),
            ElevatedButton(
              onPressed: () => Navigator.pushNamed(context, '/category/dart'),
              child: const Text('Dart Category'),
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
      appBar: AppBar(title: Text('Post $postId')),
      body: Center(
        child: Text('Viewing post with ID: $postId', style: const TextStyle(fontSize: 24)),
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
      appBar: AppBar(title: Text('Category: $slug')),
      body: Center(
        child: Text('Viewing category: $slug', style: const TextStyle(fontSize: 24)),
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

// Key concepts:
// - onGenerateRoute: Handles dynamic route matching
// - Uri.parse: Parses route path into segments
// - pathSegments: Array of path parts
// - Pattern matching: Check path structure
// - Fallback: 404 screen for unknown routes