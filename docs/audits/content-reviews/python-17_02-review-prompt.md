# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Exception Groups & Structured Concurrency
- **Lesson:** The except* Syntax - Selective Handling (ID: 17_02)
- **Difficulty:** advanced
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "17_02",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Understanding except*",
                                "content":  "The `except*` syntax (note the asterisk!) is designed specifically for handling ExceptionGroups.\n\n**Key difference from `except`:**\n- `except ValueError` - catches a single ValueError\n- `except* ValueError` - catches all ValueErrors **inside an ExceptionGroup**\n\n**How it works:**\n1. Python examines each exception in the group\n2. Matches are extracted and handled\n3. Non-matches are re-raised in a new ExceptionGroup\n4. Multiple `except*` blocks can each handle different types\n\n**Important:** `except*` and regular `except` cannot be mixed in the same try block."
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example",
                                "content":  "**Expected Output:**\n```\nHandling ValueError: Invalid email\nHandling ValueError: Invalid phone\nHandling TypeError: Expected string, got int\nUnhandled errors remain: 1 sub-exceptions\n```",
                                "code":  "# Using except* to handle different exception types\n\ndef process_data():\n    raise ExceptionGroup(\"Multiple errors\", [\n        ValueError(\"Invalid email\"),\n        ValueError(\"Invalid phone\"),\n        TypeError(\"Expected string, got int\"),\n        KeyError(\"Missing \u0027name\u0027 field\")\n    ])\n\ntry:\n    process_data()\nexcept* ValueError as eg:\n    # Handles ALL ValueErrors in the group\n    for exc in eg.exceptions:\n        print(f\"Handling ValueError: {exc}\")\nexcept* TypeError as eg:\n    # Handles ALL TypeErrors in the group\n    for exc in eg.exceptions:\n        print(f\"Handling TypeError: {exc}\")\nexcept* KeyError as eg:\n    # KeyError is not handled here, so it propagates\n    print(f\"Unhandled errors remain: {len(eg.exceptions)} sub-exceptions\")\n",
                                "language":  "python"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Syntax Breakdown",
                                "content":  "**The `except*` rules:**\n\n```python\ntry:\n    risky_operation()\nexcept* ValueError as eg:      # eg is ALWAYS an ExceptionGroup\n    for exc in eg.exceptions:   # Even if only one ValueError was caught\n        handle(exc)\nexcept* (TypeError, KeyError):  # Catch multiple types\n    pass\n```\n\n**Key behaviors:**\n1. `eg` is always an ExceptionGroup, never a single exception\n2. Multiple `except*` blocks can match the same ExceptionGroup\n3. Unhandled exceptions are automatically re-raised\n4. You can use `raise` to re-raise within `except*`\n\n**What you CAN\u0027T do:**\n```python\ntry:\n    ...\nexcept* ValueError:  # except* here...\n    ...\nexcept KeyError:     # ...cannot mix with regular except\n    ...                # SyntaxError!\n```"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **`except*`** handles exceptions inside ExceptionGroups\n- The caught value is always an ExceptionGroup (even for one match)\n- Multiple `except*` blocks can handle different types from the same group\n- Unhandled exceptions are automatically re-raised\n- Cannot mix `except*` with regular `except` in the same try block"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "17_02-challenge-1",
                           "title":  "Practice Exercise",
                           "description":  "Handle an ExceptionGroup that contains both ConnectionError and TimeoutError exceptions differently.\n\n**Requirements:**\n- Print \u0027Retrying...\u0027 for each ConnectionError\n- Print \u0027Timeout!\u0027 for each TimeoutError",
                           "instructions":  "Use except* to handle ConnectionError and TimeoutError separately.",
                           "starterCode":  "def network_calls():\n    raise ExceptionGroup(\"Network failures\", [\n        ConnectionError(\"Server 1 unreachable\"),\n        TimeoutError(\"Server 2 timed out\"),\n        ConnectionError(\"Server 3 unreachable\"),\n    ])\n\ntry:\n    network_calls()\nexcept* ConnectionError as eg:\n    for exc in eg.exceptions:\n        print(f\"Retrying... ({exc})\")\nexcept* ____ as eg:\n    for exc in eg.exceptions:\n        print(f\"Timeout! ({exc})\")\n",
                           "solution":  "def network_calls():\n    raise ExceptionGroup(\"Network failures\", [\n        ConnectionError(\"Server 1 unreachable\"),\n        TimeoutError(\"Server 2 timed out\"),\n        ConnectionError(\"Server 3 unreachable\"),\n    ])\n\ntry:\n    network_calls()\nexcept* ConnectionError as eg:\n    for exc in eg.exceptions:\n        print(f\"Retrying... ({exc})\")\nexcept* TimeoutError as eg:\n    for exc in eg.exceptions:\n        print(f\"Timeout! ({exc})\")\n",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Handles both exception types",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "**Hint:** Use `except* TimeoutError` to catch timeout exceptions."
                                         }
                                     ],
                           "commonMistakes":  [

                                              ],
                           "difficulty":  "advanced"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "The except* Syntax - Selective Handling",
    "estimatedMinutes":  30
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
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
- Search for "python The except* Syntax - Selective Handling 2024 2025" to find latest practices
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
  "lessonId": "17_02",
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

