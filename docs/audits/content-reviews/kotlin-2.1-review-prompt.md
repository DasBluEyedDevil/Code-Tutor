# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** Controlling the Flow
- **Lesson:** Lesson 2.1: Making Decisions - If Statements and Conditional Logic (ID: 2.1)
- **Difficulty:** beginner
- **Estimated Time:** 60 minutes

## Current Lesson Content

{
    "id":  "2.1",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 60 minutes\n**Difficulty**: Beginner\n**Prerequisites**: Part 1 (Kotlin fundamentals)\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nWelcome to Part 2: Controlling the Flow! You\u0027ve mastered Kotlin fundamentals—variables, data types, functions, and basic input/output. Now it\u0027s time to make your programs **intelligent** by teaching them to make decisions.\n\nUp until now, your programs have executed line-by-line in a straight path, like following a recipe exactly. But real-world programs need to adapt and respond to different situations. Should you bring an umbrella? **If** it\u0027s raining, yes. **Otherwise**, no. That\u0027s conditional logic!\n\nIn this lesson, you\u0027ll learn:\n- What conditional logic is and why it\u0027s essential\n- How to use `if`, `else`, and `else if` statements\n- Comparison operators (`==`, `!=`, `\u003c`, `\u003e`, `\u003c=`, `\u003e=`)\n- How to make decisions based on Boolean conditions\n- Kotlin\u0027s unique `if` expression feature\n- Common patterns and best practices\n\nBy the end, you\u0027ll write programs that adapt their behavior based on conditions—the foundation of all intelligent software!\n\n---\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: Conditional Logic",
                                "content":  "\n### Real-World Decision Making\n\nEvery day, you make countless decisions based on conditions:\n\n\n\n\nYour brain evaluates conditions and chooses different paths automatically. Programming lets computers do the same!\n\n### The Traffic Light Analogy\n\nThink of a traffic light controlling your program\u0027s flow:\n\n- **Red light (condition false)**: Skip this block of code\n- **Green light (condition true)**: Execute this block of code\n- **Yellow light (else)**: Default path when others are false\n\nJust as traffic lights control the flow of cars, conditional statements control the flow of code execution.\n\n### What Makes a Condition?\n\nA condition is any expression that evaluates to **true** or **false** (a Boolean value):\n\n\nThe program checks the condition and decides which code to execute based on the result.\n\n---\n\n",
                                "code":  "age \u003e= 18           // true if age is 18 or more\ntemperature \u003c 32    // true if temperature is less than 32\nname == \"Alice\"     // true if name exactly equals \"Alice\"\nisRaining           // already a Boolean variable",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "The Fundamentals: If Statements",
                                "content":  "\n### Basic If Statement\n\nThe simplest form of conditional logic is the **if statement**:\n\n\n**Output:**\n\n**How it works:**\n1. Program evaluates `temperature \u003e 90` → `95 \u003e 90` → `true`\n2. Because the condition is true, the code inside the braces `{ }` executes\n3. Program continues to the next line after the if statement\n\n**If the temperature was 85:**\n\n**Output:**\n\n### Anatomy of an If Statement\n\n\n**Parts:**\n- `if` - Keyword that starts the conditional statement\n- `(condition)` - A Boolean expression that evaluates to true or false\n- `{ }` - Code block containing statements to execute when true\n- Indentation - Makes the code readable (best practice: 4 spaces)\n\n### Multiple Independent If Statements\n\nYou can have multiple separate if statements:\n\n\n**Output:**\n\n**Important:** Each if statement is checked independently. If `score = 85`, both the second and third conditions are true, so both messages print.\n\n---\n\n",
                                "code":  "Great job!\nGood effort!",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Comparison Operators",
                                "content":  "\nTo create conditions, you need to compare values using **comparison operators**:\n\n| Operator | Meaning | Example | Result |\n|----------|---------|---------|--------|\n| `==` | Equal to | `5 == 5` | `true` |\n| `==` | Equal to | `5 == 3` | `false` |\n| `!=` | Not equal to | `5 != 3` | `true` |\n| `!=` | Not equal to | `5 != 5` | `false` |\n| `\u003c` | Less than | `3 \u003c 5` | `true` |\n| `\u003c` | Less than | `5 \u003c 3` | `false` |\n| `\u003e` | Greater than | `5 \u003e 3` | `true` |\n| `\u003c=` | Less than or equal | `5 \u003c= 5` | `true` |\n| `\u003e=` | Greater than or equal | `5 \u003e= 3` | `true` |\n\n### Common Comparison Examples\n\n**Numeric comparisons:**\n\n**String comparisons:**\n\n**Boolean comparisons:**\n\n### Critical Mistake: = vs ==\n\n**The #1 beginner mistake:**\n\n❌ **WRONG:**\n\n✅ **CORRECT:**\n\n**Remember:**\n- `=` is for **assignment** (storing a value)\n- `==` is for **comparison** (checking equality)\n\n---\n\n",
                                "code":  "if (age == 18) {  // This COMPARES age to 18\n    println(\"You are 18\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "The Else Clause",
                                "content":  "\nOften you want to do one thing if a condition is true, and something **different** if it\u0027s false. That\u0027s where `else` comes in:\n\n\n**Output:**\n\n**How it works:**\n- If the condition (`age \u003e= 18`) is **true**, execute the first block\n- If the condition is **false**, execute the else block\n- Exactly ONE of the two blocks will execute, never both\n\n### The Either/Or Pattern\n\nThink of if-else as a fork in the road:\n\n\n**Real-world example:**\n\n**Output:**\n\n---\n\n",
                                "code":  "Opening door with key\nEntering home",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Else If: Multiple Conditions",
                                "content":  "\nWhat if you have more than two possibilities? Use **else if** to chain conditions:\n\n\n**Output:**\n\n### How Else If Works\n\nThe program checks conditions **in order** from top to bottom:\n\n1. Check first condition (`score \u003e= 90`) → `85 \u003e= 90` → **false**, skip\n2. Check second condition (`score \u003e= 80`) → `85 \u003e= 80` → **true**, execute and **STOP**\n3. Don\u0027t check any remaining conditions\n\n**Critical:** Once a condition is true, the rest are ignored. Order matters!\n\n**Example showing order importance:**\n\n❌ **WRONG ORDER:**\n**Output:** `Grade: D` (Wrong! Should be A)\n\n✅ **CORRECT ORDER:**\n**Output:** `Grade: A` (Correct!)\n\n**Rule:** Put the most specific conditions first, most general conditions last.\n\n---\n\n",
                                "code":  "val score = 95\n\nif (score \u003e= 90) {\n    println(\"Grade: A\")  // This executes!\n} else if (score \u003e= 60) {\n    println(\"Grade: D\")  // Never reached (but that\u0027s okay)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Nested If Statements",
                                "content":  "\nYou can put if statements inside other if statements:\n\n\n**Output:**\n\n**How it works:**\n1. Check outer condition (`age \u003e= 16`) → true, enter outer block\n2. Print \"You are old enough to drive\"\n3. Check inner condition (`hasLicense`) → true, execute\n4. Print \"You can drive legally!\"\n\n**Nested if statement pattern:**\n\n**Alternative:** In the next lesson, you\u0027ll learn about **logical operators** (`\u0026\u0026`, `||`) which often eliminate the need for nesting.\n\n---\n\n",
                                "code":  "if (outerCondition) {\n    // Outer block\n    if (innerCondition) {\n        // Inner block (only reached if BOTH conditions are true)\n    }\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "If as an Expression (Kotlin Special Feature!)",
                                "content":  "\nHere\u0027s something unique to Kotlin: `if` is not just a statement, it\u0027s an **expression** that can return a value!\n\n**Traditional approach (statement):**\n\n**Kotlin\u0027s expression approach:**\n\nBoth do the same thing, but the expression form is cleaner and more concise!\n\n### More Expression Examples\n\n**Example 1: Max of two numbers**\n\n**Example 2: Fee calculation**\n\n**Example 3: Multi-line expression blocks**\n\n**Important:** When using if as an expression, you **must** have an else clause (the expression must always produce a value).\n\n---\n\n",
                                "code":  "val result = if (score \u003e= 60) {\n    val bonus = 10\n    score + bonus  // Last expression is returned\n} else {\n    score  // Last expression is returned\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Hands-On Practice",
                                "content":  "\n### Exercise 1: Temperature Advisor\n\n**Challenge:** Write a program that:\n1. Takes a temperature value\n2. Prints different advice based on the temperature:\n   - If temp \u003e= 100: \"Extreme heat warning! Stay indoors.\"\n   - If temp \u003e= 80: \"It\u0027s hot! Stay hydrated.\"\n   - If temp \u003e= 60: \"Nice weather!\"\n   - If temp \u003c 60: \"It\u0027s chilly! Bring a jacket.\"\n\n\u003cdetails\u003e\n\u003csummary\u003eClick to see solution\u003c/summary\u003e\n\n\n**Output:**\n\n**Key concepts:**\n- Multiple conditions with else if\n- Ordered from most specific to least specific\n- Each temperature falls into exactly one category\n\u003c/details\u003e\n\n---\n\n### Exercise 2: Even or Odd Checker\n\n**Challenge:** Write a program that:\n1. Takes a number\n2. Checks if it\u0027s even or odd\n3. Prints the result\n\n**Hint:** Use the modulo operator `%`. A number is even if `number % 2 == 0`.\n\n\u003cdetails\u003e\n\u003csummary\u003eClick to see solution\u003c/summary\u003e\n\n\n**Output:**\n\n**How it works:**\n- `%` (modulo) gives the remainder after division\n- `17 % 2` = 1 (remainder when dividing 17 by 2)\n- `1 == 0` is false, so else block executes\n\n**Even number example:**\n- `18 % 2` = 0\n- `0 == 0` is true, so if block executes\n\u003c/details\u003e\n\n---\n\n### Exercise 3: Login System\n\n**Challenge:** Create a simple login system that:\n1. Stores a correct username and password\n2. Takes user input for username and password\n3. Checks if both match\n4. Prints \"Login successful\" or \"Login failed\"\n\n\u003cdetails\u003e\n\u003csummary\u003eClick to see solution\u003c/summary\u003e\n\n\n**Sample run:**\n\n**Note:** We\u0027re using `\u0026\u0026` (AND operator) which you\u0027ll learn more about in the next lesson. For now, understand that both conditions must be true for the if block to execute.\n\u003c/details\u003e\n\n---\n\n### Exercise 4: Discount Calculator\n\n**Challenge:** Write a program that:\n1. Takes a purchase amount\n2. Applies discounts based on the amount:\n   - $100+: 20% discount\n   - $50-$99: 10% discount\n   - Under $50: No discount\n3. Prints the final price\n\n\u003cdetails\u003e\n\u003csummary\u003eClick to see solution\u003c/summary\u003e\n\n\n**Output:**\n\n**Key concepts:**\n- Using if as an expression to calculate the discount\n- Storing the result in a variable\n- Performing calculations with the result\n\u003c/details\u003e\n\n---\n\n",
                                "code":  "Original price: $75.0\nDiscount: 10.0%\nFinal price: $67.5",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "WARNING",
                                "title":  "Common Pitfalls and Best Practices",
                                "content":  "\n### Pitfall 1: Missing Braces\n\nWhile braces are optional for single statements, **always use them** for clarity:\n\n⚠️ **Risky (works but confusing):**\n\n✅ **Better (clear and safe):**\n\n### Pitfall 2: Semicolons After Conditions\n\n❌ **WRONG:**\n\nThis creates an empty if statement, and the code block always executes!\n\n✅ **CORRECT:**\n\n### Pitfall 3: Comparing Floating-Point Numbers with ==\n\nFloating-point arithmetic can be imprecise:\n\n❌ **Risky:**\n\n✅ **Better:**\n\n### Best Practice 1: Readable Conditions\n\nUse descriptive variable names and comments for complex conditions:\n\n❌ **Unclear:**\n\n✅ **Clear:**\n\n### Best Practice 2: Positive Conditions\n\nWhen possible, write conditions in positive form:\n\n⚠️ **Harder to read:**\n\n✅ **Easier to read:**\n\n---\n\n",
                                "code":  "if (isValid) {\n    // Do something\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quick Quiz",
                                "content":  "\nTest your understanding:\n\n**Question 1:** What will this code print?\n\n\u003cdetails\u003e\n\u003csummary\u003eAnswer\u003c/summary\u003e\n\n**Output:** `Keep trying!`\n\n**Explanation:** `75 \u003e= 80` is false, so the else block executes.\n\u003c/details\u003e\n\n---\n\n**Question 2:** What\u0027s wrong with this code?\n\n\u003cdetails\u003e\n\u003csummary\u003eAnswer\u003c/summary\u003e\n\n**Error:** Using `=` instead of `==`\n\n`=` is assignment, `==` is comparison. Should be:\n\u003c/details\u003e\n\n---\n\n**Question 3:** What will this print if temperature = 85?\n\n\u003cdetails\u003e\n\u003csummary\u003eAnswer\u003c/summary\u003e\n\n**Output:**\n\n**Explanation:** These are three separate if statements (not else if). Both `85 \u003e 80` and `85 \u003e 70` are true, so both B and C print.\n\u003c/details\u003e\n\n---\n\n**Question 4:** Is this valid Kotlin code?\n\n\u003cdetails\u003e\n\u003csummary\u003eAnswer\u003c/summary\u003e\n\n**Yes!** This is valid. In Kotlin, `if` is an expression and can return a value. The result will be \"Positive\" if x \u003e 0, otherwise \"Non-positive\".\n\u003c/details\u003e\n\n---\n\n",
                                "code":  "val result = if (x \u003e 0) \"Positive\" else \"Non-positive\"",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Advanced Bonus: When to Use If vs When",
                                "content":  "\nWhile you\u0027ll learn about `when` expressions in the next lesson, here\u0027s a preview of when to use each:\n\n**Use if/else for:**\n- Binary decisions (two outcomes)\n- Range comparisons\n- Simple conditions\n\n**Use when (covered next lesson) for:**\n- Multiple specific values\n- Complex condition patterns\n- More than 3-4 options\n\n**Example - if is fine here:**\n\n**Example - when is better (preview):**\n\n---\n\n",
                                "code":  "when (dayOfWeek) {\n    1 -\u003e \"Monday\"\n    2 -\u003e \"Tuesday\"\n    3 -\u003e \"Wednesday\"\n    // ... cleaner than many else ifs\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Summary",
                                "content":  "\nCongratulations! You\u0027ve mastered conditional logic with if statements. Let\u0027s recap:\n\n**Key Concepts:**\n- **Conditional logic** lets programs make decisions based on conditions\n- **If statements** execute code blocks when conditions are true\n- **Comparison operators** (`==`, `!=`, `\u003c`, `\u003e`, `\u003c=`, `\u003e=`) create conditions\n- **Else** provides an alternative path when the condition is false\n- **Else if** chains multiple conditions (checked top to bottom)\n- **Nested if** statements check conditions within conditions\n- **Kotlin\u0027s if expression** can return values (unique feature!)\n\n**Common Patterns:**\n\n**Best Practices:**\n- Always use `==` for comparison, not `=`\n- Use braces `{ }` even for single statements\n- Order else-if conditions from specific to general\n- Use descriptive variable names for complex conditions\n- Prefer positive conditions over negative when possible\n\n---\n\n",
                                "code":  "// Simple if\nif (condition) { /* code */ }\n\n// If-else\nif (condition) { /* code */ } else { /* code */ }\n\n// If-else if chain\nif (condition1) { /* code */ }\nelse if (condition2) { /* code */ }\nelse { /* code */ }\n\n// If as expression\nval result = if (condition) value1 else value2",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What\u0027s Next?",
                                "content":  "\nYou can now make basic decisions, but what if you need to combine multiple conditions? \"IF it\u0027s raining AND I don\u0027t have an umbrella, THEN get wet!\"\n\nIn the next lesson, you\u0027ll learn **logical operators** (`\u0026\u0026`, `||`, `!`) to combine and invert conditions, making your decision-making even more powerful!\n\n**Preview:**\n\n---\n\n**Great work! You\u0027ve completed Lesson 2.1. Mark it complete and continue to Lesson 2.2!** 🎉\n\n",
                                "code":  "if (isRaining \u0026\u0026 !hasUmbrella) {\n    println(\"You\u0027ll get wet!\")\n}\n\nif (age \u003c 13 || age \u003e 65) {\n    println(\"Discounted ticket\")\n}",
                                "language":  "kotlin"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "2.1.1",
                           "title":  "Age Checker",
                           "description":  "Write a program that checks if age \u003e= 18 and prints \u0027Adult\u0027 or \u0027Minor\u0027.",
                           "instructions":  "Write a program that checks if age \u003e= 18 and prints \u0027Adult\u0027 or \u0027Minor\u0027.",
                           "starterCode":  "fun main() {\n    val age = 20\n    // Use if/else to check age\n    \n}",
                           "solution":  "fun main() {\n    val age = 20\n    if (age \u003e= 18) {\n        println(\"Adult\")\n    } else {\n        println(\"Minor\")\n    }\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "For age 20, prints \u0027Adult\u0027",
                                                 "expectedOutput":  "Adult",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Age 20 is \u003e= 18, so Adult",
                                                 "expectedOutput":  "Adult",
                                                 "isVisible":  false
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use if (condition) { } else { }"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Check: age \u003e= 18"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Print appropriate message in each branch"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Using \u003e instead of \u003e=",
                                                      "consequence":  "Age 18 would be classified as Minor",
                                                      "correction":  "Use \u003e= for 18 and older"
                                                  },
                                                  {
                                                      "mistake":  "Forgetting the else branch",
                                                      "consequence":  "No output for minors",
                                                      "correction":  "Always include an else for the alternative case"
                                                  },
                                                  {
                                                      "mistake":  "Reversed condition logic",
                                                      "consequence":  "Adult and Minor are swapped",
                                                      "correction":  "Check if age \u003e= 18 for Adult"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       },
                       {
                           "type":  "FREE_CODING",
                           "id":  "2.1.2",
                           "title":  "Grade Calculator",
                           "description":  "Given a score, print the letter grade (A: 90+, B: 80+, C: 70+, D: 60+, F: below 60).",
                           "instructions":  "Given a score, print the letter grade (A: 90+, B: 80+, C: 70+, D: 60+, F: below 60).",
                           "starterCode":  "fun main() {\n    val score = 85\n    // Use if/else if/else to determine grade\n    \n}",
                           "solution":  "fun main() {\n    val score = 85\n    if (score \u003e= 90) {\n        println(\"A\")\n    } else if (score \u003e= 80) {\n        println(\"B\")\n    } else if (score \u003e= 70) {\n        println(\"C\")\n    } else if (score \u003e= 60) {\n        println(\"D\")\n    } else {\n        println(\"F\")\n    }\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "For score 85, prints \u0027B\u0027",
                                                 "expectedOutput":  "B",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Score 85 falls in B range (80-89)",
                                                 "expectedOutput":  "B",
                                                 "isVisible":  false
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use else if for multiple conditions"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Check from highest to lowest"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Score 85 should print \u0027B\u0027"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Checking conditions in wrong order",
                                                      "consequence":  "Lower grades match before higher ones",
                                                      "correction":  "Check from highest (90+) to lowest"
                                                  },
                                                  {
                                                      "mistake":  "Using wrong comparison operators",
                                                      "consequence":  "Boundary scores get wrong grades",
                                                      "correction":  "Use \u003e= for inclusive boundaries"
                                                  },
                                                  {
                                                      "mistake":  "Missing else branch for F grade",
                                                      "consequence":  "No output for scores below 60",
                                                      "correction":  "Include else for scores below 60"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "beginner",
    "title":  "Lesson 2.1: Making Decisions - If Statements and Conditional Logic",
    "estimatedMinutes":  60
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current kotlin documentation
- Search the web for the latest kotlin version and verify examples work with it
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
- Search for "kotlin Lesson 2.1: Making Decisions - If Statements and Conditional Logic 2024 2025" to find latest practices
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
  "lessonId": "2.1",
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

