---
type: "KEY_POINT"
title: "Testing the Complete Flow"
---

Test from DATABASE → FRONTEND:

1. TEST DATABASE:
psql> SELECT * FROM tasks;
Should see tasks table

2. TEST REPOSITORY (Unit test):
@Test
void shouldFindTasksByUserId() {
    List<Task> tasks = taskRepository.findByUserId(1L);
    assertFalse(tasks.isEmpty());
}

3. TEST SERVICE (Unit test):
@Test
void shouldCreateTask() {
    Task task = new Task("Buy milk", "From store", user);
    Task saved = taskService.createTask(task, 1L);
    assertNotNull(saved.getId());
}

4. TEST API (Integration test or Postman):
POST http://localhost:8080/api/tasks
{
  "title": "Test task",
  "description": "Testing"
}
Expect: 201 Created

5. TEST FRONTEND:
Open index.html in browser
- Create task → Should appear in list
- Mark complete → Should show checkmark
- Delete → Should disappear

6. TEST ERROR CASES:
- No title → Should show error
- Server down → Should show 'cannot connect'
- Delete someone else's task → Should return 403