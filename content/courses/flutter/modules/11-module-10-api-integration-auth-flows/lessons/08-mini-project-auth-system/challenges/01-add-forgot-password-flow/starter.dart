// lib/features/auth/presentation/screens/forgot_password_screen.dart

import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

class ForgotPasswordScreen extends ConsumerStatefulWidget {
  const ForgotPasswordScreen({super.key});

  @override
  ConsumerState<ForgotPasswordScreen> createState() =>
      _ForgotPasswordScreenState();
}

class _ForgotPasswordScreenState
    extends ConsumerState<ForgotPasswordScreen> {
  final _formKey = GlobalKey<FormState>();
  final _emailController = TextEditingController();
  bool _isLoading = false;
  bool _isSuccess = false;

  @override
  void dispose() {
    _emailController.dispose();
    super.dispose();
  }

  Future<void> _handleResetPassword() async {
    // TODO: Implement password reset logic
    // 1. Validate form
    // 2. Set loading state
    // 3. Call auth service
    // 4. Handle success/error
    throw UnimplementedError();
  }

  @override
  Widget build(BuildContext context) {
    // TODO: Build the forgot password UI
    // 1. Show success state if _isSuccess is true
    // 2. Otherwise show form with email field
    // 3. Include loading state handling
    throw UnimplementedError();
  }
}