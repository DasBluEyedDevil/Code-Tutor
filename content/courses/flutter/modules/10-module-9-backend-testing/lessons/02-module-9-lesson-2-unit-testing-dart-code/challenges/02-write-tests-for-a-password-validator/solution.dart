import 'package:test/test.dart';

// These classes would be in your lib folder
class ValidationResult {
  final bool isValid;
  final List<String> errors;
  ValidationResult(this.isValid, this.errors);
}

class PasswordValidator {
  ValidationResult validate(String password) {
    final errors = <String>[];
    
    if (password.length < 8) {
      errors.add('Password must be at least 8 characters');
    }
    if (!password.contains(RegExp(r'[A-Z]'))) {
      errors.add('Password must contain an uppercase letter');
    }
    if (!password.contains(RegExp(r'[0-9]'))) {
      errors.add('Password must contain a number');
    }
    if (!password.contains(RegExp(r'[!@#$%^&*]'))) {
      errors.add('Password must contain a special character');
    }
    
    return ValidationResult(errors.isEmpty, errors);
  }
}

void main() {
  group('PasswordValidator', () {
    late PasswordValidator validator;
    
    setUp(() {
      validator = PasswordValidator();
    });
    
    test('accepts valid password with all requirements', () {
      final result = validator.validate('MyP@ssw0rd');
      
      expect(result.isValid, isTrue);
      expect(result.errors, isEmpty);
    });
    
    test('rejects password shorter than 8 characters', () {
      final result = validator.validate('Ab1!');
      
      expect(result.isValid, isFalse);
      expect(result.errors, contains('Password must be at least 8 characters'));
    });
    
    test('rejects password without uppercase letter', () {
      final result = validator.validate('myp@ssw0rd');
      
      expect(result.isValid, isFalse);
      expect(result.errors, contains('Password must contain an uppercase letter'));
    });
    
    test('rejects password without number', () {
      final result = validator.validate('MyP@ssword');
      
      expect(result.isValid, isFalse);
      expect(result.errors, contains('Password must contain a number'));
    });
    
    test('rejects password without special character', () {
      final result = validator.validate('MyPassw0rd');
      
      expect(result.isValid, isFalse);
      expect(result.errors, contains('Password must contain a special character'));
    });
    
    test('collects multiple errors for invalid password', () {
      final result = validator.validate('abc');
      
      expect(result.isValid, isFalse);
      expect(result.errors, hasLength(greaterThanOrEqualTo(3)));
      expect(result.errors, contains('Password must be at least 8 characters'));
      expect(result.errors, contains('Password must contain an uppercase letter'));
      expect(result.errors, contains('Password must contain a number'));
    });
  });
}