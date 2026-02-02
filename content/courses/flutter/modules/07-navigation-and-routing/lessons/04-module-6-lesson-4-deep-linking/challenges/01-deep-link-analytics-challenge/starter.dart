// Deep Link Analytics Challenge
// Track navigation patterns in your app

import 'package:flutter/material.dart';

void main() {
  runApp(const AnalyticsApp());
}

// TODO 1: Create a singleton analytics service
class DeepLinkAnalytics {
  // Singleton pattern
  static final DeepLinkAnalytics _instance = DeepLinkAnalytics._internal();
  factory DeepLinkAnalytics() => _instance;
  DeepLinkAnalytics._internal();

  final Map<String, int> _linkCounts = {};

  void trackLink(String link) {
    // TODO: Increment count for this link
    _linkCounts[link] = (_linkCounts[link] ?? 0) + 1;
    print('Tracked: \$link (count: ${_linkCounts[link]})');
  }

  Map<String, int> get stats => Map.unmodifiable(_linkCounts);

  List<MapEntry<String, int>> get topLinks {
    // TODO 2: Sort by count descending and take top 10
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
        
        // TODO 3: Track every navigation
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
          ElevatedButton(
            onPressed: () => Navigator.pushNamed(context, '/product/1'),
            child: const Text('Product 1'),
          ),
          const SizedBox(height: 8),
          ElevatedButton(
            onPressed: () => Navigator.pushNamed(context, '/product/2'),
            child: const Text('Product 2'),
          ),
        ],
      ),
    );
  }
}

// TODO 4: Build the StatsScreen to display topLinks
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
      appBar: AppBar(title: Text('Product \$id')),
      body: Center(child: Text('Product ID: \$id')),
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