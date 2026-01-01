# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Asynchronous Python
- **Lesson:** Error Handling in Async (ID: 13_06)
- **Difficulty:** advanced
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "13_06",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "How Exceptions Propagate in Async Code",
                                "content":  "**Exceptions in async code work similarly to sync code:**\n\n```python\nasync def might_fail():\n    raise ValueError(\"Oops!\")\n\ntry:\n    await might_fail()\nexcept ValueError as e:\n    print(f\"Caught: {e}\")\n```\n\n**But with concurrent tasks, it\u0027s trickier:**\n\nWith `asyncio.gather()`, if one task fails:\n- By default, ALL tasks are cancelled\n- First exception is raised\n\n**Solution: `return_exceptions=True`**\n```python\nresults = await asyncio.gather(\n    task1(),\n    task2(),  # This might fail\n    task3(),\n    return_exceptions=True\n)\n# results = [result1, ValueError(...), result3]\n```\n\nNow exceptions are returned as values instead of raised!"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Handling Errors with TaskGroup and wait_for",
                                "content":  "**Python 3.11+ TaskGroup:**\n```python\nasync with asyncio.TaskGroup() as tg:\n    tg.create_task(task1())\n    tg.create_task(task2())\n# All tasks complete or all are cancelled on error\n```\n\n**Timeouts with wait_for:**\n```python\ntry:\n    result = await asyncio.wait_for(slow_task(), timeout=5.0)\nexcept asyncio.TimeoutError:\n    print(\"Task took too long!\")\n```",
                                "code":  "import asyncio\n\nasync def successful_task(name):\n    await asyncio.sleep(0.2)\n    return f\"{name} completed\"\n\nasync def failing_task(name):\n    await asyncio.sleep(0.1)\n    raise ValueError(f\"{name} failed!\")\n\nasync def slow_task():\n    await asyncio.sleep(5)  # Very slow\n    return \"Finally done\"\n\nasync def main():\n    print(\"=== gather() Default Behavior ===\")\n    try:\n        results = await asyncio.gather(\n            successful_task(\"Task1\"),\n            failing_task(\"Task2\"),\n            successful_task(\"Task3\")\n        )\n    except ValueError as e:\n        print(f\"  Caught exception: {e}\")\n        print(\"  (Other tasks were cancelled)\\n\")\n    \n    print(\"=== gather() with return_exceptions=True ===\")\n    results = await asyncio.gather(\n        successful_task(\"TaskA\"),\n        failing_task(\"TaskB\"),\n        successful_task(\"TaskC\"),\n        return_exceptions=True\n    )\n    \n    for i, result in enumerate(results):\n        if isinstance(result, Exception):\n            print(f\"  Task {i}: ERROR - {result}\")\n        else:\n            print(f\"  Task {i}: {result}\")\n    \n    print(\"\\n=== Timeout with wait_for() ===\")\n    try:\n        # This will timeout after 0.5 seconds\n        result = await asyncio.wait_for(\n            slow_task(),\n            timeout=0.5\n        )\n    except asyncio.TimeoutError:\n        print(\"  Task timed out after 0.5s!\")\n    \n    print(\"\\n=== Individual Error Handling ===\")\n    \n    async def safe_fetch(url):\n        \"\"\"Wrapper that handles errors gracefully\"\"\"\n        try:\n            await asyncio.sleep(0.1)\n            if \"bad\" in url:\n                raise ConnectionError(f\"Failed to connect to {url}\")\n            return {\"url\": url, \"data\": \"success\"}\n        except ConnectionError as e:\n            return {\"url\": url, \"error\": str(e)}\n    \n    urls = [\"good.com\", \"bad.com\", \"also-good.com\"]\n    results = await asyncio.gather(*[safe_fetch(url) for url in urls])\n    \n    for result in results:\n        if \"error\" in result:\n            print(f\"  FAIL: {result[\u0027url\u0027]} - {result[\u0027error\u0027]}\")\n        else:\n            print(f\"  OK: {result[\u0027url\u0027]}\")\n\nasyncio.run(main())",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "gather() with return_exceptions=True",
                                "content":  "**When to use `return_exceptions=True`:**\n\n1. **When you want ALL tasks to complete** (even if some fail)\n2. **When you need to handle each error individually**\n3. **When failures are expected** (like network requests)\n\n**Pattern for handling mixed results:**\n```python\nresults = await asyncio.gather(*tasks, return_exceptions=True)\n\nsuccesses = []\nerrors = []\n\nfor result in results:\n    if isinstance(result, Exception):\n        errors.append(result)\n    else:\n        successes.append(result)\n\nprint(f\"{len(successes)} succeeded, {len(errors)} failed\")\n```\n\n**Best practice:** Wrap individual tasks in try/except for graceful handling."
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "13_06-challenge-1",
                           "title":  "Handle Timeout in Async Operation",
                           "description":  "Use asyncio.wait_for to implement timeout handling for a slow operation.",
                           "instructions":  "Complete the `fetch_with_timeout` function that attempts to fetch data but times out after a specified duration. Return None if timeout occurs.",
                           "starterCode":  "import asyncio\n\nasync def slow_api_call(delay):\n    \"\"\"Simulates a slow API that takes \u0027delay\u0027 seconds\"\"\"\n    await asyncio.sleep(delay)\n    return {\"status\": \"success\", \"data\": \"result\"}\n\nasync def fetch_with_timeout(delay, timeout):\n    \"\"\"Fetch data with timeout. Return None if timeout occurs.\"\"\"\n    # TODO: Use asyncio.wait_for with timeout\n    # TODO: Handle TimeoutError and return None\n    pass\n\nasync def main():\n    # This should succeed (0.5s delay, 1s timeout)\n    result1 = await fetch_with_timeout(0.5, 1.0)\n    print(f\"Result 1: {result1}\")\n    \n    # This should timeout (2s delay, 0.5s timeout)\n    result2 = await fetch_with_timeout(2.0, 0.5)\n    print(f\"Result 2: {result2}\")\n\nasyncio.run(main())",
                           "solution":  "import asyncio\n\nasync def slow_api_call(delay):\n    \"\"\"Simulates a slow API that takes \u0027delay\u0027 seconds\"\"\"\n    print(f\"  API call starting (will take {delay}s)...\")\n    await asyncio.sleep(delay)\n    return {\"status\": \"success\", \"data\": \"result\"}\n\nasync def fetch_with_timeout(delay, timeout):\n    \"\"\"Fetch data with timeout. Return None if timeout occurs.\"\"\"\n    try:\n        result = await asyncio.wait_for(\n            slow_api_call(delay),\n            timeout=timeout\n        )\n        return result\n    except asyncio.TimeoutError:\n        print(f\"  Timeout after {timeout}s!\")\n        return None\n\nasync def main():\n    print(\"=== Test 1: Should succeed ===\")\n    result1 = await fetch_with_timeout(0.5, 1.0)\n    print(f\"Result 1: {result1}\\n\")\n    \n    print(\"=== Test 2: Should timeout ===\")\n    result2 = await fetch_with_timeout(2.0, 0.5)\n    print(f\"Result 2: {result2}\\n\")\n    \n    print(\"=== Test 3: Multiple with gather ===\")\n    results = await asyncio.gather(\n        fetch_with_timeout(0.2, 1.0),\n        fetch_with_timeout(0.3, 1.0),\n        fetch_with_timeout(5.0, 0.5),  # This will timeout\n    )\n    \n    for i, r in enumerate(results):\n        status = \"SUCCESS\" if r else \"TIMEOUT\"\n        print(f\"  Task {i+1}: {status}\")\n\nasyncio.run(main())",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use try/except asyncio.TimeoutError around asyncio.wait_for(coroutine, timeout=timeout)"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Catching generic Exception instead of TimeoutError",
                                                      "consequence":  "Hiding other errors",
                                                      "correction":  "Specifically catch asyncio.TimeoutError"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Error Handling in Async",
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
- Search for "python Error Handling in Async 2024 2025" to find latest practices
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
  "lessonId": "13_06",
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

