---
type: "EXAMPLE"
title: "Accessible Touch Targets"
---


Ensuring proper touch target sizes:



```dart
import 'package:flutter/material.dart';

// Minimum touch target size constant
const double kMinTouchTargetSize = 48.0;

// Widget that ensures minimum touch target size
class AccessibleTouchTarget extends StatelessWidget {
  final Widget child;
  final VoidCallback? onTap;
  final double minSize;

  const AccessibleTouchTarget({
    super.key,
    required this.child,
    this.onTap,
    this.minSize = kMinTouchTargetSize,
  });

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onTap,
      behavior: HitTestBehavior.opaque,
      child: ConstrainedBox(
        constraints: BoxConstraints(
          minWidth: minSize,
          minHeight: minSize,
        ),
        child: Center(child: child),
      ),
    );
  }
}

// Example: Small icon with proper touch target
class AccessibleSmallIcon extends StatelessWidget {
  final IconData icon;
  final String label;
  final VoidCallback onPressed;
  final double iconSize;

  const AccessibleSmallIcon({
    super.key,
    required this.icon,
    required this.label,
    required this.onPressed,
    this.iconSize = 24,
  });

  @override
  Widget build(BuildContext context) {
    return Semantics(
      label: label,
      button: true,
      child: InkWell(
        onTap: onPressed,
        customBorder: const CircleBorder(),
        child: Padding(
          // Add padding to reach minimum touch target
          padding: EdgeInsets.all((kMinTouchTargetSize - iconSize) / 2),
          child: Icon(icon, size: iconSize),
        ),
      ),
    );
  }
}

// Toolbar with proper spacing between actions
class AccessibleToolbar extends StatelessWidget {
  const AccessibleToolbar({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 8),
      child: Row(
        children: [
          // Each IconButton already has 48x48 touch target
          IconButton(
            icon: const Icon(Icons.arrow_back),
            onPressed: () => Navigator.pop(context),
            tooltip: 'Go back', // Provides accessibility label
          ),
          const Spacer(),
          // Minimum 8dp spacing between touch targets
          // IconButton includes internal padding, so they're safe
          IconButton(
            icon: const Icon(Icons.search),
            onPressed: () {},
            tooltip: 'Search',
          ),
          IconButton(
            icon: const Icon(Icons.more_vert),
            onPressed: () {},
            tooltip: 'More options',
          ),
        ],
      ),
    );
  }
}

// List with accessible tap targets
class AccessibleListTile extends StatelessWidget {
  final String title;
  final String? subtitle;
  final Widget? leading;
  final VoidCallback? onTap;
  final List<Widget>? trailingActions;

  const AccessibleListTile({
    super.key,
    required this.title,
    this.subtitle,
    this.leading,
    this.onTap,
    this.trailingActions,
  });

  @override
  Widget build(BuildContext context) {
    return ListTile(
      // ListTile already has proper height for touch targets
      leading: leading,
      title: Text(title),
      subtitle: subtitle != null ? Text(subtitle!) : null,
      onTap: onTap,
      trailing: trailingActions != null
          ? Row(
              mainAxisSize: MainAxisSize.min,
              children: trailingActions!.map((action) {
                // Ensure each action has proper touch target
                return Padding(
                  padding: const EdgeInsets.only(left: 8),
                  child: action,
                );
              }).toList(),
            )
          : null,
    );
  }
}

// Dense layout with proper touch targets
class AccessibleChipList extends StatelessWidget {
  final List<String> chips;
  final Set<String> selected;
  final ValueChanged<String> onToggle;

  const AccessibleChipList({
    super.key,
    required this.chips,
    required this.selected,
    required this.onToggle,
  });

  @override
  Widget build(BuildContext context) {
    return Wrap(
      spacing: 8, // Horizontal spacing between chips
      runSpacing: 8, // Vertical spacing between rows
      children: chips.map((chip) {
        final isSelected = selected.contains(chip);
        return FilterChip(
          label: Text(chip),
          selected: isSelected,
          onSelected: (_) => onToggle(chip),
          // FilterChip already has 48dp height by default
          // materialTapTargetSize can be set if needed:
          materialTapTargetSize: MaterialTapTargetSize.padded,
        );
      }).toList(),
    );
  }
}

// Bottom action bar with proper sizing
class AccessibleBottomBar extends StatelessWidget {
  const AccessibleBottomBar({super.key});

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(16),
      child: Row(
        children: [
          Expanded(
            child: OutlinedButton(
              onPressed: () {},
              style: OutlinedButton.styleFrom(
                minimumSize: const Size(0, 48), // Ensure minimum height
              ),
              child: const Text('Cancel'),
            ),
          ),
          const SizedBox(width: 16), // Spacing between buttons
          Expanded(
            child: ElevatedButton(
              onPressed: () {},
              style: ElevatedButton.styleFrom(
                minimumSize: const Size(0, 48), // Ensure minimum height
              ),
              child: const Text('Confirm'),
            ),
          ),
        ],
      ),
    );
  }
}
```
