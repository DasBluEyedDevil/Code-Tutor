import 'package:flutter/material.dart';

class ProfileHeader extends StatelessWidget {
  final String name;
  final String title;
  final String avatarUrl;
  final VoidCallback onEdit;
  final VoidCallback onSettings;

  const ProfileHeader({
    super.key,
    required this.name,
    required this.title,
    required this.avatarUrl,
    required this.onEdit,
    required this.onSettings,
  });

  @override
  Widget build(BuildContext context) {
    final textScaler = MediaQuery.textScalerOf(context);
    final scaleFactor = textScaler.scale(1.0);
    final useLargeLayout = scaleFactor > 1.5;

    return Card(
      margin: const EdgeInsets.all(16),
      child: Padding(
        padding: EdgeInsets.all(useLargeLayout ? 20 : 16),
        child: useLargeLayout
            ? _buildVerticalLayout(context, textScaler)
            : _buildHorizontalLayout(context, textScaler),
      ),
    );
  }

  Widget _buildHorizontalLayout(BuildContext context, TextScaler textScaler) {
    final theme = Theme.of(context);
    final iconSize = textScaler.scale(24.0).clamp(24.0, 36.0);

    return Row(
      children: [
        _buildAvatar(context, textScaler),
        const SizedBox(width: 16),
        Expanded(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            mainAxisSize: MainAxisSize.min,
            children: [
              Text(
                name,
                style: theme.textTheme.titleLarge,
                softWrap: true,
              ),
              const SizedBox(height: 4),
              Text(
                title,
                style: theme.textTheme.bodyMedium?.copyWith(
                  color: theme.colorScheme.onSurfaceVariant,
                ),
                softWrap: true,
              ),
            ],
          ),
        ),
        const SizedBox(width: 8),
        _buildActionButtons(context, iconSize),
      ],
    );
  }

  Widget _buildVerticalLayout(BuildContext context, TextScaler textScaler) {
    final theme = Theme.of(context);
    final iconSize = textScaler.scale(24.0).clamp(24.0, 36.0);

    return Column(
      crossAxisAlignment: CrossAxisAlignment.center,
      mainAxisSize: MainAxisSize.min,
      children: [
        _buildAvatar(context, textScaler, large: true),
        const SizedBox(height: 16),
        Text(
          name,
          style: theme.textTheme.titleLarge,
          textAlign: TextAlign.center,
          softWrap: true,
        ),
        const SizedBox(height: 4),
        Text(
          title,
          style: theme.textTheme.bodyMedium?.copyWith(
            color: theme.colorScheme.onSurfaceVariant,
          ),
          textAlign: TextAlign.center,
          softWrap: true,
        ),
        const SizedBox(height: 16),
        Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            _buildActionButton(
              context,
              icon: Icons.edit,
              label: 'Edit Profile',
              onPressed: onEdit,
              iconSize: iconSize,
            ),
            const SizedBox(width: 16),
            _buildActionButton(
              context,
              icon: Icons.settings,
              label: 'Settings',
              onPressed: onSettings,
              iconSize: iconSize,
            ),
          ],
        ),
      ],
    );
  }

  Widget _buildAvatar(BuildContext context, TextScaler textScaler, {bool large = false}) {
    final baseSize = large ? 80.0 : 56.0;
    final scaledSize = textScaler.scale(baseSize).clamp(baseSize, baseSize * 1.5);

    return Semantics(
      label: '$name profile picture',
      image: true,
      child: CircleAvatar(
        radius: scaledSize / 2,
        backgroundImage: NetworkImage(avatarUrl),
      ),
    );
  }

  Widget _buildActionButtons(BuildContext context, double iconSize) {
    return Row(
      mainAxisSize: MainAxisSize.min,
      children: [
        _buildActionButton(
          context,
          icon: Icons.edit,
          label: 'Edit Profile',
          onPressed: onEdit,
          iconSize: iconSize,
        ),
        const SizedBox(width: 8),
        _buildActionButton(
          context,
          icon: Icons.settings,
          label: 'Settings',
          onPressed: onSettings,
          iconSize: iconSize,
        ),
      ],
    );
  }

  Widget _buildActionButton(
    BuildContext context, {
    required IconData icon,
    required String label,
    required VoidCallback onPressed,
    required double iconSize,
  }) {
    return Semantics(
      button: true,
      label: label,
      child: ConstrainedBox(
        constraints: const BoxConstraints(
          minWidth: 48,
          minHeight: 48,
        ),
        child: IconButton(
          icon: Icon(icon, size: iconSize),
          onPressed: onPressed,
          tooltip: label,
        ),
      ),
    );
  }
}