import 'package:flutter/material.dart';
import 'package:flutter_hooks/flutter_hooks.dart';

void main() {
  runApp(const MaterialApp(home: SignupFormScreen()));
}

class SignupFormScreen extends HookWidget {
  const SignupFormScreen({super.key});

  @override
  Widget build(BuildContext context) {
    // TODO 1: Create text controllers using useTextEditingController
    // final usernameController = useTextEditingController();
    // final emailController = useTextEditingController();
    // final passwordController = useTextEditingController();

    // TODO 2: Create state for tracking form submission
    // final hasSubmitted = useState(false);
    // final showSuccess = useState(false);

    // TODO 3: Create validation functions
    // Use useMemoized to cache validation results

    // TODO 4: Create submit handler
    // void handleSubmit() { ... }

    return Scaffold(
      appBar: AppBar(title: const Text('Sign Up')),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            // TODO 5: Build username field with validation
            // TextField(
            //   controller: usernameController,
            //   decoration: InputDecoration(
            //     labelText: 'Username',
            //     errorText: ...,
            //   ),
            // ),

            // TODO 6: Build email field with validation

            // TODO 7: Build password field with validation

            // TODO 8: Build submit button

            // TODO 9: Show success message when submitted successfully
          ],
        ),
      ),
    );
  }
}