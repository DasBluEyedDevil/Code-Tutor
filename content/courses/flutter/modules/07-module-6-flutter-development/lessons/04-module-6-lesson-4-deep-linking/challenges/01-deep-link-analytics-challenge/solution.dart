// Solution: Deep Link Analytics Tracking
// Tracks and displays most popular deep links

import 'package:flutter/material.dart';

void main() {
  runApp(const AnalyticsApp());
}

// Simple analytics service
class DeepLinkAnalytics {
  static final DeepLinkAnalytics _instance = DeepLinkAnalytics._internal();
  factory DeepLinkAnalytics() => _instance;
  DeepLinkAnalytics._internal();

  final Map<String, int> _linkCounts = {};

  void trackLink(String link) {
    _linkCounts[link] = (_linkCounts[link] ?? 0) + 1;
    print('Tracked: $link (count: ${_linkCounts[link]})');
  }

  Map<String, int> get stats => Map.unmodifiable(_linkCounts);

  List<MapEntry<String, int>> get topLinks {
    final sorted = _linkCounts.entries.toList()
      ..sort((a, b) => b.value.compareTo(a.value));
    return sorted.take(10).toList();
  }
}

class AnalyticsApp extends StatelessWidget {
  const AnalyticsApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      initialRoute: '/',
      onGenerateRoute: (settings) {
        final path = settings.name ?? '/';
        
        // Track every deep link navigation
        DeepLinkAnalytics().trackLink(path);
        
        // Route handling
        if (path == '/') {
          return MaterialPageRoute(builder: (_) => const HomeScreen());
        }
        if (path == '/stats') {
          return MaterialPageRoute(builder: (_) => const StatsScreen());
        }
        if (path.startsWith('/product/')) {
          final id = path.split('/').last;
          return MaterialPageRoute(builder: (_) => ProductScreen(id: id));
        }
        if (path.startsWith('/category/')) {
          final cat = path.split('/').last;
          return MaterialPageRoute(builder: (_) => CategoryScreen(category: cat));
        }
        return MaterialPageRoute(builder: (_) => const NotFoundScreen());
      },
    );
  }
}

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Deep Link Analytics'),
        actions: [
          IconButton(
            icon: const Icon(Icons.analytics),
            onPressed: () => Navigator.pushNamed(context, '/stats'),
          ),
        ],
      ),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: [
          const Text('Tap links to track them:', style: TextStyle(fontSize: 18)),
          const SizedBox(height: 16),
          _buildLinkButton(context, '/product/1', 'Product 1'),
          _buildLinkButton(context, '/product/2', 'Product 2'),
          _buildLinkButton(context, '/product/3', 'Product 3'),
          _buildLinkButton(context, '/category/electronics', 'Electronics'),
          _buildLinkButton(context, '/category/clothing', 'Clothing'),
        ],
      ),
    );
  }

  Widget _buildLinkButton(BuildContext context, String path, String label) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 8),
      child: ElevatedButton(
        onPressed: () => Navigator.pushNamed(context, path),
        child: Text(label),
      ),
    );
  }
}

class StatsScreen extends StatelessWidget {
  const StatsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final topLinks = DeepLinkAnalytics().topLinks;

    return Scaffold(
      appBar: AppBar(title: const Text('Link Analytics')),
      body: topLinks.isEmpty
          ? const Center(child: Text('No links tracked yet'))
          : ListView.builder(
              itemCount: topLinks.length,
              itemBuilder: (_, index) {
                final entry = topLinks[index];
                return ListTile(
                  leading: CircleAvatar(child: Text('${index + 1}')),
                  title: Text(entry.key),
                  trailing: Chip(label: Text('${entry.value} visits')),
                );
              },
            ),
    );
  }
}

class ProductScreen extends StatelessWidget {
  final String id;
  const ProductScreen({super.key, required this.id});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Product $id')),
      body: Center(child: Text('Product ID: $id')),
    );
  }
}

class CategoryScreen extends StatelessWidget {
  final String category;
  const CategoryScreen({super.key, required this.category});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Category: $category')),
      body: Center(child: Text('Category: $category')),
    );
  }
}

class NotFoundScreen extends StatelessWidget {
  const NotFoundScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('404')),
      body: const Center(child: Text('Not Found')),
    );
  }
}

// Key concepts:
// - Singleton pattern for analytics service
// - Track in onGenerateRoute (catches all navigation)
// - Map for counting link visits
// - Sort by count for top links
// - In production: use Firebase Analytics or similar