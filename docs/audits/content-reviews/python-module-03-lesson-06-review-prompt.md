# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Boolean Logic
- **Lesson:** Nested Conditionals: Decisions Within Decisions (ID: module-03-lesson-06)
- **Difficulty:** beginner
- **Estimated Time:** 25 minutes

## Current Lesson Content

{
    "id":  "module-03-lesson-06",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Understanding the Concept",
                                "content":  "Imagine you\u0027re applying for a special membership program at a gym. The bouncer at the door makes decisions in layers:\n\n\u003col style=\u0027background-color: #f0f0f0; padding: 15px; margin: 10px 0;\u0027\u003e\u003cli style=\u0027margin: 8px 0;\u0027\u003e**First check:** \"Are you 18 or older?\"\u003cul style=\u0027margin-left: 20px;\u0027\u003e- If NO → \"Sorry, adults only\" (stop here)\n- If YES → (proceed to next check)\n\n\u003c/li\u003e\u003cli style=\u0027margin: 8px 0;\u0027\u003e**Second check (only if first passed):** \"Do you have a membership card?\"\u003cul style=\u0027margin-left: 20px;\u0027\u003e- If YES → \"Welcome! Enter\"\n- If NO → \"Please sign up at the desk\"\n\n\u003c/li\u003e\nNotice how the second check **only happens IF** the first check passed. This is **nested decision-making** - decisions within decisions!\n\n### In Code:\n```\nif age \u003e= 18:              # First decision (outer)\n    if has_membership:     # Second decision (inner, only checked if outer True)\n        print(\"Welcome!\") \n    else:\n        print(\"Please sign up\")\nelse:\n    print(\"Adults only\")  # First check failed, never reached inner check\n\n```\n### Real-World Nested Decision Examples:\n\n- **Online shopping discounts**:\nIF in cart \u003e= $50:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;IF is_member:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Apply 20% discount\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;ELSE:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Apply 10% discount\n- **Access control system**:\nIF is_logged_in:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;IF is_admin:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Show admin dashboard\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;ELIF is_moderator:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Show moderator panel\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;ELSE:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Show user dashboard\nELSE:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Show login page\n- **Ride safety check**:\nIF tall_enough:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;IF has_ticket:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;IF not_pregnant:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Allow on ride\n\nEach layer adds another condition that must be checked only if previous conditions passed."
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example",
                                "content":  "**Expected Output:**\n```\n✓ User is logged in\n  → Granting admin access\n  → Showing admin dashboard\n\nCart Total: $75\n✓ Qualifies for discount (cart \u003e= $50)\n  → Member: 20% discount\n  → Final total: $60.00\n\n=== Roller Coaster Safety Check ===\n✓ Height requirement met (\u003e= 48\")\n  ✓ Ticket verified\n    ✓ Safety cleared\n    → APPROVED: You may ride!\n\nAge: 16, Student: True\nCategory: Student, Price: $10\n\n=== Activity Planner ===\n✓ Temperature is comfortable\n  ✓ Weather is dry\n    → Recommendation: Walk during lunch break\n```",
                                "code":  "# Nested Conditionals: Decisions Within Decisions\n\n# Example 1: Basic Nesting - User Access Control\nis_logged_in = True\nis_admin = True\n\nif is_logged_in:\n    print(\"✓ User is logged in\")\n    \n    # This inner check only runs if logged in\n    if is_admin:\n        print(\"  → Granting admin access\")\n        print(\"  → Showing admin dashboard\")\n    else:\n        print(\"  → Regular user access\")\n        print(\"  → Showing user dashboard\")\nelse:\n    print(\"❌ Not logged in\")\n    print(\"→ Redirecting to login page\")\n\nprint()\n\n# Example 2: Discount Calculator (Layered Eligibility)\ncart_total = 75\nis_member = True\n\nprint(f\"Cart Total: ${cart_total}\")\n\nif cart_total \u003e= 50:  # First condition: minimum purchase\n    print(\"✓ Qualifies for discount (cart \u003e= $50)\")\n    \n    if is_member:  # Second condition: checked only if first is True\n        discount = 0.20  # 20% member discount\n        print(\"  → Member: 20% discount\")\n    else:\n        discount = 0.10  # 10% non-member discount\n        print(\"  → Non-member: 10% discount\")\n    \n    final_total = cart_total * (1 - discount)\n    print(f\"  → Final total: ${final_total:.2f}\")\nelse:\n    print(\"❌ No discount (cart \u003c $50)\")\n    print(f\"→ Final total: ${cart_total}\")\n\nprint()\n\n# Example 3: Three-Level Nesting - Ride Safety Check\nheight = 52  # inches\nhas_ticket = True\nis_pregnant = False\n\nprint(\"=== Roller Coaster Safety Check ===\")\n\nif height \u003e= 48:\n    print(\"✓ Height requirement met (\u003e= 48\\\")\")\n    \n    if has_ticket:\n        print(\"  ✓ Ticket verified\")\n        \n        if not is_pregnant:\n            print(\"    ✓ Safety cleared\")\n            print(\"    → APPROVED: You may ride!\")\n        else:\n            print(\"    ❌ Safety concern: pregnancy\")\n            print(\"    → DENIED: For your safety\")\n    else:\n        print(\"  ❌ No ticket found\")\n        print(\"  → Please purchase a ticket\")\nelse:\n    print(\"❌ Height requirement not met\")\n    print(f\"→ You are {height}\\\", need 48\\\" minimum\")\n\nprint()\n\n# Example 4: Age \u0026 Student Status (Nested elif)\nage = 16\nis_student = True\n\nif age \u003c 13:\n    price = 8\n    category = \"Child\"\nelse:\n    # Nested decision for 13+ age group\n    if is_student:\n        price = 10\n        category = \"Student\"\n    else:\n        if age \u003c 65:\n            price = 15\n            category = \"Adult\"\n        else:\n            price = 12\n            category = \"Senior\"\n\nprint(f\"Age: {age}, Student: {is_student}\")\nprint(f\"Category: {category}, Price: ${price}\")\n\nprint()\n\n# Example 5: Weather Activity Planner\ntemperature = 75\nis_raining = False\nis_weekend = True\n\nprint(\"=== Activity Planner ===\")\n\nif temperature \u003e 60:\n    print(\"✓ Temperature is comfortable\")\n    \n    if not is_raining:\n        print(\"  ✓ Weather is dry\")\n        \n        if is_weekend:\n            print(\"    ✓ It\u0027s the weekend!\")\n            print(\"    → Recommendation: Go to the park!\")\n        else:\n            print(\"    → Recommendation: Walk during lunch break\")\n    else:\n        print(\"  ❌ It\u0027s raining\")\n        print(\"  → Recommendation: Indoor activities\")\nelse:\n    print(\"❌ Too cold outside\")\n    print(\"→ Recommendation: Stay indoors\")",
                                "language":  "python"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Syntax Breakdown",
                                "content":  "### Nested Conditional Anatomy:\n```\nif outer_condition:           # Level 0 indentation\n    outer_statements          # Level 1 (4 spaces)\n    \n    if inner_condition:       # Level 1 (4 spaces)\n        inner_statements      # Level 2 (8 spaces)\n    else:\n        inner_else_statements # Level 2 (8 spaces)\nelse:\n    outer_else_statements     # Level 1 (4 spaces)\n\n```\n#### Indentation Levels:\n\n- **Level 0**: Main if (no indentation)\n- **Level 1**: Code inside main if (4 spaces)\n- **Level 2**: Code inside nested if (8 spaces)\n- **Level 3**: Code inside triple-nested if (12 spaces)\n\n#### Execution Flow Example:\n```\nage = 20\nhas_id = True\n\nif age \u003e= 18:              # Check outer condition\n    print(\"Adult\")         # Outer True: execute this\n    \n    if has_id:             # Now check inner condition\n        print(\"ID verified\")  # Inner True: execute this\n    else:\n        print(\"Need ID\")   # (Skipped - inner was True)\nelse:\n    print(\"Minor\")         # (Skipped - outer was True)\n\n# Output:\n# Adult\n# ID verified\n\n```\n### When to Use Nesting vs Logical Operators:\n\u003ctable border=\u00271\u0027 cellpadding=\u00275\u0027 style=\u0027border-collapse: collapse;\u0027\u003e\u003ctr\u003e\u003cth\u003eUse Nested if\u003c/th\u003e\u003cth\u003eUse Logical Operators (and/or)\u003c/th\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eWhen inner check only makes sense if outer is True\u003c/td\u003e\u003ctd\u003eWhen both conditions are equal partners\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eWhen you need different actions at each level\u003c/td\u003e\u003ctd\u003eWhen checking multiple requirements for one outcome\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003eMore readable for complex multi-level logic\u003c/td\u003e\u003ctd\u003eSimpler and more concise for 2 conditions\u003c/td\u003e\u003c/tr\u003e\u003c/table\u003e#### Example Comparison:\n```\n# Scenario 1: Use AND (both conditions equal)\n# \"Can only drive if 16+ AND have license\"\nif age \u003e= 16 and has_license:\n    print(\"Can drive\")  # Simple, clear!\n\n# Same with nesting (unnecessarily complex here):\nif age \u003e= 16:\n    if has_license:\n        print(\"Can drive\")\n# Nesting adds no value here\n\n# Scenario 2: Use NESTING (different actions at each level)\n# Different responses for logged in vs not, admin vs regular\nif is_logged_in:\n    if is_admin:\n        show_admin_panel()  # Action 1\n    else:\n        show_user_panel()   # Action 2\nelse:\n    show_login_page()       # Action 3\n# Nesting is clearer here - 3 distinct outcomes\n\n# Same with AND (awkward):\nif is_logged_in and is_admin:\n    show_admin_panel()\nelif is_logged_in and not is_admin:\n    show_user_panel()\nelse:\n    show_login_page()\n# Redundant checks, less clear\n\n```\n### Common Nesting Patterns:\n#### 1. Layered Validation\n```\nif input_is_provided:\n    if input_is_valid_format:\n        if input_is_safe:\n            process_input()\n\n```\n#### 2. Permission Checking\n```\nif is_logged_in:\n    if is_admin or is_owner:\n        allow_edit()\n    else:\n        deny_access()\n\n```\n#### 3. Tiered Eligibility\n```\nif age \u003e= 18:\n    if income \u003e= 30000:\n        if credit_score \u003e= 650:\n            approve_loan()\n\n```\n### Avoiding Deep Nesting (Readability):\n**Problem:** Too many levels become hard to read:\n\n```\n# TOO DEEP (hard to follow):\nif condition1:\n    if condition2:\n        if condition3:\n            if condition4:\n                if condition5:\n                    do_something()  # 5 levels deep!\n\n```\n**Solution 1:** Use logical operators:\n\n```\n# BETTER:\nif condition1 and condition2 and condition3 and condition4 and condition5:\n    do_something()  # Flat, easier to read\n\n```\n**Solution 2:** Early returns (preview of functions):\n\n```\n# BETTER (for functions - you\u0027ll learn in Module 6):\nif not condition1:\n    return\nif not condition2:\n    return\ndo_something()  # Flatter logic\n\n```\n**Best Practice:** Keep nesting to 2-3 levels maximum for readability.\n\n### Common Mistakes:\n\n\u003cli\u003e**Wrong indentation levels**:```\n# WRONG:\nif outer:\n  if inner:  # Only 2 spaces! Should be 4\n      action\n\n# CORRECT:\nif outer:\n    if inner:  # 4 spaces for outer, 4 more for inner\n        action  # 8 spaces total\n\n```\n\u003c/li\u003e\u003cli\u003e**Forgetting the outer logic path**:```\n# PROBLEM: What if not logged in?\nif is_logged_in:\n    if is_admin:\n        show_admin()\n    else:\n        show_user()\n# Missing else for outer if! What if not logged in?\n\n# BETTER:\nif is_logged_in:\n    if is_admin:\n        show_admin()\n    else:\n        show_user()\nelse:\n    show_login()  # Handle all cases!\n\n```\n\u003c/li\u003e\u003cli\u003e**Nesting when AND would work**:```\n# UNNECESSARILY NESTED:\nif age \u003e= 18:\n    if has_license:\n        allow_drive()\n\n# SIMPLER:\nif age \u003e= 18 and has_license:\n    allow_drive()\n\n```\n\u003c/li\u003e"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **Nested conditionals** = if statements inside other if statements\n- **Indentation is critical**: Each level adds 4 spaces (4, 8, 12...)\n- **Inner checks only run if outer checks pass** - layered decision making\n- **Use nesting when**:\n\u003cli\u003eInner decision only makes sense if outer is True\n- Different actions needed at each level\n- Complex multi-layered logic\n\n\u003c/li\u003e- **Use AND/OR instead when**:\n\u003cli\u003eConditions are equal partners\n- Simpler to express as one compound condition\n- No different actions at intermediate levels\n\n\u003c/li\u003e- **Readability matters**: Keep nesting to 2-3 levels max\n- **Common patterns**: Access control, layered validation, tiered eligibility, permission systems\n\n### Nesting vs Logical Operators Decision Guide:\n```\n✅ Use NESTING when:\n• Different actions at each level\n• Inner check only relevant if outer passes\n• Complex multi-tier logic (3+ outcomes)\n\n✅ Use AND/OR when:\n• All conditions are requirements for ONE outcome\n• Simpler and flatter\n• Two equal conditions\n\nExample:\n# AND is better here (both requirements for one outcome):\nif age \u003e= 16 and has_license:\n    allow_drive()\n\n# Nesting is better here (different actions at levels):\nif is_logged_in:\n    if is_admin:\n        admin_panel()  # Action 1\n    else:\n        user_panel()   # Action 2\nelse:\n    login_page()       # Action 3\n\n```\n### Module 3 Complete! 🎉\nYou\u0027ve mastered ALL fundamental decision-making in Python:\n\n- **Boolean values** (True/False)\n- **Comparison operators** (==, !=, \u003c, \u003e, \u003c=, \u003e=)\n- **Logical operators** (and, or, not)\n- **if statements** (conditional execution)\n- **if-else statements** (two paths)\n- **elif chains** (multiple mutually exclusive paths)\n- **Nested conditionals** (layered decisions)\n\nThese are the building blocks of EVERY decision a program makes. You can now write programs that adapt, respond, and make intelligent choices!\n\n### What\u0027s Next: Module 4 - Loops\nYou\u0027ve learned to make programs decide. Now you\u0027ll learn to make them **repeat**:\n\n- **while loops**: Repeat while a condition is True\n- **for loops**: Repeat for each item in a sequence\n- **Loop control**: break, continue, else\n- **Nested loops**: Loops inside loops\n\nLoops + conditionals = the power to build almost anything!\n\n**Before Module 4:** Take the Module 3 Quiz to test your understanding of all decision-making concepts."
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "module-03-lesson-06-challenge-1",
                           "title":  "Practice Exercise",
                           "description":  "Build a **Movie Ticket Pricing System** with age-based pricing and weekday/weekend rates, plus student discounts.\n\n**Pricing Rules:**\n\n- **Children (under 13):**\n\u003cli\u003eWeekday: $8\n- Weekend: $10\n\n\u003c/li\u003e- **Adults (13-64):**\n\u003cli\u003eStudents: $12 (any day)\n- Non-students weekday: $15\n- Non-students weekend: $18\n\n\u003c/li\u003e- **Seniors (65+):**\n\u003cli\u003eWeekday: $10\n- Weekend: $12\n\n\u003c/li\u003e\n**Your task:**\n\n- Ask for age, whether it\u0027s a weekend, and student status\n- Use nested conditionals to determine the correct price\n- Display the pricing breakdown\n\n**Example output:**\n\n\u003cpre\u003e=== Movie Ticket Pricing ===\n\nEnter your age: 19\nIs it the weekend? (yes/no): no\nAre you a student? (yes/no): yes\n\nAge Category: Adult\nDay Type: Weekday\nStudent Discount: Yes\nTicket Price: $12.00\n\u003c/pre\u003e",
                           "instructions":  "Build a **Movie Ticket Pricing System** with age-based pricing and weekday/weekend rates, plus student discounts.\n\n**Pricing Rules:**\n\n- **Children (under 13):**\n\u003cli\u003eWeekday: $8\n- Weekend: $10\n\n\u003c/li\u003e- **Adults (13-64):**\n\u003cli\u003eStudents: $12 (any day)\n- Non-students weekday: $15\n- Non-students weekend: $18\n\n\u003c/li\u003e- **Seniors (65+):**\n\u003cli\u003eWeekday: $10\n- Weekend: $12\n\n\u003c/li\u003e\n**Your task:**\n\n- Ask for age, whether it\u0027s a weekend, and student status\n- Use nested conditionals to determine the correct price\n- Display the pricing breakdown\n\n**Example output:**\n\n\u003cpre\u003e=== Movie Ticket Pricing ===\n\nEnter your age: 19\nIs it the weekend? (yes/no): no\nAre you a student? (yes/no): yes\n\nAge Category: Adult\nDay Type: Weekday\nStudent Discount: Yes\nTicket Price: $12.00\n\u003c/pre\u003e",
                           "starterCode":  "# Movie Ticket Pricing System\n# Nested conditionals for complex pricing logic\n\nprint(\"=== Movie Ticket Pricing ===\")\nprint()\n\n# Get user input\nage = int(input(\"Enter your age: \"))\nis_weekend_input = input(\"Is it the weekend? (yes/no): \")\nis_student_input = input(\"Are you a student? (yes/no): \")\n\n# Convert to boolean\nis_weekend = is_weekend_input.lower() == \"yes\"\nis_student = is_student_input.lower() == \"yes\"\n\nprint()\n\n# YOUR CODE HERE:\n# Use nested conditionals to determine price\n\nif :  # Child (under 13)\n    category = \"Child\"\n    \n    if is_weekend:\n        price = \n        day_type = \"Weekend\"\n    else:\n        price = \n        day_type = \"Weekday\"\n        \nelif :  # Adult (13-64)\n    category = \"Adult\"\n    \n    if is_student:\n        price =   # Student price (same any day)\n        day_type = \"Weekday\" if not is_weekend else \"Weekend\"\n    else:\n        if is_weekend:\n            price = \n            day_type = \"Weekend\"\n        else:\n            price = \n            day_type = \"Weekday\"\n            \nelse:  # Senior (65+)\n    category = \n    \n    if :\n        price = \n        day_type = \n    else:\n        price = \n        day_type = \n\n# Display results\nprint(f\"Age Category: {category}\")\nprint(f\"Day Type: {day_type}\")\nif category == \"Adult\" and is_student:\n    print(\"Student Discount: Yes\")\nprint(f\"Ticket Price: ${price:.2f}\")",
                           "solution":  "# Movie Ticket Pricing System - SOLUTION\n# Nested conditionals for complex pricing logic\n\nprint(\"=== Movie Ticket Pricing ===\")\nprint()\n\n# Get user input\nage = int(input(\"Enter your age: \"))\nis_weekend_input = input(\"Is it the weekend? (yes/no): \")\nis_student_input = input(\"Are you a student? (yes/no): \")\n\n# Convert to boolean\nis_weekend = is_weekend_input.lower() == \"yes\"\nis_student = is_student_input.lower() == \"yes\"\n\nprint()\n\n# Determine price using nested conditionals\nif age \u003c 13:  # Child\n    category = \"Child\"\n    \n    if is_weekend:\n        price = 10\n        day_type = \"Weekend\"\n    else:\n        price = 8\n        day_type = \"Weekday\"\n        \nelif age \u003c 65:  # Adult\n    category = \"Adult\"\n    \n    if is_student:\n        price = 12  # Student price (same any day)\n        day_type = \"Weekday\" if not is_weekend else \"Weekend\"\n    else:\n        if is_weekend:\n            price = 18\n            day_type = \"Weekend\"\n        else:\n            price = 15\n            day_type = \"Weekday\"\n            \nelse:  # Senior\n    category = \"Senior\"\n    \n    if is_weekend:\n        price = 12\n        day_type = \"Weekend\"\n    else:\n        price = 10\n        day_type = \"Weekday\"\n\n# Display results\nprint(f\"Age Category: {category}\")\nprint(f\"Day Type: {day_type}\")\nif category == \"Adult\" and is_student:\n    print(\"Student Discount: Yes\")\nprint(f\"Ticket Price: ${price:.2f}\")",
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
                                             "text":  "Start with age ranges: if age \u003c 13 (child), elif age \u003c 65 (adult), else (senior). For each age group, nest another if-else to check weekend vs weekday. For adults, add an extra layer to check if student (gets flat $12 any day) vs non-student (varies by day)."
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
    "title":  "Nested Conditionals: Decisions Within Decisions",
    "estimatedMinutes":  25
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
- Search for "python Nested Conditionals: Decisions Within Decisions 2024 2025" to find latest practices
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
  "lessonId": "module-03-lesson-06",
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

