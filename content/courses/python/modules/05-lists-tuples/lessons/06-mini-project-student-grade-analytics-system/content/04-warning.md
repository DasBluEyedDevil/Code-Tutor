---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Recalculating averages multiple times**
```python
# WRONG - inefficient: calculates average 3 times per student
for student in students:
    id, name, grades = student
    if sum(grades)/len(grades) >= 90:  # Calculation 1
        avg = sum(grades)/len(grades)  # Calculation 2
        print(f"{name}: {sum(grades)/len(grades)}")  # Calculation 3

# CORRECT - calculate once, reuse
for student in students:
    id, name, grades = student
    avg = sum(grades) / len(grades)  # Calculate once
    if avg >= 90:
        print(f"{name}: {avg}")
```

**2. Modifying list while iterating**
```python
# WRONG - changing list during iteration
for i, student in enumerate(students):
    if student[2] < 60:  # Low grades
        students.remove(student)  # Can skip items!

# CORRECT - iterate over copy or use list comprehension
students = [s for s in students if s[2] >= 60]
# Or create new filtered list
passing_students = [s for s in students if s[2] >= 60]
```

**3. Forgetting tuple immutability with nested lists**
```python
# CAUTION - tuple is immutable but nested list is NOT
student = (101, "Alice", [85, 90, 88])
student[2].append(95)  # Works! List inside tuple IS mutable
print(student)  # (101, "Alice", [85, 90, 88, 95])

# The tuple itself can't change, but mutable items inside can!
student[0] = 102  # TypeError - can't change tuple element
```

**4. Division by zero with empty grade lists**
```python
# WRONG - crashes on empty grades
for id, name, grades in students:
    avg = sum(grades) / len(grades)  # ZeroDivisionError if grades is []

# CORRECT - check for empty list
for id, name, grades in students:
    if grades:
        avg = sum(grades) / len(grades)
    else:
        avg = 0.0
        print(f"{name} has no grades")
```

**5. Sorting changes original list unexpectedly**
```python
# WRONG - sort() modifies in place
original_order = students
students.sort(key=lambda x: x[2])  # original_order also sorted!

# CORRECT - use sorted() to preserve original
original_order = students
sorted_students = sorted(students, key=lambda x: x[2])
# original_order is unchanged
```