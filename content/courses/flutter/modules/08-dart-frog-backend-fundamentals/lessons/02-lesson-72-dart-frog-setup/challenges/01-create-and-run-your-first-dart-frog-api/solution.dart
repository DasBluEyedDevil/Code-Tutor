// Solution: Modified routes/index.dart
// Your first Dart Frog API endpoint!

import 'package:dart_frog/dart_frog.dart';

/// Handles requests to the root endpoint: GET /
/// 
/// This function is called whenever someone visits http://localhost:8080/
/// The function name MUST be 'onRequest' for Dart Frog to recognize it.
Response onRequest(RequestContext context) {
  // Return a simple text response
  // The 'body' parameter is what gets sent to the client
  return Response(body: 'Hello from my first Dart Frog API!');
}

// WHAT WE LEARNED:
// 1. dart_frog create <name> - creates a new project
// 2. dart_frog dev - runs the development server with hot reload
// 3. routes/index.dart - handles the root URL (/)
// 4. onRequest function - the handler for incoming requests
// 5. Response(body: ...) - creates the HTTP response
//
// HOT RELOAD:
// - Change this file
// - Save
// - Refresh browser
// - See changes instantly!
//
// NEXT STEPS:
// - Create more routes in the routes/ folder
// - Handle different HTTP methods (GET, POST, PUT, DELETE)
// - Return JSON responses
// - Add middleware