// lib/utils/jwt_helper.dart
import 'package:dart_jsonwebtoken/dart_jsonwebtoken.dart';

const String jwtSecretKey = 'my-secret-key';

String createToken(String userId, String email) {
  final jwt = JWT({
    'userId': userId,
    'email': email,
    'exp': DateTime.now().add(Duration(hours: 24)).millisecondsSinceEpoch ~/ 1000,
  });
  
  return jwt.sign(SecretKey(jwtSecretKey));
}

Map<String, dynamic>? verifyToken(String token) {
  try {
    final jwt = JWT.verify(token, SecretKey(jwtSecretKey));
    return jwt.payload as Map<String, dynamic>;
  } catch (e) {
    return null;
  }
}

// -----------------------------------------
// routes/auth/login.dart
import 'package:dart_frog/dart_frog.dart';
import '../../lib/utils/jwt_helper.dart';

Future<Response> onRequest(RequestContext context) async {
  if (context.request.method != HttpMethod.post) {
    return Response.json(body: {'error': 'Method not allowed'}, statusCode: 405);
  }
  
  final body = await context.request.json() as Map<String, dynamic>;
  final email = body['email'] as String?;
  final password = body['password'] as String?;
  
  // Check credentials (in real app, check against database)
  if (email == 'user@test.com' && password == 'password123') {
    final token = createToken('user_001', email);
    return Response.json(
      body: {
        'message': 'Login successful',
        'token': token,
      },
    );
  }
  
  return Response.json(
    body: {'error': 'Invalid email or password'},
    statusCode: 401,
  );
}

// -----------------------------------------
// routes/api/_middleware.dart
import 'package:dart_frog/dart_frog.dart';
import '../../lib/utils/jwt_helper.dart';

Handler middleware(Handler handler) {
  return (context) async {
    final authHeader = context.request.headers['Authorization'];
    
    if (authHeader == null || !authHeader.startsWith('Bearer ')) {
      return Response.json(
        body: {'error': 'No token provided'},
        statusCode: 401,
      );
    }
    
    final token = authHeader.substring(7);
    final payload = verifyToken(token);
    
    if (payload == null) {
      return Response.json(
        body: {'error': 'Invalid or expired token'},
        statusCode: 401,
      );
    }
    
    return handler(context.provide<Map<String, dynamic>>(() => payload));
  };
}

// -----------------------------------------
// routes/api/profile.dart
import 'package:dart_frog/dart_frog.dart';

Future<Response> onRequest(RequestContext context) async {
  final user = context.read<Map<String, dynamic>>();
  
  return Response.json(
    body: {
      'userId': user['userId'],
      'email': user['email'],
      'message': 'Welcome to your profile!',
    },
  );
}