---
type: WARNING
---

**Token expiration must be handled proactively.** If you only check token validity when the user makes a request, they will see random failures throughout the app when the token expires mid-session.

Implement a token refresh strategy that:
1. Stores the token expiration timestamp alongside the token
2. Checks expiration before every API call (not after a 401 response)
3. Refreshes the token silently using the refresh token
4. Queues requests during refresh to avoid duplicate refresh calls

```dart
// WRONG - user sees error, then has to retry
final response = await api.getPosts(token);
if (response.statusCode == 401) {
  // Too late -- user already saw the failure
}

// RIGHT - proactive refresh before the call
if (tokenExpiresWithin(Duration(minutes: 5))) {
  await refreshToken();
}
final response = await api.getPosts(freshToken);
```

If the refresh token itself expires, redirect the user to the login screen with a clear message rather than showing a generic error.
