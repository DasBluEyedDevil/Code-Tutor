---
type: "EXAMPLE"
title: "Form Validation: Email and Password Requirements"
---

Form validation is critical for both user experience and security. We validate on the client side for immediate feedback and rely on server-side validation for security. Let us implement comprehensive validation.

**Email Validation**

Add this validation method to your registration screen:

```dart
import 'package:email_validator/email_validator.dart';

/// Validates email format using RFC 5322 compliant validator.
String? _validateEmail(String? value) {
  if (value == null || value.isEmpty) {
    return 'Please enter your email address';
  }
  
  // Trim whitespace (though our input formatter prevents spaces)
  final email = value.trim().toLowerCase();
  
  // Use the email_validator package for RFC 5322 compliance
  if (!EmailValidator.validate(email)) {
    return 'Please enter a valid email address';
  }
  
  // Additional checks for common issues
  if (email.length > 254) {
    return 'Email address is too long';
  }
  
  // Check for commonly mistyped domains
  final commonTypos = {
    'gmial.com': 'gmail.com',
    'gmal.com': 'gmail.com',
    'gamil.com': 'gmail.com',
    'hotmal.com': 'hotmail.com',
    'yahooo.com': 'yahoo.com',
  };
  
  final domain = email.split('@').last;
  if (commonTypos.containsKey(domain)) {
    return 'Did you mean ${email.replaceAll(domain, commonTypos[domain]!)}?';
  }
  
  return null;
}
```

**Password Strength Validation**

Create a robust password validator that checks multiple criteria:

```dart
/// Validates password strength with multiple criteria.
String? _validatePassword(String? value) {
  if (value == null || value.isEmpty) {
    return 'Please enter a password';
  }
  
  // Minimum length check
  if (value.length < 8) {
    return 'Password must be at least 8 characters';
  }
  
  // Maximum length check (prevent DoS attacks on hashing)
  if (value.length > 128) {
    return 'Password cannot exceed 128 characters';
  }
  
  // Check for required character types
  final hasUppercase = value.contains(RegExp(r'[A-Z]'));
  final hasLowercase = value.contains(RegExp(r'[a-z]'));
  final hasDigit = value.contains(RegExp(r'[0-9]'));
  final hasSpecialChar = value.contains(RegExp(r'[!@#$%^&*(),.?":{}|<>]'));
  
  final missingRequirements = <String>[];
  
  if (!hasUppercase) missingRequirements.add('uppercase letter');
  if (!hasLowercase) missingRequirements.add('lowercase letter');
  if (!hasDigit) missingRequirements.add('number');
  if (!hasSpecialChar) missingRequirements.add('special character');
  
  if (missingRequirements.isNotEmpty) {
    if (missingRequirements.length == 1) {
      return 'Password needs a ${missingRequirements.first}';
    } else {
      final last = missingRequirements.removeLast();
      return 'Password needs a ${missingRequirements.join(", ")} and $last';
    }
  }
  
  // Check for common weak passwords
  final commonPasswords = [
    'password', 'password1', '12345678', 'qwerty123',
    'letmein', 'welcome', 'admin123', 'abc12345',
  ];
  
  if (commonPasswords.contains(value.toLowerCase())) {
    return 'This password is too common. Please choose a stronger one.';
  }
  
  // Check if password contains email (if email is filled)
  if (_emailController.text.isNotEmpty) {
    final emailPrefix = _emailController.text.split('@').first.toLowerCase();
    if (emailPrefix.length > 3 && value.toLowerCase().contains(emailPrefix)) {
      return 'Password should not contain your email';
    }
  }
  
  return null;
}
```

**Password Strength Indicator Widget**

Create `lib/widgets/auth/password_strength_indicator.dart` to show visual feedback:

```dart
import 'package:flutter/material.dart';

/// Visual indicator showing password strength with colored bars.
class PasswordStrengthIndicator extends StatelessWidget {
  final String password;
  
  const PasswordStrengthIndicator({
    super.key,
    required this.password,
  });
  
  @override
  Widget build(BuildContext context) {
    final strength = _calculateStrength(password);
    
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Row(
          children: List.generate(4, (index) {
            return Expanded(
              child: Container(
                height: 4,
                margin: EdgeInsets.only(right: index < 3 ? 4 : 0),
                decoration: BoxDecoration(
                  color: index < strength.level
                      ? strength.color
                      : Colors.grey.shade300,
                  borderRadius: BorderRadius.circular(2),
                ),
              ),
            );
          }),
        ),
        if (password.isNotEmpty) ...[
          const SizedBox(height: 4),
          Text(
            strength.label,
            style: TextStyle(
              fontSize: 12,
              color: strength.color,
              fontWeight: FontWeight.w500,
            ),
          ),
        ],
      ],
    );
  }
  
  _PasswordStrength _calculateStrength(String password) {
    if (password.isEmpty) {
      return _PasswordStrength(0, '', Colors.grey);
    }
    
    int score = 0;
    
    // Length scoring
    if (password.length >= 8) score++;
    if (password.length >= 12) score++;
    if (password.length >= 16) score++;
    
    // Character variety scoring
    if (password.contains(RegExp(r'[a-z]'))) score++;
    if (password.contains(RegExp(r'[A-Z]'))) score++;
    if (password.contains(RegExp(r'[0-9]'))) score++;
    if (password.contains(RegExp(r'[!@#$%^&*(),.?":{}|<>]'))) score++;
    
    // Determine strength level
    if (score <= 2) {
      return _PasswordStrength(1, 'Weak', Colors.red);
    } else if (score <= 4) {
      return _PasswordStrength(2, 'Fair', Colors.orange);
    } else if (score <= 6) {
      return _PasswordStrength(3, 'Good', Colors.lightGreen);
    } else {
      return _PasswordStrength(4, 'Strong', Colors.green);
    }
  }
}

class _PasswordStrength {
  final int level;
  final String label;
  final Color color;
  
  _PasswordStrength(this.level, this.label, this.color);
}
```

**Real-Time Validation**

To show validation errors as the user types (not just on submit), update the form fields to validate on change:

```dart
// In your form field, add these properties:
TextFormField(
  // ... other properties
  autovalidateMode: AutovalidateMode.onUserInteraction,
  // This shows errors after the user has interacted with the field
)
```

This validation approach provides immediate feedback while being non-intrusive - errors only appear after the user has started typing in a field.

