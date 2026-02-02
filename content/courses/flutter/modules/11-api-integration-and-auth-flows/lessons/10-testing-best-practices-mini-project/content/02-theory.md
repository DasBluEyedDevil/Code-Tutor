---
type: "THEORY"
title: "Section 1: Project Requirements"
---


### Functional Requirements

**TaskMaster Pro** should allow users to:

1. **Task Management**
   - Create tasks with title, description, due date
   - Mark tasks as complete/incomplete
   - Delete tasks
   - Edit existing tasks

2. **Task Organization**
   - Filter tasks (All, Active, Completed)
   - Sort tasks (by date, by priority, by title)
   - Search tasks by title

3. **Data Persistence**
   - Save tasks locally using Hive
   - Load tasks on app startup
   - Maintain state across app restarts

4. **Statistics**
   - Show total tasks count
   - Show completed tasks percentage
   - Show overdue tasks count

### Testing Requirements

**You must implement:**

1. **Unit Tests** (70% of total tests)
   - Task model validation
   - Date utilities
   - Filtering and sorting logic
   - Statistics calculations
   - Repository operations

2. **Widget Tests** (20% of total tests)
   - Task list widget
   - Task item widget
   - Filter buttons
   - Add task form
   - Statistics widget

3. **Integration Tests** (10% of total tests)
   - Complete task creation flow
   - Complete task editing flow
   - Filter and search flow
   - Delete task flow

4. **Quality Requirements**
   - Minimum 80% code coverage
   - All tests must pass
   - Linting with no warnings
   - Formatted code (dart format)

