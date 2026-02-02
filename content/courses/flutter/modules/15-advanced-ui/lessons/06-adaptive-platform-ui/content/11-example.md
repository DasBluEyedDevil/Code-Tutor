---
type: "EXAMPLE"
title: "Using flutter_platform_widgets"
---


Build a complete adaptive app with minimal code:



```dart
import 'package:flutter/material.dart';
import 'package:flutter_platform_widgets/flutter_platform_widgets.dart';

class PlatformWidgetsApp extends StatelessWidget {
  const PlatformWidgetsApp({super.key});

  @override
  Widget build(BuildContext context) {
    // PlatformApp automatically uses MaterialApp or CupertinoApp
    return PlatformApp(
      title: 'Platform Widgets Demo',
      material: (_, __) => MaterialAppData(
        theme: ThemeData(
          colorScheme: ColorScheme.fromSeed(seedColor: Colors.blue),
          useMaterial3: true,
        ),
      ),
      cupertino: (_, __) => CupertinoAppData(
        theme: const CupertinoThemeData(
          primaryColor: CupertinoColors.systemBlue,
        ),
      ),
      home: const PlatformHomePage(),
    );
  }
}

class PlatformHomePage extends StatefulWidget {
  const PlatformHomePage({super.key});

  @override
  State<PlatformHomePage> createState() => _PlatformHomePageState();
}

class _PlatformHomePageState extends State<PlatformHomePage> {
  bool _darkMode = false;
  bool _notifications = true;
  final _nameController = TextEditingController();

  @override
  void dispose() {
    _nameController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    // PlatformScaffold adapts automatically
    return PlatformScaffold(
      appBar: PlatformAppBar(
        title: const Text('Settings'),
        trailingActions: [
          PlatformIconButton(
            icon: Icon(PlatformIcons(context).info),
            onPressed: () => _showAboutDialog(context),
          ),
        ],
      ),
      body: SafeArea(
        child: ListView(
          padding: const EdgeInsets.all(16),
          children: [
            // Platform text field
            PlatformTextField(
              controller: _nameController,
              hintText: 'Enter your name',
              material: (_, __) => MaterialTextFieldData(
                decoration: const InputDecoration(
                  labelText: 'Name',
                  border: OutlineInputBorder(),
                ),
              ),
              cupertino: (_, __) => CupertinoTextFieldData(
                placeholder: 'Enter your name',
                prefix: const Padding(
                  padding: EdgeInsets.only(left: 8),
                  child: Icon(CupertinoIcons.person),
                ),
              ),
            ),
            const SizedBox(height: 24),

            // Platform switch with label
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text('Dark Mode'),
                PlatformSwitch(
                  value: _darkMode,
                  onChanged: (value) => setState(() => _darkMode = value),
                ),
              ],
            ),
            const SizedBox(height: 16),

            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text('Notifications'),
                PlatformSwitch(
                  value: _notifications,
                  onChanged: (value) => setState(() => _notifications = value),
                ),
              ],
            ),
            const SizedBox(height: 32),

            // Platform button (filled)
            PlatformElevatedButton(
              onPressed: () => _showConfirmation(context),
              child: const Text('Save Changes'),
            ),
            const SizedBox(height: 16),

            // Platform button (text)
            PlatformTextButton(
              onPressed: () => _showOptions(context),
              child: const Text('More Options'),
            ),
            const SizedBox(height: 32),

            // Platform progress indicator
            Center(
              child: PlatformCircularProgressIndicator(),
            ),
          ],
        ),
      ),
    );
  }

  void _showConfirmation(BuildContext context) {
    showPlatformDialog(
      context: context,
      builder: (context) => PlatformAlertDialog(
        title: const Text('Save Changes?'),
        content: const Text('Your settings will be updated.'),
        actions: [
          PlatformDialogAction(
            child: const Text('Cancel'),
            onPressed: () => Navigator.pop(context),
          ),
          PlatformDialogAction(
            child: const Text('Save'),
            onPressed: () {
              Navigator.pop(context);
              // Save logic here
            },
          ),
        ],
      ),
    );
  }

  void _showOptions(BuildContext context) {
    showPlatformModalSheet(
      context: context,
      builder: (context) => PlatformWidget(
        material: (_, __) => Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            ListTile(
              leading: const Icon(Icons.share),
              title: const Text('Share'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: const Icon(Icons.download),
              title: const Text('Export'),
              onTap: () => Navigator.pop(context),
            ),
            ListTile(
              leading: const Icon(Icons.delete, color: Colors.red),
              title: const Text('Delete', style: TextStyle(color: Colors.red)),
              onTap: () => Navigator.pop(context),
            ),
          ],
        ),
        cupertino: (_, __) => CupertinoActionSheet(
          actions: [
            CupertinoActionSheetAction(
              onPressed: () => Navigator.pop(context),
              child: const Text('Share'),
            ),
            CupertinoActionSheetAction(
              onPressed: () => Navigator.pop(context),
              child: const Text('Export'),
            ),
            CupertinoActionSheetAction(
              isDestructiveAction: true,
              onPressed: () => Navigator.pop(context),
              child: const Text('Delete'),
            ),
          ],
          cancelButton: CupertinoActionSheetAction(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
        ),
      ),
    );
  }

  void _showAboutDialog(BuildContext context) {
    showPlatformDialog(
      context: context,
      builder: (context) => PlatformAlertDialog(
        title: const Text('About'),
        content: const Text(
          'This app demonstrates flutter_platform_widgets for building '
          'adaptive UIs that feel native on both iOS and Android.',
        ),
        actions: [
          PlatformDialogAction(
            child: const Text('OK'),
            onPressed: () => Navigator.pop(context),
          ),
        ],
      ),
    );
  }
}

// PlatformIcons helper for platform-specific icons
class IconsDemo extends StatelessWidget {
  const IconsDemo({super.key});

  @override
  Widget build(BuildContext context) {
    // PlatformIcons(context) returns appropriate icons per platform
    final icons = PlatformIcons(context);
    
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      children: [
        Icon(icons.home),      // Icons.home or CupertinoIcons.home
        Icon(icons.search),    // Icons.search or CupertinoIcons.search
        Icon(icons.settings),  // Icons.settings or CupertinoIcons.settings
        Icon(icons.delete),    // Icons.delete or CupertinoIcons.delete
      ],
    );
  }
}
```
