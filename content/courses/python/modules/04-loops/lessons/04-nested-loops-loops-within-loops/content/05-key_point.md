---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Nested loops** are loops inside other loops
- **Outer loop** controls how many times inner loop runs completely
- **Total iterations** = outer × inner (can get large quickly!)
- **Use for 2D data**: Grids, tables, images, game boards
- **Common patterns**:
<li>Rectangle: Fixed rows × fixed columns
- Triangle: Rows growing/shrinking
- Tables: Multiplication, coordinates
- Patterns: Pyramid, diamond, checkerboard

</li>- **break/continue** only affect innermost loop
- **Indentation critical**: Determines which loop a statement belongs to
- **Performance**: Nested loops are O(n²) - expensive for large n!
- **Print placement**: Inside inner for same row, after inner for new row

### Essential Patterns:
```
# Rectangle (m × n):
for row in range(m):
    for col in range(n):
        print("*", end="")
    print()

# Right Triangle:
for row in range(1, n+1):
    for col in range(row):
        print("*", end="")
    print()

# Times Table:
for row in range(1, n+1):
    for col in range(1, n+1):
        print(row * col, end=" ")
    print()

# Checkerboard:
for row in range(n):
    for col in range(n):
        if (row + col) % 2 == 0:
            print("*", end=" ")
        else:
            print("-", end=" ")
    print()

```
### Before Moving On:
Make sure you can:

- Write nested loops with proper indentation
- Create rectangles, triangles, and pyramids
- Understand outer × inner = total iterations
- Place print() correctly for rows vs columns
- Use break/continue understanding they only affect innermost loop

### Module 4 Almost Complete!
You've mastered:

- ✅ while loops (condition-based repetition)
- ✅ for loops (sequence iteration)
- ✅ Loop control (break, continue, pass, else)
- ✅ Nested loops (2D patterns and grids)

### Coming Up: Module 4 Final Lesson
In **Lesson 5: Mini-Project - Practical Loop Programs**, you'll build:

- Number guessing game (combining while + input validation)
- Menu-driven program (while True + break)
- Data analyzer (for loops + accumulators)
- Pattern printer (nested loops + user choice)

You'll tie together everything from Module 4 into complete, working programs!