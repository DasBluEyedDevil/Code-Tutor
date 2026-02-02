---
type: "EXAMPLE"
title: "Building the Login Form UI"
---

The login form is simpler than registration but includes additional features like "Remember Me" and "Forgot Password". Let us build a polished, accessible login screen.

**Create the Login Screen**

Create `lib/screens/auth/login_screen.dart`:

```dart
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:email_validator/email_validator.dart';
import '../../providers/auth_provider.dart';
import '../../exceptions/auth_exceptions.dart';
import '../../utils/error_handler.dart';
import '../../widgets/error_display.dart';

/// Login screen with email/password authentication, remember me, and forgot password.
class LoginScreen extends ConsumerStatefulWidget {
  /// Optional pre-filled email (e.g., from registration redirect)
  final String? initialEmail;
  
  const LoginScreen({super.key, this.initialEmail});

  @override
  ConsumerState<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends ConsumerState<LoginScreen> {
  // Form key for validation
  final _formKey = GlobalKey<FormState>();
  
  // Controllers for text fields
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  
  // Focus nodes for field navigation
  final _emailFocusNode = FocusNode();
  final _passwordFocusNode = FocusNode();
  
  // UI state
  bool _obscurePassword = true;
  bool _isLoading = false;
  bool _rememberMe = true; // Default to remembering the user
  AuthException? _error;
  
  @override
  void initState() {
    super.initState();
    // Pre-fill email if provided (e.g., from registration)
    if (widget.initialEmail != null) {
      _emailController.text = widget.initialEmail!;
    }
    // Check for saved email from previous "remember me"
    _loadSavedEmail();
  }
  
  Future<void> _loadSavedEmail() async {
    final secureStorage = ref.read(secureStorageProvider);
    final savedEmail = await secureStorage.getUserEmail();
    if (savedEmail != null && _emailController.text.isEmpty) {
      setState(() {
        _emailController.text = savedEmail;
      });
    }
  }
  
  @override
  void dispose() {
    _emailController.dispose();
    _passwordController.dispose();
    _emailFocusNode.dispose();
    _passwordFocusNode.dispose();
    super.dispose();
  }
  
  String? _validateEmail(String? value) {
    if (value == null || value.isEmpty) {
      return 'Please enter your email address';
    }
    final email = value.trim().toLowerCase();
    if (!EmailValidator.validate(email)) {
      return 'Please enter a valid email address';
    }
    return null;
  }
  
  String? _validatePassword(String? value) {
    if (value == null || value.isEmpty) {
      return 'Please enter your password';
    }
    // No strength requirements for login - just check it exists
    return null;
  }
  
  Future<void> _handleLogin() async {
    // Clear previous error
    setState(() => _error = null);
    
    // Validate form
    if (!_formKey.currentState!.validate()) {
      return;
    }
    
    // Start loading
    setState(() => _isLoading = true);
    
    try {
      final authService = ref.read(authServiceProvider);
      
      // Attempt login
      final result = await authService.signInWithEmail(
        email: _emailController.text.trim(),
        password: _passwordController.text,
        rememberMe: _rememberMe,
      );
      
      if (!mounted) return;
      
      if (result.success) {
        // Login successful - AuthNotifier will handle navigation
        // The auth state listener will redirect to home
        ref.read(authStateProvider.notifier).setAuthenticated(result.user!);
      } else {
        // Login failed
        setState(() {
          _error = ErrorHandler.handleError(Exception(result.errorMessage));
          _isLoading = false;
        });
        
        // Clear password field on failure for security
        _passwordController.clear();
      }
    } catch (e) {
      if (!mounted) return;
      setState(() {
        _error = ErrorHandler.handleError(e);
        _isLoading = false;
      });
      _passwordController.clear();
    }
  }
  
  void _navigateToForgotPassword() {
    Navigator.of(context).pushNamed(
      '/forgot-password',
      arguments: _emailController.text.trim(),
    );
  }
  
  void _navigateToRegistration() {
    Navigator.of(context).pushReplacementNamed('/register');
  }
  
  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    
    return Scaffold(
      body: SafeArea(
        child: SingleChildScrollView(
          padding: const EdgeInsets.all(24.0),
          child: AutofillGroup(
            child: Form(
              key: _formKey,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  const SizedBox(height: 48),
                  
                  // App logo or icon
                  Icon(
                    Icons.lock_outline,
                    size: 64,
                    color: theme.colorScheme.primary,
                  ),
                  const SizedBox(height: 24),
                  
                  // Title
                  Text(
                    'Welcome Back',
                    style: theme.textTheme.headlineMedium?.copyWith(
                      fontWeight: FontWeight.bold,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 8),
                  Text(
                    'Sign in to continue',
                    style: theme.textTheme.bodyLarge?.copyWith(
                      color: theme.colorScheme.onSurfaceVariant,
                    ),
                    textAlign: TextAlign.center,
                  ),
                  const SizedBox(height: 48),
                  
                  // Error display
                  if (_error != null) ...[
                    ErrorDisplay(
                      error: _error!,
                      onDismiss: () => setState(() => _error = null),
                      onRetry: _handleLogin,
                    ),
                    const SizedBox(height: 16),
                  ],
                  
                  // Email field
                  TextFormField(
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
                    autovalidateMode: AutovalidateMode.onUserInteraction,
                    inputFormatters: [
                      FilteringTextInputFormatter.deny(RegExp(r'\s')),
                    ],
                    onFieldSubmitted: (_) {
                      FocusScope.of(context).requestFocus(_passwordFocusNode);
                    },
                    validator: _validateEmail,
                  ),
                  const SizedBox(height: 16),
                  
                  // Password field
                  TextFormField(
                    controller: _passwordController,
                    focusNode: _passwordFocusNode,
                    decoration: InputDecoration(
                      labelText: 'Password',
                      hintText: 'Enter your password',
                      prefixIcon: const Icon(Icons.lock_outline),
                      border: const OutlineInputBorder(),
                      suffixIcon: IconButton(
                        icon: Icon(
                          _obscurePassword
                              ? Icons.visibility_off
                              : Icons.visibility,
                        ),
                        onPressed: () {
                          setState(() => _obscurePassword = !_obscurePassword);
                        },
                        tooltip: _obscurePassword
                            ? 'Show password'
                            : 'Hide password',
                      ),
                    ),
                    obscureText: _obscurePassword,
                    textInputAction: TextInputAction.done,
                    autofillHints: const [AutofillHints.password],
                    autovalidateMode: AutovalidateMode.onUserInteraction,
                    onFieldSubmitted: (_) => _handleLogin(),
                    validator: _validatePassword,
                  ),
                  const SizedBox(height: 8),
                  
                  // Remember me and Forgot password row
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      // Remember me checkbox
                      Row(
                        mainAxisSize: MainAxisSize.min,
                        children: [
                          Checkbox(
                            value: _rememberMe,
                            onChanged: (value) {
                              setState(() => _rememberMe = value ?? true);
                            },
                          ),
                          GestureDetector(
                            onTap: () {
                              setState(() => _rememberMe = !_rememberMe);
                            },
                            child: Text(
                              'Remember me',
                              style: theme.textTheme.bodyMedium,
                            ),
                          ),
                        ],
                      ),
                      // Forgot password link
                      TextButton(
                        onPressed: _navigateToForgotPassword,
                        child: const Text('Forgot password?'),
                      ),
                    ],
                  ),
                  const SizedBox(height: 24),
                  
                  // Login button
                  FilledButton(
                    onPressed: _isLoading ? null : _handleLogin,
                    style: FilledButton.styleFrom(
                      padding: const EdgeInsets.symmetric(vertical: 16),
                    ),
                    child: _isLoading
                        ? const SizedBox(
                            height: 20,
                            width: 20,
                            child: CircularProgressIndicator(
                              strokeWidth: 2,
                              valueColor:
                                  AlwaysStoppedAnimation<Color>(Colors.white),
                            ),
                          )
                        : const Text(
                            'Sign In',
                            style: TextStyle(fontSize: 16),
                          ),
                  ),
                  const SizedBox(height: 24),
                  
                  // Divider with "or" text
                  Row(
                    children: [
                      const Expanded(child: Divider()),
                      Padding(
                        padding: const EdgeInsets.symmetric(horizontal: 16),
                        child: Text(
                          'or',
                          style: theme.textTheme.bodySmall?.copyWith(
                            color: theme.colorScheme.onSurfaceVariant,
                          ),
                        ),
                      ),
                      const Expanded(child: Divider()),
                    ],
                  ),
                  const SizedBox(height: 24),
                  
                  // Social login buttons (placeholder - implement based on your needs)
                  OutlinedButton.icon(
                    onPressed: () {
                      // Implement Google Sign-In
                    },
                    icon: const Icon(Icons.g_mobiledata),
                    label: const Text('Continue with Google'),
                    style: OutlinedButton.styleFrom(
                      padding: const EdgeInsets.symmetric(vertical: 12),
                    ),
                  ),
                  const SizedBox(height: 32),
                  
                  // Registration link
                  Row(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Text(
                        "Don't have an account? ",
                        style: theme.textTheme.bodyMedium,
                      ),
                      TextButton(
                        onPressed: _navigateToRegistration,
                        child: const Text('Sign Up'),
                      ),
                    ],
                  ),
                ],
              ),
            ),
          ),
        ),
      ),
    );
  }
}
```

This login form includes all essential features: email and password fields with validation, remember me checkbox, forgot password link, loading states, error display, and navigation to registration.

