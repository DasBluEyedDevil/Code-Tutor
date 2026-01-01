# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** C# Programming (csharp)
- **Module:** Building Interactive UIs with Blazor
- **Lesson:** Component Parameters (Customization) (ID: lesson-13-04)
- **Difficulty:** advanced
- **Estimated Time:** 15 minutes

## Current Lesson Content

{
    "id":  "lesson-13-04",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "Understanding the Concept",
                                "content":  "Parameters are like function arguments for components!\n\nFunction with parameters:\nvoid Greet(string name, int age) { }\n\nComponent with parameters:\n\u003cPerson Name=\"Alice\" Age=\"30\" /\u003e\n\nParameters let you:\n✅ Customize component behavior\n✅ Pass data from parent to child\n✅ Make components reusable\n✅ Configure appearance/logic\n\nTypes of parameters:\n• Simple values (string, int, bool)\n• Complex objects (Product, Customer)\n• Collections (List\u003cT\u003e, arrays)\n• Event callbacks (EventCallback\u003cT\u003e)\n• RenderFragments (child content)\n\nThink: Parameters = \u0027The inputs that make your component flexible and reusable!\u0027"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example",
                                "content":  "This example demonstrates the concepts in action.",
                                "code":  "// SIMPLE PARAMETERS\n@code {\n    [Parameter]\n    public string Title { get; set; } = \"Default Title\";\n    \n    [Parameter]\n    public int Count { get; set; } = 0;\n    \n    [Parameter]\n    public bool IsVisible { get; set; } = true;\n}\n\n// COMPLEX OBJECT PARAMETER\npublic class Product {\n    public string Name { get; set; }\n    public decimal Price { get; set; }\n}\n\n@code {\n    [Parameter]\n    public Product ProductData { get; set; }\n}\n\u003cp\u003e@ProductData.Name: $@ProductData.Price\u003c/p\u003e\n\n// COLLECTION PARAMETER\n@code {\n    [Parameter]\n    public List\u003cstring\u003e Items { get; set; } = new();\n}\n@foreach (var item in Items) {\n    \u003cli\u003e@item\u003c/li\u003e\n}\n\n// EVENT CALLBACK PARAMETER\n@code {\n    [Parameter]\n    public EventCallback\u003cint\u003e OnValueChanged { get; set; }\n    \n    private async Task NotifyParent(int value) {\n        await OnValueChanged.InvokeAsync(value);\n    }\n}\n\n// CHILD CONTENT (RENDERFRAGMENT)\n@code {\n    [Parameter]\n    public RenderFragment ChildContent { get; set; }\n}\n\u003cdiv class=\"card\"\u003e\n    @ChildContent\n\u003c/div\u003e\n// Usage: \u003cCard\u003e\u003cp\u003eThis goes inside!\u003c/p\u003e\u003c/Card\u003e\n\n// CASCADING PARAMETERS\n\u003cCascadingValue Value=\"@currentUser\"\u003e\n    \u003cChildComponent /\u003e  // Receives currentUser automatically\n\u003c/CascadingValue\u003e\n\n@code {\n    [CascadingParameter]\n    public User CurrentUser { get; set; }\n}",
                                "language":  "csharp"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Syntax Breakdown",
                                "content":  "## Breaking Down the Syntax\n\n**`[Parameter] public Type Name { get; set; }`**: Standard parameter. Must be public with [Parameter] attribute. Can set default value with = \"default\". Passed from parent component.\n\n**`EventCallback\u003cT\u003e`**: Parameter for events. T is data type passed to parent. Use InvokeAsync(value) to trigger. EventCallback (no \u003cT\u003e) for no data.\n\n**`RenderFragment`**: Special parameter type for child content. Lets parent pass HTML/components as parameter. Name it \u0027ChildContent\u0027 for default slot.\n\n**`[CascadingParameter]`**: Receives value from CascadingValue ancestor. No need to pass through every level! Useful for themes, user context, etc."
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "lesson-13-04-challenge-01",
                           "title":  "Practice Challenge",
                           "description":  "Apply what you\u0027ve learned in this interactive coding challenge.",
                           "instructions":  "Build a flexible Button component with many parameters!\n\n1. CustomButton.razor:\n   - [Parameter] string Text\n   - [Parameter] string Color (\"primary\", \"danger\", \"success\")\n   - [Parameter] bool IsDisabled\n   - [Parameter] EventCallback OnClick\n   - Apply Bootstrap classes based on Color\n   - Disable if IsDisabled\n\n2. ButtonGroup.razor that uses CustomButton:\n   - Create 3 buttons: Save (success), Delete (danger), Cancel (primary)\n   - Track which button was clicked\n   - Display last action\n\n3. Demonstrate parameter flexibility\n\nPrint both components!",
                           "starterCode":  "// CustomButton.razor\n\u003cbutton \n    class=\"btn btn-@Color\" \n    disabled=\"@IsDisabled\"\n    @onclick=\"HandleClick\"\u003e\n    @Text\n\u003c/button\u003e\n\n@code {\n    [Parameter]\n    public string Text { get; set; } = \"Button\";\n    \n    [Parameter]\n    public string Color { get; set; } = \"primary\";\n    \n    [Parameter]\n    public bool IsDisabled { get; set; } = false;\n    \n    [Parameter]\n    public EventCallback OnClick { get; set; }\n    \n    private async Task HandleClick()\n    {\n        if (!IsDisabled)\n            await OnClick.InvokeAsync();\n    }\n}\n\n// ButtonGroup.razor\n\u003cdiv\u003e\n    \u003ch4\u003eActions\u003c/h4\u003e\n    \n    \u003cCustomButton \n        Text=\"Save\" \n        Color=\"success\" \n        OnClick=\"() =\u003e HandleAction(\\\"Save\\\")\" /\u003e\n    \n    \u003c!-- Add Delete and Cancel buttons --\u003e\n    \n    \u003cp class=\"mt-3\"\u003eLast Action: @lastAction\u003c/p\u003e\n\u003c/div\u003e\n\n@code {\n    private string lastAction = \"None\";\n    \n    private void HandleAction(string action)\n    {\n        lastAction = action;\n    }\n}",
                           "solution":  "using System;\n\nConsole.WriteLine(@\"\n=== CUSTOMBUTTON COMPONENT ===\");\nConsole.WriteLine(@\"\n// CustomButton.razor\n\u003cbutton \n    class=\"\"btn btn-@Color @(IsDisabled ? \\\"\"disabled\\\"\" : \"\"\"\")\"\" \n    disabled=\"\"@IsDisabled\"\"\n    @onclick=\"\"HandleClick\"\"\n    style=\"\"margin: 5px;\"\"\u003e\n    @Text\n\u003c/button\u003e\n\n@code {\n    [Parameter]\n    public string Text { get; set; } = \"\"Button\"\";\n    \n    [Parameter]\n    public string Color { get; set; } = \"\"primary\"\";\n    \n    [Parameter]\n    public bool IsDisabled { get; set; } = false;\n    \n    [Parameter]\n    public EventCallback\u003cstring\u003e OnClick { get; set; }\n    \n    private async Task HandleClick()\n    {\n        if (!IsDisabled)\n        {\n            Console.WriteLine($\"\"Button \u0027{Text}\u0027 clicked!\"\");\n            await OnClick.InvokeAsync(Text);\n        }\n    }\n}\n\"\");\n\nConsole.WriteLine(@\"\n=== BUTTONGROUP COMPONENT ===\");\nConsole.WriteLine(@\"\n// ButtonGroup.razor\n\u003cdiv class=\"\"button-group-demo\"\"\u003e\n    \u003ch4\u003eAction Buttons\u003c/h4\u003e\n    \n    \u003cdiv class=\"\"btn-group\"\"\u003e\n        \u003cCustomButton \n            Text=\"\"Save\"\" \n            Color=\"\"success\" \n            IsDisabled=\"\"false\"\n            OnClick=\"\"HandleAction\"\" /\u003e\n        \n        \u003cCustomButton \n            Text=\"\"Delete\"\" \n            Color=\"\"danger\" \n            IsDisabled=\"\"false\"\n            OnClick=\"\"HandleAction\"\" /\u003e\n        \n        \u003cCustomButton \n            Text=\"\"Cancel\"\" \n            Color=\"\"secondary\" \n            IsDisabled=\"\"false\"\n            OnClick=\"\"HandleAction\"\" /\u003e\n        \n        \u003cCustomButton \n            Text=\"\"Disabled\"\" \n            Color=\"\"primary\" \n            IsDisabled=\"\"true\"\n            OnClick=\"\"HandleAction\"\" /\u003e\n    \u003c/div\u003e\n    \n    \u003cdiv class=\"\"alert alert-info mt-3\"\"\u003e\n        \u003cstrong\u003eLast Action:\u003c/strong\u003e @lastAction\n    \u003c/div\u003e\n    \n    @if (actionCount \u003e 0)\n    {\n        \u003cp\u003e\u003csmall\u003eTotal actions: @actionCount\u003c/small\u003e\u003c/p\u003e\n    }\n\u003c/div\u003e\n\n@code {\n    private string lastAction = \"\"None\"\";\n    private int actionCount = 0;\n    \n    private void HandleAction(string buttonText)\n    {\n        lastAction = buttonText;\n        actionCount++;\n        Console.WriteLine($\"\"Action performed: {buttonText} (Total: {actionCount})\"\");\n    }\n}\n\"\");\n\nConsole.WriteLine(@\"\n=== PARAMETER BENEFITS ===\");\nConsole.WriteLine(\"✓ ONE CustomButton component\");\nConsole.WriteLine(\"✓ MULTIPLE instances with different:\");\nConsole.WriteLine(\"  - Text (\\\"Save\\\", \\\"Delete\\\", \\\"Cancel\\\")\");\nConsole.WriteLine(\"  - Color (success, danger, secondary)\");\nConsole.WriteLine(\"  - IsDisabled (true/false)\");\nConsole.WriteLine(\"  - OnClick (different actions)\");\nConsole.WriteLine(\"\\n✓ Reusability + Flexibility = Powerful components!\");",
                           "language":  "csharp",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Output should contain \"CustomButton\"",
                                                 "expectedOutput":  "CustomButton",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Output should contain \"ButtonGroup\"",
                                                 "expectedOutput":  "ButtonGroup",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-3",
                                                 "description":  "Output should contain \"[Parameter]\"",
                                                 "expectedOutput":  "[Parameter]",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-4",
                                                 "description":  "Output should contain \"Color\"",
                                                 "expectedOutput":  "Color",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-5",
                                                 "description":  "Output should contain \"IsDisabled\"",
                                                 "expectedOutput":  "IsDisabled",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-6",
                                                 "description":  "Output should contain \"OnClick\"",
                                                 "expectedOutput":  "OnClick",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "[Parameter] makes property configurable. EventCallback\u003cT\u003e for passing data back. Invoke with await callback.InvokeAsync(data). Bootstrap: btn-primary, btn-success, btn-danger."
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Required parameters: No built-in [Required] for Blazor parameters! Check in OnParametersSet(): \u0027if (string.IsNullOrEmpty(Name)) throw new ArgumentNullException();\u0027."
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Parameter change detection: OnParametersSet() called when parent changes parameters. Use to react: \u0027protected override void OnParametersSet() { UpdateData(); }\u0027."
                                         },
                                         {
                                             "level":  4,
                                             "text":  "Two-way binding: For @bind, need [Parameter] Value AND [Parameter] ValueChanged EventCallback\u003cT\u003e. Or use @bind-Value syntax. Naming convention matters!"
                                         },
                                         {
                                             "level":  5,
                                             "text":  "Default values: Set default in property initializer or constructor, NOT in OnInitialized! Property initializer: \u0027public string Color { get; set; } = \"primary\";\u0027."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Required parameters",
                                                      "consequence":  "No built-in [Required] for Blazor parameters! Check in OnParametersSet(): \u0027if (string.IsNullOrEmpty(Name)) throw new ArgumentNullException();\u0027.",
                                                      "correction":  "Review the correct syntax and best practices for this concept."
                                                  },
                                                  {
                                                      "mistake":  "Parameter change detection",
                                                      "consequence":  "OnParametersSet() called when parent changes parameters. Use to react: \u0027protected override void OnParametersSet() { UpdateData(); }\u0027.",
                                                      "correction":  "Review the correct syntax and best practices for this concept."
                                                  },
                                                  {
                                                      "mistake":  "Two-way binding",
                                                      "consequence":  "For @bind, need [Parameter] Value AND [Parameter] ValueChanged EventCallback\u003cT\u003e. Or use @bind-Value syntax. Naming convention matters!",
                                                      "correction":  "Review the correct syntax and best practices for this concept."
                                                  },
                                                  {
                                                      "mistake":  "Default values",
                                                      "consequence":  "Set default in property initializer or constructor, NOT in OnInitialized! Property initializer: \u0027public string Color { get; set; } = \"primary\";\u0027.",
                                                      "correction":  "Review the correct syntax and best practices for this concept."
                                                  }
                                              ],
                           "difficulty":  "advanced"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Component Parameters (Customization)",
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
- Search for "csharp Component Parameters (Customization) 2024 2025" to find latest practices
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
  "lessonId": "lesson-13-04",
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

