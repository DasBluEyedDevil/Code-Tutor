// routes/api/todos.dart
import 'package:dart_frog/dart_frog.dart';

// In-memory storage for todos
final List<Map<String, dynamic>> todos = [];

Future<Response> onRequest(RequestContext context) async {
  switch (context.request.method) {
    case HttpMethod.get:
      // TODO: Return all todos
      return Response(body: 'Not implemented');
    
    case HttpMethod.post:
      // TODO: Read JSON body, create new todo, add to list
      // TODO: Return created todo with 201 status
      return Response(body: 'Not implemented');
    
    default:
      // TODO: Return 405 Method Not Allowed
      return Response(body: 'Not implemented');
  }
}