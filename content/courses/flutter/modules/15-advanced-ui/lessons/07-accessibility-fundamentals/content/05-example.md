---
type: "EXAMPLE"
title: "Screen Reader Testing and Live Announcements"
---


Test and enhance screen reader support:



```dart
import 'package:flutter/material.dart';
import 'package:flutter/semantics.dart';

// Live announcements for dynamic content changes
class LiveAnnouncementExample extends StatefulWidget {
  const LiveAnnouncementExample({super.key});

  @override
  State<LiveAnnouncementExample> createState() => _LiveAnnouncementExampleState();
}

class _LiveAnnouncementExampleState extends State<LiveAnnouncementExample> {
  int _itemCount = 0;
  bool _isLoading = false;

  void _addItem() {
    setState(() => _itemCount++);
    
    // Announce the change to screen readers
    SemanticsService.announce(
      'Item added. Total items: $_itemCount',
      TextDirection.ltr,
    );
  }

  void _removeItem() {
    if (_itemCount > 0) {
      setState(() => _itemCount--);
      
      SemanticsService.announce(
        'Item removed. Total items: $_itemCount',
        TextDirection.ltr,
      );
    }
  }

  Future<void> _loadData() async {
    setState(() => _isLoading = true);
    
    // Announce loading state
    SemanticsService.announce(
      'Loading data, please wait',
      TextDirection.ltr,
    );
    
    await Future.delayed(const Duration(seconds: 2));
    
    setState(() => _isLoading = false);
    
    // Announce completion
    SemanticsService.announce(
      'Data loaded successfully',
      TextDirection.ltr,
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Live Announcements')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            // Semantics for the counter display
            Semantics(
              liveRegion: true, // Automatically announces changes
              child: Text(
                'Items: $_itemCount',
                style: const TextStyle(fontSize: 48),
              ),
            ),
            const SizedBox(height: 32),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: [
                ElevatedButton(
                  onPressed: _removeItem,
                  child: const Text('Remove'),
                ),
                const SizedBox(width: 16),
                ElevatedButton(
                  onPressed: _addItem,
                  child: const Text('Add'),
                ),
              ],
            ),
            const SizedBox(height: 32),
            ElevatedButton(
              onPressed: _isLoading ? null : _loadData,
              child: _isLoading
                  ? const SizedBox(
                      width: 20,
                      height: 20,
                      child: CircularProgressIndicator(strokeWidth: 2),
                    )
                  : const Text('Load Data'),
            ),
          ],
        ),
      ),
    );
  }
}

// Accessibility testing helper widget
class AccessibilityDebugger extends StatelessWidget {
  final Widget child;
  final bool showSemanticsBounds;

  const AccessibilityDebugger({
    super.key,
    required this.child,
    this.showSemanticsBounds = false,
  });

  @override
  Widget build(BuildContext context) {
    // Enable semantics debugger in debug mode
    // This shows the accessibility tree visually
    if (showSemanticsBounds) {
      return SemanticsDebugger(child: child);
    }
    return child;
  }
}

// Usage in main.dart for testing:
// void main() {
//   runApp(const AccessibilityDebugger(
//     showSemanticsBounds: true, // Set to true to debug
//     child: MyApp(),
//   ));
// }

// Custom semantics for complex widgets
class AccessibleRatingWidget extends StatelessWidget {
  final double rating;
  final int maxRating;
  final ValueChanged<double>? onChanged;

  const AccessibleRatingWidget({
    super.key,
    required this.rating,
    this.maxRating = 5,
    this.onChanged,
  });

  @override
  Widget build(BuildContext context) {
    return Semantics(
      label: 'Rating',
      value: '${rating.round()} out of $maxRating stars',
      hint: onChanged != null ? 'Swipe up or down to adjust' : null,
      slider: onChanged != null,
      onIncrease: onChanged != null && rating < maxRating
          ? () => onChanged!(rating + 1)
          : null,
      onDecrease: onChanged != null && rating > 0
          ? () => onChanged!(rating - 1)
          : null,
      child: Row(
        mainAxisSize: MainAxisSize.min,
        children: List.generate(maxRating, (index) {
          return GestureDetector(
            onTap: onChanged != null
                ? () => onChanged!((index + 1).toDouble())
                : null,
            child: Icon(
              index < rating ? Icons.star : Icons.star_border,
              color: Colors.amber,
              size: 32,
            ),
          );
        }),
      ),
    );
  }
}
```
