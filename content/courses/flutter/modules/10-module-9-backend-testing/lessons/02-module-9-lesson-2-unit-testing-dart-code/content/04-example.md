---
type: "EXAMPLE"
title: "Matcher Examples in Practice"
---


Here are practical examples of using different matchers:



```dart
import 'package:test/test.dart';

void main() {
  group('Matcher Examples', () {
    
    test('numeric comparisons', () {
      final temperature = 25.5;
      
      expect(temperature, greaterThan(20));
      expect(temperature, lessThan(30));
      expect(temperature, inInclusiveRange(20, 30));
      
      // For floating point comparisons, use closeTo
      expect(temperature, closeTo(25.0, 1.0)); // Within 1.0 of 25.0
    });
    
    test('string operations', () {
      final email = 'user@example.com';
      
      expect(email, contains('@'));
      expect(email, endsWith('.com'));
      expect(email, matches(RegExp(r'^[\w]+@[\w]+\.[\w]+$')));
    });
    
    test('collection operations', () {
      final users = ['Alice', 'Bob', 'Charlie'];
      
      expect(users, hasLength(3));
      expect(users, contains('Bob'));
      expect(users, containsAll(['Alice', 'Charlie']));
      expect(users, isNot(contains('David')));
    });
    
    test('map operations', () {
      final config = {'host': 'localhost', 'port': 8080};
      
      expect(config, containsPair('host', 'localhost'));
      expect(config, hasLength(2));
      expect(config.keys, contains('port'));
    });
    
    test('combining matchers', () {
      final value = 15;
      
      // Use allOf for AND logic
      expect(value, allOf([
        greaterThan(10),
        lessThan(20),
        isA<int>(),
      ]));
      
      // Use anyOf for OR logic
      expect(value, anyOf([equals(15), equals(20)]));
    });
  });
}
```
