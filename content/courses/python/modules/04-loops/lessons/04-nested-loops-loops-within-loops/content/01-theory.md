---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine you're organizing a classroom seating chart with 5 rows and 6 seats per row:

<pre style='background-color: #f0f0f0; padding: 10px;'>FOR each row (1 to 5):        ← Outer loop (rows)
    FOR each seat (1 to 6):   ← Inner loop (columns)
        Assign a student
        Print seat position
    Move to next row
</pre>This is a **nested loop** - a loop inside another loop. The outer loop runs once per row, and for EACH row, the inner loop runs completely through all seats.

### How It Works:

- **Outer loop**: Controls the number of times the inner loop runs
- **Inner loop**: Runs completely for each iteration of the outer loop
- **Total iterations**: Outer × Inner (5 rows × 6 seats = 30 total assignments)

### Visual Example:
<pre>Outer loop (row 1):
  Inner loop: seat 1, 2, 3, 4, 5, 6
Outer loop (row 2):
  Inner loop: seat 1, 2, 3, 4, 5, 6
Outer loop (row 3):
  Inner loop: seat 1, 2, 3, 4, 5, 6
...
</pre>### Real-World Examples:

- **Game board (Chess, Checkers)**:
FOR each row (1-8):
&nbsp;&nbsp;&nbsp;&nbsp;FOR each column (A-H):
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Draw square
- **Multiplication table**:
FOR each number (1-10):
&nbsp;&nbsp;&nbsp;&nbsp;FOR each multiplier (1-10):
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Print number × multiplier
- **Image pixels**:
FOR each row of pixels:
&nbsp;&nbsp;&nbsp;&nbsp;FOR each column of pixels:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Set pixel color
- **Calendar month**:
FOR each week:
&nbsp;&nbsp;&nbsp;&nbsp;FOR each day in week:
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Display date

Nested loops are essential for working with 2D data - grids, tables, images, game boards, spreadsheets!