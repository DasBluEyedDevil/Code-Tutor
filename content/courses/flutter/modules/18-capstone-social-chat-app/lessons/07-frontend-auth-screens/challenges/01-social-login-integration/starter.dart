// lib/features/auth/presentation/widgets/social_login_buttons.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class SocialLoginButtons extends ConsumerWidget {
  const SocialLoginButtons({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // TODO: Implement social login buttons
    // 1. Add Google Sign-In button
    // 2. Add Apple Sign-In button (show only on iOS/macOS/web)
    // 3. Handle loading states
    // 4. Show errors in snackbar
    throw UnimplementedError();
  }
}

// lib/features/auth/data/social_auth_repository.dart
import 'package:flutter_riverpod/flutter_riverpod.dart';

class SocialAuthRepository {
  final Ref _ref;

  SocialAuthRepository(this._ref);

  /// Sign in with Google
  Future<SocialAuthResult> signInWithGoogle() async {
    // TODO: Implement Google Sign-In
    throw UnimplementedError();
  }

  /// Sign in with Apple
  Future<SocialAuthResult> signInWithApple() async {
    // TODO: Implement Apple Sign-In
    throw UnimplementedError();
  }

  /// Link social account to existing user
  Future<bool> linkSocialAccount({
    required String provider,
    required String token,
  }) async {
    // TODO: Implement account linking
    throw UnimplementedError();
  }
}

class SocialAuthResult {
  final bool success;
  final bool needsLinking;
  final String? email;
  final String? errorMessage;

  SocialAuthResult({
    required this.success,
    this.needsLinking = false,
    this.email,
    this.errorMessage,
  });
}