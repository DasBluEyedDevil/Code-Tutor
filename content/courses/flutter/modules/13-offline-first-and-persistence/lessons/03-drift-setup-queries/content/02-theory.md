---
type: "THEORY"
title: "Table Definitions"
---


**Column Types:**



```dart
class Tasks extends Table {
  // Auto-incrementing primary key
  IntColumn get id => integer().autoIncrement()();
  
  // Required text with length constraints
  TextColumn get title => text().withLength(min: 1, max: 100)();
  
  // Optional text (nullable)
  TextColumn get description => text().nullable()();
  
  // Boolean with default value
  BoolColumn get isCompleted => boolean().withDefault(const Constant(false))();
  
  // DateTime with default to current time
  DateTimeColumn get createdAt => dateTime().withDefault(currentDateAndTime)();
  
  // Nullable DateTime
  DateTimeColumn get dueDate => dateTime().nullable()();
  
  // Integer with custom default
  IntColumn get priority => integer().withDefault(const Constant(0))();
  
  // Foreign key reference
  IntColumn get projectId => integer().nullable().references(Projects, #id)();
  
  // Enum stored as integer
  IntColumn get status => intEnum<TaskStatus>()();
  
  // Unique constraint
  TextColumn get externalId => text().unique().nullable()();
}

enum TaskStatus {
  pending,
  inProgress,
  completed,
  cancelled
}

// Custom primary key
class Settings extends Table {
  TextColumn get key => text()();
  TextColumn get value => text()();
  
  @override
  Set<Column> get primaryKey => {key};
}
```
