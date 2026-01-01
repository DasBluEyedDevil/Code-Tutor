---
type: "EXAMPLE"
title: "Traditional REST vs Serverpod Client"
---


Let us compare the traditional approach to the Serverpod approach for fetching a user:

**Traditional REST API (Manual HTTP Calls):**



```dart
// Traditional approach - lots of boilerplate and no type safety
import 'dart:convert';
import 'package:http/http.dart' as http;

class UserService {
  final String baseUrl = 'https://api.myapp.com';
  
  Future<User?> getUser(int id) async {
    try {
      // 1. Construct URL manually (typo-prone)
      final url = Uri.parse('$baseUrl/api/users/$id');
      
      // 2. Make HTTP request
      final response = await http.get(
        url,
        headers: {'Authorization': 'Bearer $token'},
      );
      
      // 3. Check status code
      if (response.statusCode == 200) {
        // 4. Parse JSON manually
        final json = jsonDecode(response.body);
        
        // 5. Convert to User object (no type safety!)
        return User(
          id: json['id'] as int,           // Could crash at runtime!
          name: json['name'] as String,    // What if server sends 'userName'?
          email: json['email'] as String,
        );
      } else if (response.statusCode == 404) {
        return null;
      } else {
        throw Exception('Failed to load user: ${response.statusCode}');
      }
    } catch (e) {
      // 6. Handle network errors separately
      throw Exception('Network error: $e');
    }
  }
}

// You also need a separate User class that must match the server!
class User {
  final int id;
  final String name;
  final String email;
  
  User({required this.id, required this.name, required this.email});
}
```
