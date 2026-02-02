// routes/api/messages/index.dart
// Handles GET /api/messages and POST /api/messages

import 'package:dart_frog/dart_frog.dart';

Future<Response> onRequest(RequestContext context) async {
  switch (context.request.method) {
    case HttpMethod.get:
      // Return list of messages
      return Response.json(
        body: {
          'messages': [
            {
              'id': '1',
              'author': 'Alice',
              'content': 'Hello everyone!',
            },
            {
              'id': '2',
              'author': 'Bob',
              'content': 'Welcome to the chat!',
            },
            {
              'id': '3',
              'author': 'Charlie',
              'content': 'Great to be here!',
            },
          ],
        },
      );

    case HttpMethod.post:
      // Read the incoming message data
      final body = await context.request.json() as Map<String, dynamic>;
      final author = body['author'] as String;
      final content = body['content'] as String;

      // Create new message with generated ID
      final newMessage = {
        'id': DateTime.now().millisecondsSinceEpoch.toString(),
        'author': author,
        'content': content,
      };

      // Return created message with 201 status
      return Response.json(
        body: {
          'message': 'Message created',
          'data': newMessage,
        },
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