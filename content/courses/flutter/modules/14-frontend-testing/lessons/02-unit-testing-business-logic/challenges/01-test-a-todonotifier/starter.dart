// Given TodoNotifier:
class TodoNotifier extends Notifier<List<Todo>> {
  @override
  List<Todo> build() => [];

  void addTodo(String title) {
    state = [...state, Todo(id: uuid(), title: title)];
  }

  void toggleTodo(String id) {
    state = state.map((t) => t.id == id 
      ? t.copyWith(completed: !t.completed) 
      : t
    ).toList();
  }

  void removeTodo(String id) {
    state = state.where((t) => t.id != id).toList();
  }
}

// TODO: Write tests
void main() {
  // Add your tests here
}