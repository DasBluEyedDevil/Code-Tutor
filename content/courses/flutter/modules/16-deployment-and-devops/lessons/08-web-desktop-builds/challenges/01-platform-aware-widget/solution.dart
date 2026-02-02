import 'dart:io' show Platform;
import 'package:flutter/foundation.dart' show kIsWeb;
import 'package:flutter/material.dart';

class PlatformAwareCard extends StatelessWidget {
  final Widget child;
  
  const PlatformAwareCard({super.key, required this.child});
  
  String get _platformName {
    if (kIsWeb) {
      return 'Web';
    }
    if (Platform.isWindows || Platform.isMacOS || Platform.isLinux) {
      return 'Desktop';
    }
    return 'Mobile';
  }
  
  BorderRadius get _borderRadius {
    if (kIsWeb) {
      return BorderRadius.circular(16);
    }
    if (!kIsWeb && (Platform.isWindows || Platform.isMacOS || Platform.isLinux)) {
      return BorderRadius.circular(8);
    }
    return BorderRadius.circular(12);
  }
  
  double get _elevation {
    if (kIsWeb) return 2;
    if (!kIsWeb && (Platform.isWindows || Platform.isMacOS || Platform.isLinux)) {
      return 4;
    }
    return 1;
  }
  
  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: _elevation,
      shape: RoundedRectangleBorder(borderRadius: _borderRadius),
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'Platform: $_platformName',
              style: Theme.of(context).textTheme.labelSmall,
            ),
            const SizedBox(height: 8),
            child,
          ],
        ),
      ),
    );
  }
}