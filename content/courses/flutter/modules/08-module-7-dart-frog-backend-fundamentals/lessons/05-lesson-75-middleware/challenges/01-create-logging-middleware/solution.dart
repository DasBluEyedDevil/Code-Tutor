// routes/_middleware.dart
import 'package:dart_frog/dart_frog.dart';

Handler middleware(Handler handler) {
  return (context) async {
    // Log the incoming request
    final method = context.request.method.name.toUpperCase();
    final path = context.request.uri.path;
    print('[REQUEST] $method $path');
    
    // Call the route handler and get the response
    final response = await handler(context);
    
    // Log the response status
    print('[RESPONSE] ${response.statusCode}');
    
    // Return the response to the client
    return response;
  };
}