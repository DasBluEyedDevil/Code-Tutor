---
type: "EXAMPLE"
title: "Sealed Classes for API Results"
---

Model API responses with sealed classes for type-safe result handling. Generic type parameters let you reuse the pattern across different data types. Exhaustive switches ensure all outcomes are handled.

```dart
// Model API responses safely
sealed class ApiResult<T> {}

class ApiSuccess<T> extends ApiResult<T> {
  final T data;
  ApiSuccess(this.data);
}

class ApiError<T> extends ApiResult<T> {
  final int statusCode;
  final String message;
  ApiError(this.statusCode, this.message);
}

class ApiLoading<T> extends ApiResult<T> {}

// Simulate API call
ApiResult<List<String>> fetchUsers() {
  // Simulate different outcomes
  var random = DateTime.now().second % 3;
  
  return switch (random) {
    0 => ApiSuccess(['Alice', 'Bob', 'Charlie']),
    1 => ApiError(404, 'Users not found'),
    _ => ApiLoading(),
  };
}

// Handle all cases exhaustively
void displayResult(ApiResult<List<String>> result) {
  switch (result) {
    case ApiSuccess(data: var users):
      print('Found ${users.length} users:');
      for (var user in users) {
        print('  - $user');
      }
    case ApiError(statusCode: var code, message: var msg):
      print('Error $code: $msg');
    case ApiLoading():
      print('Loading...');
  }
}

void main() {
  var result = fetchUsers();
  displayResult(result);
}
```
