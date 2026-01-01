import 'package:test/test.dart';
import 'package:serverpod_test/serverpod_test.dart';

void main() {
  late TestSession session;
  late TodoEndpoint todoEndpoint;

  setUpAll(() async {
    await TestServer.start();
  });

  tearDownAll(() async {
    await TestServer.stop();
  });

  setUp(() async {
    session = await TestSession.create();
    todoEndpoint = TodoEndpoint();
  });

  tearDown(() async {
    await session.close();
    await TestDatabase.truncateAll();
  });

  group('TodoEndpoint', () {
    group('createTodo', () {
      // TODO: Test creating todos
    });

    group('getTodo', () {
      // TODO: Test getting todos
    });

    group('listTodos', () {
      // TODO: Test listing todos
    });

    group('completeTodo', () {
      // TODO: Test completing todos
    });

    group('deleteTodo', () {
      // TODO: Test deleting todos
    });
  });
}