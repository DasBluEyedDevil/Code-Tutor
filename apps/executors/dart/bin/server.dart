import 'dart:async';
import 'dart:convert';
import 'dart:io';
import 'package:shelf/shelf.dart';
import 'package:shelf/shelf_io.dart' as io;
import 'package:shelf_router/shelf_router.dart';
import 'package:uuid/uuid.dart';

Future<void> main() async {
  final app = Router();

  app.get('/health', (Request request) {
    return Response.ok(
      jsonEncode({'status': 'ok', 'service': 'dart-executor'}),
      headers: {'Content-Type': 'application/json'},
    );
  });

  app.post('/execute', (Request request) async {
    try {
      final body = await request.readAsString();
      final json = jsonDecode(body);

      if (json['code'] == null || json['code'].isEmpty) {
        return Response.badRequest(
          body: jsonEncode({
            'success': false,
            'error': 'No code provided',
          }),
          headers: {'Content-Type': 'application/json'},
        );
      }

      final result = await executeDartCode(json['code']);
      return Response.ok(
        jsonEncode(result),
        headers: {'Content-Type': 'application/json'},
      );
    } catch (e) {
      return Response.internalServerError(
        body: jsonEncode({
          'success': false,
          'error': 'Server error: $e',
        }),
        headers: {'Content-Type': 'application/json'},
      );
    }
  });

  final handler = Pipeline()
      .addMiddleware(logRequests())
      .addMiddleware(_cors())
      .addHandler(app);

  final server = await io.serve(handler, '0.0.0.0', 4007);
  print('🎯 Dart executor service running on port ${server.port}...');
}

Middleware _cors() {
  return (Handler handler) {
    return (Request request) async {
      if (request.method == 'OPTIONS') {
        return Response.ok('', headers: _corsHeaders);
      }

      final response = await handler(request);
      return response.change(headers: _corsHeaders);
    };
  };
}

final _corsHeaders = {
  'Access-Control-Allow-Origin': '*',
  'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS',
  'Access-Control-Allow-Headers': 'Content-Type, Authorization',
};

Future<Map<String, dynamic>> executeDartCode(String code) async {
  final stopwatch = Stopwatch()..start();

  try {
    // Create temporary directory
    final uuid = const Uuid().v4();
    final tempDir = Directory('/tmp/dart-exec-$uuid');
    await tempDir.create();

    // Wrap code in main function if needed
    if (!code.contains('void main')) {
      code = 'void main() {\n$code\n}';
    }

    // Write source file
    final sourceFile = File('${tempDir.path}/main.dart');
    await sourceFile.writeAsString(code);

    // Execute with timeout
    Process? process;
    try {
      process = await Process.start(
        'dart',
        ['run', sourceFile.path],
        workingDirectory: tempDir.path,
      );

      final outputBuffer = StringBuffer();
      final errorBuffer = StringBuffer();

      process.stdout.transform(utf8.decoder).listen(outputBuffer.write);
      process.stderr.transform(utf8.decoder).listen(errorBuffer.write);

      final exitCode = await process.exitCode.timeout(
        const Duration(seconds: 5),
        onTimeout: () {
          process?.kill();
          return -1;
        },
      );

      // Cleanup
      await tempDir.delete(recursive: true);

      if (exitCode == -1) {
        return {
          'success': false,
          'output': outputBuffer.toString(),
          'error': 'Execution timed out after 5 seconds',
          'executionTime': stopwatch.elapsedMilliseconds,
        };
      }

      if (exitCode != 0) {
        return {
          'success': false,
          'output': outputBuffer.toString(),
          'error': 'Process exited with code: $exitCode\n${errorBuffer.toString()}',
          'executionTime': stopwatch.elapsedMilliseconds,
        };
      }

      final output = outputBuffer.toString();
      return {
        'success': true,
        'output': output.isEmpty ? '(No output)' : output,
        'error': null,
        'executionTime': stopwatch.elapsedMilliseconds,
      };
    } catch (e) {
      await tempDir.delete(recursive: true);
      return {
        'success': false,
        'output': '',
        'error': 'Execution error: $e',
        'executionTime': stopwatch.elapsedMilliseconds,
      };
    }
  } catch (e) {
    return {
      'success': false,
      'output': '',
      'error': 'Execution error: $e',
      'executionTime': stopwatch.elapsedMilliseconds,
    };
  }
}
