---
type: "EXAMPLE"
title: "Building Accessible Forms"
---


Complete accessible form implementation:



```dart
import 'package:flutter/material.dart';
import 'package:flutter/semantics.dart';

/// Accessible text field with proper labeling and error handling
class AccessibleTextField extends StatefulWidget {
  final TextEditingController controller;
  final String label;
  final String? hint;
  final String? errorText;
  final bool isRequired;
  final TextInputType? keyboardType;
  final bool obscureText;
  final String? Function(String?)? validator;
  final void Function(String)? onChanged;
  final FocusNode? focusNode;

  const AccessibleTextField({
    super.key,
    required this.controller,
    required this.label,
    this.hint,
    this.errorText,
    this.isRequired = false,
    this.keyboardType,
    this.obscureText = false,
    this.validator,
    this.onChanged,
    this.focusNode,
  });

  @override
  State<AccessibleTextField> createState() => _AccessibleTextFieldState();
}

class _AccessibleTextFieldState extends State<AccessibleTextField> {
  late FocusNode _focusNode;
  bool _isFocused = false;
  String? _previousError;

  @override
  void initState() {
    super.initState();
    _focusNode = widget.focusNode ?? FocusNode();
    _focusNode.addListener(_onFocusChange);
  }

  @override
  void dispose() {
    if (widget.focusNode == null) {
      _focusNode.dispose();
    }
    super.dispose();
  }

  void _onFocusChange() {
    setState(() => _isFocused = _focusNode.hasFocus);
  }

  @override
  void didUpdateWidget(AccessibleTextField oldWidget) {
    super.didUpdateWidget(oldWidget);
    // Announce new errors to screen readers
    if (widget.errorText != null && widget.errorText != _previousError) {
      SemanticsService.announce(
        'Error: ${widget.errorText}',
        TextDirection.ltr,
      );
    }
    _previousError = widget.errorText;
  }

  @override
  Widget build(BuildContext context) {
    final theme = Theme.of(context);
    final hasError = widget.errorText != null;
    final requiredIndicator = widget.isRequired ? ' *' : '';

    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      mainAxisSize: MainAxisSize.min,
      children: [
        // Visible label (not just placeholder)
        Semantics(
          label: '${widget.label}${widget.isRequired ? ', required' : ''}',
          child: Text(
            '${widget.label}$requiredIndicator',
            style: theme.textTheme.labelLarge?.copyWith(
              color: hasError
                  ? theme.colorScheme.error
                  : _isFocused
                      ? theme.colorScheme.primary
                      : null,
            ),
          ),
        ),
        const SizedBox(height: 8),
        // Text field
        TextFormField(
          controller: widget.controller,
          focusNode: _focusNode,
          keyboardType: widget.keyboardType,
          obscureText: widget.obscureText,
          validator: widget.validator,
          onChanged: widget.onChanged,
          decoration: InputDecoration(
            hintText: widget.hint,
            border: const OutlineInputBorder(),
            enabledBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: hasError
                    ? theme.colorScheme.error
                    : theme.colorScheme.outline,
              ),
            ),
            focusedBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: hasError
                    ? theme.colorScheme.error
                    : theme.colorScheme.primary,
                width: 2,
              ),
            ),
            errorBorder: OutlineInputBorder(
              borderSide: BorderSide(
                color: theme.colorScheme.error,
                width: 2,
              ),
            ),
            // Icon for error state (not color alone)
            suffixIcon: hasError
                ? Icon(
                    Icons.error,
                    color: theme.colorScheme.error,
                    semanticLabel: 'Error',
                  )
                : null,
          ),
        ),
        // Error message
        if (hasError) ...[
          const SizedBox(height: 8),
          Row(
            children: [
              Icon(
                Icons.error_outline,
                size: 16,
                color: theme.colorScheme.error,
              ),
              const SizedBox(width: 8),
              Expanded(
                child: Text(
                  widget.errorText!,
                  style: theme.textTheme.bodySmall?.copyWith(
                    color: theme.colorScheme.error,
                  ),
                ),
              ),
            ],
          ),
        ],
      ],
    );
  }
}

/// Complete accessible form example
class AccessibleFormExample extends StatefulWidget {
  const AccessibleFormExample({super.key});

  @override
  State<AccessibleFormExample> createState() => _AccessibleFormExampleState();
}

class _AccessibleFormExampleState extends State<AccessibleFormExample> {
  final _formKey = GlobalKey<FormState>();
  final _nameController = TextEditingController();
  final _emailController = TextEditingController();
  final _passwordController = TextEditingController();
  
  final _nameFocus = FocusNode();
  final _emailFocus = FocusNode();
  final _passwordFocus = FocusNode();

  String? _nameError;
  String? _emailError;
  String? _passwordError;
  bool _isSubmitting = false;

  @override
  void dispose() {
    _nameController.dispose();
    _emailController.dispose();
    _passwordController.dispose();
    _nameFocus.dispose();
    _emailFocus.dispose();
    _passwordFocus.dispose();
    super.dispose();
  }

  void _validateAndSubmit() {
    setState(() {
      _nameError = _validateName(_nameController.text);
      _emailError = _validateEmail(_emailController.text);
      _passwordError = _validatePassword(_passwordController.text);
    });

    // Focus first field with error
    if (_nameError != null) {
      _nameFocus.requestFocus();
      _announceErrors();
      return;
    } else if (_emailError != null) {
      _emailFocus.requestFocus();
      _announceErrors();
      return;
    } else if (_passwordError != null) {
      _passwordFocus.requestFocus();
      _announceErrors();
      return;
    }

    // All valid - submit
    _submitForm();
  }

  void _announceErrors() {
    final errors = <String>[];
    if (_nameError != null) errors.add('Name: $_nameError');
    if (_emailError != null) errors.add('Email: $_emailError');
    if (_passwordError != null) errors.add('Password: $_passwordError');

    if (errors.isNotEmpty) {
      SemanticsService.announce(
        'Form has ${errors.length} error${errors.length > 1 ? 's' : ''}. ${errors.first}',
        TextDirection.ltr,
      );
    }
  }

  String? _validateName(String? value) {
    if (value == null || value.trim().isEmpty) {
      return 'Name is required';
    }
    if (value.length < 2) {
      return 'Name must be at least 2 characters';
    }
    return null;
  }

  String? _validateEmail(String? value) {
    if (value == null || value.trim().isEmpty) {
      return 'Email is required';
    }
    final emailRegex = RegExp(r'^[^@]+@[^@]+\.[^@]+');
    if (!emailRegex.hasMatch(value)) {
      return 'Please enter a valid email address';
    }
    return null;
  }

  String? _validatePassword(String? value) {
    if (value == null || value.isEmpty) {
      return 'Password is required';
    }
    if (value.length < 8) {
      return 'Password must be at least 8 characters';
    }
    return null;
  }

  Future<void> _submitForm() async {
    setState(() => _isSubmitting = true);
    
    SemanticsService.announce('Submitting form', TextDirection.ltr);
    
    // Simulate API call
    await Future.delayed(const Duration(seconds: 2));
    
    setState(() => _isSubmitting = false);
    
    SemanticsService.announce(
      'Account created successfully',
      TextDirection.ltr,
    );
    
    if (mounted) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(
          content: Text('Account created successfully!'),
        ),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Create Account'),
      ),
      body: SingleChildScrollView(
        padding: const EdgeInsets.all(16),
        child: Form(
          key: _formKey,
          child: FocusTraversalGroup(
            policy: OrderedTraversalPolicy(),
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                // Form description
                Semantics(
                  header: true,
                  child: Text(
                    'Enter your details to create an account',
                    style: Theme.of(context).textTheme.bodyLarge,
                  ),
                ),
                const SizedBox(height: 24),
                
                // Name field
                FocusTraversalOrder(
                  order: const NumericFocusOrder(1),
                  child: AccessibleTextField(
                    controller: _nameController,
                    focusNode: _nameFocus,
                    label: 'Full Name',
                    hint: 'Enter your full name',
                    isRequired: true,
                    errorText: _nameError,
                    onChanged: (_) {
                      if (_nameError != null) {
                        setState(() => _nameError = null);
                      }
                    },
                  ),
                ),
                const SizedBox(height: 16),
                
                // Email field
                FocusTraversalOrder(
                  order: const NumericFocusOrder(2),
                  child: AccessibleTextField(
                    controller: _emailController,
                    focusNode: _emailFocus,
                    label: 'Email Address',
                    hint: 'you@example.com',
                    isRequired: true,
                    keyboardType: TextInputType.emailAddress,
                    errorText: _emailError,
                    onChanged: (_) {
                      if (_emailError != null) {
                        setState(() => _emailError = null);
                      }
                    },
                  ),
                ),
                const SizedBox(height: 16),
                
                // Password field
                FocusTraversalOrder(
                  order: const NumericFocusOrder(3),
                  child: AccessibleTextField(
                    controller: _passwordController,
                    focusNode: _passwordFocus,
                    label: 'Password',
                    hint: 'Minimum 8 characters',
                    isRequired: true,
                    obscureText: true,
                    errorText: _passwordError,
                    onChanged: (_) {
                      if (_passwordError != null) {
                        setState(() => _passwordError = null);
                      }
                    },
                  ),
                ),
                const SizedBox(height: 32),
                
                // Submit button
                FocusTraversalOrder(
                  order: const NumericFocusOrder(4),
                  child: ElevatedButton(
                    onPressed: _isSubmitting ? null : _validateAndSubmit,
                    style: ElevatedButton.styleFrom(
                      minimumSize: const Size(double.infinity, 48),
                    ),
                    child: _isSubmitting
                        ? const SizedBox(
                            width: 24,
                            height: 24,
                            child: CircularProgressIndicator(
                              strokeWidth: 2,
                            ),
                          )
                        : const Text('Create Account'),
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
```
