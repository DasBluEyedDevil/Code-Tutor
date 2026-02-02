---
type: "EXAMPLE"
title: "Screen Rendering Performance"
---


Monitor frame rendering to detect jank and frozen frames:



```dart
// lib/widgets/performance_aware_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter/scheduler.dart';
import 'package:firebase_performance/firebase_performance.dart';

/// Mixin for monitoring screen rendering performance
mixin ScreenPerformanceMonitor<T extends StatefulWidget> on State<T> {
  Trace? _screenTrace;
  int _frameCount = 0;
  int _slowFrameCount = 0;
  DateTime? _screenStartTime;
  
  String get screenName;
  
  @override
  void initState() {
    super.initState();
    _startScreenTrace();
    _startFrameMonitoring();
  }
  
  @override
  void dispose() {
    _stopScreenTrace();
    super.dispose();
  }
  
  void _startScreenTrace() async {
    _screenStartTime = DateTime.now();
    _screenTrace = FirebasePerformance.instance.newTrace('screen_$screenName');
    await _screenTrace?.start();
  }
  
  void _stopScreenTrace() async {
    if (_screenTrace != null && _screenStartTime != null) {
      final duration = DateTime.now().difference(_screenStartTime!);
      
      // Add metrics
      _screenTrace!.incrementMetric('frame_count', _frameCount);
      _screenTrace!.incrementMetric('slow_frames', _slowFrameCount);
      _screenTrace!.incrementMetric('duration_ms', duration.inMilliseconds);
      
      // Calculate slow frame percentage
      if (_frameCount > 0) {
        final slowFramePercent = (_slowFrameCount / _frameCount * 100).round();
        _screenTrace!.putAttribute(
          'slow_frame_percent',
          slowFramePercent.toString(),
        );
      }
      
      await _screenTrace!.stop();
    }
  }
  
  void _startFrameMonitoring() {
    SchedulerBinding.instance.addPostFrameCallback(_onFrame);
  }
  
  void _onFrame(Duration timestamp) {
    if (!mounted) return;
    
    _frameCount++;
    
    // Check if frame took too long (>16.67ms for 60fps)
    // Note: This is a simplified check
    SchedulerBinding.instance.addPostFrameCallback((newTimestamp) {
      if (!mounted) return;
      
      final frameDuration = newTimestamp - timestamp;
      if (frameDuration.inMilliseconds > 16) {
        _slowFrameCount++;
      }
      
      _onFrame(newTimestamp);
    });
  }
}

// Usage:
class ProductListScreen extends StatefulWidget {
  const ProductListScreen({super.key});
  
  @override
  State<ProductListScreen> createState() => _ProductListScreenState();
}

class _ProductListScreenState extends State<ProductListScreen>
    with ScreenPerformanceMonitor {
  
  @override
  String get screenName => 'product_list';
  
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Products')),
      body: ListView.builder(
        itemCount: 100,
        itemBuilder: (context, index) {
          return ListTile(
            title: Text('Product $index'),
            leading: const CircleAvatar(child: Icon(Icons.shopping_bag)),
          );
        },
      ),
    );
  }
}
```
