---
type: "THEORY"
title: "Thymeleaf Path: Forms, Objects, and Fragments"
---

THYMELEAF PATH (continued)

Form Handling with th:action, th:object, and th:field:
Thymeleaf provides powerful form binding that connects HTML forms directly to Java objects.

```html
<!-- templates/tasks/form.html -->
<form th:action="@{/tasks}" th:object="${taskForm}" method="post">
    <div>
        <label for="title">Title</label>
        <input type="text" th:field="*{title}" id="title"
               class="w-full border rounded px-3 py-2" />
        <p th:if="${#fields.hasErrors('title')}"
           th:errors="*{title}" class="text-red-500 text-sm"></p>
    </div>

    <div>
        <label for="description">Description</label>
        <textarea th:field="*{description}" id="description" rows="4"
                  class="w-full border rounded px-3 py-2"></textarea>
    </div>

    <div>
        <label for="priority">Priority</label>
        <select th:field="*{priority}" id="priority"
                class="w-full border rounded px-3 py-2">
            <option value="LOW">Low</option>
            <option value="MEDIUM">Medium</option>
            <option value="HIGH">High</option>
            <option value="URGENT">Urgent</option>
        </select>
    </div>

    <div>
        <label for="dueDate">Due Date</label>
        <input type="date" th:field="*{dueDate}" id="dueDate"
               class="w-full border rounded px-3 py-2" />
    </div>

    <div>
        <label for="categoryId">Category</label>
        <select th:field="*{categoryId}" id="categoryId"
                class="w-full border rounded px-3 py-2">
            <option value="">No Category</option>
            <option th:each="cat : ${categories}"
                    th:value="${cat.id}"
                    th:text="${cat.name}">Category Name</option>
        </select>
    </div>

    <button type="submit"
            class="bg-blue-600 text-white px-6 py-2 rounded hover:bg-blue-700">
        Save Task
    </button>
</form>
```

How form binding works:
- th:object="${taskForm}" binds the form to a Java object added to the model
- th:field="*{title}" generates name="title", id="title", and pre-fills the value
- th:errors="*{title}" displays validation error messages for that field
- th:action="@{/tasks}" sets the form's action URL

Fragments and Layouts:
Fragments let you reuse common HTML across pages. Define a layout template and include it everywhere.

```html
<!-- templates/fragments/layout.html -->
<!DOCTYPE html>
<html xmlns:th="http://www.thymeleaf.org">
<head th:fragment="head(title)">
    <meta charset="UTF-8" />
    <title th:text="${title}">Task Manager</title>
    <link th:href="@{/css/style.css}" rel="stylesheet" />
</head>
<body>
    <nav th:fragment="navbar">
        <div class="bg-blue-600 text-white p-4 flex justify-between items-center">
            <a href="/tasks" class="text-xl font-bold">Task Manager</a>
            <div>
                <span th:text="${#authentication.name}">user@email.com</span>
                <form th:action="@{/logout}" method="post" class="inline">
                    <button type="submit" class="ml-4 hover:underline">Logout</button>
                </form>
            </div>
        </div>
    </nav>

    <footer th:fragment="footer">
        <div class="text-center p-4 text-gray-500 text-sm">
            Task Manager Capstone Project
        </div>
    </footer>
</body>
</html>
```

Using fragments in pages:
```html
<!-- templates/tasks/list.html -->
<!DOCTYPE html>
<html xmlns:th="http://www.thymeleaf.org">
<head th:replace="~{fragments/layout :: head('My Tasks')}">
    <title>My Tasks</title>
</head>
<body>
    <nav th:replace="~{fragments/layout :: navbar}"></nav>

    <main class="max-w-6xl mx-auto p-6">
        <!-- Page content here -->
    </main>

    <footer th:replace="~{fragments/layout :: footer}"></footer>
</body>
</html>
```

th:replace="~{fragments/layout :: navbar}" replaces the entire element with the fragment content. th:insert would insert the fragment inside the element instead of replacing it.

th:classappend -- Conditionally add CSS classes:
```html
<div th:classappend="${task.priority == 'URGENT'} ? 'border-red-500' : 'border-gray-200'"
     class="border rounded p-4">
    <!-- task content -->
</div>
```

This is how you build dynamic styling without JavaScript -- the server decides which classes to apply based on the data.
