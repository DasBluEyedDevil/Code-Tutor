---
type: "EXAMPLE"
title: "Setup and Teardown"
---


Use `setUp` and `tearDown` to prepare test environments and clean up afterward:



```dart
import 'package:test/test.dart';

void main() {
  // Runs once before ALL tests in this file
  setUpAll(() async {
    print('Initializing test database...');
    await TestDatabase.initialize();
  });
  
  // Runs once after ALL tests complete
  tearDownAll(() async {
    print('Closing test database...');
    await TestDatabase.close();
  });
  
  group('UserRepository', () {
    late UserRepository repository;
    late TestDatabase db;
    
    // Runs before EACH test in this group
    setUp(() async {
      db = await TestDatabase.create();
      repository = UserRepository(db);
      
      // Seed test data
      await db.seed([
        {'id': '1', 'name': 'Alice', 'email': 'alice@test.com'},
        {'id': '2', 'name': 'Bob', 'email': 'bob@test.com'},
      ]);
    });
    
    // Runs after EACH test in this group
    tearDown(() async {
      // Clean up test data
      await db.clear();
      await db.disconnect();
    });
    
    test('finds existing user by ID', () async {
      final user = await repository.findById('1');
      
      expect(user, isNotNull);
      expect(user!.name, 'Alice');
    });
    
    test('returns null for non-existent user', () async {
      final user = await repository.findById('999');
      
      expect(user, isNull);
    });
    
    test('saves new user to database', () async {
      final newUser = User(id: '3', name: 'Charlie', email: 'charlie@test.com');
      await repository.save(newUser);
      
      final saved = await repository.findById('3');
      expect(saved?.name, 'Charlie');
    });
  });
}
```
