---
type: "EXAMPLE"
title: "Writing Your First Test"
---


Let's write a complete test file from scratch. We will test a simple calculator class.



```dart
// lib/src/calculator.dart
class Calculator {
  double add(double a, double b) => a + b;
  double subtract(double a, double b) => a - b;
  double multiply(double a, double b) => a * b;
  
  double divide(double a, double b) {
    if (b == 0) {
      throw ArgumentError('Cannot divide by zero');
    }
    return a / b;
  }
}

// test/calculator_test.dart
import 'package:test/test.dart';
import 'package:my_project/src/calculator.dart';

void main() {
  // Create an instance to test
  late Calculator calculator;
  
  // setUp runs before EACH test
  setUp(() {
    calculator = Calculator();
  });
  
  // Group related tests together
  group('Calculator', () {
    
    group('add', () {
      test('adds two positive numbers', () {
        expect(calculator.add(2, 3), equals(5));
      });
      
      test('adds negative numbers', () {
        expect(calculator.add(-2, -3), equals(-5));
      });
      
      test('adds zero', () {
        expect(calculator.add(5, 0), equals(5));
      });
    });
    
    group('divide', () {
      test('divides two numbers', () {
        expect(calculator.divide(10, 2), equals(5));
      });
      
      test('throws ArgumentError when dividing by zero', () {
        expect(
          () => calculator.divide(10, 0),
          throwsA(isA<ArgumentError>()),
        );
      });
    });
  });
}
```
