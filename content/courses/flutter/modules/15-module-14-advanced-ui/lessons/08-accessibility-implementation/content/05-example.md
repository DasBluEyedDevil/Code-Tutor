---
type: "EXAMPLE"
title: "Building Text-Scale-Aware Layouts"
---


Creating layouts that adapt to user text size preferences:



```dart
import 'package:flutter/material.dart';

/// Extension to easily check text scale preferences
extension TextScaleExtension on BuildContext {
  TextScaler get textScaler => MediaQuery.textScalerOf(this);
  
  double get textScaleFactor => textScaler.scale(1.0);
  
  bool get prefersLargerText => textScaleFactor > 1.3;
  
  bool get prefersVeryLargerText => textScaleFactor > 1.8;
}

/// A list tile that adapts to text scaling
class AdaptiveListTile extends StatelessWidget {
  final String title;
  final String subtitle;
  final Widget? leading;
  final Widget? trailing;
  final VoidCallback? onTap;

  const AdaptiveListTile({
    super.key,
    required this.title,
    required this.subtitle,
    this.leading,
    this.trailing,
    this.onTap,
  });

  @override
  Widget build(BuildContext context) {
    final prefersLarger = context.prefersLargerText;
    final prefersVeryLarge = context.prefersVeryLargerText;

    // At very large text sizes, switch to vertical layout
    if (prefersVeryLarge) {
      return _buildVerticalLayout(context);
    }

    return InkWell(
      onTap: onTap,
      child: Padding(
        padding: EdgeInsets.symmetric(
          horizontal: 16,
          // Increase padding at larger text sizes
          vertical: prefersLarger ? 16 : 12,
        ),
        child: Row(
          children: [
            if (leading != null) ...[
              leading!,
              SizedBox(width: prefersLarger ? 20 : 16),
            ],
            Expanded(
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                mainAxisSize: MainAxisSize.min,
                children: [
                  Text(
                    title,
                    style: Theme.of(context).textTheme.titleMedium,
                    // Allow text to wrap
                    softWrap: true,
                  ),
                  const SizedBox(height: 4),
                  Text(
                    subtitle,
                    style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                      color: Theme.of(context).colorScheme.onSurfaceVariant,
                    ),
                    softWrap: true,
                  ),
                ],
              ),
            ),
            if (trailing != null) ...[
              SizedBox(width: prefersLarger ? 20 : 16),
              trailing!,
            ],
          ],
        ),
      ),
    );
  }

  Widget _buildVerticalLayout(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            if (leading != null) ...[
              leading!,
              const SizedBox(height: 12),
            ],
            Text(
              title,
              style: Theme.of(context).textTheme.titleMedium,
            ),
            const SizedBox(height: 4),
            Text(
              subtitle,
              style: Theme.of(context).textTheme.bodyMedium?.copyWith(
                color: Theme.of(context).colorScheme.onSurfaceVariant,
              ),
            ),
            if (trailing != null) ...[
              const SizedBox(height: 12),
              Align(
                alignment: Alignment.centerRight,
                child: trailing,
              ),
            ],
          ],
        ),
      ),
    );
  }
}

/// Button row that wraps to column at large text sizes
class AdaptiveButtonRow extends StatelessWidget {
  final List<Widget> buttons;
  final MainAxisAlignment alignment;

  const AdaptiveButtonRow({
    super.key,
    required this.buttons,
    this.alignment = MainAxisAlignment.end,
  });

  @override
  Widget build(BuildContext context) {
    if (context.prefersLargerText) {
      // Stack buttons vertically at large text sizes
      return Column(
        crossAxisAlignment: CrossAxisAlignment.stretch,
        children: buttons.map((button) {
          return Padding(
            padding: const EdgeInsets.only(bottom: 8),
            child: button,
          );
        }).toList(),
      );
    }

    // Horizontal layout at normal text sizes
    return Row(
      mainAxisAlignment: alignment,
      children: buttons.map((button) {
        return Padding(
          padding: const EdgeInsets.only(left: 8),
          child: button,
        );
      }).toList(),
    );
  }
}

/// Card that scales its padding and spacing with text
class ScalableCard extends StatelessWidget {
  final Widget child;
  final EdgeInsets? padding;

  const ScalableCard({
    super.key,
    required this.child,
    this.padding,
  });

  @override
  Widget build(BuildContext context) {
    final scaleFactor = context.textScaleFactor;
    
    // Scale padding with text (but cap it)
    final scaledPadding = padding ?? EdgeInsets.all(
      (16 * scaleFactor).clamp(16.0, 32.0),
    );

    return Card(
      child: Padding(
        padding: scaledPadding,
        child: child,
      ),
    );
  }
}

/// Icon that scales with text size
class ScalableIcon extends StatelessWidget {
  final IconData icon;
  final double baseSize;
  final Color? color;

  const ScalableIcon({
    super.key,
    required this.icon,
    this.baseSize = 24,
    this.color,
  });

  @override
  Widget build(BuildContext context) {
    final textScaler = context.textScaler;
    // Scale icon with text, but cap the maximum
    final scaledSize = textScaler.scale(baseSize).clamp(baseSize, baseSize * 1.5);

    return Icon(
      icon,
      size: scaledSize,
      color: color,
    );
  }
}

/// Example: A complete settings page that scales properly
class AccessibleSettingsPage extends StatelessWidget {
  const AccessibleSettingsPage({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Settings'),
      ),
      body: ListView(
        children: [
          AdaptiveListTile(
            leading: const ScalableIcon(icon: Icons.person),
            title: 'Account',
            subtitle: 'Manage your account settings and preferences',
            trailing: const ScalableIcon(icon: Icons.chevron_right),
            onTap: () {},
          ),
          const Divider(height: 1),
          AdaptiveListTile(
            leading: const ScalableIcon(icon: Icons.notifications),
            title: 'Notifications',
            subtitle: 'Configure how and when you receive alerts',
            trailing: const ScalableIcon(icon: Icons.chevron_right),
            onTap: () {},
          ),
          const Divider(height: 1),
          AdaptiveListTile(
            leading: const ScalableIcon(icon: Icons.accessibility),
            title: 'Accessibility',
            subtitle: 'Adjust display, motion, and assistive features',
            trailing: const ScalableIcon(icon: Icons.chevron_right),
            onTap: () {},
          ),
          const SizedBox(height: 24),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 16),
            child: AdaptiveButtonRow(
              buttons: [
                OutlinedButton(
                  onPressed: () {},
                  child: const Text('Cancel'),
                ),
                ElevatedButton(
                  onPressed: () {},
                  child: const Text('Save Changes'),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
```
