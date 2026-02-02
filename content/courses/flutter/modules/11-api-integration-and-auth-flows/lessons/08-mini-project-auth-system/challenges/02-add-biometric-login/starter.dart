// lib/features/auth/data/biometric_service.dart

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
// import 'package:local_auth/local_auth.dart';

enum BiometricResult {
  success,
  failed,
  cancelled,
  notAvailable,
  notEnrolled,
}

class BiometricService {
  // final LocalAuthentication _localAuth = LocalAuthentication();
  final FlutterSecureStorage _storage;
  static const _biometricEnabledKey = 'biometric_enabled';

  BiometricService({FlutterSecureStorage? storage})
      : _storage = storage ?? const FlutterSecureStorage();

  /// Check if biometric authentication is available on this device.
  Future<bool> isBiometricAvailable() async {
    // TODO: Implement using local_auth
    // 1. Check if device supports biometrics
    // 2. Check if biometrics are enrolled
    throw UnimplementedError();
  }

  /// Check if user has enabled biometric login.
  Future<bool> isBiometricEnabled() async {
    // TODO: Read from secure storage
    throw UnimplementedError();
  }

  /// Enable or disable biometric login.
  Future<void> setBiometricEnabled(bool enabled) async {
    // TODO: Store in secure storage
    throw UnimplementedError();
  }

  /// Authenticate using biometrics.
  Future<BiometricResult> authenticate() async {
    // TODO: Implement biometric authentication
    // 1. Check availability
    // 2. Show biometric prompt
    // 3. Return appropriate result
    throw UnimplementedError();
  }
}