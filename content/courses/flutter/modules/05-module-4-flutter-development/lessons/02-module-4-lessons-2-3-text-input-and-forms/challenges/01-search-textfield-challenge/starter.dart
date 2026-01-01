// Search TextField Challenge
// Create a TextField that filters a list in real-time

import 'package:flutter/material.dart';

void main() {
  runApp(const SearchApp());
}

class SearchApp extends StatelessWidget {
  const SearchApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Search Demo')),
        body: const SearchScreen(),
      ),
    );
  }
}

class SearchScreen extends StatefulWidget {
  const SearchScreen({super.key});

  @override
  State<SearchScreen> createState() => _SearchScreenState();
}

class _SearchScreenState extends State<SearchScreen> {
  // TODO: Create TextEditingController
  final TextEditingController _searchController = TextEditingController();
  
  final List<String> allItems = [
    'Apple', 'Banana', 'Cherry', 'Date', 'Elderberry',
    'Fig', 'Grape', 'Honeydew', 'Kiwi', 'Lemon',
  ];
  
  List<String> filteredItems = [];
  
  @override
  void initState() {
    super.initState();
    filteredItems = allItems;
    // TODO: Add listener to controller
  }
  
  @override
  void dispose() {
    _searchController.dispose(); // Don't forget to dispose!
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // TODO: Add TextField with prefixIcon: Icon(Icons.search)
        Padding(
          padding: const EdgeInsets.all(16),
          child: TextField(
            controller: _searchController,
            decoration: const InputDecoration(
              hintText: 'Search...',
              prefixIcon: Icon(Icons.search),
            ),
          ),
        ),
        
        // TODO: Display filtered list with ListView.builder
        Expanded(
          child: ListView.builder(
            itemCount: filteredItems.length,
            itemBuilder: (context, index) {
              return ListTile(title: Text(filteredItems[index]));
            },
          ),
        ),
      ],
    );
  }
}