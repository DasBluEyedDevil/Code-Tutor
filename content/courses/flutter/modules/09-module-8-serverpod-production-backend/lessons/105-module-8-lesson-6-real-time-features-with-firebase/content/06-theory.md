---
type: "THEORY"
title: "Part 2: Online/Offline Presence"
---


Track when users are online or offline!

### Presence Service


### Online Indicator Widget




```dart
// lib/widgets/online_indicator.dart
import 'package:flutter/material.dart';
import '../services/presence_service.dart';

class OnlineIndicator extends StatelessWidget {
  final String userId;
  final double size;

  const OnlineIndicator({
    super.key,
    required this.userId,
    this.size = 12,
  });

  @override
  Widget build(BuildContext context) {
    final presenceService = PresenceService();

    return StreamBuilder<bool>(
      stream: presenceService.getUserOnlineStatus(userId),
      builder: (context, snapshot) {
        final isOnline = snapshot.data ?? false;

        return Container(
          width: size,
          height: size,
          decoration: BoxDecoration(
            color: isOnline ? Colors.green : Colors.grey,
            shape: BoxShape.circle,
            border: Border.all(color: Colors.white, width: 2),
          ),
        );
      },
    );
  }
}
```
