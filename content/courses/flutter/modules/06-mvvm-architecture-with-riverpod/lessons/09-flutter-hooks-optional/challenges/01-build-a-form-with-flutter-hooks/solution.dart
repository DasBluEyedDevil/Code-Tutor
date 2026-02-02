import 'package:flutter/material.dart';
import 'package:flutter_hooks/flutter_hooks.dart';

void main() {
  runApp(const MaterialApp(home: SignupFormScreen()));
}

class SignupFormScreen extends HookWidget {
  const SignupFormScreen({super.key});

  @override
  Widget build(BuildContext context) {
    // ===== HOOKS: All at top of build() =====

    // Text controllers (auto-disposed)
    final usernameController = useTextEditingController();
    final emailController = useTextEditingController();
    final passwordController = useTextEditingController();

    // Form state
    final hasSubmitted = useState(false);
    final showSuccess = useState(false);

    // Force rebuild when text changes (for button enable state)
    useListenable(usernameController);
    useListenable(emailController);
    useListenable(passwordController);

    // ===== VALIDATION (computed from current values) =====

    final username = usernameController.text;
    final email = emailController.text;
    final password = passwordController.text;

    // Validation checks
    final isUsernameValid = username.length >= 3;
    final isEmailValid = email.contains('@');
    final isPasswordValid = password.length >= 8;
    final isFormValid = isUsernameValid && isEmailValid && isPasswordValid;

    // Error messages (only show after first submit attempt)
    String? usernameError;
    String? emailError;
    String? passwordError;

    if (hasSubmitted.value) {
      if (!isUsernameValid) usernameError = 'Username must be at least 3 characters';
      if (!isEmailValid) emailError = 'Please enter a valid email address';
      if (!isPasswordValid) passwordError = 'Password must be at least 8 characters';
    }

    // Check if all fields have content (for button enable)
    final allFieldsFilled = username.isNotEmpty && email.isNotEmpty && password.isNotEmpty;

    // ===== SUBMIT HANDLER =====

    void handleSubmit() {
      hasSubmitted.value = true;

      if (isFormValid) {
        showSuccess.value = true;
      }
    }

    void handleReset() {
      usernameController.clear();
      emailController.clear();
      passwordController.clear();
      hasSubmitted.value = false;
      showSuccess.value = false;
    }

    // ===== BUILD UI =====

    return Scaffold(
      appBar: AppBar(title: const Text('Sign Up')),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            // Success message
            if (showSuccess.value)
              Card(
                color: Colors.green.shade50,
                child: Padding(
                  padding: const EdgeInsets.all(16),
                  child: Column(
                    children: [
                      const Icon(Icons.check_circle, color: Colors.green, size: 48),
                      const SizedBox(height: 8),
                      Text(
                        'Welcome, $username!',
                        style: Theme.of(context).textTheme.titleLarge,
                      ),
                      const SizedBox(height: 4),
                      const Text('Your account has been created.'),
                      const SizedBox(height: 16),
                      ElevatedButton(
                        onPressed: handleReset,
                        child: const Text('Sign Up Another User'),
                      ),
                    ],
                  ),
                ),
              )
            else ...[
              // Username field
              TextField(
                controller: usernameController,
                decoration: InputDecoration(
                  labelText: 'Username',
                  hintText: 'At least 3 characters',
                  prefixIcon: const Icon(Icons.person),
                  errorText: usernameError,
                  border: const OutlineInputBorder(),
                ),
                textInputAction: TextInputAction.next,
              ),
              const SizedBox(height: 16),

              // Email field
              TextField(
                controller: emailController,
                decoration: InputDecoration(
                  labelText: 'Email',
                  hintText: 'example@email.com',
                  prefixIcon: const Icon(Icons.email),
                  errorText: emailError,
                  border: const OutlineInputBorder(),
                ),
                keyboardType: TextInputType.emailAddress,
                textInputAction: TextInputAction.next,
              ),
              const SizedBox(height: 16),

              // Password field
              TextField(
                controller: passwordController,
                decoration: InputDecoration(
                  labelText: 'Password',
                  hintText: 'At least 8 characters',
                  prefixIcon: const Icon(Icons.lock),
                  errorText: passwordError,
                  border: const OutlineInputBorder(),
                ),
                obscureText: true,
                textInputAction: TextInputAction.done,
                onSubmitted: (_) => handleSubmit(),
              ),
              const SizedBox(height: 24),

              // Submit button
              ElevatedButton(
                onPressed: allFieldsFilled ? handleSubmit : null,
                style: ElevatedButton.styleFrom(
                  padding: const EdgeInsets.symmetric(vertical: 16),
                ),
                child: const Text('Create Account'),
              ),

              // Validation hints
              const SizedBox(height: 24),
              Text(
                'Requirements:',
                style: Theme.of(context).textTheme.titleSmall,
              ),
              const SizedBox(height: 8),
              _RequirementRow(
                text: 'Username: 3+ characters',
                isMet: isUsernameValid,
              ),
              _RequirementRow(
                text: 'Email: valid format',
                isMet: isEmailValid,
              ),
              _RequirementRow(
                text: 'Password: 8+ characters',
                isMet: isPasswordValid,
              ),
            ],
          ],
        ),
      ),
    );
  }
}

class _RequirementRow extends StatelessWidget {
  final String text;
  final bool isMet;

  const _RequirementRow({required this.text, required this.isMet});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.symmetric(vertical: 4),
      child: Row(
        children: [
          Icon(
            isMet ? Icons.check_circle : Icons.circle_outlined,
            color: isMet ? Colors.green : Colors.grey,
            size: 20,
          ),
          const SizedBox(width: 8),
          Text(
            text,
            style: TextStyle(
              color: isMet ? Colors.green : Colors.grey.shade600,
              decoration: isMet ? TextDecoration.lineThrough : null,
            ),
          ),
        ],
      ),
    );
  }
}

// =====================================================
// KEY LEARNING POINTS:
// =====================================================
//
// 1. All hooks called at top of build() - never conditionally
//
// 2. useTextEditingController() creates auto-disposed controllers
//
// 3. useState() for local state that triggers rebuilds
//
// 4. useListenable() to rebuild when controller text changes
//    (needed for the submit button enabled state)
//
// 5. Validation logic is just regular Dart - no hooks needed
//    (hooks are for state that needs Flutter lifecycle)
//
// 6. Error messages only shown after hasSubmitted is true
//    (better UX than showing errors immediately)
//
// 7. Button disabled when fields empty (allFieldsFilled check)