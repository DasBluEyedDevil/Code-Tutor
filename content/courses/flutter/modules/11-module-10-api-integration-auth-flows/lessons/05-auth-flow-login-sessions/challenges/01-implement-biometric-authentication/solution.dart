// lib/services/biometric_service.dart
import 'package:flutter/services.dart';
import 'package:local_auth/local_auth.dart';

class BiometricService {
  final LocalAuthentication _localAuth = LocalAuthentication();
  
  /// Check if device supports biometric authentication
  Future<bool> isBiometricAvailable() async {
    try {
      // Check if hardware supports biometrics
      final canCheckBiometrics = await _localAuth.canCheckBiometrics;
      // Check if device supports authentication (includes PIN/pattern)
      final isDeviceSupported = await _localAuth.isDeviceSupported();
      
      if (!canCheckBiometrics || !isDeviceSupported) {
        return false;
      }
      
      // Check if any biometrics are enrolled
      final availableBiometrics = await _localAuth.getAvailableBiometrics();
      return availableBiometrics.isNotEmpty;
    } on PlatformException {
      return false;
    }
  }
  
  /// Get list of available biometric types
  Future<List<BiometricType>> getAvailableBiometrics() async {
    try {
      return await _localAuth.getAvailableBiometrics();
    } on PlatformException {
      return [];
    }
  }
  
  /// Authenticate using biometrics
  Future<bool> authenticate({required String reason}) async {
    try {
      return await _localAuth.authenticate(
        localizedReason: reason,
        options: const AuthenticationOptions(
          stickyAuth: true,
          biometricOnly: true,
        ),
      );
    } on PlatformException catch (e) {
      if (e.code == 'NotAvailable') {
        // Biometrics not available
        return false;
      } else if (e.code == 'NotEnrolled') {
        // No biometrics enrolled
        return false;
      } else if (e.code == 'LockedOut') {
        // Too many attempts
        throw BiometricLockedException();
      }
      return false;
    }
  }
}

class BiometricLockedException implements Exception {
  final String message = 'Biometric authentication is locked. Please try again later.';
}

// lib/widgets/biometric_login_button.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class BiometricLoginButton extends ConsumerStatefulWidget {
  final VoidCallback onAuthenticated;
  
  const BiometricLoginButton({super.key, required this.onAuthenticated});
  
  @override
  ConsumerState<BiometricLoginButton> createState() => _BiometricLoginButtonState();
}

class _BiometricLoginButtonState extends ConsumerState<BiometricLoginButton> {
  bool _isAvailable = false;
  bool _isLoading = false;
  
  @override
  void initState() {
    super.initState();
    _checkAvailability();
  }
  
  Future<void> _checkAvailability() async {
    final biometricService = ref.read(biometricServiceProvider);
    final secureStorage = ref.read(secureStorageProvider);
    
    final isBiometricAvailable = await biometricService.isBiometricAvailable();
    final hasCredentials = await secureStorage.hasAuthCredentials();
    final biometricEnabled = await secureStorage.isBiometricLoginEnabled();
    
    if (mounted) {
      setState(() {
        _isAvailable = isBiometricAvailable && hasCredentials && biometricEnabled;
      });
    }
  }
  
  Future<void> _handleBiometricLogin() async {
    setState(() => _isLoading = true);
    
    try {
      final biometricService = ref.read(biometricServiceProvider);
      final authenticated = await biometricService.authenticate(
        reason: 'Authenticate to sign in',
      );
      
      if (authenticated) {
        widget.onAuthenticated();
      }
    } on BiometricLockedException catch (e) {
      ScaffoldMessenger.of(context).showSnackBar(
        SnackBar(content: Text(e.message)),
      );
    } finally {
      if (mounted) {
        setState(() => _isLoading = false);
      }
    }
  }
  
  @override
  Widget build(BuildContext context) {
    if (!_isAvailable) return const SizedBox.shrink();
    
    return OutlinedButton.icon(
      onPressed: _isLoading ? null : _handleBiometricLogin,
      icon: _isLoading
          ? const SizedBox(width: 20, height: 20, child: CircularProgressIndicator(strokeWidth: 2))
          : const Icon(Icons.fingerprint),
      label: const Text('Sign in with Biometrics'),
      style: OutlinedButton.styleFrom(
        padding: const EdgeInsets.symmetric(vertical: 12),
      ),
    );
  }
}