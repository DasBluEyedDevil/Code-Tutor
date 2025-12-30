import json
import os

filepath = 'content/courses/flutter/course.json'

with open(filepath, 'r') as f:
    data = json.load(f)

# Module 13 Updates
module_13_updates = {
    "13.2": {
        "Provider Types": """Riverpod offers several provider types to handle different state scenarios:
- **Provider**: Ideal for static values or simple dependency injection.
- **StateProvider**: Use this for simple state like toggles or counters.
- **FutureProvider**: Perfect for asynchronous operations like API calls.
- **StreamProvider**: Great for real-time data streams like Firebase or WebSockets.
- **NotifierProvider**: The go-to for complex state logic that requires methods to modify state."""
    },
    "13.3": {
        "Building a Todo Notifier": """A `Notifier` is a class that holds state and methods to modify it. Unlike `StateProvider`, it allows you to encapsulate business logic.
Here we define `TodosNotifier` which extends `Notifier<TodosState>`. The `build()` method initializes the state. Methods like `addTodo` and `toggleTodo` modify the state immutably.""",
        "Using the Notifier": """To use the notifier in your UI, use `ref.watch(todosProvider)` to listen for changes and rebuild the widget.
To call methods on the notifier, use `ref.read(todosProvider.notifier)`. This gives you access to the class instance without listening to state changes."""
    },
    "13.4": {
        "Generated Providers": """The `@riverpod` annotation (from `riverpod_annotation` package) simplifies provider creation.
It automatically determines the provider type based on your function signature.
- If you return a value, it creates a `Provider` or `FutureProvider`.
- If you return a class extending `_$ClassName`, it creates a `NotifierProvider`."""
    },
    "13.5": {
        "Using .when()": """`AsyncValue` forces you to handle all three possible states of asynchronous data:
1. **data**: When the data is successfully loaded.
2. **loading**: When the operation is in progress.
3. **error**: When an exception occurs.
This ensures your UI never crashes due to unhandled loading or error states.""",
        "Pattern Matching Alternative": """Dart 3 introduced pattern matching, which provides a more concise syntax for handling `AsyncValue`.
Using a switch expression, you can destruct the `AsyncValue` to access `value` or `error` directly, making the code cleaner and more readable."""
    },
    "13.6": {
        "Before Hooks (StatefulWidget)": """Using `StatefulWidget` requires significant boilerplate:
- Defining a State class.
- Initializing controllers in `initState`.
- Disposing controllers in `dispose`.
- Managing `FocusNodes` and other resources manually.""",
        "After Hooks (HookWidget)": """With `flutter_hooks`, the same logic is condensed into a `HookWidget`.
- `useTextEditingController` automatically handles creation and disposal.
- No `initState` or `dispose` methods needed.
- Code is linear and easier to read."""
    },
    "13.7": {
        "Search with Debounce": """This example demonstrates the power of combining Riverpod and Hooks.
- We use **Hooks** (`useTextEditingController`, `useDebounced`) for local UI state that doesn't need to be global.
- We use **Riverpod** (`searchProductsProvider`) to fetch and cache the search results based on the query.
This separation of concerns keeps your app architecture clean."""
    },
    "13.8": {
        "Project Structure": """A scalable folder structure is key for larger apps.
- **models**: Data classes (e.g., Freezed classes).
- **providers**: Riverpod providers and notifiers.
- **screens**: UI pages.
- **widgets**: Reusable UI components.
This organization helps separate data, logic, and UI.""",
        "Core Providers": """Here we define the core state of our Todo app.
- `Todo`: An immutable data class generated with Freezed.
- `Todos`: A `Notifier` that manages the list of todos with methods for CRUD operations.
- `Filter`: A simple `Notifier` to track the current filter state.
- `filteredTodos`: A computed provider that filters the list based on the current filter selection."""
    }
}

count = 0
for module in data['modules']:
    if module['id'] == 'module-13':
        for lesson in module['lessons']:
            lesson_id = lesson['id']
            if lesson_id in module_13_updates:
                updates = module_13_updates[lesson_id]
                if 'contentSections' in lesson:
                    for section in lesson['contentSections']:
                        title = section['title']
                        if title in updates:
                            # Only update if content is empty or very short
                            current_content = section.get('content', '').strip()
                            if len(current_content) < 10:
                                section['content'] = updates[title]
                                print(f"Updated Lesson {lesson_id} - Section '{title}'")
                                count += 1

print(f"Total updates applied: {count}")

with open(filepath, 'w') as f:
    json.dump(data, f, indent=2)
