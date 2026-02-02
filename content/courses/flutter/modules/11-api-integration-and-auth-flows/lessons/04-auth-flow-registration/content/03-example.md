---
type: "EXAMPLE"
title: "Building the Registration Form UI"
---

Now let us build a polished registration form with proper text fields, visual feedback, and accessibility support. The form will have fields for email, password, and password confirmation.

**Create the Registration Screen**

Create `lib/screens/auth/registration_screen.dart`:

```dart
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../providers/auth_provider.dart';
import '../../widgets/auth/password_strength_indicator.dart';

/// Registration screen with form validation and error handling.
class RegistrationScreen extends ConsumerStatefulWidget {
  const RegistrationScreen({super.key});

  @override
  ConsumerState<RegistrationScreen> createState() => _RegistrationScreenState();
}

class _RegistrationScreenState extends ConsumerState<RegistrationScreen> {
  // Form key for validation
  final _formKey = GlobalKey<FormState>();
  
  // Controllers for text fields
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  final _confirmPasswordController = TextEditingController();
  final _nameController = TextEditingController();
  
  // Focus nodes for field navigation
  final _emailFocusNode = FocusNode();
  final _passwordFocusNode = FocusNode();
  final _confirmPasswordFocusNode = FocusNode();
  final _nameFocusNode = FocusNode();
  
  // UI state
  bool _obscurePassword = true;
  bool _obscureConfirmPassword = true;
  bool _isLoading = false;
  bool _acceptedTerms = false;
  String? _errorMessage;
  
  @override
  void dispose() {
    // Always dispose controllers and focus nodes
    _emailController.dispose();
    _passwordController.dispose();
    _confirmPasswordController.dispose();
    _nameController.dispose();
    _emailFocusNode.dispose();
    _passwordFocusNode.dispose();
    _confirmPasswordFocusNode.dispose();
    _nameFocusNode.dispose();
    super.dispose();
  }
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    
    return Scaffold(
      appBar: AppBar(
        title: const Text('Create Account'),
        centerTitle: true,
      ),
      body: SafeArea(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(24.0),
          child: Form(
            key: _formKey,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                // Header
                Text(
                  'Join Us Today',
                  style: theme.textTheme.headlineMedium?.copyWith(
                    fontWeight: FontWeight.bold,
                  ),
                  textAlign: TextAlign.center,
                ),
                const SizedBox(height: 8),
                Text(
                  'Create your account to get started',
                  style: theme.textTheme.bodyLarge?.copyWith(
                    color: theme.colorScheme.onSurfaceVariant,
                  ),
                  textAlign: TextAlign.center,
                ),
                const SizedBox(height: 32),
                
                // Error message banner
                if (_errorMessage != null) ...[
                  _buildErrorBanner(),
                  const SizedBox(height: 16),
                ],
                
                // Name field
                _buildNameField(),
                const SizedBox(height: 16),
                
                // Email field
                _buildEmailField(),
                const SizedBox(height: 16),
                
                // Password field
                _buildPasswordField(),
                const SizedBox(height: 8),
                
                // Password strength indicator
                PasswordStrengthIndicator(
                  password: _passwordController.text,
                ),
                const SizedBox(height: 16),
                
                // Confirm password field
                _buildConfirmPasswordField(),
                const SizedBox(height: 24),
                
                // Terms and conditions checkbox
                _buildTermsCheckbox(),
                const SizedBox(height: 24),
                
                // Register button
                _buildRegisterButton(),
                const SizedBox(height: 16),
                
                // Login link
                _buildLoginLink(),
              ],
            ),
          ),
        ),
      ),
    );
  }
  
  Widget _buildErrorBanner() {
    return Container(
      padding: const EdgeInsets.all(12),
      decoration: BoxDecoration(
        color: Theme.of(context).colorScheme.errorContainer,
        borderRadius: BorderRadius.circular(8),
      ),
      child: Row(
        children: [
          Icon(
            Icons.error_outline,
            color: Theme.of(context).colorScheme.error,
          ),
          const SizedBox(width: 12),
          Expanded(
            child: Text(
              _errorMessage!,
              style: TextStyle(
                color: Theme.of(context).colorScheme.onErrorContainer,
              ),
            ),
          ),
          IconButton(
            icon: const Icon(Icons.close),
            onPressed: () => setState(() => _errorMessage = null),
            color: Theme.of(context).colorScheme.onErrorContainer,
          ),
        ],
      ),
    );
  }
  
  Widget _buildNameField() {
    return TextFormField(
      controller: _nameController,
      focusNode: _nameFocusNode,
      decoration: const InputDecoration(
        labelText: 'Full Name',
        hintText: 'Enter your full name',
        prefixIcon: Icon(Icons.person_outline),
        border: OutlineInputBorder(),
      ),
      textInputAction: TextInputAction.next,
      textCapitalization: TextCapitalization.words,
      autofillHints: const [AutofillHints.name],
      onFieldSubmitted: (_) {
        FocusScope.of(context).requestFocus(_emailFocusNode);
      },
      validator: (value) {
        if (value == null || value.trim().isEmpty) {
          return 'Please enter your name';
        }
        if (value.trim().length < 2) {
          return 'Name must be at least 2 characters';
        }
        return null;
      },
    );
  }
  
  Widget _buildEmailField() {
    return TextFormField(
      controller: _emailController,
      focusNode: _emailFocusNode,
      decoration: const InputDecoration(
        labelText: 'Email Address',
        hintText: 'you@example.com',
        prefixIcon: Icon(Icons.email_outlined),
        border: OutlineInputBorder(),
      ),
      keyboardType: TextInputType.emailAddress,
      textInputAction: TextInputAction.next,
      autocorrect: false,
      autofillHints: const [AutofillHints.email],
      inputFormatters: [
        // Prevent spaces in email
        FilteringTextInputFormatter.deny(RegExp(r'\s')),
      ],
      onFieldSubmitted: (_) {
        FocusScope.of(context).requestFocus(_passwordFocusNode);
      },
      validator: _validateEmail,
    );
  }
  
  Widget _buildPasswordField() {
    return TextFormField(
      controller: _passwordController,
      focusNode: _passwordFocusNode,
      decoration: InputDecoration(
        labelText: 'Password',
        hintText: 'Create a strong password',
        prefixIcon: const Icon(Icons.lock_outline),
        border: const OutlineInputBorder(),
        suffixIcon: IconButton(
          icon: Icon(
            _obscurePassword ? Icons.visibility_off : Icons.visibility,
          ),
          onPressed: () {
            setState(() => _obscurePassword = !_obscurePassword);
          },
          tooltip: _obscurePassword ? 'Show password' : 'Hide password',
        ),
      ),
      obscureText: _obscurePassword,
      textInputAction: TextInputAction.next,
      autofillHints: const [AutofillHints.newPassword],
      onChanged: (_) {
        // Rebuild to update password strength indicator
        setState(() {});
      },
      onFieldSubmitted: (_) {
        FocusScope.of(context).requestFocus(_confirmPasswordFocusNode);
      },
      validator: _validatePassword,
    );
  }
  
  Widget _buildConfirmPasswordField() {
    return TextFormField(
      controller: _confirmPasswordController,
      focusNode: _confirmPasswordFocusNode,
      decoration: InputDecoration(
        labelText: 'Confirm Password',
        hintText: 'Re-enter your password',
        prefixIcon: const Icon(Icons.lock_outline),
        border: const OutlineInputBorder(),
        suffixIcon: IconButton(
          icon: Icon(
            _obscureConfirmPassword ? Icons.visibility_off : Icons.visibility,
          ),
          onPressed: () {
            setState(() => _obscureConfirmPassword = !_obscureConfirmPassword);
          },
          tooltip: _obscureConfirmPassword ? 'Show password' : 'Hide password',
        ),
      ),
      obscureText: _obscureConfirmPassword,
      textInputAction: TextInputAction.done,
      onFieldSubmitted: (_) => _handleRegistration(),
      validator: (value) {
        if (value == null || value.isEmpty) {
          return 'Please confirm your password';
        }
        if (value != _passwordController.text) {
          return 'Passwords do not match';
        }
        return null;
      },
    );
  }
  
  Widget _buildTermsCheckbox() {
    return Row(
      children: [
        Checkbox(
          value: _acceptedTerms,
          onChanged: (value) {
            setState(() => _acceptedTerms = value ?? false);
          },
        ),
        Expanded(
          child: GestureDetector(
            onTap: () => setState(() => _acceptedTerms = !_acceptedTerms),
            child: RichText(
              text: TextSpan(
                style: Theme.of(context).textTheme.bodyMedium,
                children: [
                  const TextSpan(text: 'I agree to the '),
                  TextSpan(
                    text: 'Terms of Service',
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.primary,
                      decoration: TextDecoration.underline,
                    ),
                  ),
                  const TextSpan(text: ' and '),
                  TextSpan(
                    text: 'Privacy Policy',
                    style: TextStyle(
                      color: Theme.of(context).colorScheme.primary,
                      decoration: TextDecoration.underline,
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      ],
    );
  }
  
  Widget _buildRegisterButton() {
    return FilledButton(
      onPressed: _isLoading ? null : _handleRegistration,
      style: FilledButton.styleFrom(
        padding: const EdgeInsets.symmetric(vertical: 16),
      ),
      child: _isLoading
          ? const SizedBox(
              height: 20,
              width: 20,
              child: CircularProgressIndicator(
                strokeWidth: 2,
                valueColor: AlwaysStoppedAnimation<Color>(Colors.white),
              ),
            )
          : const Text(
              'Create Account',
              style: TextStyle(fontSize: 16),
            ),
    );
  }
  
  Widget _buildLoginLink() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.center,
      children: [
        Text(
          'Already have an account? ',
          style: Theme.of(context).textTheme.bodyMedium,
        ),
        TextButton(
          onPressed: () {
            Navigator.of(context).pushReplacementNamed('/login');
          },
          child: const Text('Sign In'),
        ),
      ],
    );
  }
  
  // Validation and submission methods are covered in the next sections
  String? _validateEmail(String? value) {
    // Covered in Form Validation section
    return null;
  }
  
  String? _validatePassword(String? value) {
    // Covered in Form Validation section
    return null;
  }
  
  Future<void> _handleRegistration() async {
    // Covered in Connecting to Serverpod section
  }
}
```

This form UI includes all the essential elements for a professional registration experience: proper field navigation with focus nodes, password visibility toggles, loading states, and error display.

