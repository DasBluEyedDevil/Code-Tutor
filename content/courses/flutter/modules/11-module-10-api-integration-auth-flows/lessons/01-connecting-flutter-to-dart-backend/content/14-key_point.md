---
type: "KEY_POINT"
title: "Lesson Summary"
---


You have learned how to connect your Flutter app to a Serverpod backend. Here are the key concepts:

**Serverpod's Type-Safe Client:**
- Generated automatically from your server code
- No manual URL construction or JSON parsing
- Shared model classes between server and client
- Compile-time errors when server changes

**Setting Up the Client:**
- Add `serverpod_flutter` and your client package to dependencies
- Initialize the Client with your server URL
- Configure `FlutterAuthenticationKeyManager` for persistent sessions

**Making API Calls:**
- Call endpoints directly: `client.endpoint.method(args)`
- The Session is handled automatically
- Authentication tokens flow through every request

**Session Management:**
- `FlutterAuthenticationKeyManager` stores tokens securely
- `SessionManager` tracks login state and user info
- Server endpoints access user via `session.auth.authenticatedUserId`

**Error Handling:**
- Catch `ServerpodClientException` for server errors
- Handle network errors separately (`SocketException`, `TimeoutException`)
- Use the Result pattern for clean error handling
- Implement retry logic for transient failures

**Repository Pattern:**
- Abstracts data access from UI code
- Enables caching and offline support
- Makes testing easier with mock implementations
- Combines well with Riverpod for state management

In the next lesson, you will implement complete authentication flows including email/password, social login, and session refresh.

