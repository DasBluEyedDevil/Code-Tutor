// lib/features/auth/data/biometric_auth_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:local_auth/local_auth.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';

class BiometricAuthRepository {
  final LocalAuthentication _localAuth;
  final FlutterSecureStorage _secureStorage;

  BiometricAuthRepository(this._localAuth, this._secureStorage);

  /// Check if biometric auth is available
  Future<BiometricCapability> checkBiometricCapability() async {
    // TODO: Implement
    throw UnimplementedError();
  }

  /// Enable biometric authentication for current user
  Future<bool> enableBiometric({
    required String email,
    required String authToken,
  }) async {
    // TODO: Implement
    throw UnimplementedError();
  }

  /// Authenticate with biometrics
  Future<BiometricAuthResult> authenticateWithBiometric() async {
    // TODO: Implement
    throw UnimplementedError();
  }

  /// Disable biometric authentication
  Future<void> disableBiometric() async {
    // TODO: Implement
    throw UnimplementedError();
  }

  /// Check if biometric is enabled for current user
  Future<bool> isBiometricEnabled() async {
    // TODO: Implement
    throw UnimplementedError();
  }
}

class BiometricCapability {
  final bool isAvailable;
  final bool isEnrolled;
  final List<BiometricType> availableTypes;
  final String? errorMessage;

  BiometricCapability({
    required this.isAvailable,
    required this.isEnrolled,
    required this.availableTypes,
    this.errorMessage,
  });
}

class BiometricAuthResult {
  final bool success;
  final String? authToken;
  final String? email;
  final String? errorMessage;
  final bool shouldFallback;

  BiometricAuthResult({
    required this.success,
    this.authToken,
    this.email,
    this.errorMessage,
    this.shouldFallback = false,
  });
}