---
type: "EXAMPLE"
title: "Complete Localization Usage"
---


Using all localization features in a real widget:



```dart
import 'package:flutter/material.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

// Extension for cleaner access
extension L10nExtension on BuildContext {
  AppLocalizations get l10n => AppLocalizations.of(this);
}

class TaskListPage extends StatelessWidget {
  final List<Task> tasks;
  final User currentUser;

  const TaskListPage({
    super.key,
    required this.tasks,
    required this.currentUser,
  });

  @override
  Widget build(BuildContext context) {
    final l10n = context.l10n;
    final completedCount = tasks.where((t) => t.isCompleted).length;
    final pendingCount = tasks.length - completedCount;

    return Scaffold(
      appBar: AppBar(
        title: Text(l10n.appTitle),
      ),
      body: Column(
        children: [
          // Greeting with placeholder
          Padding(
            padding: const EdgeInsets.all(16),
            child: Text(
              l10n.welcomeUser(currentUser.name),
              style: Theme.of(context).textTheme.headlineSmall,
            ),
          ),
          
          // Task summary with pluralization
          Card(
            margin: const EdgeInsets.symmetric(horizontal: 16),
            child: Padding(
              padding: const EdgeInsets.all(16),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceAround,
                children: [
                  _StatItem(
                    label: l10n.taskCount(tasks.length),
                    icon: Icons.list,
                  ),
                  _StatItem(
                    label: l10n.completedTasks(completedCount),
                    icon: Icons.check_circle,
                  ),
                  _StatItem(
                    label: l10n.itemsRemaining(pendingCount),
                    icon: Icons.pending,
                  ),
                ],
              ),
            ),
          ),
          
          const SizedBox(height: 16),
          
          // Task list
          Expanded(
            child: ListView.builder(
              itemCount: tasks.length,
              itemBuilder: (context, index) {
                final task = tasks[index];
                return TaskTile(
                  task: task,
                  // Date formatting handled by ARB
                  dueDate: task.dueDate != null
                      ? l10n.dueDate(task.dueDate!)
                      : null,
                );
              },
            ),
          ),
        ],
      ),
      floatingActionButton: FloatingActionButton.extended(
        onPressed: () => _addTask(context),
        icon: const Icon(Icons.add),
        label: Text(l10n.addTask),
      ),
    );
  }

  void _addTask(BuildContext context) {
    // Show add task dialog
  }
}

class _StatItem extends StatelessWidget {
  final String label;
  final IconData icon;

  const _StatItem({required this.label, required this.icon});

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        Icon(icon, size: 32),
        const SizedBox(height: 8),
        Text(label, textAlign: TextAlign.center),
      ],
    );
  }
}

class TaskTile extends StatelessWidget {
  final Task task;
  final String? dueDate;

  const TaskTile({super.key, required this.task, this.dueDate});

  @override
  Widget build(BuildContext context) {
    return ListTile(
      leading: Checkbox(
        value: task.isCompleted,
        onChanged: (_) {},
      ),
      title: Text(task.title),
      subtitle: dueDate != null ? Text(dueDate!) : null,
    );
  }
}

// Model classes
class Task {
  final String title;
  final bool isCompleted;
  final DateTime? dueDate;
  
  Task({required this.title, this.isCompleted = false, this.dueDate});
}

class User {
  final String name;
  User({required this.name});
}
```
