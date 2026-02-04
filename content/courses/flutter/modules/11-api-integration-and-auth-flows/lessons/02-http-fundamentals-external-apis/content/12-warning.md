---
type: WARNING
---

**Never assume HTTP requests succeed.** Every network call can fail due to timeouts, server errors, or connectivity loss. Always check the response status code before parsing the body.

```dart
// WRONG - crashes on non-200 responses
final response = await http.get(Uri.parse(url));
final data = jsonDecode(response.body); // May parse error HTML as JSON

// RIGHT - check status before parsing
final response = await http.get(Uri.parse(url));
if (response.statusCode == 200) {
  final data = jsonDecode(response.body);
} else {
  throw HttpException('Request failed: ${response.statusCode}');
}
```

Wrap all HTTP calls in try-catch to handle `SocketException` (no internet), `TimeoutException` (slow server), and `FormatException` (invalid JSON). Show user-friendly error messages instead of letting the app crash.
