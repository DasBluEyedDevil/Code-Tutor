// lib/models/user.dart
class User {
  final String id;
  final String email;
  final String passwordHash;
  final DateTime createdAt;

  User({
    required this.id,
    required this.email,
    required this.passwordHash,
    required this.createdAt,
  });

  Map<String, dynamic> toJson() => {
    'id': id,
    'email': email,
    'createdAt': createdAt.toIso8601String(),
  };
}

// lib/models/note.dart
class Note {
  final String id;
  final String userId;
  final String title;
  final String content;
  final DateTime createdAt;
  final DateTime updatedAt;

  Note({
    required this.id,
    required this.userId,
    required this.title,
    required this.content,
    required this.createdAt,
    required this.updatedAt,
  });

  Note copyWith({String? title, String? content, DateTime? updatedAt}) {
    return Note(
      id: id,
      userId: userId,
      title: title ?? this.title,
      content: content ?? this.content,
      createdAt: createdAt,
      updatedAt: updatedAt ?? this.updatedAt,
    );
  }

  Map<String, dynamic> toJson() => {
    'id': id,
    'title': title,
    'content': content,
    'createdAt': createdAt.toIso8601String(),
    'updatedAt': updatedAt.toIso8601String(),
  };
}

// lib/utils/jwt_helper.dart
import 'package:dart_jsonwebtoken/dart_jsonwebtoken.dart';

const String jwtSecretKey = 'my-super-secret-key';

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

// lib/services/auth_service.dart
import 'package:bcrypt/bcrypt.dart';
import 'package:uuid/uuid.dart';
import '../models/user.dart';
import '../utils/jwt_helper.dart';

class AuthService {
  final Map<String, User> _users = {};
  final _uuid = Uuid();

  User? register(String email, String password) {
    if (_users.values.any((u) => u.email == email)) return null;
    final passwordHash = BCrypt.hashpw(password, BCrypt.gensalt());
    final user = User(
      id: 'usr_${_uuid.v4().substring(0, 8)}',
      email: email,
      passwordHash: passwordHash,
      createdAt: DateTime.now(),
    );
    _users[user.id] = user;
    return user;
  }

  String? login(String email, String password) {
    final user = _users.values.where((u) => u.email == email).firstOrNull;
    if (user == null) return null;
    if (!BCrypt.checkpw(password, user.passwordHash)) return null;
    return createToken(user.id, user.email);
  }
}

final authService = AuthService();

// lib/services/note_service.dart
import 'package:uuid/uuid.dart';
import '../models/note.dart';

class NoteService {
  final Map<String, Note> _notes = {};
  final _uuid = Uuid();

  List<Note> getUserNotes(String userId) {
    return _notes.values.where((n) => n.userId == userId).toList()
      ..sort((a, b) => b.updatedAt.compareTo(a.updatedAt));
  }

  Note? getNoteById(String noteId, String userId) {
    final note = _notes[noteId];
    if (note == null || note.userId != userId) return null;
    return note;
  }

  Note create(String userId, String title, String content) {
    final now = DateTime.now();
    final note = Note(
      id: 'note_${_uuid.v4().substring(0, 8)}',
      userId: userId,
      title: title,
      content: content,
      createdAt: now,
      updatedAt: now,
    );
    _notes[note.id] = note;
    return note;
  }

  Note? update(String noteId, String userId, {String? title, String? content}) {
    final note = getNoteById(noteId, userId);
    if (note == null) return null;
    final updated = note.copyWith(title: title, content: content, updatedAt: DateTime.now());
    _notes[noteId] = updated;
    return updated;
  }

  bool delete(String noteId, String userId) {
    if (getNoteById(noteId, userId) == null) return false;
    _notes.remove(noteId);
    return true;
  }
}

final noteService = NoteService();

// routes/auth/register.dart
import 'package:dart_frog/dart_frog.dart';
import '../../lib/services/auth_service.dart';

Future<Response> onRequest(RequestContext context) async {
  if (context.request.method != HttpMethod.post) {
    return Response.json(body: {'error': 'Method not allowed'}, statusCode: 405);
  }
  final body = await context.request.json() as Map<String, dynamic>;
  final user = authService.register(body['email'] as String, body['password'] as String);
  if (user == null) {
    return Response.json(body: {'error': 'Email already registered'}, statusCode: 400);
  }
  return Response.json(body: {'message': 'User created', 'userId': user.id}, statusCode: 201);
}

// routes/auth/login.dart
import 'package:dart_frog/dart_frog.dart';
import '../../lib/services/auth_service.dart';

Future<Response> onRequest(RequestContext context) async {
  if (context.request.method != HttpMethod.post) {
    return Response.json(body: {'error': 'Method not allowed'}, statusCode: 405);
  }
  final body = await context.request.json() as Map<String, dynamic>;
  final token = authService.login(body['email'] as String, body['password'] as String);
  if (token == null) {
    return Response.json(body: {'error': 'Invalid credentials'}, statusCode: 401);
  }
  return Response.json(body: {'token': token});
}

// routes/notes/_middleware.dart
import 'package:dart_frog/dart_frog.dart';
import '../../lib/utils/jwt_helper.dart';

Handler middleware(Handler handler) {
  return (context) async {
    final authHeader = context.request.headers['Authorization'];
    if (authHeader == null || !authHeader.startsWith('Bearer ')) {
      return Response.json(body: {'error': 'No token'}, statusCode: 401);
    }
    final payload = verifyToken(authHeader.substring(7));
    if (payload == null) {
      return Response.json(body: {'error': 'Invalid token'}, statusCode: 401);
    }
    return handler(context.provide<Map<String, dynamic>>(() => payload));
  };
}

// routes/notes/index.dart
import 'package:dart_frog/dart_frog.dart';
import '../../lib/services/note_service.dart';

Future<Response> onRequest(RequestContext context) async {
  final user = context.read<Map<String, dynamic>>();
  final userId = user['userId'] as String;
  
  switch (context.request.method) {
    case HttpMethod.get:
      final notes = noteService.getUserNotes(userId);
      return Response.json(body: {'notes': notes.map((n) => n.toJson()).toList()});
    case HttpMethod.post:
      final body = await context.request.json() as Map<String, dynamic>;
      final title = body['title'] as String?;
      if (title == null || title.isEmpty) {
        return Response.json(body: {'error': 'Title required'}, statusCode: 400);
      }
      final note = noteService.create(userId, title, body['content'] as String? ?? '');
      return Response.json(body: note.toJson(), statusCode: 201);
    default:
      return Response.json(body: {'error': 'Method not allowed'}, statusCode: 405);
  }
}

// routes/notes/[id].dart
import 'package:dart_frog/dart_frog.dart';
import '../../lib/services/note_service.dart';

Future<Response> onRequest(RequestContext context, String id) async {
  final user = context.read<Map<String, dynamic>>();
  final userId = user['userId'] as String;
  
  switch (context.request.method) {
    case HttpMethod.get:
      final note = noteService.getNoteById(id, userId);
      if (note == null) return Response.json(body: {'error': 'Not found'}, statusCode: 404);
      return Response.json(body: note.toJson());
    case HttpMethod.put:
      final body = await context.request.json() as Map<String, dynamic>;
      final note = noteService.update(id, userId, title: body['title'] as String?, content: body['content'] as String?);
      if (note == null) return Response.json(body: {'error': 'Not found'}, statusCode: 404);
      return Response.json(body: note.toJson());
    case HttpMethod.delete:
      if (!noteService.delete(id, userId)) {
        return Response.json(body: {'error': 'Not found'}, statusCode: 404);
      }
      return Response.json(body: {'message': 'Deleted'});
    default:
      return Response.json(body: {'error': 'Method not allowed'}, statusCode: 405);
  }
}