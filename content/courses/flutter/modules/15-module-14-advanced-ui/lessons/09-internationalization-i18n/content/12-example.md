---
type: "EXAMPLE"
title: "RTL-Aware Widget Implementation"
---


Building widgets that work correctly in both LTR and RTL:



```dart
import 'package:flutter/material.dart';

/// Extension for easy direction checking
extension DirectionExtension on BuildContext {
  bool get isRtl => Directionality.of(this) == TextDirection.rtl;
  TextDirection get textDirection => Directionality.of(this);
}

/// A list tile that adapts to RTL layout
class DirectionalListTile extends StatelessWidget {
  final Widget? leading;
  final Widget? trailing;
  final Widget title;
  final Widget? subtitle;
  final VoidCallback? onTap;

  const DirectionalListTile({
    super.key,
    this.leading,
    this.trailing,
    required this.title,
    this.subtitle,
    this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Padding(
        // Use Directional padding - start/end instead of left/right
        padding: const EdgeInsetsDirectional.symmetric(
          horizontal: 16,
          vertical: 12,
        ),
        child: Row(
          children: [
            // Leading widget (appears at start - left in LTR, right in RTL)
            if (leading != null) ...[
              leading!,
              const SizedBox(width: 16),
            ],
            
            // Content in the middle
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  title,
                  if (subtitle != null) ...[
                    const SizedBox(height: 4),
                    subtitle!,
                  ],
                ],
              ),
            ),
            
            // Trailing widget (appears at end)
            if (trailing != null) ...[
              const SizedBox(width: 16),
              trailing!,
            ],
          ],
        ),
      ),
    );
  }
}

/// Navigation arrow that flips for RTL
class DirectionalArrow extends StatelessWidget {
  final bool forward;
  final double size;
  final Color? color;

  const DirectionalArrow({
    super.key,
    this.forward = true,
    this.size = 24,
    this.color,
  });

  @override
  Widget build(BuildContext context) {
    final isRtl = context.isRtl;
    
    // In RTL, forward arrow points left, back arrow points right
    IconData icon;
    if (forward) {
      icon = isRtl ? Icons.arrow_back : Icons.arrow_forward;
    } else {
      icon = isRtl ? Icons.arrow_forward : Icons.arrow_back;
    }
    
    return Icon(icon, size: size, color: color);
  }
}

/// Chevron that adapts to text direction
class DirectionalChevron extends StatelessWidget {
  final bool forward;
  final double size;
  final Color? color;

  const DirectionalChevron({
    super.key,
    this.forward = true,
    this.size = 24,
    this.color,
  });

  @override
  Widget build(BuildContext context) {
    final isRtl = context.isRtl;
    final shouldFlip = isRtl;
    
    return Transform.flip(
      flipX: shouldFlip,
      child: Icon(
        forward ? Icons.chevron_right : Icons.chevron_left,
        size: size,
        color: color,
      ),
    );
  }
}

/// Card with directional layout
class DirectionalCard extends StatelessWidget {
  final Widget image;
  final String title;
  final String description;
  final VoidCallback? onTap;

  const DirectionalCard({
    super.key,
    required this.image,
    required this.title,
    required this.description,
    this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      child: InkWell(
        onTap: onTap,
        child: Padding(
          padding: const EdgeInsetsDirectional.all(16),
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              // Image at start
              ClipRRect(
                borderRadius: BorderRadius.circular(8),
                child: SizedBox(
                  width: 80,
                  height: 80,
                  child: image,
                ),
              ),
              const SizedBox(width: 16),
              // Content
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(
                      title,
                      style: Theme.of(context).textTheme.titleMedium,
                    ),
                    const SizedBox(height: 4),
                    Text(
                      description,
                      style: Theme.of(context).textTheme.bodyMedium,
                      maxLines: 2,
                      overflow: TextOverflow.ellipsis,
                    ),
                  ],
                ),
              ),
              // Chevron at end
              const DirectionalChevron(),
            ],
          ),
        ),
      ),
    );
  }
}

/// Force a specific text direction for a subtree
class ForceDirection extends StatelessWidget {
  final TextDirection direction;
  final Widget child;

  const ForceDirection({
    super.key,
    required this.direction,
    required this.child,
  });

  @override
  Widget build(BuildContext context) {
    return Directionality(
      textDirection: direction,
      child: child,
    );
  }
}

/// Example: Settings page with RTL support
class LocalizedSettingsPage extends StatelessWidget {
  const LocalizedSettingsPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Settings'),
        // Back button automatically flips in RTL
      ),
      body: ListView(
        children: [
          DirectionalListTile(
            leading: const Icon(Icons.language),
            title: const Text('Language'),
            subtitle: Text(Localizations.localeOf(context).toString()),
            trailing: const DirectionalChevron(),
            onTap: () {},
          ),
          const Divider(),
          DirectionalListTile(
            leading: const Icon(Icons.notifications),
            title: const Text('Notifications'),
            trailing: Switch(
              value: true,
              onChanged: (_) {},
            ),
          ),
          const Divider(),
          DirectionalListTile(
            leading: const Icon(Icons.dark_mode),
            title: const Text('Dark Mode'),
            trailing: Switch(
              value: false,
              onChanged: (_) {},
            ),
          ),
          const Divider(),
          DirectionalListTile(
            leading: const Icon(Icons.info),
            title: const Text('About'),
            trailing: const DirectionalChevron(),
            onTap: () {},
          ),
        ],
      ),
    );
  }
}

/// Positioned widget that respects RTL
class DirectionalPositioned extends StatelessWidget {
  final double? start;
  final double? end;
  final double? top;
  final double? bottom;
  final Widget child;

  const DirectionalPositioned({
    super.key,
    this.start,
    this.end,
    this.top,
    this.bottom,
    required this.child,
  });

  @override
  Widget build(BuildContext context) {
    return PositionedDirectional(
      start: start,
      end: end,
      top: top,
      bottom: bottom,
      child: child,
    );
  }
}
```
