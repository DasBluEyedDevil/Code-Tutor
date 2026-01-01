# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Loops
- **Lesson:** Nested Loops: Loops Within Loops (ID: module-04-lesson-04)
- **Difficulty:** beginner
- **Estimated Time:** 26 minutes

## Current Lesson Content

{
    "id":  "module-04-lesson-04",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Understanding the Concept",
                                "content":  "Imagine you\u0027re organizing a classroom seating chart with 5 rows and 6 seats per row:\n\n\u003cpre style=\u0027background-color: #f0f0f0; padding: 10px;\u0027\u003eFOR each row (1 to 5):        ← Outer loop (rows)\n    FOR each seat (1 to 6):   ← Inner loop (columns)\n        Assign a student\n        Print seat position\n    Move to next row\n\u003c/pre\u003eThis is a **nested loop** - a loop inside another loop. The outer loop runs once per row, and for EACH row, the inner loop runs completely through all seats.\n\n### How It Works:\n\n- **Outer loop**: Controls the number of times the inner loop runs\n- **Inner loop**: Runs completely for each iteration of the outer loop\n- **Total iterations**: Outer × Inner (5 rows × 6 seats = 30 total assignments)\n\n### Visual Example:\n\u003cpre\u003eOuter loop (row 1):\n  Inner loop: seat 1, 2, 3, 4, 5, 6\nOuter loop (row 2):\n  Inner loop: seat 1, 2, 3, 4, 5, 6\nOuter loop (row 3):\n  Inner loop: seat 1, 2, 3, 4, 5, 6\n...\n\u003c/pre\u003e### Real-World Examples:\n\n- **Game board (Chess, Checkers)**:\nFOR each row (1-8):\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;FOR each column (A-H):\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Draw square\n- **Multiplication table**:\nFOR each number (1-10):\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;FOR each multiplier (1-10):\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Print number × multiplier\n- **Image pixels**:\nFOR each row of pixels:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;FOR each column of pixels:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Set pixel color\n- **Calendar month**:\nFOR each week:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;FOR each day in week:\n\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;\u0026nbsp;Display date\n\nNested loops are essential for working with 2D data - grids, tables, images, game boards, spreadsheets!"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example",
                                "content":  "**Expected Output:**\n```\n=== Rectangle Pattern (3x5) ===\n*****\n*****\n*****\n\n=== Right Triangle ===\n*\n**\n***\n****\n*****\n\n=== Multiplication Table (1-5) ===\n1x1= 1  1x2= 2  1x3= 3  1x4= 4  1x5= 5  1x6= 6  1x7= 7  1x8= 8  1x9= 9  1x10=10  \n2x1= 2  2x2= 4  2x3= 6  2x4= 8  2x5=10  2x6=12  2x7=14  2x8=16  2x9=18  2x10=20  \n3x1= 3  3x2= 6  3x3= 9  3x4=12  3x5=15  3x6=18  3x7=21  3x8=24  3x9=27  3x10=30  \n4x1= 4  4x2= 8  4x3=12  4x4=16  4x5=20  4x6=24  4x7=28  4x8=32  4x9=36  4x10=40  \n5x1= 5  5x2=10  5x3=15  5x4=20  5x5=25  5x6=30  5x7=35  5x8=40  5x9=45  5x10=50  \n\n=== Coordinate Grid (3x3) ===\n(1,1) (1,2) (1,3) \n(2,1) (2,2) (2,3) \n(3,1) (3,2) (3,3) \n\n=== Inverted Triangle ===\n*****\n****\n***\n**\n*\n\n=== Numbered Grid ===\n 1  2  3 \n 4  5  6 \n 7  8  9 \n10 11 12 \n\n=== Pyramid Pattern ===\n    *\n   ***\n  *****\n *******\n*********\n\n=== break Only Affects Inner Loop ===\nOuter loop: 0\n  Inner: 0\n  Inner: 1\n  Breaking inner at 2\n  Outer continues...\nOuter loop: 1\n  Inner: 0\n  Inner: 1\n  Breaking inner at 2\n  Outer continues...\nOuter loop: 2\n  Inner: 0\n  Inner: 1\n  Breaking inner at 2\n  Outer continues...\n\n=== Full Times Table ===\n       1   2   3   4   5   6   7   8   9  10\n    ----------------------------------------\n 1 |   1   2   3   4   5   6   7   8   9  10\n 2 |   2   4   6   8  10  12  14  16  18  20\n 3 |   3   6   9  12  15  18  21  24  27  30\n 4 |   4   8  12  16  20  24  28  32  36  40\n 5 |   5  10  15  20  25  30  35  40  45  50\n 6 |   6  12  18  24  30  36  42  48  54  60\n 7 |   7  14  21  28  35  42  49  56  63  70\n 8 |   8  16  24  32  40  48  56  64  72  80\n 9 |   9  18  27  36  45  54  63  72  81  90\n10 |  10  20  30  40  50  60  70  80  90 100\n\n=== Iterating 2D List ===\nRow 1, Seat 1: Alice\nRow 1, Seat 2: Bob\nRow 1, Seat 3: Charlie\nRow 2, Seat 1: David\nRow 2, Seat 2: Eve\nRow 2, Seat 3: Frank\nRow 3, Seat 1: Grace\nRow 3, Seat 2: Henry\nRow 3, Seat 3: Iris\n```",
                                "code":  "# Nested Loops: Loops Within Loops\n\n# Example 1: Basic Nested Loop (Rectangle)\nprint(\"=== Rectangle Pattern (3x5) ===\")\n\nfor row in range(3):  # Outer loop: 3 rows\n    for col in range(5):  # Inner loop: 5 columns per row\n        print(\"*\", end=\"\")  # Print star without newline\n    print()  # Newline after each row\n\n# Output:\n# *****\n# *****\n# *****\n\nprint()\n\n# Example 2: Right Triangle (Growing Rows)\nprint(\"=== Right Triangle ===\")\n\nfor row in range(1, 6):  # Rows: 1, 2, 3, 4, 5\n    for col in range(row):  # Columns: matches row number\n        print(\"*\", end=\"\")\n    print()\n\n# Output:\n# *\n# **\n# ***\n# ****\n# *****\n\nprint()\n\n# Example 3: Multiplication Table\nprint(\"=== Multiplication Table (1-5) ===\")\n\nfor num in range(1, 6):  # Numbers 1-5\n    for mult in range(1, 11):  # Multipliers 1-10\n        result = num * mult\n        print(f\"{num}x{mult}={result:2}\", end=\"  \")  # :2 pads to 2 digits\n    print()  # Newline after each number\u0027s row\n\nprint()\n\n# Example 4: Coordinates Grid\nprint(\"=== Coordinate Grid (3x3) ===\")\n\nfor x in range(1, 4):  # x coordinates: 1, 2, 3\n    for y in range(1, 4):  # y coordinates: 1, 2, 3\n        print(f\"({x},{y})\", end=\" \")\n    print()\n\n# Output:\n# (1,1) (1,2) (1,3)\n# (2,1) (2,2) (2,3)\n# (3,1) (3,2) (3,3)\n\nprint()\n\n# Example 5: Inverted Triangle\nprint(\"=== Inverted Triangle ===\")\n\nfor row in range(5, 0, -1):  # 5, 4, 3, 2, 1 (countdown)\n    for col in range(row):\n        print(\"*\", end=\"\")\n    print()\n\n# Output:\n# *****\n# ****\n# ***\n# **\n# *\n\nprint()\n\n# Example 6: Numbered Grid\nprint(\"=== Numbered Grid ===\")\n\nnumber = 1\n\nfor row in range(4):  # 4 rows\n    for col in range(3):  # 3 columns\n        print(f\"{number:2}\", end=\" \")  # Print with padding\n        number = number + 1\n    print()\n\n# Output:\n#  1  2  3\n#  4  5  6\n#  7  8  9\n# 10 11 12\n\nprint()\n\n# Example 7: Pyramid Pattern (Centered)\nprint(\"=== Pyramid Pattern ===\")\n\nheight = 5\n\nfor row in range(1, height + 1):\n    # Print leading spaces\n    for space in range(height - row):\n        print(\" \", end=\"\")\n    \n    # Print stars\n    for star in range(2 * row - 1):\n        print(\"*\", end=\"\")\n    \n    print()  # Newline\n\n# Output:\n#     *\n#    ***\n#   *****\n#  *******\n# *********\n\nprint()\n\n# Example 8: break/continue in Nested Loops\nprint(\"=== break Only Affects Inner Loop ===\")\n\nfor outer in range(3):\n    print(f\"Outer loop: {outer}\")\n    \n    for inner in range(5):\n        if inner == 2:\n            print(f\"  Breaking inner at {inner}\")\n            break  # Only exits INNER loop\n        print(f\"  Inner: {inner}\")\n    \n    print(\"  Outer continues...\")  # This still runs!\n\nprint()\n\n# Example 9: Times Table (Full 10x10)\nprint(\"=== Full Times Table ===\")\n\n# Header row\nprint(\"    \", end=\"\")\nfor i in range(1, 11):\n    print(f\"{i:4}\", end=\"\")\nprint()\nprint(\"    \" + \"-\" * 40)\n\n# Table rows\nfor row in range(1, 11):\n    print(f\"{row:2} |\", end=\"\")\n    for col in range(1, 11):\n        product = row * col\n        print(f\"{product:4}\", end=\"\")\n    print()\n\nprint()\n\n# Example 10: Nested List Iteration (Preview)\nprint(\"=== Iterating 2D List ===\")\n\nclassroom = [\n    [\"Alice\", \"Bob\", \"Charlie\"],\n    [\"David\", \"Eve\", \"Frank\"],\n    [\"Grace\", \"Henry\", \"Iris\"]\n]\n\nfor row_num, row in enumerate(classroom):\n    for seat_num, student in enumerate(row):\n        print(f\"Row {row_num + 1}, Seat {seat_num + 1}: {student}\")",
                                "language":  "python"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Syntax Breakdown",
                                "content":  "### Nested Loop Anatomy:\n```\nfor outer_var in outer_sequence:    # Outer loop\n    # Outer loop body\n    \n    for inner_var in inner_sequence:  # Inner loop\n        # Inner loop body\n        # Can use both outer_var and inner_var here\n    \n    # Back to outer loop\n\n```\n#### Execution Flow:\n\n- Outer loop starts (first iteration)\n- Inner loop runs completely (all its iterations)\n- Outer loop continues (second iteration)\n- Inner loop runs completely again\n- Repeat until outer loop finishes\n\n#### Visual Example:\n```\nfor row in range(3):      # Outer: rows\n    for col in range(2):  # Inner: columns\n        print(\"*\", end=\"\")\n    print()  # Newline\n\n# Execution trace:\n# row=0: col=0 (*), col=1 (*), newline\n# row=1: col=0 (*), col=1 (*), newline\n# row=2: col=0 (*), col=1 (*), newline\n\n# Output:\n# **\n# **\n# **\n\n```\n### Total Iterations:\n```\nOuter iterations × Inner iterations = Total\n\nfor i in range(3):    # Runs 3 times\n    for j in range(4):  # Runs 4 times per outer\n        print(\"X\")\n\n# Total: 3 × 4 = 12 times \"X\" is printed\n\n```\n\u003ctable border=\u00271\u0027 cellpadding=\u00275\u0027 style=\u0027border-collapse: collapse;\u0027\u003e\u003ctr\u003e\u003cth\u003eOuter (i)\u003c/th\u003e\u003cth\u003eInner (j) iterations\u003c/th\u003e\u003cth\u003eTotal so far\u003c/th\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003e0\u003c/td\u003e\u003ctd\u003e0, 1, 2, 3\u003c/td\u003e\u003ctd\u003e4\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003e1\u003c/td\u003e\u003ctd\u003e0, 1, 2, 3\u003c/td\u003e\u003ctd\u003e8\u003c/td\u003e\u003c/tr\u003e\u003ctr\u003e\u003ctd\u003e2\u003c/td\u003e\u003ctd\u003e0, 1, 2, 3\u003c/td\u003e\u003ctd\u003e12\u003c/td\u003e\u003c/tr\u003e\u003c/table\u003e### Common Patterns:\n#### 1. Rectangle/Grid (Fixed Dimensions)\n```\nfor row in range(rows):    # Fixed number of rows\n    for col in range(cols):  # Fixed number per row\n        print(\"*\", end=\"\")\n    print()\n\n```\n#### 2. Right Triangle (Growing)\n```\nfor row in range(1, n+1):  # Row numbers\n    for col in range(row):   # Number of cols = row number\n        print(\"*\", end=\"\")\n    print()\n\n```\n#### 3. Inverted Triangle (Shrinking)\n```\nfor row in range(n, 0, -1):  # Countdown\n    for col in range(row):     # Decreasing columns\n        print(\"*\", end=\"\")\n    print()\n\n```\n#### 4. Multiplication Table\n```\nfor num in range(1, 11):     # Numbers 1-10\n    for mult in range(1, 11):  # Times 1-10\n        print(num * mult, end=\" \")\n    print()\n\n```\n#### 5. Coordinate Grid\n```\nfor x in range(width):\n    for y in range(height):\n        process_cell(x, y)\n\n```\n### break and continue in Nested Loops:\n**Critical:** break/continue only affect the innermost loop they\u0027re in!\n\n```\nfor outer in range(3):\n    print(f\"Outer: {outer}\")\n    \n    for inner in range(5):\n        if inner == 2:\n            break  # Only exits THIS (inner) loop\n        print(f\"  Inner: {inner}\")\n    \n    # Outer loop continues!\n    print(\"  Back to outer\")\n\n# Output:\n# Outer: 0\n#   Inner: 0\n#   Inner: 1\n#   Back to outer\n# Outer: 1\n#   Inner: 0\n#   Inner: 1\n#   Back to outer\n# Outer: 2\n#   Inner: 0\n#   Inner: 1\n#   Back to outer\n\n```\nTo break out of multiple loops, you need a different strategy:\n\n```\n# Method 1: Flag variable\nbreak_outer = False\n\nfor outer in range(10):\n    for inner in range(10):\n        if condition:\n            break_outer = True\n            break\n    if break_outer:\n        break\n\n# Method 2: Function with return (better!)\ndef search_2d():\n    for outer in range(10):\n        for inner in range(10):\n            if found_it:\n                return  # Exits function (and all loops)\n\n```\n### Performance Considerations:\nNested loops can get expensive quickly:\n\n```\nfor i in range(100):      # 100 times\n    for j in range(100):  # × 100 times\n        # Total: 10,000 iterations!\n\nfor i in range(1000):     # 1,000 times\n    for j in range(1000): # × 1,000 times\n        # Total: 1,000,000 iterations!\n\n```\n**Big O Notation (preview):**\n\n- Single loop: O(n) - linear time\n- Nested loop: O(n²) - quadratic time (much slower!)\n- Triple nested: O(n³) - cubic time (very slow!)\n\n**Best practices:**\n\n- Minimize nesting depth (2-3 levels max)\n- Use break to exit early when possible\n- Consider alternatives for very large datasets\n\n### Common Mistakes:\n\n\u003cli\u003e**Forgetting newline after inner loop**:```\n# WRONG: Everything on one line\nfor row in range(3):\n    for col in range(5):\n        print(\"*\", end=\"\")\n# Output: *************** (all one line!)\n\n# CORRECT: Newline after each row\nfor row in range(3):\n    for col in range(5):\n        print(\"*\", end=\"\")\n    print()  # Newline after inner loop\n\n```\n\u003c/li\u003e\u003cli\u003e**Using wrong variable in inner loop**:```\n# WRONG:\nfor row in range(5):\n    for col in range(row):  # Uses \u0027row\u0027, not a fixed number\n        print(row, end=\"\")  # Prints row number, not col!\n\n# If you want a square, use a fixed number:\nfor row in range(5):\n    for col in range(5):  # Fixed 5 columns\n        print(\"*\", end=\"\")\n\n```\n\u003c/li\u003e\u003cli\u003e**Confusing indentation**:```\n# WRONG: print() indented inside inner loop\nfor row in range(3):\n    for col in range(5):\n        print(\"*\", end=\"\")\n        print()  # Newline after EACH star!\n# Output: *\n#         *\n#         * (each star on new line)\n\n# CORRECT: print() at outer loop level\nfor row in range(3):\n    for col in range(5):\n        print(\"*\", end=\"\")\n    print()  # Newline after each ROW\n\n```\n\u003c/li\u003e"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **Nested loops** are loops inside other loops\n- **Outer loop** controls how many times inner loop runs completely\n- **Total iterations** = outer × inner (can get large quickly!)\n- **Use for 2D data**: Grids, tables, images, game boards\n- **Common patterns**:\n\u003cli\u003eRectangle: Fixed rows × fixed columns\n- Triangle: Rows growing/shrinking\n- Tables: Multiplication, coordinates\n- Patterns: Pyramid, diamond, checkerboard\n\n\u003c/li\u003e- **break/continue** only affect innermost loop\n- **Indentation critical**: Determines which loop a statement belongs to\n- **Performance**: Nested loops are O(n²) - expensive for large n!\n- **Print placement**: Inside inner for same row, after inner for new row\n\n### Essential Patterns:\n```\n# Rectangle (m × n):\nfor row in range(m):\n    for col in range(n):\n        print(\"*\", end=\"\")\n    print()\n\n# Right Triangle:\nfor row in range(1, n+1):\n    for col in range(row):\n        print(\"*\", end=\"\")\n    print()\n\n# Times Table:\nfor row in range(1, n+1):\n    for col in range(1, n+1):\n        print(row * col, end=\" \")\n    print()\n\n# Checkerboard:\nfor row in range(n):\n    for col in range(n):\n        if (row + col) % 2 == 0:\n            print(\"*\", end=\" \")\n        else:\n            print(\"-\", end=\" \")\n    print()\n\n```\n### Before Moving On:\nMake sure you can:\n\n- Write nested loops with proper indentation\n- Create rectangles, triangles, and pyramids\n- Understand outer × inner = total iterations\n- Place print() correctly for rows vs columns\n- Use break/continue understanding they only affect innermost loop\n\n### Module 4 Almost Complete!\nYou\u0027ve mastered:\n\n- ✅ while loops (condition-based repetition)\n- ✅ for loops (sequence iteration)\n- ✅ Loop control (break, continue, pass, else)\n- ✅ Nested loops (2D patterns and grids)\n\n### Coming Up: Module 4 Final Lesson\nIn **Lesson 5: Mini-Project - Practical Loop Programs**, you\u0027ll build:\n\n- Number guessing game (combining while + input validation)\n- Menu-driven program (while True + break)\n- Data analyzer (for loops + accumulators)\n- Pattern printer (nested loops + user choice)\n\nYou\u0027ll tie together everything from Module 4 into complete, working programs!"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "module-04-lesson-04-challenge-1",
                           "title":  "Practice Exercise",
                           "description":  "Build a **Pattern Generator Program** that creates three different visual patterns based on user input.\n\n**Patterns to create:**\n\n- **Diamond Pattern** (combines growing and shrinking triangles)\n- **Checkerboard** (alternating characters)\n- **Multiplication Table** (user-specified size)\n\n**Example output:**\n\n\u003cpre\u003e=== Pattern Generator ===\"\n\nEnter size: 5\n\nPattern 1 - Diamond:\n    *\n   ***\n  *****\n   ***\n    *\n\nPattern 2 - Checkerboard:\n* - * - *\n- * - * -\n* - * - *\n- * - * -\n* - * - *\n\nPattern 3 - Times Table (5x5):\n  1  2  3  4  5\n  2  4  6  8 10\n  3  6  9 12 15\n  4  8 12 16 20\n  5 10 15 20 25\n\u003c/pre\u003e**Hints:**\n\n- Diamond: Create top half (growing pyramid), then bottom half (shrinking)\n- Checkerboard: Use (row + col) % 2 to alternate\n- Times Table: Multiply row × column",
                           "instructions":  "Build a **Pattern Generator Program** that creates three different visual patterns based on user input.\n\n**Patterns to create:**\n\n- **Diamond Pattern** (combines growing and shrinking triangles)\n- **Checkerboard** (alternating characters)\n- **Multiplication Table** (user-specified size)\n\n**Example output:**\n\n\u003cpre\u003e=== Pattern Generator ===\"\n\nEnter size: 5\n\nPattern 1 - Diamond:\n    *\n   ***\n  *****\n   ***\n    *\n\nPattern 2 - Checkerboard:\n* - * - *\n- * - * -\n* - * - *\n- * - * -\n* - * - *\n\nPattern 3 - Times Table (5x5):\n  1  2  3  4  5\n  2  4  6  8 10\n  3  6  9 12 15\n  4  8 12 16 20\n  5 10 15 20 25\n\u003c/pre\u003e**Hints:**\n\n- Diamond: Create top half (growing pyramid), then bottom half (shrinking)\n- Checkerboard: Use (row + col) % 2 to alternate\n- Times Table: Multiply row × column",
                           "starterCode":  "# Pattern Generator Program\n# Create visual patterns using nested loops\n\nprint(\"=== Pattern Generator ===\")\nprint()\n\nsize = int(input(\"Enter size: \"))\n\nprint()\n\n# YOUR CODE HERE:\n\n# Pattern 1: Diamond\nprint(\"Pattern 1 - Diamond:\")\n\n# Top half (growing pyramid)\nfor row in range(1, size + 1):\n    # Print spaces\n    for space in range(size - row):\n        print(\" \", end=\"\")\n    # Print stars\n    for star in range(2 * row - 1):\n        print(\"*\", end=\"\")\n    print()\n\n# Bottom half (shrinking)\nfor row in range(size - 1, 0, -1):\n    # Your code for bottom half\n    pass\n\nprint()\n\n# Pattern 2: Checkerboard\nprint(\"Pattern 2 - Checkerboard:\")\n\nfor row in range(size):\n    for col in range(size):\n        # Use (row + col) % 2 to alternate\n        if :  # Even sum\n            print(\"*\", end=\" \")\n        else:  # Odd sum\n            print(\"-\", end=\" \")\n    print()\n\nprint()\n\n# Pattern 3: Times Table\nprint(f\"Pattern 3 - Times Table ({size}x{size}):\")\n\nfor row in range(1, size + 1):\n    for col in range(1, size + 1):\n        product = \n        print(f\"{product:3}\", end=\" \")  # :3 pads to 3 characters\n    print()\n",
                           "solution":  "# Pattern Generator Program - SOLUTION\n# Create visual patterns using nested loops\n\nprint(\"=== Pattern Generator ===\")\nprint()\n\nsize = int(input(\"Enter size: \"))\n\nprint()\n\n# Pattern 1: Diamond\nprint(\"Pattern 1 - Diamond:\")\n\n# Top half (growing pyramid)\nfor row in range(1, size + 1):\n    # Print leading spaces\n    for space in range(size - row):\n        print(\" \", end=\"\")\n    # Print stars\n    for star in range(2 * row - 1):\n        print(\"*\", end=\"\")\n    print()\n\n# Bottom half (shrinking pyramid)\nfor row in range(size - 1, 0, -1):\n    # Print leading spaces\n    for space in range(size - row):\n        print(\" \", end=\"\")\n    # Print stars\n    for star in range(2 * row - 1):\n        print(\"*\", end=\"\")\n    print()\n\nprint()\n\n# Pattern 2: Checkerboard\nprint(\"Pattern 2 - Checkerboard:\")\n\nfor row in range(size):\n    for col in range(size):\n        # Alternate based on position sum\n        if (row + col) % 2 == 0:\n            print(\"*\", end=\" \")\n        else:\n            print(\"-\", end=\" \")\n    print()\n\nprint()\n\n# Pattern 3: Times Table\nprint(f\"Pattern 3 - Times Table ({size}x{size}):\")\n\nfor row in range(1, size + 1):\n    for col in range(1, size + 1):\n        product = row * col\n        print(f\"{product:3}\", end=\" \")  # :3 pads to 3 characters\n    print()",
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
                                             "text":  "For the diamond bottom half, use range(size-1, 0, -1) and same logic as top but with decreasing rows. For checkerboard, (row + col) % 2 == 0 means even position. For times table, multiply row * col. Remember to print() after inner loops to create newlines!"
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
    "title":  "Nested Loops: Loops Within Loops",
    "estimatedMinutes":  26
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
- Search for "python Nested Loops: Loops Within Loops 2024 2025" to find latest practices
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
  "lessonId": "module-04-lesson-04",
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

