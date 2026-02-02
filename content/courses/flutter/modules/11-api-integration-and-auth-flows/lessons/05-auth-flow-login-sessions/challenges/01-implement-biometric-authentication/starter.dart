// lib/services/biometric_service.dart
import 'package:local_auth/local_auth.dart';

class BiometricService {
  final LocalAuthentication _localAuth = LocalAuthentication();
  
  /// Check if device supports biometric authentication
  Future<bool> isBiometricAvailable() async {
    // TODO: Check if device can check biometrics
    // TODO: Check if any biometrics are enrolled
    throw UnimplementedError();
  }
  
  /// Get list of available biometric types
  Future<List<BiometricType>> getAvailableBiometrics() async {
    // TODO: Return available biometric types
    throw UnimplementedError();
  }
  
  /// Authenticate using biometrics
  Future<bool> authenticate({required String reason}) async {
    // TODO: Trigger biometric authentication
    // TODO: Handle errors and return result
    throw UnimplementedError();
  }
}

// In your login screen, add a biometric button that:
// 1. Only shows when biometrics are available AND user has stored credentials
// 2. Authenticates with biometrics
// 3. Retrieves stored credentials and logs in automatically