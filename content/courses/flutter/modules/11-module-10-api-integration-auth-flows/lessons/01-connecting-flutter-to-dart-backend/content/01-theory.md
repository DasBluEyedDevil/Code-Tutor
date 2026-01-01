---
type: "THEORY"
title: "Introduction: Why Serverpod's Client is Special"
---


You have built a complete Serverpod backend in Module 8. You tested it thoroughly in Module 9. Now it is time to connect your Flutter app to that backend and bring everything together.

**The Problem with Traditional API Integration:**

When connecting a mobile app to a backend, developers typically face these challenges:

1. **Manual URL Construction**: Building API endpoint URLs as strings is error-prone. A typo in `/api/users` versus `/api/user` causes silent failures.

2. **JSON Serialization**: Converting Dart objects to JSON for requests and parsing JSON responses back to objects requires boilerplate code and careful type checking.

3. **Type Safety**: REST APIs return dynamic JSON. A backend change that renames a field from `userName` to `username` breaks your app at runtime, not compile time.

4. **Duplicate Models**: You define a User class on the server, then manually create a matching User class in Flutter. When one changes, you must remember to update the other.

5. **Error Handling**: HTTP status codes, network failures, timeout errors, and server exceptions all need different handling strategies.

**Serverpod's Revolutionary Approach:**

Serverpod eliminates all of these problems through **code generation**. When you create a Serverpod project, you actually get three packages:

- `my_project_server`: Your backend code (endpoints, database models)
- `my_project_client`: Auto-generated Flutter client (what we use in this lesson)
- `my_project_flutter`: Pre-configured Flutter app with the client already connected

The client package is not written by hand. It is **generated** from your server code. This means:

- **Type Safety**: The same Dart classes exist on both server and client
- **No URL Strings**: You call methods directly, like `client.user.getUser(id)`
- **Automatic Serialization**: Objects convert to/from JSON automatically
- **Compile-Time Errors**: If you rename a field on the server, the client code regenerates and your IDE shows errors immediately

This is the power of a **full-stack Dart** solution. One language, shared types, generated client code.

