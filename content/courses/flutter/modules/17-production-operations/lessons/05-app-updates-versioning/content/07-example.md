---
type: "EXAMPLE"
title: "Forced Update Screen"
---


Implement a blocking screen for critical updates that prevents app usage:



```dart
// lib/screens/forced_update_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import '../services/update_service.dart';

/// Full-screen blocking update required screen
class ForcedUpdateScreen extends StatelessWidget {
  final UpdateInfo updateInfo;
  final VoidCallback? onRetryCheck;
  
  const ForcedUpdateScreen({
    super.key,
    required this.updateInfo,
    this.onRetryCheck,
  });
  
  @override
  Widget build(BuildContext context) {
    return PopScope(
      canPop: false, // Disable back button
      child: Scaffold(
        body: SafeArea(
          child: Padding(
            padding: const EdgeInsets.all(32),
            child: Column(
              children: [
                const Spacer(),
                // Update illustration
                Container(
                  width: 150,
                  height: 150,
                  decoration: BoxDecoration(
                    color: Colors.red.withValues(alpha: 0.1),
                    shape: BoxShape.circle,
                  ),
                  child: const Icon(
                    Icons.system_update,
                    size: 80,
                    color: Colors.red,
                  ),
                ),
                const SizedBox(height: 32),
                
                // Title
                const Text(
                  'Update Required',
                  style: TextStyle(
                    fontSize: 28,
                    fontWeight: FontWeight.bold,
                  ),
                ),
                const SizedBox(height: 16),
                
                // Description
                Text(
                  'A critical update is available. Please update to continue using the app.',
                  textAlign: TextAlign.center,
                  style: TextStyle(
                    fontSize: 16,
                    color: Colors.grey[600],
                  ),
                ),
                const SizedBox(height: 24),
                
                // Version info
                Container(
                  padding: const EdgeInsets.all(16),
                  decoration: BoxDecoration(
                    color: Colors.grey[100],
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: Column(
                    children: [
                      _versionRow(
                        'Current Version',
                        updateInfo.currentVersion ?? 'Unknown',
                        Colors.grey,
                      ),
                      const Divider(height: 16),
                      _versionRow(
                        'Required Version',
                        updateInfo.latestVersion ?? 'Unknown',
                        Colors.green,
                      ),
                    ],
                  ),
                ),
                
                // Release notes
                if (updateInfo.releaseNotes != null) ...[
                  const SizedBox(height: 24),
                  const Align(
                    alignment: Alignment.centerLeft,
                    child: Text(
                      'What\'s New:',
                      style: TextStyle(
                        fontSize: 16,
                        fontWeight: FontWeight.bold,
                      ),
                    ),
                  ),
                  const SizedBox(height: 8),
                  Container(
                    width: double.infinity,
                    padding: const EdgeInsets.all(12),
                    decoration: BoxDecoration(
                      border: Border.all(color: Colors.grey[300]!),
                      borderRadius: BorderRadius.circular(8),
                    ),
                    child: Text(
                      updateInfo.releaseNotes!,
                      style: const TextStyle(fontSize: 14),
                    ),
                  ),
                ],
                
                const Spacer(),
                
                // Update button
                SizedBox(
                  width: double.infinity,
                  height: 56,
                  child: ElevatedButton(
                    onPressed: () => UpdateService.openStore(),
                    style: ElevatedButton.styleFrom(
                      backgroundColor: Colors.red,
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(12),
                      ),
                    ),
                    child: const Row(
                      mainAxisAlignment: MainAxisAlignment.center,
                      children: [
                        Icon(Icons.download),
                        SizedBox(width: 8),
                        Text(
                          'Update Now',
                          style: TextStyle(fontSize: 18),
                        ),
                      ],
                    ),
                  ),
                ),
                const SizedBox(height: 12),
                
                // Retry check button
                TextButton(
                  onPressed: onRetryCheck,
                  child: const Text('I\'ve already updated'),
                ),
                const SizedBox(height: 16),
              ],
            ),
          ),
        ),
      ),
    );
  }
  
  Widget _versionRow(String label, String version, Color color) {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Text(label),
        Container(
          padding: const EdgeInsets.symmetric(horizontal: 12, vertical: 4),
          decoration: BoxDecoration(
            color: color.withValues(alpha: 0.1),
            borderRadius: BorderRadius.circular(16),
          ),
          child: Text(
            version,
            style: TextStyle(
              fontWeight: FontWeight.bold,
              color: color,
            ),
          ),
        ),
      ],
    );
  }
}

/// Wrapper widget that checks for required updates on launch
class UpdateCheckWrapper extends StatefulWidget {
  final Widget child;
  
  const UpdateCheckWrapper({super.key, required this.child});
  
  @override
  State<UpdateCheckWrapper> createState() => _UpdateCheckWrapperState();
}

class _UpdateCheckWrapperState extends State<UpdateCheckWrapper> {
  UpdateInfo? _updateInfo;
  bool _isLoading = true;
  
  @override
  void initState() {
    super.initState();
    _checkForUpdates();
  }
  
  Future<void> _checkForUpdates() async {
    setState(() => _isLoading = true);
    
    try {
      final updateInfo = await UpdateService.checkForUpdates();
      setState(() {
        _updateInfo = updateInfo;
        _isLoading = false;
      });
    } catch (e) {
      setState(() {
        _updateInfo = null;
        _isLoading = false;
      });
    }
  }
  
  @override
  Widget build(BuildContext context) {
    if (_isLoading) {
      return const MaterialApp(
        home: Scaffold(
          body: Center(
            child: CircularProgressIndicator(),
          ),
        ),
      );
    }
    
    // Show forced update screen if required
    if (_updateInfo?.status == UpdateStatus.requiredUpdate) {
      return MaterialApp(
        home: ForcedUpdateScreen(
          updateInfo: _updateInfo!,
          onRetryCheck: _checkForUpdates,
        ),
      );
    }
    
    // Show app and optionally prompt for updates
    return _UpdatePromptHandler(
      updateInfo: _updateInfo,
      child: widget.child,
    );
  }
}

/// Handles showing optional/recommended update prompts
class _UpdatePromptHandler extends StatefulWidget {
  final UpdateInfo? updateInfo;
  final Widget child;
  
  const _UpdatePromptHandler({
    required this.updateInfo,
    required this.child,
  });
  
  @override
  State<_UpdatePromptHandler> createState() => _UpdatePromptHandlerState();
}

class _UpdatePromptHandlerState extends State<_UpdatePromptHandler> {
  bool _promptShown = false;
  
  @override
  void didChangeDependencies() {
    super.didChangeDependencies();
    _showPromptIfNeeded();
  }
  
  void _showPromptIfNeeded() {
    if (_promptShown || widget.updateInfo == null) return;
    if (!widget.updateInfo!.hasUpdate) return;
    
    _promptShown = true;
    
    // Wait for first frame, then show dialog
    WidgetsBinding.instance.addPostFrameCallback((_) {
      if (mounted) {
        UpdateDialogs.showUpdateDialog(context, widget.updateInfo!);
      }
    });
  }
  
  @override
  Widget build(BuildContext context) => widget.child;
}

// Usage in main.dart:
// void main() async {
//   WidgetsFlutterBinding.ensureInitialized();
//   await VersionInfo.initialize();
//   
//   runApp(
//     UpdateCheckWrapper(
//       child: MyApp(),
//     ),
//   );
// }
```
