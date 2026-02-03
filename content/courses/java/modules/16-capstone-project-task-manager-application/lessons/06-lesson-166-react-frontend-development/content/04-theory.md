---
type: "THEORY"
title: "Thymeleaf Path: Building the Task Manager Pages"
---

THYMELEAF PATH (continued)

Now let us build the actual Task Manager pages. We need a task list page, a task form page (for create and edit), and Spring MVC controllers to serve them.

Task List Page (templates/tasks/list.html):
```html
<!DOCTYPE html>
<html xmlns:th="http://www.thymeleaf.org">
<head th:replace="~{fragments/layout :: head('My Tasks')}">
    <title>My Tasks</title>
</head>
<body class="bg-gray-100">
    <nav th:replace="~{fragments/layout :: navbar}"></nav>

    <main class="max-w-6xl mx-auto p-6">
        <div class="flex justify-between items-center mb-6">
            <h1 class="text-2xl font-bold">My Tasks</h1>
            <a th:href="@{/tasks/new}"
               class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                + New Task
            </a>
        </div>

        <!-- Flash messages -->
        <div th:if="${successMessage}"
             class="bg-green-100 border border-green-400 text-green-700 px-4 py-3 rounded mb-4">
            <span th:text="${successMessage}">Success</span>
        </div>

        <!-- Task cards -->
        <div th:if="${#lists.isEmpty(tasks)}" class="text-center py-12 text-gray-500">
            <p>No tasks yet. Create your first task to get started!</p>
        </div>

        <div class="grid gap-4 md:grid-cols-2 lg:grid-cols-3">
            <div th:each="task : ${tasks}"
                 th:classappend="${task.priority.name() == 'URGENT'} ? 'border-red-500' :
                                 (${task.priority.name() == 'HIGH'} ? 'border-orange-500' : 'border-gray-200')"
                 class="bg-white rounded-lg shadow p-4 border-l-4">

                <div class="flex justify-between items-start">
                    <h3 class="font-semibold" th:text="${task.title}">Task Title</h3>
                    <span th:text="${task.priority}"
                          th:classappend="${task.priority.name() == 'URGENT'} ? 'bg-red-100 text-red-800' : 'bg-gray-100'"
                          class="text-xs px-2 py-1 rounded">MEDIUM</span>
                </div>

                <p th:if="${task.description}" th:text="${task.description}"
                   class="text-gray-600 text-sm mt-2">Description</p>

                <div class="flex justify-between items-center mt-4 text-sm">
                    <span th:if="${task.category}" th:text="${task.category.name}"
                          class="text-blue-600">Category</span>
                    <span th:if="${task.dueDate}" th:text="${#temporals.format(task.dueDate, 'MMM dd, yyyy')}"
                          th:classappend="${task.dueDate.isBefore(T(java.time.LocalDate).now()) and task.status.name() != 'COMPLETED'} ? 'text-red-600 font-bold' : 'text-gray-500'">
                        Due Date
                    </span>
                </div>

                <div class="flex gap-2 mt-4">
                    <a th:href="@{/tasks/{id}/edit(id=${task.id})}"
                       class="text-blue-600 hover:underline text-sm">Edit</a>
                    <form th:action="@{/tasks/{id}/delete(id=${task.id})}" method="post" class="inline">
                        <button type="submit" class="text-red-600 hover:underline text-sm"
                                onclick="return confirm('Delete this task?')">Delete</button>
                    </form>
                </div>
            </div>
        </div>
    </main>

    <footer th:replace="~{fragments/layout :: footer}"></footer>
</body>
</html>
```

Spring MVC Controller for Thymeleaf Views:
```java
@Controller
@RequestMapping("/tasks")
public class TaskViewController {

    private final TaskService taskService;
    private final CategoryService categoryService;

    public TaskViewController(TaskService taskService,
                              CategoryService categoryService) {
        this.taskService = taskService;
        this.categoryService = categoryService;
    }

    @GetMapping
    public String listTasks(@AuthenticationPrincipal User user, Model model) {
        var tasks = taskService.getTasksForUser(user);
        model.addAttribute("tasks", tasks);
        return "tasks/list";
    }

    @GetMapping("/new")
    public String showCreateForm(@AuthenticationPrincipal User user, Model model) {
        model.addAttribute("taskForm", new TaskRequest());
        model.addAttribute("categories", categoryService.getCategoriesForUser(user));
        return "tasks/form";
    }

    @PostMapping
    public String createTask(@AuthenticationPrincipal User user,
                             @Valid @ModelAttribute("taskForm") TaskRequest request,
                             BindingResult bindingResult,
                             Model model,
                             RedirectAttributes redirectAttributes) {
        if (bindingResult.hasErrors()) {
            model.addAttribute("categories", categoryService.getCategoriesForUser(user));
            return "tasks/form";
        }
        taskService.createTask(request, user);
        redirectAttributes.addFlashAttribute("successMessage", "Task created successfully!");
        return "redirect:/tasks";
    }

    @GetMapping("/{id}/edit")
    public String showEditForm(@PathVariable Long id,
                               @AuthenticationPrincipal User user,
                               Model model) {
        var task = taskService.getTask(id, user);
        model.addAttribute("taskForm", TaskRequest.fromTask(task));
        model.addAttribute("categories", categoryService.getCategoriesForUser(user));
        model.addAttribute("taskId", id);
        return "tasks/form";
    }

    @PostMapping("/{id}")
    public String updateTask(@PathVariable Long id,
                             @AuthenticationPrincipal User user,
                             @Valid @ModelAttribute("taskForm") TaskRequest request,
                             BindingResult bindingResult,
                             Model model,
                             RedirectAttributes redirectAttributes) {
        if (bindingResult.hasErrors()) {
            model.addAttribute("categories", categoryService.getCategoriesForUser(user));
            model.addAttribute("taskId", id);
            return "tasks/form";
        }
        taskService.updateTask(id, request, user);
        redirectAttributes.addFlashAttribute("successMessage", "Task updated successfully!");
        return "redirect:/tasks";
    }

    @PostMapping("/{id}/delete")
    public String deleteTask(@PathVariable Long id,
                             @AuthenticationPrincipal User user,
                             RedirectAttributes redirectAttributes) {
        taskService.deleteTask(id, user);
        redirectAttributes.addFlashAttribute("successMessage", "Task deleted.");
        return "redirect:/tasks";
    }
}
```

Key patterns:
- @Controller (not @RestController) returns view names, not JSON
- @ModelAttribute binds form data to Java objects
- BindingResult captures validation errors (must follow @Valid parameter)
- RedirectAttributes.addFlashAttribute passes messages across redirects
- Post-Redirect-Get pattern prevents duplicate form submissions
