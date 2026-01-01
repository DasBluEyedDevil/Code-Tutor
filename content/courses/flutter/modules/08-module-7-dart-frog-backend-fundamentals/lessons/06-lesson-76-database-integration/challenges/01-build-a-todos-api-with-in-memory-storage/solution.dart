// routes/api/todos.dart
import 'package:dart_frog/dart_frog.dart';

// In-memory storage for todos
final List<Map<String, dynamic>> todos = [];

Future<Response> onRequest(RequestContext context) async {
  switch (context.request.method) {
    case HttpMethod.get:
      // Return all todos
      return Response.json(
        body: {'todos': todos},
      );
    
    case HttpMethod.post:
      // Read the JSON body
      final body = await context.request.json() as Map<String, dynamic>;
      final title = body['title'] as String;
      
      // Create a new todo with generated ID
      final newTodo = {
        'id': DateTime.now().millisecondsSinceEpoch.toString(),
        'title': title,
        'completed': false,
      };
      
      // Add to our in-memory list
      todos.add(newTodo);
      
      // Return the created todo with 201 status
      return Response.json(
        body: newTodo,
        statusCode: 201,
      );
    
    default:
      // Method not allowed
      return Response.json(
        body: {'error': 'Method not allowed'},
        statusCode: 405,
      );
  }
}