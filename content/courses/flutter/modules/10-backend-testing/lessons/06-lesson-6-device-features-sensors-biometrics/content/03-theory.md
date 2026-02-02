---
type: "THEORY"
title: "Part 1: Biometric Authentication"
---


### Setup

**pubspec.yaml:**

**Android Configuration (`android/app/src/main/AndroidManifest.xml`):**

**iOS Configuration (`ios/Runner/Info.plist`):**

### Basic Biometric Authentication




```dart
import 'package:flutter/material.dart';
import 'package:local_auth/local_auth.dart';
import 'package:local_auth/error_codes.dart' as auth_error;

class BiometricAuthScreen extends StatefulWidget {
  @override
  State<BiometricAuthScreen> createState() => _BiometricAuthScreenState();
}

class _BiometricAuthScreenState extends State<BiometricAuthScreen> {
  final LocalAuthentication _localAuth = LocalAuthentication();
  bool _isAuthenticated = false;
  List<BiometricType> _availableBiometrics = [];

  @override
  void initState() {
    super.initState();
    _checkBiometrics();
  }

  // Check what biometrics are available
  Future<void> _checkBiometrics() async {
    try {
      // Check if device supports biometrics
      final canCheckBiometrics = await _localAuth.canCheckBiometrics;
      final isDeviceSupported = await _localAuth.isDeviceSupported();

      if (canCheckBiometrics && isDeviceSupported) {
        // Get list of available biometrics
        final availableBiometrics = await _localAuth.getAvailableBiometrics();

        setState(() {
          _availableBiometrics = availableBiometrics;
        });

        print('Available biometrics: $_availableBiometrics');
        // Possible values:
        // - BiometricType.face (Face ID on iOS, face unlock on Android)
        // - BiometricType.fingerprint (Touch ID on iOS, fingerprint on Android)
        // - BiometricType.iris (Iris scanner on Samsung devices)
      }
    } catch (e) {
      print('Error checking biometrics: $e');
    }
  }

  // Authenticate with biometrics
  Future<void> _authenticate() async {
    try {
      final authenticated = await _localAuth.authenticate(
        localizedReason: 'Please authenticate to access your account',
        options: AuthenticationOptions(
          stickyAuth: true,  // Show auth dialog until user interacts
          biometricOnly: false,  // Allow PIN/password fallback
        ),
      );

      setState(() {
        _isAuthenticated = authenticated;
      });

      if (authenticated) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Authentication successful!'),
            backgroundColor: Colors.green,
          ),
        );
      }
    } on PlatformException catch (e) {
      // Handle errors
      if (e.code == auth_error.notAvailable) {
        print('Biometrics not available');
      } else if (e.code == auth_error.notEnrolled) {
        print('No biometrics enrolled');
      } else if (e.code == auth_error.lockedOut) {
        print('Too many failed attempts');
      } else if (e.code == auth_error.permanentlyLockedOut) {
        print('Permanently locked out');
      }

      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text('Authentication failed: ${e.message}')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text('Biometric Authentication')),
      body: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              _isAuthenticated ? Icons.lock_open : Icons.lock,
              size: 100,
              color: _isAuthenticated ? Colors.green : Colors.red,
            ),

            SizedBox(height: 20),

            Text(
              _isAuthenticated ? 'Authenticated ✓' : 'Not Authenticated ✗',
              style: TextStyle(
                fontSize: 24,
                fontWeight: FontWeight.bold,
                color: _isAuthenticated ? Colors.green : Colors.red,
              ),
            ),

            SizedBox(height: 40),

            // Available biometrics
            if (_availableBiometrics.isNotEmpty) ...[
              Text('Available biometrics:', style: TextStyle(fontSize: 16)),
              SizedBox(height: 10),
              ..._availableBiometrics.map((biometric) => Chip(
                    label: Text(biometric.toString().split('.').last),
                  )),
            ],

            SizedBox(height: 40),

            ElevatedButton.icon(
              onPressed: _authenticate,
              icon: Icon(Icons.fingerprint),
              label: Text('Authenticate'),
              style: ElevatedButton.styleFrom(
                padding: EdgeInsets.symmetric(horizontal: 32, vertical: 16),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
```
