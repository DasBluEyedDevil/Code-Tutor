// lib/features/settings/presentation/screens/delete_account_screen.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';

class DeleteAccountScreen extends ConsumerStatefulWidget {
  const DeleteAccountScreen({super.key});

  @override
  ConsumerState<DeleteAccountScreen> createState() =>
      _DeleteAccountScreenState();
}

class _DeleteAccountScreenState extends ConsumerState<DeleteAccountScreen> {
  final _passwordController = TextEditingController();
  final _confirmController = TextEditingController();
  bool _isExporting = false;
  bool _isDeleting = false;
  int _currentStep = 0;

  @override
  Widget build(BuildContext context) {
    // TODO: Implement account deletion flow
    // 1. Step 1: Warning about deletion consequences
    // 2. Step 2: Optional data export
    // 3. Step 3: Password re-authentication
    // 4. Step 4: Type 'DELETE' confirmation
    // 5. Final deletion with loading state
    throw UnimplementedError();
  }

  Widget _buildStep1Warning() {
    // TODO: Build warning step explaining consequences
    throw UnimplementedError();
  }

  Widget _buildStep2DataExport() {
    // TODO: Build data export option step
    throw UnimplementedError();
  }

  Widget _buildStep3ReAuth() {
    // TODO: Build password re-entry step
    throw UnimplementedError();
  }

  Widget _buildStep4Confirmation() {
    // TODO: Build type 'DELETE' confirmation step
    throw UnimplementedError();
  }

  Future<void> _exportData() async {
    // TODO: Request data export
    throw UnimplementedError();
  }

  Future<void> _verifyPassword() async {
    // TODO: Re-authenticate user
    throw UnimplementedError();
  }

  Future<void> _deleteAccount() async {
    // TODO: Delete account and clear local data
    throw UnimplementedError();
  }
}