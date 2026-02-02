// lib/features/auth/data/biometric_service.dart

import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:local_auth/local_auth.dart';
import 'package:local_auth/error_codes.dart' as auth_error;

enum BiometricResult {
  success,
  failed,
  cancelled,
  notAvailable,
  notEnrolled,
}

class BiometricService {
  final LocalAuthentication _localAuth = LocalAuthentication();
  final FlutterSecureStorage _storage;
  static const _biometricEnabledKey = 'biometric_enabled';

  BiometricService({FlutterSecureStorage? storage})
      : _storage = storage ?? const FlutterSecureStorage();

  /// Check if biometric authentication is available on this device.
  Future<bool> isBiometricAvailable() async {
    try {
      // Check if device supports biometrics
      final canCheckBiometrics = await _localAuth.canCheckBiometrics;
      final isDeviceSupported = await _localAuth.isDeviceSupported();

      if (!canCheckBiometrics || !isDeviceSupported) {
        return false;
      }

      // Check if biometrics are enrolled
      final availableBiometrics = await _localAuth.getAvailableBiometrics();
      return availableBiometrics.isNotEmpty;
    } catch (e) {
      return false;
    }
  }

  /// Get list of available biometric types.
  Future<List<BiometricType>> getAvailableBiometrics() async {
    try {
      return await _localAuth.getAvailableBiometrics();
    } catch (e) {
      return [];
    }
  }

  /// Check if user has enabled biometric login.
  Future<bool> isBiometricEnabled() async {
    final value = await _storage.read(key: _biometricEnabledKey);
    return value == 'true';
  }

  /// Enable or disable biometric login.
  Future<void> setBiometricEnabled(bool enabled) async {
    await _storage.write(
      key: _biometricEnabledKey,
      value: enabled.toString(),
    );
  }

  /// Authenticate using biometrics.
  Future<BiometricResult> authenticate() async {
    try {
      // Check availability first
      final isAvailable = await isBiometricAvailable();
      if (!isAvailable) {
        return BiometricResult.notAvailable;
      }

      // Attempt authentication
      final didAuthenticate = await _localAuth.authenticate(
        localizedReason: 'Please authenticate to login',
        options: const AuthenticationOptions(
          stickyAuth: true,
          biometricOnly: true,
        ),
      );

      return didAuthenticate
          ? BiometricResult.success
          : BiometricResult.failed;
    } on PlatformException catch (e) {
      if (e.code == auth_error.notAvailable) {
        return BiometricResult.notAvailable;
      }
      if (e.code == auth_error.notEnrolled) {
        return BiometricResult.notEnrolled;
      }
      if (e.code == auth_error.lockedOut ||
          e.code == auth_error.permanentlyLockedOut) {
        return BiometricResult.failed;
      }
      return BiometricResult.cancelled;
    } catch (e) {
      return BiometricResult.failed;
    }
  }
}

// Add biometric provider:
// final biometricServiceProvider = Provider<BiometricService>((ref) {
//   return BiometricService();
// });

// Usage in SplashScreen:
// final biometricService = ref.read(biometricServiceProvider);
// if (await biometricService.isBiometricEnabled()) {
//   final result = await biometricService.authenticate();
//   if (result == BiometricResult.success) {
//     // User authenticated, restore session
//   }
// }