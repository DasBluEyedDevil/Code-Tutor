---
type: "EXAMPLE"
title: "Complete Mocking Example"
---


Let's build a complete example with a `UserRepository` that depends on a `DatabaseClient`:



```dart
// lib/src/database_client.dart
abstract class DatabaseClient {
  Future<Map<String, dynamic>?> findById(String collection, String id);
  Future<void> insert(String collection, Map<String, dynamic> data);
  Future<void> delete(String collection, String id);
}

// lib/src/user_repository.dart
class User {
  final String id;
  final String name;
  final String email;
  
  User({required this.id, required this.name, required this.email});
  
  factory User.fromMap(Map<String, dynamic> map) => User(
    id: map['id'] as String,
    name: map['name'] as String,
    email: map['email'] as String,
  );
}

class UserRepository {
  final DatabaseClient _db;
  
  UserRepository(this._db);
  
  Future<User?> findById(String id) async {
    final data = await _db.findById('users', id);
    if (data == null) return null;
    return User.fromMap(data);
  }
  
  Future<void> save(User user) async {
    await _db.insert('users', {
      'id': user.id,
      'name': user.name,
      'email': user.email,
    });
  }
  
  Future<void> delete(String id) async {
    await _db.delete('users', id);
  }
}

// test/user_repository_test.dart
import 'package:test/test.dart';
import 'package:mocktail/mocktail.dart';
import 'package:my_api/src/database_client.dart';
import 'package:my_api/src/user_repository.dart';

// Create mock class
class MockDatabaseClient extends Mock implements DatabaseClient {}

void main() {
  late MockDatabaseClient mockDb;
  late UserRepository repository;
  
  setUp(() {
    mockDb = MockDatabaseClient();
    repository = UserRepository(mockDb);
  });
  
  group('UserRepository', () {
    
    group('findById', () {
      test('returns user when found in database', () async {
        // Arrange: Set up the mock
        when(() => mockDb.findById('users', '123')).thenAnswer(
          (_) async => {'id': '123', 'name': 'Alice', 'email': 'alice@test.com'},
        );
        
        // Act: Call the method
        final user = await repository.findById('123');
        
        // Assert: Check the result
        expect(user, isNotNull);
        expect(user!.id, '123');
        expect(user.name, 'Alice');
        expect(user.email, 'alice@test.com');
        
        // Verify: Check the mock was called correctly
        verify(() => mockDb.findById('users', '123')).called(1);
      });
      
      test('returns null when user not found', () async {
        when(() => mockDb.findById('users', '999'))
            .thenAnswer((_) async => null);
        
        final user = await repository.findById('999');
        
        expect(user, isNull);
      });
    });
    
    group('save', () {
      test('inserts user data into database', () async {
        // Arrange
        when(() => mockDb.insert(any(), any()))
            .thenAnswer((_) async {});
        
        final user = User(id: '456', name: 'Bob', email: 'bob@test.com');
        
        // Act
        await repository.save(user);
        
        // Verify the correct data was inserted
        verify(() => mockDb.insert('users', {
          'id': '456',
          'name': 'Bob',
          'email': 'bob@test.com',
        })).called(1);
      });
    });
    
    group('delete', () {
      test('deletes user from database', () async {
        when(() => mockDb.delete(any(), any()))
            .thenAnswer((_) async {});
        
        await repository.delete('123');
        
        verify(() => mockDb.delete('users', '123')).called(1);
      });
    });
  });
}
```
