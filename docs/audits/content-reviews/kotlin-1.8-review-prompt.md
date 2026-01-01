# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Kotlin Multiplatform Complete Course (kotlin)
- **Module:** The Absolute Basics
- **Lesson:** Lesson 1.8: Functions with Parameters and Return Values (ID: 1.8)
- **Difficulty:** beginner
- **Estimated Time:** 65 minutes

## Current Lesson Content

{
    "id":  "1.8",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Introduction",
                                "content":  "**Estimated Time**: 65 minutes\n\n**Difficulty**: Beginner\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Topic Introduction",
                                "content":  "\nYou\u0027ve already learned the basics of functions—reusable blocks of code that help organize your program. But so far, your functions have been like vending machines that dispense the same item every time. What if you want to customize what you get?\n\nIn this lesson, you\u0027ll learn how to make your functions truly flexible and powerful by:\n- **Passing data INTO functions** (parameters)\n- **Getting data BACK from functions** (return values)\n- **Creating reusable, customizable code blocks** that adapt to different situations\n\nThink of it this way: A chef doesn\u0027t just make \"a sandwich\"—they take specific ingredients (parameters) and create a customized sandwich (return value) based on what you ordered. That\u0027s exactly what we\u0027re learning today!\n\nBy the end of this lesson, you\u0027ll be able to write functions that accept input, process it, and give you back exactly what you need.\n\n---\n\n"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept",
                                "content":  "\n### The Recipe Analogy\n\n**Simple Functions** (what you know already):\n\n**Functions with Parameters** (what you\u0027re learning now):\n\n**Real-World Examples**:\n- **Coffee Shop**: `makeCoffee(size, type, milk)` → Takes your preferences, returns your custom coffee\n- **ATM Machine**: `withdraw(accountNumber, amount)` → Takes account and amount, returns cash\n- **Calculator**: `add(number1, number2)` → Takes two numbers, returns their sum\n\n### Parameters vs Arguments\n\nThese terms are often confused, but they\u0027re different:\n\n- **Parameter**: The placeholder variable in the function definition (like a recipe ingredient slot)\n- **Argument**: The actual value you pass when calling the function (like the real ingredient)\n\n\nThink of it like a form:\n- **Parameter**: The blank field \"Name: _______\"\n- **Argument**: What you write in that field \"Name: Alice\"\n\n---\n\n",
                                "code":  "fun greet(name: String) {  // \u0027name\u0027 is a PARAMETER\n    println(\"Hello, $name!\")\n}\n\nfun main() {\n    greet(\"Alice\")  // \"Alice\" is an ARGUMENT\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Parameters: Giving Functions Input",
                                "content":  "\n### Single Parameter\n\nThe simplest case—one input to customize the function:\n\n\n**Output**:\n\n**Breaking it down**:\n\n---\n\n### Multiple Parameters\n\nFunctions can accept multiple inputs:\n\n\n**Output**:\n\n**Important**: Order matters!\n- First argument → first parameter\n- Second argument → second parameter\n- Third argument → third parameter\n\n\n---\n\n### Parameters with Different Types\n\nYou can mix and match any data types:\n\n\n**Output**:\n\n---\n\n### Practical Example: Calculation Function\n\n\n**Output**:\n\n---\n\n",
                                "code":  "Item Price: $19.99\nQuantity: 3\nSubtotal: $59.97\nTax (8.0%): $4.80\nTotal: $64.77",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Return Values: Getting Results Back",
                                "content":  "\nSo far, our functions only **do** things (print output). But what if you want a function to **calculate** something and give you the result to use elsewhere?\n\n**That\u0027s where return values come in!**\n\n### The Return Statement\n\n\n**Output**:\n\n**Anatomy of a Return Function**:\n\n---\n\n### Return Types Explained\n\nThe return type tells you what kind of value the function will give back:\n\n\n---\n\n### Using Return Values\n\nOnce a function returns a value, you can use it in many ways:\n\n#### 1. Store in a Variable\n\n#### 2. Use Directly in Expressions\n\n#### 3. Print Directly\n\n#### 4. Use in Conditions\n\n#### 5. Chain Function Calls\n\n---\n\n### Functions with Early Return\n\nA function can have multiple return statements:\n\n\n**How it works**:\n- When a return is executed, the function immediately exits\n- No code after the return runs\n- Very useful for handling different cases\n\n---\n\n### Void Functions (Unit Type)\n\nWhat about functions that don\u0027t return anything meaningful?\n\n\n**Unit** is Kotlin\u0027s way of saying \"this function doesn\u0027t return a useful value.\" It\u0027s like `void` in other languages, but in Kotlin, you usually just omit it.\n\n---\n\n",
                                "code":  "fun printWelcome(name: String): Unit {\n    println(\"Welcome, $name!\")\n    // No return statement needed\n}\n\n// Unit can be omitted (it\u0027s the default)\nfun printGoodbye(name: String) {\n    println(\"Goodbye, $name!\")\n}\n\nfun main() {\n    printWelcome(\"Alice\")  // Welcome, Alice!\n    printGoodbye(\"Bob\")    // Goodbye, Bob!\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Single-Expression Functions",
                                "content":  "\nWhen a function is simple and returns a single expression, Kotlin has a shortcut:\n\n### Traditional Way vs. Shortcut\n\n\nAll three versions do exactly the same thing, but the single-expression version is more concise!\n\n---\n\n### More Single-Expression Examples\n\n\n**When to use single-expression functions**:\n- ✅ Function body is one simple expression\n- ✅ Makes code more readable and concise\n- ❌ Don\u0027t use if the logic is complex or needs multiple lines\n\n---\n\n",
                                "code":  "// Math operations\nfun square(x: Int) = x * x\nfun cube(x: Int) = x * x * x\nfun double(x: Int) = x * 2\n\n// Boolean checks\nfun isEven(n: Int) = n % 2 == 0\nfun isPositive(n: Int) = n \u003e 0\nfun isAdult(age: Int) = age \u003e= 18\n\n// String operations\nfun greet(name: String) = \"Hello, $name!\"\nfun shout(text: String) = text.uppercase() + \"!\"\n\n// Conditional expressions\nfun max(a: Int, b: Int) = if (a \u003e b) a else b\nfun min(a: Int, b: Int) = if (a \u003c b) a else b\nfun absoluteValue(n: Int) = if (n \u003e= 0) n else -n\n\nfun main() {\n    println(square(5))           // 25\n    println(isEven(4))           // true\n    println(greet(\"Alice\"))      // Hello, Alice!\n    println(max(10, 20))         // 20\n    println(absoluteValue(-7))   // 7\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Default Parameters",
                                "content":  "\nKotlin lets you provide default values for parameters:\n\n\n**Output**:\n\n---\n\n### Multiple Default Parameters\n\n\n**Output**:\n\n---\n\n### Named Arguments\n\nYou can specify parameter names when calling functions:\n\n\n**Benefits of named arguments**:\n- Code is more readable\n- Order doesn\u0027t matter\n- Great when functions have many parameters\n- Especially useful with default parameters\n\n---\n\n",
                                "code":  "fun makeRecipe(dish: String, cookTime: Int, difficulty: String, serves: Int) {\n    println(\"$dish - Serves $serves\")\n    println(\"Cooking time: $cookTime minutes\")\n    println(\"Difficulty: $difficulty\")\n    println()\n}\n\nfun main() {\n    // Positional arguments (order matters)\n    makeRecipe(\"Pizza\", 30, \"Easy\", 4)\n\n    // Named arguments (order doesn\u0027t matter!)\n    makeRecipe(\n        dish = \"Pasta\",\n        serves = 2,\n        difficulty = \"Medium\",\n        cookTime = 20\n    )\n\n    // Mix of both\n    makeRecipe(\"Cake\", cookTime = 45, difficulty = \"Hard\", serves = 8)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Hands-On Exercises",
                                "content":  "\n### Exercise 1: Temperature Converter\n\n**Goal**: Create a comprehensive temperature converter.\n\n**Requirements**:\n1. Create `celsiusToFahrenheit(celsius: Double): Double`\n2. Create `fahrenheitToCelsius(fahrenheit: Double): Double`\n3. Create `celsiusToKelvin(celsius: Double): Double`\n4. Create `displayConversions(temp: Double, unit: String)` that shows all conversions\n5. Test with different temperatures\n\n**Formulas**:\n- F = (C × 9/5) + 32\n- C = (F - 32) × 5/9\n- K = C + 273.15\n\n**Try it yourself first, then check the solution!**\n\n\u003cdetails\u003e\n\u003csummary\u003eClick to see Solution\u003c/summary\u003e\n\n\n**Output**:\n\n\u003c/details\u003e\n\n---\n\n### Exercise 2: Shopping Cart Calculator\n\n**Goal**: Create a shopping cart calculator with tax and discounts.\n\n**Requirements**:\n1. Create `calculateSubtotal(price: Double, quantity: Int): Double`\n2. Create `calculateTax(amount: Double, taxRate: Double = 0.08): Double`\n3. Create `applyDiscount(amount: Double, discountPercent: Double = 0.0): Double`\n4. Create `calculateTotal(price: Double, quantity: Int, taxRate: Double, discountPercent: Double): Double`\n5. Create `displayReceipt(itemName: String, price: Double, quantity: Int, taxRate: Double, discountPercent: Double)`\n\n\u003cdetails\u003e\n\u003csummary\u003eClick to see Solution\u003c/summary\u003e\n\n\n\u003c/details\u003e\n\n---\n\n### Exercise 3: Grade Calculator\n\n**Goal**: Create a student grade calculator.\n\n**Requirements**:\n1. Create `calculateAverage(score1: Int, score2: Int, score3: Int): Double`\n2. Create `getLetterGrade(average: Double): String`\n3. Create `isPassing(grade: String): Boolean`\n4. Create `displayGradeReport(name: String, score1: Int, score2: Int, score3: Int)`\n\n**Grading Scale**:\n- A: 90-100\n- B: 80-89\n- C: 70-79\n- D: 60-69\n- F: Below 60\n- Passing: C or better\n\n\u003cdetails\u003e\n\u003csummary\u003eClick to see Solution\u003c/summary\u003e\n\n\n**Output**:\n\n\u003c/details\u003e\n\n---\n\n### Exercise 4: BMI Calculator\n\n**Goal**: Create a Body Mass Index calculator with health recommendations.\n\n**Requirements**:\n1. Create `calculateBMI(weightKg: Double, heightM: Double): Double`\n2. Create `getBMICategory(bmi: Double): String`\n3. Create `getHealthAdvice(category: String): String`\n4. Test with different values\n\n**BMI Categories**:\n- Underweight: \u003c 18.5\n- Normal: 18.5-24.9\n- Overweight: 25-29.9\n- Obese: ≥ 30\n\n**Formula**: BMI = weight (kg) / height² (m)\n\n\u003cdetails\u003e\n\u003csummary\u003eClick to see Solution\u003c/summary\u003e\n\n\n\u003c/details\u003e\n\n---\n\n",
                                "code":  "fun calculateBMI(weightKg: Double, heightM: Double): Double {\n    return weightKg / (heightM * heightM)\n}\n\nfun getBMICategory(bmi: Double): String {\n    return when {\n        bmi \u003c 18.5 -\u003e \"Underweight\"\n        bmi \u003c 25.0 -\u003e \"Normal weight\"\n        bmi \u003c 30.0 -\u003e \"Overweight\"\n        else -\u003e \"Obese\"\n    }\n}\n\nfun getHealthAdvice(category: String): String {\n    return when (category) {\n        \"Underweight\" -\u003e \"Consider consulting a nutritionist to gain weight healthily.\"\n        \"Normal weight\" -\u003e \"Great! Maintain your current healthy lifestyle.\"\n        \"Overweight\" -\u003e \"Consider a balanced diet and regular exercise.\"\n        \"Obese\" -\u003e \"Consult a healthcare provider for a personalized health plan.\"\n        else -\u003e \"Unknown category\"\n    }\n}\n\nfun displayBMIReport(name: String, weightKg: Double, heightM: Double) {\n    val bmi = calculateBMI(weightKg, heightM)\n    val category = getBMICategory(bmi)\n    val advice = getHealthAdvice(category)\n\n    println(\"═══════════════════════════════════════\")\n    println(\"         BMI HEALTH REPORT\")\n    println(\"═══════════════════════════════════════\")\n    println()\n    println(\"Name: $name\")\n    println(\"Weight: ${weightKg}kg\")\n    println(\"Height: ${heightM}m\")\n    println()\n    println(\"BMI: ${\"%.1f\".format(bmi)}\")\n    println(\"Category: $category\")\n    println()\n    println(\"Health Advice:\")\n    println(advice)\n    println()\n    println(\"═══════════════════════════════════════\")\n    println()\n}\n\nfun main() {\n    displayBMIReport(\"Alice\", 65.0, 1.70)\n    displayBMIReport(\"Bob\", 95.0, 1.80)\n    displayBMIReport(\"Charlie\", 55.0, 1.75)\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "WARNING",
                                "title":  "Common Pitfalls and Best Practices",
                                "content":  "\n### Common Mistakes\n\n#### Mistake 1: Wrong Number of Arguments\n\n\n---\n\n#### Mistake 2: Wrong Argument Type\n\n\n---\n\n#### Mistake 3: Wrong Argument Order\n\n\n---\n\n#### Mistake 4: Forgetting Return Statement\n\n\n---\n\n#### Mistake 5: Incorrect Return Type\n\n\n---\n\n### Best Practices\n\n#### 1. Use Descriptive Parameter Names\n\n\n---\n\n#### 2. Keep Functions Focused (Single Responsibility)\n\n\n---\n\n#### 3. Use Default Parameters for Optional Values\n\n\n---\n\n#### 4. Use Single-Expression Functions for Simple Logic\n\n\n---\n\n#### 5. Validate Input Parameters\n\n\n---\n\n",
                                "code":  "fun divide(a: Double, b: Double): Double {\n    if (b == 0.0) {\n        println(\"Error: Cannot divide by zero!\")\n        return 0.0\n    }\n    return a / b\n}\n\nfun createUser(name: String, age: Int) {\n    if (name.isBlank()) {\n        println(\"Error: Name cannot be empty!\")\n        return\n    }\n    if (age \u003c 0 || age \u003e 150) {\n        println(\"Error: Invalid age!\")\n        return\n    }\n    println(\"User created: $name, age $age\")\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Quick Quiz",
                                "content":  "\nTest your understanding!\n\n### Question 1\nWhat\u0027s the difference between a parameter and an argument?\n\nA) They are the same thing\nB) Parameter is in the function definition, argument is the actual value passed\nC) Argument is in the function definition, parameter is the actual value passed\nD) Parameters are for strings, arguments are for numbers\n\n\u003cdetails\u003e\n\u003csummary\u003eShow Answer\u003c/summary\u003e\n\n**Answer: B) Parameter is in the function definition, argument is the actual value passed**\n\nExplanation:\n\nParameters are placeholders in the function signature. Arguments are the actual values you provide when calling the function.\n\n\u003c/details\u003e\n\n---\n\n### Question 2\nWhat does this function return?\n\n\nA) 0\nB) The value of x multiplied by 2\nC) Nothing - it\u0027s an error\nD) Unit\n\n\u003cdetails\u003e\n\u003csummary\u003eShow Answer\u003c/summary\u003e\n\n**Answer: C) Nothing - it\u0027s an error**\n\nExplanation: The function has a return type of `Int` but no `return` statement. The calculation `x * 2` happens but the result is not returned.\n\n**Correct version**:\n\n\u003c/details\u003e\n\n---\n\n### Question 3\nWhich of the following is a valid single-expression function?\n\nA) `fun add(a: Int, b: Int): Int { a + b }`\nB) `fun add(a: Int, b: Int) = a + b`\nC) `fun add(a: Int, b: Int) =\u003e a + b`\nD) `fun add(a: Int, b: Int) return a + b`\n\n\u003cdetails\u003e\n\u003csummary\u003eShow Answer\u003c/summary\u003e\n\n**Answer: B) `fun add(a: Int, b: Int) = a + b`**\n\nExplanation: Single-expression functions use `=` instead of curly braces and don\u0027t need the `return` keyword.\n\n\n\u003c/details\u003e\n\n---\n\n### Question 4\nWhat will this code output?\n\n\nA) Error: Missing arguments\nB) Hello, Guest!\nC) Guest, Hello!\nD) Nothing\n\n\u003cdetails\u003e\n\u003csummary\u003eShow Answer\u003c/summary\u003e\n\n**Answer: B) Hello, Guest!**\n\nExplanation: When a function has default parameters, you can call it without providing those arguments. The default values are used:\n- `name` defaults to \"Guest\"\n- `greeting` defaults to \"Hello\"\n\nSo `greet()` becomes `greet(\"Guest\", \"Hello\")` which prints \"Hello, Guest!\"\n\n\u003c/details\u003e\n\n---\n\n",
                                "code":  "fun greet(name: String = \"Guest\", greeting: String = \"Hello\") {\n    println(\"$greeting, $name!\")\n}\n\nfun main() {\n    greet()\n}",
                                "language":  "kotlin"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Summary",
                                "content":  "\nCongratulations! You\u0027ve learned how to create powerful, flexible functions in Kotlin:\n\n### Key Concepts Covered:\n\n**Parameters**:\n- Parameters are inputs that customize function behavior\n- Can have multiple parameters of different types\n- Order matters (unless using named arguments)\n\n**Return Values**:\n- Functions can return values using the `return` keyword\n- Return type is specified after the parameter list: `: Type`\n- Returned values can be stored, used in expressions, or passed to other functions\n\n**Single-Expression Functions**:\n- Use `=` instead of `{}` for simple functions\n- More concise and readable for simple logic\n- Syntax: `fun name(params) = expression`\n\n**Default Parameters**:\n- Provide default values for parameters\n- Make parameters optional\n- Syntax: `param: Type = defaultValue`\n\n**Named Arguments**:\n- Specify parameter names when calling functions\n- Make code more readable\n- Allow calling parameters in any order\n\n**Best Practices**:\n- Use descriptive parameter names\n- Keep functions focused (single responsibility)\n- Validate input parameters\n- Use single-expression functions for simple logic\n- Provide sensible default values\n\n---\n\n"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "What\u0027s Next?",
                                "content":  "\nYou\u0027ve now mastered all the fundamental building blocks of Kotlin programming! In the next lesson, **Lesson 1.9: Part 1 Capstone - Personal Profile Generator**, you\u0027ll put everything together:\n\n- Variables and data types\n- User input\n- Functions with parameters\n- Return values\n- String templates\n- Calculations\n\nYou\u0027ll build a complete, interactive program that showcases all your new skills!\n\n---\n\n**Congratulations on completing Lesson 1.8!**\n\nYou now know how to create flexible, reusable functions that are the foundation of organized, maintainable code. Functions with parameters and return values are essential tools in every programmer\u0027s toolkit.\n\nKeep practicing, and get ready for the capstone project!\n\n"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "1.8.1",
                           "title":  "Function with Parameter",
                           "description":  "Create a function \u0027square\u0027 that takes an Int and returns its square.",
                           "instructions":  "Create a function \u0027square\u0027 that takes an Int and returns its square.",
                           "starterCode":  "fun square(n: Int): Int {\n    // Return n * n\n    \n}\n\nfun main() {\n    val result = square(5)\n    println(result)\n}",
                           "solution":  "fun square(n: Int): Int {\n    return n * n\n}\n\nfun main() {\n    val result = square(5)\n    println(result)\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Prints 25 (5 squared)",
                                                 "expectedOutput":  "25",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Function correctly squares the input",
                                                 "expectedOutput":  "25",
                                                 "isVisible":  false
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use \u0027return\u0027 to send back a value"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Multiply n by itself: n * n"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Function signature is already provided"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting the return statement",
                                                      "consequence":  "Function returns Unit instead of Int",
                                                      "correction":  "Use return n * n to return the result"
                                                  },
                                                  {
                                                      "mistake":  "Using addition instead of multiplication",
                                                      "consequence":  "Returns n + n instead of n * n",
                                                      "correction":  "Square means multiply by itself: n * n"
                                                  },
                                                  {
                                                      "mistake":  "Printing instead of returning",
                                                      "consequence":  "Function does not return a value",
                                                      "correction":  "Use return to give back a value"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       },
                       {
                           "type":  "FREE_CODING",
                           "id":  "1.8.2",
                           "title":  "Function with Multiple Parameters",
                           "description":  "Create a function \u0027multiply\u0027 that takes two Ints and returns their product.",
                           "instructions":  "Create a function \u0027multiply\u0027 that takes two Ints and returns their product.",
                           "starterCode":  "fun multiply(a: Int, b: Int): Int {\n    // Return a * b\n    \n}\n\nfun main() {\n    println(multiply(6, 7))\n}",
                           "solution":  "fun multiply(a: Int, b: Int): Int {\n    return a * b\n}\n\nfun main() {\n    println(multiply(6, 7))\n}",
                           "language":  "kotlin",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Prints 42",
                                                 "expectedOutput":  "42",
                                                 "isVisible":  true
                                             },
                                             {
                                                 "id":  "test-2",
                                                 "description":  "Function multiplies 6 and 7 correctly",
                                                 "expectedOutput":  "42",
                                                 "isVisible":  false
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Return a * b"
                                         },
                                         {
                                             "level":  2,
                                             "text":  "Use return keyword"
                                         },
                                         {
                                             "level":  3,
                                             "text":  "Test with multiply(6, 7) = 42"
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Using wrong operator",
                                                      "consequence":  "Returns a + b instead of a * b",
                                                      "correction":  "Use * for multiplication"
                                                  },
                                                  {
                                                      "mistake":  "Mixing up parameter order",
                                                      "consequence":  "Can cause issues with non-commutative operations",
                                                      "correction":  "Multiplication is commutative, but keep order consistent"
                                                  },
                                                  {
                                                      "mistake":  "Forgetting the return statement",
                                                      "consequence":  "Function returns Unit instead of Int",
                                                      "correction":  "Use return a * b"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "beginner",
    "title":  "Lesson 1.8: Functions with Parameters and Return Values",
    "estimatedMinutes":  65
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
- Search for "kotlin Lesson 1.8: Functions with Parameters and Return Values 2024 2025" to find latest practices
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
  "lessonId": "1.8",
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

