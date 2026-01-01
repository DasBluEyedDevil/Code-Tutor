# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Exception Groups & Structured Concurrency
- **Lesson:** Partial Failure Handling Patterns (ID: 17_04)
- **Difficulty:** advanced
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "17_04",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Real-World Partial Failures",
                                "content":  "In production systems, partial success is often acceptable:\n\n**Examples:**\n- Sending notifications to 1000 users - 5 fail, 995 succeed\n- Uploading 50 files - 2 timeout, 48 succeed\n- Validating 20 fields - 3 have errors, 17 are valid\n\n**The pattern:**\n1. Attempt all operations\n2. Collect successes and failures separately\n3. Report both to the caller\n4. Let the caller decide what to do\n\nThis is where ExceptionGroups truly shine."
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example",
                                "content":  "**Expected Output:**\n```\nSuccessfully sent to: [\u0027user1@email.com\u0027, \u0027user3@email.com\u0027]\nFailed to send to 2 recipients:\n  - user2@email.com: Mailbox full\n  - user4@email.com: Invalid address\n```",
                                "code":  "import asyncio\nfrom dataclasses import dataclass\n\n@dataclass\nclass EmailResult:\n    email: str\n    success: bool\n    error: Exception | None = None\n\nclass EmailError(Exception):\n    def __init__(self, email: str, reason: str):\n        self.email = email\n        super().__init__(f\"{email}: {reason}\")\n\nasync def send_email(email: str) -\u003e EmailResult:\n    await asyncio.sleep(0.1)\n    # Simulate some failures\n    if \"2\" in email:\n        raise EmailError(email, \"Mailbox full\")\n    if \"4\" in email:\n        raise EmailError(email, \"Invalid address\")\n    return EmailResult(email=email, success=True)\n\nasync def send_bulk_emails_safe(emails: list[str]):\n    \"\"\"Send emails with partial failure handling.\"\"\"\n    successes = []\n    failures = []\n    \n    async def safe_send(email: str):\n        try:\n            await send_email(email)\n            return (\"success\", email)\n        except EmailError as e:\n            return (\"failure\", e)\n    \n    async with asyncio.TaskGroup() as tg:\n        tasks = [tg.create_task(safe_send(email)) for email in emails]\n    \n    for task in tasks:\n        status, result = task.result()\n        if status == \"success\":\n            successes.append(result)\n        else:\n            failures.append(result)\n    \n    print(f\"Successfully sent to: {successes}\")\n    if failures:\n        print(f\"Failed to send to {len(failures)} recipients:\")\n        for err in failures:\n            print(f\"  - {err}\")\n    \n    return successes, failures\n\nasyncio.run(send_bulk_emails_safe([\n    \"user1@email.com\",\n    \"user2@email.com\",\n    \"user3@email.com\",\n    \"user4@email.com\"\n]))\n",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **Wrap individual operations** to catch failures without stopping the group\n- Return `(status, result)` tuples to distinguish success/failure\n- **Collect both successes and failures** separately\n- Let the **caller decide** how to handle partial success\n- Use dataclasses for structured error information"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "17_04-challenge-1",
                           "title":  "Practice Exercise",
                           "description":  "Implement a file processor that handles partial failures.\n\n**Requirements:**\n- Process a list of filenames\n- Some files may \u0027fail\u0027 (contain \u0027bad\u0027 in the name)\n- Return lists of successful and failed filenames",
                           "instructions":  "Create a partial failure handler for file processing.",
                           "starterCode":  "import asyncio\n\nasync def process_file(filename: str) -\u003e str:\n    if \"bad\" in filename:\n        raise ValueError(f\"Cannot process {filename}\")\n    return f\"Processed {filename}\"\n\nasync def process_files(filenames: list[str]):\n    successes = []\n    failures = []\n    \n    async def safe_process(filename):\n        try:\n            result = await process_file(filename)\n            return (\"success\", result)\n        except ValueError as e:\n            return (\"failure\", str(e))\n    \n    async with asyncio.TaskGroup() as tg:\n        tasks = [tg.____(safe_process(f)) for f in filenames]\n    \n    for task in tasks:\n        status, result = task.____\n        if status == \"success\":\n            successes.append(result)\n        else:\n            failures.append(result)\n    \n    return successes, failures\n",
                           "solution":  "import asyncio\n\nasync def process_file(filename: str) -\u003e str:\n    if \"bad\" in filename:\n        raise ValueError(f\"Cannot process {filename}\")\n    return f\"Processed {filename}\"\n\nasync def process_files(filenames: list[str]):\n    successes = []\n    failures = []\n    \n    async def safe_process(filename):\n        try:\n            result = await process_file(filename)\n            return (\"success\", result)\n        except ValueError as e:\n            return (\"failure\", str(e))\n    \n    async with asyncio.TaskGroup() as tg:\n        tasks = [tg.create_task(safe_process(f)) for f in filenames]\n    \n    for task in tasks:\n        status, result = task.result()\n        if status == \"success\":\n            successes.append(result)\n        else:\n            failures.append(result)\n    \n    return successes, failures\n",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Handles partial failures",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "**Hint:** Use `tg.create_task()` and `task.result()` to get the tuple."
                                         }
                                     ],
                           "commonMistakes":  [

                                              ],
                           "difficulty":  "advanced"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Partial Failure Handling Patterns",
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
- Search for "python Partial Failure Handling Patterns 2024 2025" to find latest practices
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
  "lessonId": "17_04",
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

