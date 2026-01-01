---
type: "EXAMPLE"
title: "In-App Update Prompts"
---


Create flexible update prompt dialogs for different update types:



```dart
// lib/widgets/update_dialogs.dart
import 'package:flutter/material.dart';
import '../services/update_service.dart';

/// Shows appropriate update dialog based on update status
class UpdateDialogs {
  /// Show update dialog and return user's choice
  static Future<bool> showUpdateDialog(
    BuildContext context,
    UpdateInfo updateInfo,
  ) async {
    switch (updateInfo.status) {
      case UpdateStatus.optionalUpdate:
        return await _showOptionalUpdateDialog(context, updateInfo);
      case UpdateStatus.recommendedUpdate:
        return await _showRecommendedUpdateDialog(context, updateInfo);
      case UpdateStatus.requiredUpdate:
        return await _showRequiredUpdateDialog(context, updateInfo);
      default:
        return false;
    }
  }
  
  /// Optional update - can be dismissed
  static Future<bool> _showOptionalUpdateDialog(
    BuildContext context,
    UpdateInfo updateInfo,
  ) async {
    return await showDialog<bool>(
      context: context,
      barrierDismissible: true,
      builder: (context) => AlertDialog(
        title: const Text('Update Available'),
        content: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'A new version (${updateInfo.latestVersion}) is available.',
            ),
            if (updateInfo.releaseNotes != null) ...[
              const SizedBox(height: 12),
              const Text(
                'What\'s New:',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 4),
              Text(
                updateInfo.releaseNotes!,
                style: const TextStyle(fontSize: 14),
              ),
            ],
          ],
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            child: const Text('Later'),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.pop(context, true);
              UpdateService.openStore();
            },
            child: const Text('Update'),
          ),
        ],
      ),
    ) ?? false;
  }
  
  /// Recommended update - persistent but can skip
  static Future<bool> _showRecommendedUpdateDialog(
    BuildContext context,
    UpdateInfo updateInfo,
  ) async {
    return await showDialog<bool>(
      context: context,
      barrierDismissible: false, // Force interaction
      builder: (context) => AlertDialog(
        title: Row(
          children: [
            const Icon(Icons.recommend, color: Colors.orange),
            const SizedBox(width: 8),
            const Text('Update Recommended'),
          ],
        ),
        content: Column(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text(
              'Version ${updateInfo.latestVersion} includes important improvements.',
            ),
            const SizedBox(height: 8),
            const Text(
              'We strongly recommend updating for the best experience.',
              style: TextStyle(fontWeight: FontWeight.w500),
            ),
            if (updateInfo.releaseNotes != null) ...[
              const SizedBox(height: 12),
              const Text(
                'What\'s New:',
                style: TextStyle(fontWeight: FontWeight.bold),
              ),
              const SizedBox(height: 4),
              Container(
                padding: const EdgeInsets.all(8),
                decoration: BoxDecoration(
                  color: Colors.grey[100],
                  borderRadius: BorderRadius.circular(4),
                ),
                child: Text(
                  updateInfo.releaseNotes!,
                  style: const TextStyle(fontSize: 13),
                ),
              ),
            ],
          ],
        ),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(context, false),
            child: const Text('Skip This Version'),
          ),
          ElevatedButton(
            onPressed: () {
              Navigator.pop(context, true);
              UpdateService.openStore();
            },
            style: ElevatedButton.styleFrom(
              backgroundColor: Colors.orange,
            ),
            child: const Text('Update Now'),
          ),
        ],
      ),
    ) ?? false;
  }
  
  /// Required update - cannot dismiss, blocks app
  static Future<bool> _showRequiredUpdateDialog(
    BuildContext context,
    UpdateInfo updateInfo,
  ) async {
    return await showDialog<bool>(
      context: context,
      barrierDismissible: false,
      builder: (context) => PopScope(
        canPop: false, // Prevent back button
        child: AlertDialog(
          title: Row(
            children: [
              const Icon(Icons.security_update_warning, color: Colors.red),
              const SizedBox(width: 8),
              const Text('Update Required'),
            ],
          ),
          content: Column(
            mainAxisSize: MainAxisSize.min,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              const Text(
                'A critical update is required to continue using this app.',
                style: TextStyle(fontWeight: FontWeight.w500),
              ),
              const SizedBox(height: 12),
              Text(
                'Current version: ${updateInfo.currentVersion}',
                style: TextStyle(color: Colors.grey[600]),
              ),
              Text(
                'Required version: ${updateInfo.latestVersion}',
                style: TextStyle(color: Colors.grey[600]),
              ),
              if (updateInfo.releaseNotes != null) ...[
                const SizedBox(height: 12),
                Text(
                  updateInfo.releaseNotes!,
                  style: const TextStyle(fontSize: 13),
                ),
              ],
            ],
          ),
          actions: [
            SizedBox(
              width: double.infinity,
              child: ElevatedButton(
                onPressed: () {
                  Navigator.pop(context, true);
                  UpdateService.openStore();
                },
                style: ElevatedButton.styleFrom(
                  backgroundColor: Colors.red,
                  padding: const EdgeInsets.symmetric(vertical: 12),
                ),
                child: const Text(
                  'Update Now',
                  style: TextStyle(fontSize: 16),
                ),
              ),
            ),
          ],
        ),
      ),
    ) ?? true;
  }
}

/// Bottom sheet style update prompt (less intrusive)
class UpdateBottomSheet {
  static Future<void> show(
    BuildContext context,
    UpdateInfo updateInfo,
  ) async {
    await showModalBottomSheet(
      context: context,
      isDismissible: updateInfo.status != UpdateStatus.requiredUpdate,
      enableDrag: updateInfo.status != UpdateStatus.requiredUpdate,
      builder: (context) => Container(
        padding: const EdgeInsets.all(24),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            Icon(
              updateInfo.isRequired 
                  ? Icons.system_update 
                  : Icons.upgrade,
              size: 48,
              color: updateInfo.isRequired ? Colors.red : Colors.blue,
            ),
            const SizedBox(height: 16),
            Text(
              updateInfo.isRequired 
                  ? 'Update Required' 
                  : 'Update Available',
              style: const TextStyle(
                fontSize: 20,
                fontWeight: FontWeight.bold,
              ),
            ),
            const SizedBox(height: 8),
            Text(
              'Version ${updateInfo.latestVersion} is now available',
              style: TextStyle(color: Colors.grey[600]),
            ),
            if (updateInfo.releaseNotes != null) ...[
              const SizedBox(height: 16),
              Text(
                updateInfo.releaseNotes!,
                textAlign: TextAlign.center,
              ),
            ],
            const SizedBox(height: 24),
            Row(
              children: [
                if (!updateInfo.isRequired)
                  Expanded(
                    child: OutlinedButton(
                      onPressed: () => Navigator.pop(context),
                      child: const Text('Maybe Later'),
                    ),
                  ),
                if (!updateInfo.isRequired) const SizedBox(width: 12),
                Expanded(
                  child: ElevatedButton(
                    onPressed: () {
                      Navigator.pop(context);
                      UpdateService.openStore();
                    },
                    child: const Text('Update'),
                  ),
                ),
              ],
            ),
            const SizedBox(height: 8),
          ],
        ),
      ),
    );
  }
}
```
