---
type: "THEORY"
title: "Step 3: Database Service (SQLite)"
---


**lib/services/database_service.dart:**



```dart
import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';
import '../models/workout.dart';

class DatabaseService {
  static final DatabaseService instance = DatabaseService._internal();
  factory DatabaseService() => instance;
  DatabaseService._internal();

  static Database? _database;

  Future<Database> get database async {
    if (_database != null) return _database!;
    _database = await _initDatabase();
    return _database!;
  }

  Future<Database> _initDatabase() async {
    final dbPath = await getDatabasesPath();
    final path = join(dbPath, 'fitness_tracker.db');

    return await openDatabase(
      path,
      version: 1,
      onCreate: _onCreate,
    );
  }

  Future<void> _onCreate(Database db, int version) async {
    await db.execute('''
      CREATE TABLE workouts (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        type TEXT NOT NULL,
        start_time INTEGER NOT NULL,
        end_time INTEGER NOT NULL,
        distance REAL,
        calories INTEGER NOT NULL,
        notes TEXT,
        route_json TEXT
      )
    ''');

    await db.execute('''
      CREATE TABLE daily_steps (
        date TEXT PRIMARY KEY,
        steps INTEGER NOT NULL
      )
    ''');
  }

  // Workout methods
  Future<int> insertWorkout(Workout workout) async {
    final db = await database;
    return await db.insert('workouts', workout.toMap());
  }

  Future<List<Workout>> getAllWorkouts() async {
    final db = await database;
    final maps = await db.query('workouts', orderBy: 'start_time DESC');
    return maps.map((map) => Workout.fromMap(map)).toList();
  }

  Future<List<Workout>> getWorkoutsByType(String type) async {
    final db = await database;
    final maps = await db.query(
      'workouts',
      where: 'type = ?',
      whereArgs: [type],
      orderBy: 'start_time DESC',
    );
    return maps.map((map) => Workout.fromMap(map)).toList();
  }

  Future<List<Workout>> getWorkoutsInDateRange(DateTime start, DateTime end) async {
    final db = await database;
    final maps = await db.query(
      'workouts',
      where: 'start_time >= ? AND start_time <= ?',
      whereArgs: [start.millisecondsSinceEpoch, end.millisecondsSinceEpoch],
      orderBy: 'start_time DESC',
    );
    return maps.map((map) => Workout.fromMap(map)).toList();
  }

  Future<int> deleteWorkout(int id) async {
    final db = await database;
    return await db.delete('workouts', where: 'id = ?', whereArgs: [id]);
  }

  // Step counter methods
  Future<void> saveDailySteps(String date, int steps) async {
    final db = await database;
    await db.insert(
      'daily_steps',
      {'date': date, 'steps': steps},
      conflictAlgorithm: ConflictAlgorithm.replace,
    );
  }

  Future<int?> getStepsForDate(String date) async {
    final db = await database;
    final results = await db.query(
      'daily_steps',
      where: 'date = ?',
      whereArgs: [date],
    );

    return results.isNotEmpty ? results.first['steps'] as int : null;
  }

  Future<Map<String, int>> getStepsForWeek() async {
    final db = await database;
    final now = DateTime.now();
    final weekAgo = now.subtract(Duration(days: 7));

    final results = await db.query(
      'daily_steps',
      where: 'date >= ?',
      whereArgs: [weekAgo.toIso8601String().split('T')[0]],
      orderBy: 'date ASC',
    );

    return {
      for (var row in results) row['date'] as String: row['steps'] as int
    };
  }

  // Statistics
  Future<Map<String, dynamic>> getWorkoutStats() async {
    final db = await database;

    final totalWorkouts = Sqflite.firstIntValue(
      await db.rawQuery('SELECT COUNT(*) FROM workouts'),
    ) ?? 0;

    final totalDistance = (await db.rawQuery(
      'SELECT SUM(distance) as total FROM workouts WHERE distance IS NOT NULL',
    ))[0]['total'] ?? 0.0;

    final totalCalories = Sqflite.firstIntValue(
      await db.rawQuery('SELECT SUM(calories) FROM workouts'),
    ) ?? 0;

    final workoutsByType = await db.rawQuery('''
      SELECT type, COUNT(*) as count
      FROM workouts
      GROUP BY type
      ORDER BY count DESC
    ''');

    return {
      'totalWorkouts': totalWorkouts,
      'totalDistance': totalDistance,
      'totalCalories': totalCalories,
      'workoutsByType': workoutsByType,
    };
  }
}
```
