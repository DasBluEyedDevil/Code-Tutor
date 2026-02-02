---
type: "EXAMPLE"
title: "Detecting and Reporting Jank"
---


Create a more sophisticated jank detection system:



```dart
// lib/performance/jank_detector.dart
import 'dart:async';
import 'dart:ui';
import 'package:flutter/scheduler.dart';
import 'package:firebase_performance/firebase_performance.dart';

/// Detects frame rendering issues and reports to Firebase Performance
class JankDetector {
  static JankDetector? _instance;
  
  final FirebasePerformance _performance;
  final List<FrameTiming> _recentFrames = [];
  final int _maxFrames = 120; // Keep last 2 seconds at 60fps
  
  bool _isMonitoring = false;
  SchedulerBinding? _binding;
  
  JankDetector._internal()
      : _performance = FirebasePerformance.instance;
  
  factory JankDetector() {
    return _instance ??= JankDetector._internal();
  }
  
  /// Start monitoring frame timings
  void startMonitoring() {
    if (_isMonitoring) return;
    _isMonitoring = true;
    
    _binding = SchedulerBinding.instance;
    _binding?.addTimingsCallback(_handleFrameTimings);
  }
  
  /// Stop monitoring frame timings
  void stopMonitoring() {
    _isMonitoring = false;
    _binding?.removeTimingsCallback(_handleFrameTimings);
  }
  
  void _handleFrameTimings(List<FrameTiming> timings) {
    for (final timing in timings) {
      _recentFrames.add(timing);
      
      // Check for jank
      final totalDuration = timing.totalSpan.inMilliseconds;
      
      if (totalDuration > 700) {
        // Frozen frame (>700ms)
        _reportFrozenFrame(timing);
      } else if (totalDuration > 16) {
        // Slow frame (missed 60fps target)
        _reportSlowFrame(timing);
      }
    }
    
    // Keep only recent frames
    while (_recentFrames.length > _maxFrames) {
      _recentFrames.removeAt(0);
    }
  }
  
  void _reportSlowFrame(FrameTiming timing) {
    final trace = _performance.newTrace('slow_frame');
    
    trace.putAttribute('severity', 'slow');
    trace.incrementMetric(
      'build_duration_ms',
      timing.buildDuration.inMilliseconds,
    );
    trace.incrementMetric(
      'raster_duration_ms',
      timing.rasterDuration.inMilliseconds,
    );
    trace.incrementMetric(
      'total_duration_ms',
      timing.totalSpan.inMilliseconds,
    );
    
    trace.start();
    trace.stop();
  }
  
  void _reportFrozenFrame(FrameTiming timing) {
    final trace = _performance.newTrace('frozen_frame');
    
    trace.putAttribute('severity', 'frozen');
    trace.incrementMetric(
      'build_duration_ms',
      timing.buildDuration.inMilliseconds,
    );
    trace.incrementMetric(
      'raster_duration_ms',
      timing.rasterDuration.inMilliseconds,
    );
    trace.incrementMetric(
      'total_duration_ms',
      timing.totalSpan.inMilliseconds,
    );
    
    trace.start();
    trace.stop();
  }
  
  /// Get current frame statistics
  FrameStats getFrameStats() {
    if (_recentFrames.isEmpty) {
      return FrameStats.empty();
    }
    
    int slowFrames = 0;
    int frozenFrames = 0;
    int totalDuration = 0;
    
    for (final frame in _recentFrames) {
      final duration = frame.totalSpan.inMilliseconds;
      totalDuration += duration;
      
      if (duration > 700) {
        frozenFrames++;
      } else if (duration > 16) {
        slowFrames++;
      }
    }
    
    return FrameStats(
      totalFrames: _recentFrames.length,
      slowFrames: slowFrames,
      frozenFrames: frozenFrames,
      averageFrameTime: totalDuration / _recentFrames.length,
    );
  }
}

class FrameStats {
  final int totalFrames;
  final int slowFrames;
  final int frozenFrames;
  final double averageFrameTime;
  
  FrameStats({
    required this.totalFrames,
    required this.slowFrames,
    required this.frozenFrames,
    required this.averageFrameTime,
  });
  
  factory FrameStats.empty() {
    return FrameStats(
      totalFrames: 0,
      slowFrames: 0,
      frozenFrames: 0,
      averageFrameTime: 0,
    );
  }
  
  double get fps => averageFrameTime > 0 ? 1000 / averageFrameTime : 0;
  double get slowFramePercent => 
      totalFrames > 0 ? (slowFrames / totalFrames) * 100 : 0;
}

// Initialize in main.dart:
void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  await Firebase.initializeApp(
    options: DefaultFirebaseOptions.currentPlatform,
  );
  
  // Start jank detection
  JankDetector().startMonitoring();
  
  runApp(const MyApp());
}
```
