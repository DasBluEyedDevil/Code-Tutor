# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** C# Programming (csharp)
- **Module:** Building Interactive UIs with Blazor
- **Lesson:** Event Handling (@onclick, @onchange) (ID: lesson-13-05)
- **Difficulty:** advanced
- **Estimated Time:** 15 minutes

## Current Lesson Content

{
    "id":  "lesson-13-05",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "Understanding the Concept",
                                "content":  "Events are like doorbells:\n\nUser clicks button = Someone rings doorbell\nYour C# method runs = You answer the door\n\nBlazor events:\n• @onclick - Button clicks, div clicks\n• @onchange - Input value changes\n• @oninput - Every keystroke\n• @onsubmit - Form submission\n• @onmouseover - Mouse hover\n\nEvent handling:\n1. User interacts (click, type, hover)\n2. Browser fires event\n3. Blazor calls your C# method\n4. Method updates state\n5. UI re-renders automatically!\n\nThink: Events = \u0027User does something → Your C# code responds!\u0027"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example",
                                "content":  "This example demonstrates the concepts in action.",
                                "code":  "// CLICK EVENTS\n\u003cbutton @onclick=\"HandleClick\"\u003eClick Me\u003c/button\u003e\n\u003cbutton @onclick=\"() =\u003e count++\"\u003eIncrement\u003c/button\u003e\n\u003cbutton @onclick=\"@(() =\u003e DoWork(5))\"\u003ePass Parameter\u003c/button\u003e\n\n@code {\n    private void HandleClick() {\n        Console.WriteLine(\"Button clicked!\");\n    }\n    \n    private void DoWork(int value) {\n        Console.WriteLine($\"Working with {value}\");\n    }\n}\n\n// CHANGE EVENTS\n\u003cinput @onchange=\"HandleNameChange\" /\u003e\n\u003cselect @onchange=\"HandleCategoryChange\"\u003e\n    \u003coption\u003eCategory 1\u003c/option\u003e\n\u003c/select\u003e\n\n@code {\n    private void HandleNameChange(ChangeEventArgs e) {\n        string newValue = e.Value.ToString();\n        Console.WriteLine($\"Name changed to: {newValue}\");\n    }\n}\n\n// INPUT EVENTS (every keystroke)\n\u003cinput @oninput=\"HandleInput\" /\u003e\n\u003cp\u003eYou typed: @currentInput\u003c/p\u003e\n\n@code {\n    private string currentInput = \"\";\n    \n    private void HandleInput(ChangeEventArgs e) {\n        currentInput = e.Value.ToString();\n    }\n}\n\n// KEYBOARD EVENTS\n\u003cinput @onkeydown=\"HandleKeyDown\" @onkeyup=\"HandleKeyUp\" /\u003e\n\n@code {\n    private void HandleKeyDown(KeyboardEventArgs e) {\n        if (e.Key == \"Enter\") {\n            Console.WriteLine(\"Enter pressed!\");\n        }\n    }\n}\n\n// MOUSE EVENTS\n\u003cdiv @onmouseover=\"() =\u003e isHovered = true\" \n     @onmouseout=\"() =\u003e isHovered = false\"\n     class=\"@(isHovered ? \\\"highlight\\\" : \\\"\\\")\"\u003e\n    Hover over me!\n\u003c/div\u003e\n\n// FORM SUBMIT\n\u003cEditForm Model=\"@person\" OnValidSubmit=\"HandleSubmit\"\u003e\n    \u003cbutton type=\"submit\"\u003eSubmit\u003c/button\u003e\n\u003c/EditForm\u003e\n\n@code {\n    private Person person = new();\n    \n    private void HandleSubmit() {\n        Console.WriteLine(\"Form submitted!\");\n    }\n}",
                                "language":  "csharp"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Syntax Breakdown",
                                "content":  "## Breaking Down the Syntax\n\n**`@onclick=\"MethodName\"`**: Calls C# method on click. No parentheses if no parameters. For lambda: @onclick=\"() =\u003e code\". Event fires, method runs, UI updates.\n\n**`ChangeEventArgs`**: Event argument object. Use e.Value for new value. Different types: KeyboardEventArgs, MouseEventArgs, FocusEventArgs, etc.\n\n**`@oninput vs @onchange`**: @oninput fires on every keystroke. @onchange fires when input loses focus (blur). Use @oninput for live updates, @onchange for final value.\n\n**`Event with parameters`**: Use lambda to pass parameters: @onclick=\"() =\u003e Delete(item.Id)\". Or: @onclick=\"@(() =\u003e Process(item))\". Lambda wraps method call."
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "lesson-13-05-challenge-01",
                           "title":  "Practice Challenge",
                           "description":  "Apply what you\u0027ve learned in this interactive coding challenge.",
                           "instructions":  "Create an interactive task manager with events!\n\n1. TaskManager component:\n   - Input for task name (@oninput updates as you type)\n   - Button \"Add Task\" (@onclick)\n   - List of tasks\n   - Each task has:\n     - Checkbox (@onchange to mark complete)\n     - Delete button (@onclick)\n   - Show count of total and completed tasks\n\n2. Events to handle:\n   - Input change (live preview)\n   - Add task (button click)\n   - Toggle complete (checkbox change)\n   - Delete task (button click)\n\n3. Print component structure!",
                           "starterCode":  "// TaskManager.razor\n\u003cdiv class=\"task-manager\"\u003e\n    \u003ch3\u003eTask Manager\u003c/h3\u003e\n    \n    \u003cdiv class=\"input-group\"\u003e\n        \u003cinput \n            @oninput=\"HandleTaskInput\" \n            @onkeydown=\"HandleKeyPress\"\n            placeholder=\"Enter task name\" /\u003e\n        \u003cbutton @onclick=\"AddTask\"\u003eAdd Task\u003c/button\u003e\n    \u003c/div\u003e\n    \n    \u003cp\u003ePreview: @currentTaskName\u003c/p\u003e\n    \n    \u003cul\u003e\n        @foreach (var task in tasks)\n        {\n            \u003cli\u003e\n                \u003cinput \n                    type=\"checkbox\" \n                    checked=\"@task.IsCompleted\"\n                    @onchange=\"() =\u003e ToggleTask(task.Id)\" /\u003e\n                \u003cspan class=\"@(task.IsCompleted ? \\\"completed\\\" : \\\"\\\")\"\u003e@task.Name\u003c/span\u003e\n                \u003cbutton @onclick=\"() =\u003e DeleteTask(task.Id)\"\u003eDelete\u003c/button\u003e\n            \u003c/li\u003e\n        }\n    \u003c/ul\u003e\n    \n    \u003cp\u003eTotal: @tasks.Count | Completed: @tasks.Count(t =\u003e t.IsCompleted)\u003c/p\u003e\n\u003c/div\u003e\n\n@code {\n    // Implement task management\n}",
                           "solution":  "Console.WriteLine(@\"\n// TaskManager.razor\nusing System;\nusing System.Collections.Generic;\nusing System.Linq;\n\n\u003cdiv class=\"\"task-manager\"\"\u003e\n    \u003ch3\u003e📝 Task Manager\u003c/h3\u003e\n    \n    \u003cdiv class=\"\"input-group mb-3\"\"\u003e\n        \u003cinput \n            class=\"\"form-control\"\"\n            @bind=\"\"currentTaskName\"\"\n            @onkeydown=\"\"HandleKeyPress\"\"\n            placeholder=\"\"Enter task name\"\" /\u003e\n        \u003cbutton class=\"\"btn btn-primary\"\" @onclick=\"\"AddTask\"\"\u003eAdd Task\u003c/button\u003e\n    \u003c/div\u003e\n    \n    \u003cdiv class=\"\"alert alert-info\"\"\u003e\n        Preview: @(string.IsNullOrEmpty(currentTaskName) ? \"\"(empty)\"\" : currentTaskName)\n    \u003c/div\u003e\n    \n    \u003cul class=\"\"list-group\"\"\u003e\n        @foreach (var task in tasks)\n        {\n            \u003cli class=\"\"list-group-item\"\"\u003e\n                \u003cinput \n                    type=\"\"checkbox\"\" \n                    checked=\"\"@task.IsCompleted\"\"\n                    @onchange=\"\"() =\u003e ToggleTask(task.Id)\"\" /\u003e\n                \u003cspan class=\"\"@(task.IsCompleted ? \\\"\"text-decoration-line-through\\\"\" : \"\"\"\")\"\"\u003e\n                    @task.Name\n                \u003c/span\u003e\n                \u003cbutton \n                    class=\"\"btn btn-sm btn-danger float-end\"\" \n                    @onclick=\"\"() =\u003e DeleteTask(task.Id)\"\"\u003eDelete\u003c/button\u003e\n            \u003c/li\u003e\n        }\n    \u003c/ul\u003e\n    \n    \u003cdiv class=\"\"mt-3\"\"\u003e\n        \u003cstrong\u003eTotal: @tasks.Count\u003c/strong\u003e | \n        \u003cstrong\u003eCompleted: @tasks.Count(t =\u003e t.IsCompleted)\u003c/strong\u003e |\n        \u003cstrong\u003eRemaining: @tasks.Count(t =\u003e !t.IsCompleted)\u003c/strong\u003e\n    \u003c/div\u003e\n\u003c/div\u003e\n\n@code {\n    private class TaskItem {\n        public int Id { get; set; }\n        public string Name { get; set; } = \"\"\"\";\n        public bool IsCompleted { get; set; }\n    }\n    \n    private List\u003cTaskItem\u003e tasks = new();\n    private string currentTaskName = \"\"\"\";\n    private int nextId = 1;\n    \n    private void AddTask() {\n        if (!string.IsNullOrWhiteSpace(currentTaskName)) {\n            tasks.Add(new TaskItem { \n                Id = nextId++, \n                Name = currentTaskName, \n                IsCompleted = false \n            });\n            currentTaskName = \"\"\"\";\n            Console.WriteLine($\"\"Task added! Total: {tasks.Count}\"\");\n        }\n    }\n    \n    private void HandleKeyPress(KeyboardEventArgs e) {\n        if (e.Key == \"\"Enter\"\") {\n            AddTask();\n        }\n    }\n    \n    private void ToggleTask(int taskId) {\n        var task = tasks.FirstOrDefault(t =\u003e t.Id == taskId);\n        if (task != null) {\n            task.IsCompleted = !task.IsCompleted;\n            Console.WriteLine($\"\"{task.Name}: {(task.IsCompleted ? \\\"\"Completed\\\"\" : \\\"\"Incomplete\\\"\")}\"\");\n        }\n    }\n    \n    private void DeleteTask(int taskId) {\n        var task = tasks.FirstOrDefault(t =\u003e t.Id == taskId);\n        if (task != null) {\n            tasks.Remove(task);\n            Console.WriteLine($\"\"Deleted: {task.Name}\"\");\n        }\n    }\n}\n\"\");\n\nConsole.WriteLine(@\"\n=== EVENTS DEMONSTRATED ===\");\nConsole.WriteLine(\"✓ @bind - Two-way binding for input\");\nConsole.WriteLine(\"✓ @onkeydown - Enter key to add task\");\nConsole.WriteLine(\"✓ @onclick - Add and Delete buttons\");\nConsole.WriteLine(\"✓ @onchange - Checkbox toggle\");\nConsole.WriteLine(\"✓ Lambda with parameters: () =\u003e DeleteTask(id)\");\nConsole.WriteLine(\"\\n✓ All events → C# methods → UI updates!\");",
                           "language":  "csharp",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Output should contain \"TaskManager\"",
                                                 "expectedOutput":  "TaskManager",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Output should contain \"@onclick\"",
                                                 "expectedOutput":  "@onclick",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-3",
                                                 "description":  "Output should contain \"@onchange\"",
                                                 "expectedOutput":  "@onchange",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-4",
                                                 "description":  "Output should contain \"@bind\"",
                                                 "expectedOutput":  "@bind",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-5",
                                                 "description":  "Output should contain \"ToggleTask\"",
                                                 "expectedOutput":  "ToggleTask",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-6",
                                                 "description":  "Output should contain \"DeleteTask\"",
                                                 "expectedOutput":  "DeleteTask",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "@onclick for buttons. @onchange for checkbox. @bind for two-way input binding. @onkeydown to detect Enter key. Use lambdas to pass parameters: () =\u003e Method(id)."
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Event propagation: @onclick on parent and child? Both fire! Use @onclick:stopPropagation to prevent bubbling. Or handle in only one place."
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Async event handlers: Can use async methods! \u0027@onclick=\"HandleClickAsync\"\u0027 with \u0027private async Task HandleClickAsync()\u0027. Blazor awaits automatically."
                                         },
                                         {
                                             "level":  4,
                                             "text":  "Event args: Different events have different args! ChangeEventArgs for input/select, MouseEventArgs for mouse, KeyboardEventArgs for keyboard. Check e.Key, e.Button, etc."
                                         },
                                         {
                                             "level":  5,
                                             "text":  "Lambda scope: Variables in lambda must be in scope! \u0027@onclick=\"() =\u003e Delete(item.Id)\"\u0027 in foreach works. Outside foreach, \u0027item\u0027 not available!"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Event propagation",
                                                      "consequence":  "@onclick on parent and child? Both fire! Use @onclick:stopPropagation to prevent bubbling. Or handle in only one place.",
                                                      "correction":  "Review the correct syntax and best practices for this concept."
                                                  },
                                                  {
                                                      "mistake":  "Async event handlers",
                                                      "consequence":  "Can use async methods! \u0027@onclick=\"HandleClickAsync\"\u0027 with \u0027private async Task HandleClickAsync()\u0027. Blazor awaits automatically.",
                                                      "correction":  "Review the correct syntax and best practices for this concept."
                                                  },
                                                  {
                                                      "mistake":  "Event args",
                                                      "consequence":  "Different events have different args! ChangeEventArgs for input/select, MouseEventArgs for mouse, KeyboardEventArgs for keyboard. Check e.Key, e.Button, etc.",
                                                      "correction":  "Review the correct syntax and best practices for this concept."
                                                  },
                                                  {
                                                      "mistake":  "Lambda scope",
                                                      "consequence":  "Variables in lambda must be in scope! \u0027@onclick=\"() =\u003e Delete(item.Id)\"\u0027 in foreach works. Outside foreach, \u0027item\u0027 not available!",
                                                      "correction":  "Review the correct syntax and best practices for this concept."
                                                  }
                                              ],
                           "difficulty":  "advanced"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Event Handling (@onclick, @onchange)",
    "estimatedMinutes":  15
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current csharp documentation
- Search the web for the latest csharp version and verify examples work with it
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
- Search for "csharp Event Handling (@onclick, @onchange) 2024 2025" to find latest practices
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
  "lessonId": "lesson-13-05",
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

