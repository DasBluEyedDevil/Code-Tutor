// lib/utils/jwt_helper.dart
import 'package:dart_jsonwebtoken/dart_jsonwebtoken.dart';

const String jwtSecretKey = 'my-secret-key';

String createToken(String userId, String email) {
  // TODO: Create and sign JWT token
  // Include: userId, email, exp (24 hours from now)
  return '';
}

Map<String, dynamic>? verifyToken(String token) {
  // TODO: Verify token and return payload
  // Return null if invalid/expired
  return null;
}

// -----------------------------------------
// routes/auth/login.dart
import 'package:dart_frog/dart_frog.dart';

Future<Response> onRequest(RequestContext context) async {
  if (context.request.method != HttpMethod.post) {
    return Response.json(body: {'error': 'Method not allowed'}, statusCode: 405);
  }
  
  // TODO: Read email and password from body
  // TODO: Check if email == 'user@test.com' && password == 'password123'
  // TODO: If valid, create token and return it
  // TODO: If invalid, return 401 error
  
  return Response(body: 'Not implemented');
}

// -----------------------------------------
// routes/api/_middleware.dart
import 'package:dart_frog/dart_frog.dart';

Handler middleware(Handler handler) {
  return (context) async {
    // TODO: Get Authorization header
    // TODO: Extract Bearer token
    // TODO: Verify token
    // TODO: If invalid, return 401
    // TODO: If valid, provide user payload to context
    
    return handler(context);
  };
}

// -----------------------------------------
// routes/api/profile.dart
import 'package:dart_frog/dart_frog.dart';

Future<Response> onRequest(RequestContext context) async {
  // TODO: Read user from context (provided by middleware)
  // TODO: Return user profile as JSON
  
  return Response(body: 'Not implemented');
}