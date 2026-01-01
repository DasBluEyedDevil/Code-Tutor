---
type: "EXAMPLE"
title: "Cupertino Widgets in Action"
---


Building an iOS-style interface:



```dart
import 'package:flutter/cupertino.dart';

// Full Cupertino app structure
class CupertinoAppExample extends StatelessWidget {
  const CupertinoAppExample({super.key});

  @override
  Widget build(BuildContext context) {
    return const CupertinoApp(
      title: 'iOS App',
      theme: CupertinoThemeData(
        primaryColor: CupertinoColors.systemBlue,
        brightness: Brightness.light,
      ),
      home: CupertinoHomePage(),
    );
  }
}

class CupertinoHomePage extends StatefulWidget {
  const CupertinoHomePage({super.key});

  @override
  State<CupertinoHomePage> createState() => _CupertinoHomePageState();
}

class _CupertinoHomePageState extends State<CupertinoHomePage> {
  bool _switchValue = false;
  double _sliderValue = 0.5;

  @override
  Widget build(BuildContext context) {
    return CupertinoPageScaffold(
      // iOS-style navigation bar
      navigationBar: const CupertinoNavigationBar(
        middle: Text('Settings'),
        // Large title style
        previousPageTitle: 'Back',
      ),
      child: SafeArea(
        child: ListView(
          children: [
            // iOS-style grouped list
            CupertinoListSection.insetGrouped(
              header: const Text('PREFERENCES'),
              children: [
                CupertinoListTile(
                  title: const Text('Notifications'),
                  trailing: CupertinoSwitch(
                    value: _switchValue,
                    onChanged: (value) {
                      setState(() => _switchValue = value);
                    },
                  ),
                ),
                CupertinoListTile(
                  title: const Text('Volume'),
                  subtitle: CupertinoSlider(
                    value: _sliderValue,
                    onChanged: (value) {
                      setState(() => _sliderValue = value);
                    },
                  ),
                ),
                CupertinoListTile(
                  title: const Text('Account'),
                  trailing: const CupertinoListTileChevron(),
                  onTap: () {
                    // Navigate
                  },
                ),
              ],
            ),
            
            const SizedBox(height: 20),
            
            // iOS-style buttons
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16),
              child: Column(
                children: [
                  // Filled button
                  SizedBox(
                    width: double.infinity,
                    child: CupertinoButton.filled(
                      onPressed: () => _showActionSheet(context),
                      child: const Text('Show Action Sheet'),
                    ),
                  ),
                  const SizedBox(height: 12),
                  // Plain button
                  CupertinoButton(
                    onPressed: () => _showAlert(context),
                    child: const Text('Show Alert'),
                  ),
                ],
              ),
            ),
          ],
        ),
      ),
    );
  }

  void _showActionSheet(BuildContext context) {
    showCupertinoModalPopup<void>(
      context: context,
      builder: (context) => CupertinoActionSheet(
        title: const Text('Choose Option'),
        message: const Text('Select an action to perform'),
        actions: [
          CupertinoActionSheetAction(
            onPressed: () => Navigator.pop(context),
            child: const Text('Option 1'),
          ),
          CupertinoActionSheetAction(
            onPressed: () => Navigator.pop(context),
            child: const Text('Option 2'),
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
    );
  }

  void _showAlert(BuildContext context) {
    showCupertinoDialog<void>(
      context: context,
      builder: (context) => CupertinoAlertDialog(
        title: const Text('Alert'),
        content: const Text('This is an iOS-style alert dialog.'),
        actions: [
          CupertinoDialogAction(
            onPressed: () => Navigator.pop(context),
            child: const Text('Cancel'),
          ),
          CupertinoDialogAction(
            isDefaultAction: true,
            onPressed: () => Navigator.pop(context),
            child: const Text('OK'),
          ),
        ],
      ),
    );
  }
}
```
