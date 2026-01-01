# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Loops
- **Lesson:** Loop Control: break, continue, and pass (ID: module-04-lesson-03)
- **Difficulty:** beginner
- **Estimated Time:** 23 minutes

## Current Lesson Content

{
    "id":  "module-04-lesson-03",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Understanding the Concept",
                                "content":  "Imagine you\u0027re searching through a stack of 100 papers for one specific document:\n\n\u003cul style=\u0027background-color: #f0f0f0; padding: 15px;\u0027\u003e- **Normal loop:** Check all 100 papers, even after finding the one you need (wasteful!)\n- **With break:** Stop immediately when you find it - paper #23? Done! Skip the remaining 77.\n\nOr imagine processing a list of test scores, but some entries are marked as \"invalid\":\n\n\u003cul style=\u0027background-color: #e3f2fd; padding: 15px;\u0027\u003e- **Normal loop:** Try to process every entry, causing errors on invalid ones\n- **With continue:** Skip invalid entries, continue with the next valid one\n\nThis is **loop control** - fine-tuning how loops execute with three special statements:\n\n- **break**: \"Stop the loop entirely and exit\"\n- **continue**: \"Skip the rest of this iteration and move to the next\"\n- **pass**: \"Do nothing, but don\u0027t leave this block empty\"\n\n### Real-World Examples:\n\n- **Password attempts (break)**:\nWHILE attempts \u003c 3:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Get password\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;IF correct → **break** (exit loop)\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;attempts++\n- **Processing records (continue)**:\nFOR each record:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;IF record is corrupted → **continue** (skip it)\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Process valid record\n- **Menu system (break)**:\nWHILE True:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Show menu\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Get choice\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;IF choice == \"quit\" → **break**\n- **Data filtering (continue)**:\nFOR each email:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;IF is_spam → **continue**\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Add to inbox\n\nThese statements give you surgical precision over loop execution!"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example",
                                "content":  "**Expected Output:**\n```\n=== Finding a Number (break) ===\nChecking 1\nChecking 2\nChecking 3\nChecking 4\nChecking 5\nFound 5! Stopping search.\nLoop ended\n\n=== Printing Odd Numbers (continue) ===\n1\n3\n5\n7\n9\n\n=== Password System (3 attempts) ===\nEnter password: wrong\n❌ Incorrect. 2 attempts remaining.\nEnter password: nope\n❌ Incorrect. 1 attempts remaining.\nEnter password: python123\n✓ Access granted!\n\n=== Processing Valid Scores ===\nSkipping invalid score: -1\nProcessing valid score: 85\nProcessing valid score: 92\nSkipping invalid score: 150\nProcessing valid score: 78\nSkipping invalid score: -5\nProcessing valid score: 88\n\nAverage of valid scores: 85.75\n\n=== Searching for a Value (with else) ===\n6 not found in the list\n\n=== Using pass ===\nProcessing 0\nProcessing 1\nProcessing 3\nProcessing 4\n\n=== Prime Number Checker ===\n17 is prime!\n\n=== Simple Menu ===\n1. Say Hello\n2. Say Goodbye\n3. Exit\nChoose an option: 1\nHello!\n\n1. Say Hello\n2. Say Goodbye\n3. Exit\nChoose an option: 3\nExiting program...\nProgram ended.\n```",
                                "code":  "# Loop Control: break, continue, and pass\n\n# Example 1: break - Exit Loop Early\nprint(\"=== Finding a Number (break) ===\")\n\nfor num in range(1, 11):\n    print(f\"Checking {num}\")\n    \n    if num == 5:\n        print(\"Found 5! Stopping search.\")\n        break  # Exit the loop immediately\n    \nprint(\"Loop ended\\n\")\n# Output: Checks 1-5, then stops\n\n# Example 2: continue - Skip Current Iteration\nprint(\"=== Printing Odd Numbers (continue) ===\")\n\nfor num in range(1, 11):\n    if num % 2 == 0:  # If even\n        continue  # Skip to next iteration\n    \n    print(num)  # Only prints odd numbers\n\nprint()\n\n# Example 3: Practical break - Password System\nprint(\"=== Password System (3 attempts) ===\")\n\ncorrect_password = \"python123\"\nattempts = 0\nmax_attempts = 3\n\nwhile attempts \u003c max_attempts:\n    password = input(\"Enter password: \")\n    attempts = attempts + 1\n    \n    if password == correct_password:\n        print(\"✓ Access granted!\")\n        break  # Stop asking for password\n    else:\n        remaining = max_attempts - attempts\n        if remaining \u003e 0:\n            print(f\"❌ Incorrect. {remaining} attempts remaining.\")\n\nif attempts == max_attempts and password != correct_password:\n    print(\"🔒 Account locked!\")\n\nprint()\n\n# Example 4: continue for Data Filtering\nprint(\"=== Processing Valid Scores ===\")\n\nscores = [85, -1, 92, 150, 78, -5, 88]  # Some invalid scores\n\ntotal = 0\nvalid_count = 0\n\nfor score in scores:\n    # Skip invalid scores (negative or \u003e 100)\n    if score \u003c 0 or score \u003e 100:\n        print(f\"Skipping invalid score: {score}\")\n        continue  # Skip to next iteration\n    \n    # Process valid score\n    print(f\"Processing valid score: {score}\")\n    total = total + score\n    valid_count = valid_count + 1\n\naverage = total / valid_count\nprint(f\"\\nAverage of valid scores: {average}\")\nprint()\n\n# Example 5: Loop with else Clause\nprint(\"=== Searching for a Value (with else) ===\")\n\nnumbers = [1, 3, 5, 7, 9]\nsearch_for = 6\n\nfor num in numbers:\n    if num == search_for:\n        print(f\"Found {search_for}!\")\n        break\nelse:\n    # Runs only if loop completed without break\n    print(f\"{search_for} not found in the list\")\n\nprint()\n\n# Example 6: pass - Placeholder\nprint(\"=== Using pass ===\")\n\nfor i in range(5):\n    if i == 2:\n        pass  # TODO: Add special handling later\n    else:\n        print(f\"Processing {i}\")\n\nprint()\n\n# Example 7: Prime Number Checker (break with else)\nprint(\"=== Prime Number Checker ===\")\n\nnumber = 17\n\nif number \u003c 2:\n    print(f\"{number} is not prime\")\nelse:\n    for i in range(2, number):\n        if number % i == 0:\n            print(f\"{number} is not prime (divisible by {i})\")\n            break\n    else:\n        # Only runs if loop completed without break\n        print(f\"{number} is prime!\")\n\nprint()\n\n# Example 8: Menu System with break\nprint(\"=== Simple Menu ===\")\n\nwhile True:  # Infinite loop\n    print(\"\\n1. Say Hello\")\n    print(\"2. Say Goodbye\")\n    print(\"3. Exit\")\n    \n    choice = input(\"Choose an option: \")\n    \n    if choice == \"1\":\n        print(\"Hello!\")\n    elif choice == \"2\":\n        print(\"Goodbye!\")\n    elif choice == \"3\":\n        print(\"Exiting program...\")\n        break  # Exit the infinite loop\n    else:\n        print(\"Invalid choice!\")\n\nprint(\"Program ended.\")",
                                "language":  "python"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Syntax Breakdown",
                                "content":  "### The break Statement:\n```\nfor item in sequence:\n    if condition:\n        break  # Exit loop immediately\n    # Rest of loop body\n# Code here runs after break\n\n```\n**What break does:**\n\n- Immediately exits the innermost loop\n- Skips any remaining iterations\n- Continues with code after the loop\n\n#### Visual Flow:\n```\nfor num in range(1, 6):\n    if num == 3:\n        break  # ← Exit here\n    print(num)\nprint(\"Done\")\n\n# Output:\n# 1\n# 2\n# Done\n# (Never prints 3, 4, 5 - loop exited early)\n\n```\n### The continue Statement:\n```\nfor item in sequence:\n    if condition:\n        continue  # Skip rest, go to next iteration\n    # This code is skipped when continue runs\n    process(item)\n\n```\n**What continue does:**\n\n- Skips remaining code in current iteration\n- Jumps to the next iteration\n- Loop continues normally\n\n#### Visual Flow:\n```\nfor num in range(1, 6):\n    if num == 3:\n        continue  # ← Skip when num is 3\n    print(num)\n\n# Output:\n# 1\n# 2\n# 4  (skipped 3!)\n# 5\n# (Loop continued, just skipped one iteration)\n\n```\n### The pass Statement:\n```\nfor item in sequence:\n    if condition:\n        pass  # Do nothing (placeholder)\n    else:\n        process(item)\n\n```\n**What pass does:**\n\n- Literally nothing - it\u0027s a placeholder\n- Used when syntax requires a statement but you have nothing to do\n- Common in early development (\"TODO: implement this later\")\n\n#### Example Uses:\n```\n# Placeholder for future code:\nfor item in data:\n    if item.needs_special_handling():\n        pass  # TODO: Add special handling\n    else:\n        process(item)\n\n# Empty function (syntax requires a body):\ndef coming_soon():\n    pass  # Will implement later\n\n# Empty if block:\nif condition:\n    pass  # Nothing to do for this case\nelse:\n    do_something()\n\n```\n### Loop else Clause (Advanced):\n```\nfor item in sequence:\n    if found_what_we_need:\n        break\nelse:\n    # Runs ONLY if loop completed without break\n    print(\"Didn\u0027t find it\")\n\n```\n**How it works:**\n\n- else block runs if loop finishes normally (no break)\n- else block is skipped if break was executed\n- Useful for search operations\n\n#### Example - Searching:\n```\nstudents = [\"Alice\", \"Bob\", \"Charlie\"]\nsearch_for = \"David\"\n\nfor student in students:\n    if student == search_for:\n        print(f\"Found {search_for}!\")\n        break\nelse:\n    # Only runs if we never broke\n    print(f\"{search_for} not in class\")\n\n# Output: David not in class\n\n```\n\u003ctable border=\u00271\u0027 cellpadding=\u00275\u0027 style=\u0027border-collapse: collapse;\u0027\u003e\u003ctr\u003e\u003cth\u003eScenario\u003c/th\u003e\u003cth\u003ebreak executed?\u003c/th\u003e\u003cth\u003eelse runs?\u003c/th\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eFound \"Alice\"\u003c/td\u003e\u003ctd\u003eYes\u003c/td\u003e\u003ctd\u003eNo\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eFound \"David\" (not in list)\u003c/td\u003e\u003ctd\u003eNo\u003c/td\u003e\u003ctd\u003eYes\u003c/td\u003e\u003c/tr\u003e\u003c/table\u003e### break vs continue vs return:\n\u003ctable border=\u00271\u0027 cellpadding=\u00275\u0027 style=\u0027border-collapse: collapse;\u0027\u003e\u003ctr\u003e\u003cth\u003eStatement\u003c/th\u003e\u003cth\u003eExits\u003c/th\u003e\u003cth\u003eEffect\u003c/th\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003e`break`\u003c/td\u003e\u003ctd\u003eLoop\u003c/td\u003e\u003ctd\u003eExit loop, continue after it\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003e`continue`\u003c/td\u003e\u003ctd\u003eCurrent iteration\u003c/td\u003e\u003ctd\u003eSkip to next iteration\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003e`return`\u003c/td\u003e\u003ctd\u003eFunction\u003c/td\u003e\u003ctd\u003eExit function entirely (Module 6)\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003e`pass`\u003c/td\u003e\u003ctd\u003eNothing\u003c/td\u003e\u003ctd\u003eDo nothing, continue normally\u003c/td\u003e\u003c/tr\u003e\u003c/table\u003e### Common Patterns:\n#### 1. Early Exit (break)\n```\n# Search and stop when found\nfor item in large_list:\n    if item == target:\n        print(\"Found it!\")\n        break  # No need to keep searching\n\n```\n#### 2. Skip Invalid Data (continue)\n```\n# Process only valid entries\nfor entry in data:\n    if not entry.is_valid():\n        continue  # Skip invalid entries\n    process(entry)  # Only valid ones reach here\n\n```\n#### 3. User-Controlled Loop (break)\n```\n# Infinite loop until user quits\nwhile True:\n    action = input(\"Command: \")\n    if action == \"quit\":\n        break\n    handle(action)\n\n```\n#### 4. Flag Alternative (else)\n```\n# Without else (old way):\nfound = False\nfor item in items:\n    if item == target:\n        found = True\n        break\nif not found:\n    print(\"Not found\")\n\n# With else (cleaner):\nfor item in items:\n    if item == target:\n        break\nelse:\n    print(\"Not found\")\n\n```\n### Common Mistakes:\n\n\u003cli\u003e**Using break/continue outside loops**:```\n# ERROR:\nif condition:\n    break  # SyntaxError! break only works in loops\n\n```\n\u003c/li\u003e\u003cli\u003e**Forgetting break in infinite loops**:```\n# INFINITE LOOP:\nwhile True:\n    print(\"Running forever!\")\n    # Forgot break statement!\n\n# CORRECT:\nwhile True:\n    if should_stop:\n        break\n\n```\n\u003c/li\u003e\u003cli\u003e**Confusing break with return**:```\ndef search(items):\n    for item in items:\n        if item == target:\n            break  # Exits loop, but stays in function\n    print(\"After loop\")  # This still runs\n    \n    # Use return to exit function:\n    for item in items:\n        if item == target:\n            return item  # Exits function entirely\n\n```\n\u003c/li\u003e"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **break** exits the loop immediately, skipping remaining iterations\n- **continue** skips the rest of current iteration, moves to next\n- **pass** does nothing (placeholder for future code)\n- **Loop else** runs only if loop completes without break\n- **Use break for**: Early exit, search operations, user-controlled loops\n- **Use continue for**: Skipping invalid data, filtering, conditional processing\n- **Use else for**: Search validation, avoiding flag variables\n- **break/continue work in**: Both for and while loops\n- **They only affect**: The innermost loop they\u0027re in\n\n### Quick Reference:\n```\n# break: \"Stop the loop\"\nfor item in items:\n    if found_it:\n        break  # Exit loop\n\n# continue: \"Skip to next iteration\"\nfor item in items:\n    if skip_this:\n        continue  # Next iteration\n    process(item)\n\n# pass: \"Do nothing\"\nfor item in items:\n    if special_case:\n        pass  # Placeholder\n    else:\n        process(item)\n\n# else: \"If loop wasn\u0027t broken\"\nfor item in items:\n    if item == target:\n        break\nelse:\n    print(\"Not found\")  # Only if no break\n\n```\n### When to Use Each:\n\u003ctable border=\u00271\u0027 cellpadding=\u00275\u0027 style=\u0027border-collapse: collapse;\u0027\u003e\u003ctr\u003e\u003cth\u003eScenario\u003c/th\u003e\u003cth\u003eStatement\u003c/th\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eFound what you need\u003c/td\u003e\u003ctd\u003ebreak\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eSkip invalid/unwanted items\u003c/td\u003e\u003ctd\u003econtinue\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eEmpty block placeholder\u003c/td\u003e\u003ctd\u003epass\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eDetect \"not found\" case\u003c/td\u003e\u003ctd\u003eelse\u003c/td\u003e\u003c/tr\u003e\u003c/table\u003e### Before Moving On:\nMake sure you can:\n\n- Use break to exit loops early\n- Use continue to skip iterations\n- Understand when loop else executes\n- Explain the difference between break and continue\n- Choose the right control statement for each scenario\n\n### Coming Up Next:\nIn **Lesson 4: Nested Loops**, you\u0027ll learn to:\n\n- Put loops inside other loops\n- Create 2D grids and patterns\n- Process multi-dimensional data\n- Combine break/continue with nesting\n- Build multiplication tables, calendars, game boards\n\nNested loops unlock the power to work with grids, matrices, and complex data structures!"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "module-04-lesson-03-challenge-1",
                           "title":  "Practice Exercise",
                           "description":  "Build a **Number Search and Statistics Program** that processes a list of numbers with the following features:\n\n- Search for a specific number and stop when found (break)\n- Skip negative numbers when calculating average (continue)\n- Use loop else to report if number wasn\u0027t found\n\n**Requirements:**\n\n- Ask user for a number to search for\n- Loop through the list: [23, -5, 42, 17, -3, 8, 56, 12, -9, 34]\n- If the number is found, print its position and stop searching\n- While searching, skip negative numbers when calculating the average\n- If not found, print a \"not found\" message\n- Display the average of positive numbers\n\n**Example output (searching for 42):**\n\n\u003cpre\u003e=== Number Search and Statistics ===\n\nEnter a number to search for: 42\n\nSearching...\nSkipping negative: -5\nFound 42 at position 2!\n\nAverage of positive numbers encountered: 32.5\n(Processed: 23, 42)\n\u003c/pre\u003e**Example output (searching for 99):**\n\n\u003cpre\u003eEnter a number to search for: 99\n\nSearching...\nSkipping negative: -5\nSkipping negative: -3\nSkipping negative: -9\n99 not found in the list.\n\nAverage of positive numbers: 27.42\n(Processed all: 23, 42, 17, 8, 56, 12, 34)\n\u003c/pre\u003e",
                           "instructions":  "Build a **Number Search and Statistics Program** that processes a list of numbers with the following features:\n\n- Search for a specific number and stop when found (break)\n- Skip negative numbers when calculating average (continue)\n- Use loop else to report if number wasn\u0027t found\n\n**Requirements:**\n\n- Ask user for a number to search for\n- Loop through the list: [23, -5, 42, 17, -3, 8, 56, 12, -9, 34]\n- If the number is found, print its position and stop searching\n- While searching, skip negative numbers when calculating the average\n- If not found, print a \"not found\" message\n- Display the average of positive numbers\n\n**Example output (searching for 42):**\n\n\u003cpre\u003e=== Number Search and Statistics ===\n\nEnter a number to search for: 42\n\nSearching...\nSkipping negative: -5\nFound 42 at position 2!\n\nAverage of positive numbers encountered: 32.5\n(Processed: 23, 42)\n\u003c/pre\u003e**Example output (searching for 99):**\n\n\u003cpre\u003eEnter a number to search for: 99\n\nSearching...\nSkipping negative: -5\nSkipping negative: -3\nSkipping negative: -9\n99 not found in the list.\n\nAverage of positive numbers: 27.42\n(Processed all: 23, 42, 17, 8, 56, 12, 34)\n\u003c/pre\u003e",
                           "starterCode":  "# Number Search and Statistics Program\n# Uses break, continue, and loop else\n\nprint(\"=== Number Search and Statistics ===\")\nprint()\n\n# The data\nnumbers = [23, -5, 42, 17, -3, 8, 56, 12, -9, 34]\n\n# Get search target\ntarget = int(input(\"Enter a number to search for: \"))\n\nprint(\"\\nSearching...\")\n\n# YOUR CODE HERE:\n# Initialize variables for statistics\ntotal = 0\ncount = 0\nprocessed = []  # Track which positive numbers we processed\n\n# Loop through the list with enumeration (position tracking)\nfor position, num in enumerate(numbers):\n    \n    # Skip negative numbers (continue)\n    if :  # If negative\n        print(f\"Skipping negative: {num}\")\n        continue  # Skip to next iteration\n    \n    # Add to running total for positive numbers\n    total = total + num\n    count = count + 1\n    processed.append(num)\n    \n    # Check if this is the target (break)\n    if :  # If found\n        print(f\"Found {target} at position {position}!\")\n        break  # Exit loop early\n\nelse:\n    # Runs only if loop completed without break\n    print(f\"{target} not found in the list.\")\n\n# Calculate and display average\nif count \u003e 0:\n    average = total / count\n    print(f\"\\nAverage of positive numbers encountered: {average:.2f}\")\n    print(f\"(Processed: {\u0027, \u0027.join(map(str, processed))})\")\nelse:\n    print(\"\\nNo positive numbers encountered.\")",
                           "solution":  "# Number Search and Statistics Program - SOLUTION\n# Uses break, continue, and loop else\n\nprint(\"=== Number Search and Statistics ===\")\nprint()\n\n# The data\nnumbers = [23, -5, 42, 17, -3, 8, 56, 12, -9, 34]\n\n# Get search target\ntarget = int(input(\"Enter a number to search for: \"))\n\nprint(\"\\nSearching...\")\n\n# Initialize variables for statistics\ntotal = 0\ncount = 0\nprocessed = []  # Track which positive numbers we processed\n\n# Loop through the list with enumeration (position tracking)\nfor position, num in enumerate(numbers):\n    \n    # Skip negative numbers (continue)\n    if num \u003c 0:\n        print(f\"Skipping negative: {num}\")\n        continue  # Skip to next iteration\n    \n    # Add to running total for positive numbers\n    total = total + num\n    count = count + 1\n    processed.append(num)\n    \n    # Check if this is the target (break)\n    if num == target:\n        print(f\"Found {target} at position {position}!\")\n        break  # Exit loop early\n\nelse:\n    # Runs only if loop completed without break\n    print(f\"{target} not found in the list.\")\n\n# Calculate and display average\nif count \u003e 0:\n    average = total / count\n    print(f\"\\nAverage of positive numbers encountered: {average:.2f}\")\n    print(f\"(Processed: {\u0027, \u0027.join(map(str, processed))})\")\nelse:\n    print(\"\\nNo positive numbers encountered.\")",
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
                                             "text":  "For skipping negatives, check if num \u003c 0 and use continue. For finding the target, check if num == target and use break. The else clause after the for loop will automatically run only if break was never executed. Use enumerate(numbers) to get both position and value."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting the colon after if/for/while",
                                                      "consequence":  "SyntaxError",
                                                      "correction":  "Add : at the end of the line"
                                                  },
                                                  {
                                                      "mistake":  "Using = instead of == for comparison",
                                                      "consequence":  "Assignment instead of comparison",
                                                      "correction":  "Use == for equality checks"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect indentation",
                                                      "consequence":  "IndentationError",
                                                      "correction":  "Use consistent 4-space indentation"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "beginner",
    "title":  "Loop Control: break, continue, and pass",
    "estimatedMinutes":  23
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
- Search for "python Loop Control: break, continue, and pass 2024 2025" to find latest practices
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
  "lessonId": "module-04-lesson-03",
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

