# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Exception Groups & Structured Concurrency
- **Lesson:** Structured Concurrency with asyncio.TaskGroup (ID: 17_03)
- **Difficulty:** advanced
- **Estimated Time:** 35 minutes

## Current Lesson Content

{
    "id":  "17_03",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "What is Structured Concurrency?",
                                "content":  "**Structured concurrency** is a design pattern where concurrent tasks have clear lifetimes tied to a scope.\n\n**The old way (asyncio.gather):**\n```python\nresults = await asyncio.gather(task1(), task2(), task3())\n# Problem: If task2 fails, what happens to task1 and task3?\n# Answer: They might keep running in the background!\n```\n\n**The new way (asyncio.TaskGroup - Python 3.11+):**\n```python\nasync with asyncio.TaskGroup() as tg:\n    tg.create_task(task1())\n    tg.create_task(task2())\n    tg.create_task(task3())\n# When the block exits, ALL tasks are guaranteed to be done\n# If any fail, all others are cancelled and errors are grouped\n```\n\n**Benefits:**\n- No orphaned background tasks\n- All errors collected in an ExceptionGroup\n- Clean cancellation on failure"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example",
                                "content":  "**Expected Output (all succeed):**\n```\nFetched: https://api.example.com/users\nFetched: https://api.example.com/posts\nFetched: https://api.example.com/comments\nAll fetches completed!\n```\n\n**Expected Output (one fails):**\n```\nFetched: https://api.example.com/users\nFetched: https://api.example.com/posts\nSome fetches failed:\n  - Connection error for https://api.example.com/bad\n```",
                                "code":  "import asyncio\n\nasync def fetch_url(url: str) -\u003e str:\n    await asyncio.sleep(0.1)  # Simulate network delay\n    if \"bad\" in url:\n        raise ConnectionError(f\"Connection error for {url}\")\n    print(f\"Fetched: {url}\")\n    return f\"Data from {url}\"\n\nasync def fetch_all(urls: list[str]):\n    try:\n        async with asyncio.TaskGroup() as tg:\n            tasks = [tg.create_task(fetch_url(url)) for url in urls]\n        \n        # Only reached if ALL tasks succeed\n        print(\"All fetches completed!\")\n        return [t.result() for t in tasks]\n    \n    except* ConnectionError as eg:\n        print(\"Some fetches failed:\")\n        for exc in eg.exceptions:\n            print(f\"  - {exc}\")\n        return None\n\n# Run with all good URLs\nasyncio.run(fetch_all([\n    \"https://api.example.com/users\",\n    \"https://api.example.com/posts\",\n    \"https://api.example.com/comments\"\n]))\n\n# Run with one bad URL\nasyncio.run(fetch_all([\n    \"https://api.example.com/users\",\n    \"https://api.example.com/posts\",\n    \"https://api.example.com/bad\"  # This will fail\n]))\n",
                                "language":  "python"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "TaskGroup vs gather",
                                "content":  "| Feature | asyncio.gather | asyncio.TaskGroup |\n|---------|----------------|-------------------|\n| Error handling | First error raised | All errors in ExceptionGroup |\n| Cancellation | Manual | Automatic on failure |\n| Task cleanup | Background tasks may linger | All tasks guaranteed done |\n| Syntax | `await gather(...)` | `async with TaskGroup()` |\n| Python version | 3.4+ | 3.11+ |\n\n**When to use TaskGroup:**\n- Multiple concurrent operations that should succeed or fail together\n- When you need all errors, not just the first\n- When clean cancellation is important\n\n**When to use gather:**\n- Legacy code (pre-3.11)\n- When you need `return_exceptions=True` behavior"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **`asyncio.TaskGroup`** provides structured concurrency (Python 3.11+)\n- Use as context manager: `async with asyncio.TaskGroup() as tg:`\n- Create tasks with `tg.create_task(coroutine)`\n- All tasks complete or cancel together\n- Errors are collected in an ExceptionGroup\n- Use `except*` to handle errors from failed tasks"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "17_03-challenge-1",
                           "title":  "Practice Exercise",
                           "description":  "Convert this gather-based code to use TaskGroup.\n\n**Original:**\n```python\nresults = await asyncio.gather(\n    process_item(1),\n    process_item(2),\n    process_item(3)\n)\n```",
                           "instructions":  "Rewrite using asyncio.TaskGroup.",
                           "starterCode":  "import asyncio\n\nasync def process_item(item_id: int) -\u003e str:\n    await asyncio.sleep(0.1)\n    return f\"Processed {item_id}\"\n\nasync def process_all():\n    async with asyncio.____() as tg:\n        t1 = tg.____(process_item(1))\n        t2 = tg.____(process_item(2))\n        t3 = tg.____(process_item(3))\n    \n    return [t1.result(), t2.result(), t3.result()]\n\nprint(asyncio.run(process_all()))\n",
                           "solution":  "import asyncio\n\nasync def process_item(item_id: int) -\u003e str:\n    await asyncio.sleep(0.1)\n    return f\"Processed {item_id}\"\n\nasync def process_all():\n    async with asyncio.TaskGroup() as tg:\n        t1 = tg.create_task(process_item(1))\n        t2 = tg.create_task(process_item(2))\n        t3 = tg.create_task(process_item(3))\n    \n    return [t1.result(), t2.result(), t3.result()]\n\nprint(asyncio.run(process_all()))\n",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Uses TaskGroup correctly",
                                                 "expectedOutput":  "Processed",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "**Hint:** Use `asyncio.TaskGroup()` and `tg.create_task(coroutine)`."
                                         }
                                     ],
                           "commonMistakes":  [

                                              ],
                           "difficulty":  "advanced"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Structured Concurrency with asyncio.TaskGroup",
    "estimatedMinutes":  35
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
- Search for "python Structured Concurrency with asyncio.TaskGroup 2024 2025" to find latest practices
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
  "lessonId": "17_03",
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

