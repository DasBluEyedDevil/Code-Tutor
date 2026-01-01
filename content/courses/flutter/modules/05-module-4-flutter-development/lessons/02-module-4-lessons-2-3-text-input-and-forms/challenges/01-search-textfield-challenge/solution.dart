// Solution: Search TextField with Filtering
// Real-time search that filters a list of items

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
  final TextEditingController _searchController = TextEditingController();
  
  // Sample data
  final List<String> allItems = [
    'Apple', 'Banana', 'Cherry', 'Date', 'Elderberry',
    'Fig', 'Grape', 'Honeydew', 'Kiwi', 'Lemon',
    'Mango', 'Nectarine', 'Orange', 'Papaya', 'Quince',
  ];
  
  List<String> filteredItems = [];
  
  @override
  void initState() {
    super.initState();
    filteredItems = allItems; // Start with all items
    _searchController.addListener(_onSearchChanged);
  }
  
  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }
  
  void _onSearchChanged() {
    final query = _searchController.text.toLowerCase();
    setState(() {
      if (query.isEmpty) {
        filteredItems = allItems;
      } else {
        filteredItems = allItems
            .where((item) => item.toLowerCase().contains(query))
            .toList();
      }
    });
  }
  
  void _clearSearch() {
    _searchController.clear();
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        // Search TextField with icon
        Padding(
          padding: const EdgeInsets.all(16),
          child: TextField(
            controller: _searchController,
            decoration: InputDecoration(
              hintText: 'Search fruits...',
              prefixIcon: const Icon(Icons.search),
              suffixIcon: _searchController.text.isNotEmpty
                  ? IconButton(
                      icon: const Icon(Icons.clear),
                      onPressed: _clearSearch,
                    )
                  : null,
              border: OutlineInputBorder(
                borderRadius: BorderRadius.circular(12),
              ),
              filled: true,
              fillColor: Colors.grey.shade100,
            ),
          ),
        ),
        
        // Results count
        Padding(
          padding: const EdgeInsets.symmetric(horizontal: 16),
          child: Text(
            'Found ${filteredItems.length} items',
            style: TextStyle(color: Colors.grey.shade600),
          ),
        ),
        const SizedBox(height: 8),
        
        // Filtered list
        Expanded(
          child: filteredItems.isEmpty
              ? const Center(child: Text('No items found'))
              : ListView.builder(
                  itemCount: filteredItems.length,
                  itemBuilder: (context, index) {
                    return ListTile(
                      leading: const Icon(Icons.local_grocery_store),
                      title: Text(filteredItems[index]),
                    );
                  },
                ),
        ),
      ],
    );
  }
}

// Key concepts:
// - TextEditingController: Manages text input
// - addListener: Responds to text changes
// - where + contains: Filter list by search query
// - prefixIcon/suffixIcon: Icons inside TextField
// - dispose: Clean up controller