---
type: "EXAMPLE"
title: "Notes Routes Implementation"
---


Now let's implement the routes that use our services:



```dart
// routes/notes/index.dart
// Handles: GET /notes (list) and POST /notes (create)
import 'package:dart_frog/dart_frog.dart';
import '../../lib/services/note_service.dart';

Future<Response> onRequest(RequestContext context) async {
  // Get authenticated user from middleware
  final user = context.read<Map<String, dynamic>>();
  final userId = user['userId'] as String;

  switch (context.request.method) {
    // GET /notes - List all user's notes
    case HttpMethod.get:
      final notes = noteService.getUserNotes(userId);
      return Response.json(
        body: {
          'notes': notes.map((n) => n.toJson()).toList(),
          'count': notes.length,
        },
      );

    // POST /notes - Create new note
    case HttpMethod.post:
      final body = await context.request.json() as Map<String, dynamic>;
      final title = body['title'] as String?;
      final content = body['content'] as String?;

      // Validate required fields
      if (title == null || title.isEmpty) {
        return Response.json(
          body: {'error': 'Title is required'},
          statusCode: 400,
        );
      }

      final note = noteService.create(
        userId,
        title,
        content ?? '',
      );

      return Response.json(
        body: note.toJson(),
        statusCode: 201, // Created
      );

    default:
      return Response.json(
        body: {'error': 'Method not allowed'},
        statusCode: 405,
      );
  }
}

// ─────────────────────────────────────────────────────────
// routes/notes/[id].dart
// Handles: GET, PUT, DELETE /notes/:id
import 'package:dart_frog/dart_frog.dart';
import '../../lib/services/note_service.dart';

Future<Response> onRequest(RequestContext context, String id) async {
  final user = context.read<Map<String, dynamic>>();
  final userId = user['userId'] as String;

  switch (context.request.method) {
    // GET /notes/:id - Get single note
    case HttpMethod.get:
      final note = noteService.getNoteById(id, userId);
      if (note == null) {
        return Response.json(
          body: {'error': 'Note not found'},
          statusCode: 404,
        );
      }
      return Response.json(body: note.toJson());

    // PUT /notes/:id - Update note
    case HttpMethod.put:
      final body = await context.request.json() as Map<String, dynamic>;
      final updatedNote = noteService.update(
        id,
        userId,
        title: body['title'] as String?,
        content: body['content'] as String?,
      );

      if (updatedNote == null) {
        return Response.json(
          body: {'error': 'Note not found'},
          statusCode: 404,
        );
      }
      return Response.json(body: updatedNote.toJson());

    // DELETE /notes/:id - Delete note
    case HttpMethod.delete:
      final deleted = noteService.delete(id, userId);
      if (!deleted) {
        return Response.json(
          body: {'error': 'Note not found'},
          statusCode: 404,
        );
      }
      return Response.json(
        body: {'message': 'Note deleted successfully'},
      );

    default:
      return Response.json(
        body: {'error': 'Method not allowed'},
        statusCode: 405,
      );
  }
}
```
