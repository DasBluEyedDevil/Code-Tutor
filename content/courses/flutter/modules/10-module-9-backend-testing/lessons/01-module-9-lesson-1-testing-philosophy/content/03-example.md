---
type: "EXAMPLE"
title: "Test Pyramid in Practice"
---


Let's see how the test pyramid applies to a Dart backend with a user registration feature:



```dart
// === UNIT TESTS (Many) ===
// Test individual functions in isolation

// Test password validation logic
test('validates password has minimum length', () {
  expect(isValidPassword('abc'), false);
  expect(isValidPassword('abcdefgh'), true);
});

// Test email formatting
test('validates email format correctly', () {
  expect(isValidEmail('user@example.com'), true);
  expect(isValidEmail('invalid-email'), false);
});

// Test password hashing
test('hashes password with salt', () {
  final hash = hashPassword('mypassword', 'salt123');
  expect(hash, isNotEmpty);
  expect(hash, isNot(equals('mypassword'))); // Not plaintext
});

// === INTEGRATION TESTS (Some) ===
// Test components working together

test('UserRepository saves user to database', () async {
  final db = await createTestDatabase();
  final repo = UserRepository(db);
  
  final user = User(email: 'test@test.com', name: 'Test');
  await repo.save(user);
  
  final found = await repo.findByEmail('test@test.com');
  expect(found?.name, 'Test');
});

// === E2E TESTS (Few) ===
// Test complete workflows

test('full registration flow via API', () async {
  final response = await http.post(
    Uri.parse('http://localhost:8080/api/register'),
    body: jsonEncode({
      'email': 'newuser@test.com',
      'password': 'securepassword123',
      'name': 'New User',
    }),
  );
  
  expect(response.statusCode, 201);
  final body = jsonDecode(response.body);
  expect(body['user']['email'], 'newuser@test.com');
});
```
