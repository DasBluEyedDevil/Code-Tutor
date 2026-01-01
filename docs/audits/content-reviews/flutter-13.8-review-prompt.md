# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Flutter Complete Development Course (dart)
- **Module:** Module 13: Advanced State Management with Riverpod & Hooks
- **Lesson:** Module 13, Lesson 8: Mini-Project - Todo App with Riverpod (ID: 13.8)
- **Difficulty:** advanced
- **Estimated Time:** 90 minutes

## Current Lesson Content

{
    "id":  "13.8",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Project Overview",
                                "content":  "\nBuild a complete Todo app using everything from this module:\n\n**Features:**\n- Add, edit, delete todos\n- Filter by status (all, active, completed)\n- Persist to local storage\n- Pull-to-refresh simulation\n\n**Stack:**\n- Riverpod for state management\n- Code generation with @riverpod\n- Flutter Hooks for local state\n- AsyncValue for loading states\n\n"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Project Structure",
                                "content":  "\n",
                                "code":  "lib/\n  main.dart\n  models/\n    todo.dart\n  providers/\n    todos_provider.dart      # Notifier + Provider\n    filter_provider.dart     # Filter state\n    filtered_todos.dart      # Computed provider\n  screens/\n    todos_screen.dart        # Main UI\n  widgets/\n    todo_tile.dart           # Single todo item\n    add_todo_dialog.dart     # Add/edit form",
                                "language":  "plaintext"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Core Providers",
                                "content":  "\n",
                                "code":  "// models/todo.dart\n@freezed\nclass Todo with _$Todo {\n  const factory Todo({\n    required String id,\n    required String title,\n    @Default(false) bool completed,\n  }) = _Todo;\n}\n\n// providers/todos_provider.dart\n@riverpod\nclass Todos extends _$Todos {\n  @override\n  Future\u003cList\u003cTodo\u003e\u003e build() async {\n    // Simulate API fetch\n    await Future.delayed(const Duration(seconds: 1));\n    return [\n      const Todo(id: \u00271\u0027, title: \u0027Learn Riverpod\u0027),\n      const Todo(id: \u00272\u0027, title: \u0027Build awesome apps\u0027),\n    ];\n  }\n\n  void addTodo(String title) {\n    final newTodo = Todo(id: const Uuid().v4(), title: title);\n    state = AsyncData([...state.value ?? [], newTodo]);\n  }\n\n  void toggleTodo(String id) {\n    state = AsyncData(\n      state.value?.map((t) =\u003e t.id == id ? t.copyWith(completed: !t.completed) : t).toList() ?? [],\n    );\n  }\n\n  void deleteTodo(String id) {\n    state = AsyncData(\n      state.value?.where((t) =\u003e t.id != id).toList() ?? [],\n    );\n  }\n}\n\n// providers/filter_provider.dart\nenum TodoFilter { all, active, completed }\n\n@riverpod\nclass Filter extends _$Filter {\n  @override\n  TodoFilter build() =\u003e TodoFilter.all;\n\n  void setFilter(TodoFilter filter) =\u003e state = filter;\n}\n\n// providers/filtered_todos.dart\n@riverpod\nList\u003cTodo\u003e filteredTodos(FilteredTodosRef ref) {\n  final filter = ref.watch(filterProvider);\n  final todosAsync = ref.watch(todosProvider);\n\n  return todosAsync.when(\n    data: (todos) =\u003e switch (filter) {\n      TodoFilter.all =\u003e todos,\n      TodoFilter.active =\u003e todos.where((t) =\u003e !t.completed).toList(),\n      TodoFilter.completed =\u003e todos.where((t) =\u003e t.completed).toList(),\n    },\n    loading: () =\u003e [],\n    error: (_, __) =\u003e [],\n  );\n}",
                                "language":  "dart"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "PROJECT",
                           "id":  "13.8-project-0",
                           "title":  "Complete Todo App",
                           "description":  "Build a fully functional Todo app with Riverpod + Hooks.",
                           "instructions":  "1. Set up project with riverpod_generator\\n2. Create Todo model with freezed\\n3. Implement TodosNotifier with CRUD operations\\n4. Add filter functionality\\n5. Build UI with HookConsumerWidget\\n6. Add pull-to-refresh\\n7. Handle loading and error states",
                           "starterCode":  "// Start with: flutter create todo_riverpod\n// Add dependencies:\n// flutter_riverpod, riverpod_annotation, riverpod_generator\n// hooks_riverpod, flutter_hooks\n// freezed_annotation, freezed, build_runner\n// uuid",
                           "solution":  "// See complete project at:\n// github.com/flutter-examples/riverpod-todo-app\n\n// Key implementation patterns shown in lesson content above.",
                           "language":  "dart",
                           "testCases":  [

                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Run \u0027dart run build_runner watch\u0027 during development"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Use AsyncValue.when for all loading states"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting to wrap app with ProviderScope",
                                                      "consequence":  "Riverpod providers don\u0027t work",
                                                      "correction":  "Wrap MaterialApp with ProviderScope in main()"
                                                  }
                                              ],
                           "difficulty":  "advanced"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Module 13, Lesson 8: Mini-Project - Todo App with Riverpod",
    "estimatedMinutes":  90
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current dart documentation
- Search the web for the latest dart version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "dart Module 13, Lesson 8: Mini-Project - Todo App with Riverpod 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "13.8",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

