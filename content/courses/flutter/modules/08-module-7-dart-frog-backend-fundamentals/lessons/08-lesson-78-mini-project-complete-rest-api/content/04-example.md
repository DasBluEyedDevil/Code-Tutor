---
type: "EXAMPLE"
title: "Auth Service Implementation"
---


The auth service handles user registration and login:



```dart
// lib/services/auth_service.dart
import '../models/user.dart';
import '../utils/jwt_helper.dart';
import 'package:bcrypt/bcrypt.dart';
import 'package:uuid/uuid.dart';

class AuthService {
  // In-memory storage (use database in production)
  final Map<String, User> _users = {};
  final _uuid = Uuid();

  /// Register a new user
  /// Returns the User if successful, null if email already exists
  User? register(String email, String password) {
    // Check if email already registered
    if (_users.values.any((u) => u.email == email)) {
      return null;
    }

    // Hash the password (NEVER store plain text!)
    final passwordHash = BCrypt.hashpw(password, BCrypt.gensalt());

    // Create user with generated ID
    final user = User(
      id: 'usr_${_uuid.v4().substring(0, 8)}',
      email: email,
      passwordHash: passwordHash,
      createdAt: DateTime.now(),
    );

    _users[user.id] = user;
    return user;
  }

  /// Login user and return JWT token
  /// Returns token if credentials valid, null otherwise
  String? login(String email, String password) {
    // Find user by email
    final user = _users.values.where((u) => u.email == email).firstOrNull;
    
    if (user == null) {
      return null; // User not found
    }

    // Verify password against hash
    if (!BCrypt.checkpw(password, user.passwordHash)) {
      return null; // Wrong password
    }

    // Create and return JWT
    return createToken(user.id, user.email);
  }

  /// Get user by ID (for profile endpoint)
  User? getUserById(String userId) {
    return _users[userId];
  }
}

// Global instance (in production, use dependency injection)
final authService = AuthService();
```
