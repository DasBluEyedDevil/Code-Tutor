---
type: "EXAMPLE"
title: "Hooks + Riverpod Together"
---

The `hooks_riverpod` package provides **HookConsumerWidget** which combines the power of both:
- **Hooks** for local UI state (controllers, animations, focus)
- **Riverpod** for shared/global state (user data, app settings, API data)

### When to Use Each

| Use Hooks For | Use Riverpod For |
|--------------|------------------|
| TextEditingController | User authentication state |
| FocusNode | Shopping cart contents |
| AnimationController | API data (products, users) |
| Local form validation | App settings/preferences |
| Scroll position | Cached network responses |

### HookConsumerWidget

Extend `HookConsumerWidget` instead of `ConsumerWidget` or `HookWidget`:

```dart
class ProfileEditor extends HookConsumerWidget {
  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // Use Riverpod for shared state
    final user = ref.watch(currentUserProvider);

    // Use hooks for local UI state
    final nameController = useTextEditingController(text: user.name);
    final emailController = useTextEditingController(text: user.email);
    final isEditing = useState(false);

    // Both work together seamlessly!
    return Column(
      children: [
        TextField(
          controller: nameController,
          enabled: isEditing.value,
        ),
        ElevatedButton(
          onPressed: () async {
            // Read from hook, write to Riverpod
            await ref.read(currentUserProvider.notifier).updateProfile(
              name: nameController.text,
              email: emailController.text,
            );
            isEditing.value = false;
          },
          child: Text('Save'),
        ),
      ],
    );
  }
}
```

### Real-World Example: Contact Form

This example shows a complete contact form using hooks for form management and Riverpod for submission:

```dart
// Complete example: Contact form with Hooks + Riverpod

import 'package:flutter/material.dart';
import 'package:flutter_hooks/flutter_hooks.dart';
import 'package:hooks_riverpod/hooks_riverpod.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

part 'contact_form.g.dart';

// =====================================================
// RIVERPOD: Handles form submission (shared/async state)
// =====================================================

@riverpod
class ContactFormSubmission extends _$ContactFormSubmission {
  @override
  AsyncValue<String?> build() => const AsyncData(null);

  Future<void> submit({
    required String name,
    required String email,
    required String message,
  }) async {
    state = const AsyncLoading();

    state = await AsyncValue.guard(() async {
      // Simulate API call
      await Future.delayed(const Duration(seconds: 2));

      // Validate on server (could throw)
      if (message.length < 10) {
        throw Exception('Message must be at least 10 characters');
      }

      // Return success message
      return 'Thank you, $name! We will contact you at $email.';
    });
  }

  void reset() {
    state = const AsyncData(null);
  }
}

// =====================================================
// HOOKS + RIVERPOD: The UI Widget
// =====================================================

class ContactFormScreen extends HookConsumerWidget {
  const ContactFormScreen({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    // ===== HOOKS: Local UI state =====

    // Text controllers (auto-disposed)
    final nameController = useTextEditingController();
    final emailController = useTextEditingController();
    final messageController = useTextEditingController();

    // Focus nodes (auto-disposed)
    final nameFocus = useFocusNode();
    final emailFocus = useFocusNode();
    final messageFocus = useFocusNode();

    // Local validation state
    final isNameValid = useState(true);
    final isEmailValid = useState(true);
    final isMessageValid = useState(true);

    // Auto-focus name field on mount
    useEffect(() {
      nameFocus.requestFocus();
      return null;
    }, []);

    // ===== RIVERPOD: Submission state =====
    final submissionState = ref.watch(contactFormSubmissionProvider);

    // ===== VALIDATION FUNCTIONS =====

    bool validateForm() {
      final nameOk = nameController.text.trim().isNotEmpty;
      final emailOk = emailController.text.contains('@');
      final messageOk = messageController.text.trim().length >= 10;

      isNameValid.value = nameOk;
      isEmailValid.value = emailOk;
      isMessageValid.value = messageOk;

      return nameOk && emailOk && messageOk;
    }

    void handleSubmit() {
      if (!validateForm()) return;

      ref.read(contactFormSubmissionProvider.notifier).submit(
        name: nameController.text.trim(),
        email: emailController.text.trim(),
        message: messageController.text.trim(),
      );
    }

    void handleReset() {
      nameController.clear();
      emailController.clear();
      messageController.clear();
      isNameValid.value = true;
      isEmailValid.value = true;
      isMessageValid.value = true;
      ref.read(contactFormSubmissionProvider.notifier).reset();
      nameFocus.requestFocus();
    }

    // ===== BUILD UI =====

    return Scaffold(
      appBar: AppBar(title: const Text('Contact Us')),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            // Success message from Riverpod
            if (submissionState.hasValue && submissionState.value != null)
              Card(
                color: Colors.green.shade50,
                child: Padding(
                  padding: const EdgeInsets.all(16),
                  child: Column(
                    children: [
                      const Icon(Icons.check_circle, color: Colors.green, size: 48),
                      const SizedBox(height: 8),
                      Text(
                        submissionState.value!,
                        textAlign: TextAlign.center,
                        style: const TextStyle(color: Colors.green),
                      ),
                      const SizedBox(height: 16),
                      ElevatedButton(
                        onPressed: handleReset,
                        child: const Text('Send Another Message'),
                      ),
                    ],
                  ),
                ),
              )
            else ...[
              // Name field
              TextField(
                controller: nameController,
                focusNode: nameFocus,
                decoration: InputDecoration(
                  labelText: 'Name',
                  errorText: isNameValid.value ? null : 'Name is required',
                  prefixIcon: const Icon(Icons.person),
                ),
                textInputAction: TextInputAction.next,
                onSubmitted: (_) => emailFocus.requestFocus(),
                onChanged: (_) => isNameValid.value = true,
              ),
              const SizedBox(height: 16),

              // Email field
              TextField(
                controller: emailController,
                focusNode: emailFocus,
                decoration: InputDecoration(
                  labelText: 'Email',
                  errorText: isEmailValid.value ? null : 'Valid email required',
                  prefixIcon: const Icon(Icons.email),
                ),
                keyboardType: TextInputType.emailAddress,
                textInputAction: TextInputAction.next,
                onSubmitted: (_) => messageFocus.requestFocus(),
                onChanged: (_) => isEmailValid.value = true,
              ),
              const SizedBox(height: 16),

              // Message field
              TextField(
                controller: messageController,
                focusNode: messageFocus,
                decoration: InputDecoration(
                  labelText: 'Message',
                  errorText: isMessageValid.value ? null : 'At least 10 characters',
                  prefixIcon: const Icon(Icons.message),
                  alignLabelWithHint: true,
                ),
                maxLines: 5,
                onChanged: (_) => isMessageValid.value = true,
              ),
              const SizedBox(height: 24),

              // Error message from Riverpod
              if (submissionState.hasError)
                Padding(
                  padding: const EdgeInsets.only(bottom: 16),
                  child: Text(
                    'Error: ${submissionState.error}',
                    style: TextStyle(color: Theme.of(context).colorScheme.error),
                  ),
                ),

              // Submit button
              ElevatedButton(
                onPressed: submissionState.isLoading ? null : handleSubmit,
                child: submissionState.isLoading
                    ? const SizedBox(
                        height: 20,
                        width: 20,
                        child: CircularProgressIndicator(strokeWidth: 2),
                      )
                    : const Text('Send Message'),
              ),
            ],
          ],
        ),
      ),
    );
  }
}

// =====================================================
// KEY POINTS:
// =====================================================
//
// HOOKS handle:
// - TextEditingController (nameController, emailController, messageController)
// - FocusNode (nameFocus, emailFocus, messageFocus)
// - Local validation state (isNameValid, isEmailValid, isMessageValid)
// - Side effects (auto-focus on mount)
//
// RIVERPOD handles:
// - Form submission state (loading, error, success)
// - Async operation (API call simulation)
// - State that needs to be accessible outside this widget
//
// This separation keeps code clean:
// - UI concerns stay in hooks (easy to read, auto-cleanup)
// - Business logic stays in Riverpod (testable, shareable)
```
