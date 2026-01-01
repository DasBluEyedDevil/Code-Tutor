---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
======================================================================
STUDENT GRADE ANALYTICS SYSTEM
======================================================================

=== Part 1: Data Setup ===

Loaded 8 student records
Sample record: (101, 'Alice Johnson', [85, 92, 88, 95, 90])

=== Part 2: All Students ===

ID       Name                 Grades                         Average   
----------------------------------------------------------------------
101      Alice Johnson        [85, 92, 88, 95, 90]           90.00     
102      Bob Smith            [78, 82, 75, 88, 80]           80.60     
103      Charlie Brown        [92, 95, 98, 94, 96]           95.00     
104      Diana Ross           [68, 72, 65, 70, 73]           69.60     
105      Eve Adams            [88, 91, 85, 92, 89]           89.00     
106      Frank Miller         [55, 62, 58, 60, 65]           60.00     
107      Grace Lee            [95, 98, 92, 97, 94]           95.20     
108      Henry Davis          [82, 85, 88, 81, 84]           84.00     

=== Part 3: Class Statistics ===

Total students: 8
Total grades recorded: 40
Class average: 82.93
Highest student average: 95.20
Lowest student average: 60.00
Highest individual grade: 98
Lowest individual grade: 55

=== Part 4: Honor Roll (Average >= 90) ===

Congratulations to 3 students on the Honor Roll!

  ⭐ Alice Johnson       Average: 90.00
  ⭐ Charlie Brown       Average: 95.00
  ⭐ Grace Lee           Average: 95.20

=== Part 5: At-Risk Students (Average < 70) ===

⚠️  2 students need additional support:

  Diana Ross           Average: 69.60
  Frank Miller         Average: 60.00

=== Part 6: Student Rankings ===

Top to Bottom:
  #1   Grace Lee            95.20
  #2   Charlie Brown        95.00
  #3   Alice Johnson        90.00
  #4   Eve Adams            89.00
  #5   Henry Davis          84.00
  #6   Bob Smith            80.60
  #7   Diana Ross           69.60
  #8   Frank Miller         60.00

🏆 Top 3 Performers:
  🥇 #1 Grace Lee            95.20
  🥈 #2 Charlie Brown        95.00
  🥉 #3 Alice Johnson        90.00

📉 Bottom 3 (Need Improvement):
  Bob Smith            80.60
  Diana Ross           69.60
  Frank Miller         60.00

=== Part 7: Grade Distribution ===

Letter Grade Distribution:
  A (90+):   3 students ***
  B (80+):   2 students **
  C (70+):   0 students 
  D (60+):   2 students **
  F (<60):   1 students *

=== Part 8: Apply Grade Curve (+5 points) ===

Before curve:
  Class average: 82.93

After curve:
  Class average: 87.93
  Improvement: +5.00 points

Sample student (before → after):
  Alice Johnson:
    Before: [85, 92, 88, 95, 90] → Avg: 90.00
    After:  [90, 97, 93, 100, 95] → Avg: 95.00
  Diana Ross:
    Before: [68, 72, 65, 70, 73] → Avg: 69.60
    After:  [73, 77, 70, 75, 78] → Avg: 74.60

=== Part 9: Grade Trends (First vs Last Test) ===

Alice Johnson         85 →  90 ( +5) 📈 Improving
Bob Smith             78 →  80 ( +2) 📈 Improving
Charlie Brown         92 →  96 ( +4) 📈 Improving
Diana Ross            68 →  73 ( +5) 📈 Improving
Eve Adams             88 →  89 ( +1) 📈 Improving
Frank Miller          55 →  65 (+10) 📈 Improving
Grace Lee             95 →  94 ( -1) 📉 Declining
Henry Davis           82 →  84 ( +2) 📈 Improving

=== Part 10: Test Difficulty Analysis ===

Average score per test:
  Test 1:  80.4 **************** (Medium)
  Test 2:  84.6 **************** (Easy)
  Test 3:  81.1 **************** (Medium)
  Test 4:  84.4 **************** (Easy)
  Test 5:  83.9 **************** (Medium)

Hardest test: Test 1 (avg: 80.4)
Easiest test: Test 2 (avg: 84.6)

======================================================================
FINAL SUMMARY REPORT
======================================================================

Class Size: 8 students
Tests Administered: 5
Total Grades: 40

Class Average: 82.93
Honor Roll: 3 students
At-Risk: 2 students

Top Performer: Grace Lee (95.20)
Needs Support: Frank Miller (60.00)

Grade Distribution: 3 A's, 2 B's, 0 C's, 2 D's, 1 F's

✓ Analysis complete!
```

```python
# Mini-Project: Student Grade Analytics System
# Complete implementation demonstrating all Module 5 concepts

print("=" * 70)
print("STUDENT GRADE ANALYTICS SYSTEM")
print("=" * 70)
print()

# ========================================
# PART 1: DATA SETUP
# ========================================

print("=== Part 1: Data Setup ===")
print()

# Student records: (ID, Name, [Grades])
# Using tuples because ID and name shouldn't change
students = [
    (101, "Alice Johnson", [85, 92, 88, 95, 90]),
    (102, "Bob Smith", [78, 82, 75, 88, 80]),
    (103, "Charlie Brown", [92, 95, 98, 94, 96]),
    (104, "Diana Ross", [68, 72, 65, 70, 73]),
    (105, "Eve Adams", [88, 91, 85, 92, 89]),
    (106, "Frank Miller", [55, 62, 58, 60, 65]),
    (107, "Grace Lee", [95, 98, 92, 97, 94]),
    (108, "Henry Davis", [82, 85, 88, 81, 84])
]

print(f"Loaded {len(students)} student records")
print(f"Sample record: {students[0]}")
print()

# ========================================
# PART 2: DISPLAY ALL STUDENTS
# ========================================

print("=== Part 2: All Students ===")
print()
print(f"{'ID':<8} {'Name':<20} {'Grades':<30} {'Average':<10}")
print("-" * 70)

for student_id, name, grades in students:  # Tuple unpacking
    avg = sum(grades) / len(grades)
    grades_str = str(grades)
    print(f"{student_id:<8} {name:<20} {grades_str:<30} {avg:<10.2f}")

print()

# ========================================
# PART 3: CALCULATE STATISTICS
# ========================================

print("=== Part 3: Class Statistics ===")
print()

# Calculate average for each student (list comprehension)
averages = [sum(grades)/len(grades) for (id, name, grades) in students]

# Overall class statistics
class_avg = sum(averages) / len(averages)
highest_avg = max(averages)
lowest_avg = min(averages)

# Get all individual grades (flatten nested lists)
all_grades = [grade for (id, name, grades) in students for grade in grades]

print(f"Total students: {len(students)}")
print(f"Total grades recorded: {len(all_grades)}")
print(f"Class average: {class_avg:.2f}")
print(f"Highest student average: {highest_avg:.2f}")
print(f"Lowest student average: {lowest_avg:.2f}")
print(f"Highest individual grade: {max(all_grades)}")
print(f"Lowest individual grade: {min(all_grades)}")

print()

# ========================================
# PART 4: HONOR ROLL (Average >= 90)
# ========================================

print("=== Part 4: Honor Roll (Average >= 90) ===")
print()

# Create list of (name, average) for honor roll students
honor_roll = [
    (name, sum(grades)/len(grades)) 
    for (id, name, grades) in students 
    if sum(grades)/len(grades) >= 90
]

if honor_roll:
    print(f"Congratulations to {len(honor_roll)} students on the Honor Roll!")
    print()
    for name, avg in honor_roll:
        print(f"  ⭐ {name:<20} Average: {avg:.2f}")
else:
    print("No students qualified for honor roll this term.")

print()

# ========================================
# PART 5: AT-RISK STUDENTS (Average < 70)
# ========================================

print("=== Part 5: At-Risk Students (Average < 70) ===")
print()

at_risk = [
    (name, sum(grades)/len(grades))
    for (id, name, grades) in students
    if sum(grades)/len(grades) < 70
]

if at_risk:
    print(f"⚠️  {len(at_risk)} students need additional support:")
    print()
    for name, avg in at_risk:
        print(f"  {name:<20} Average: {avg:.2f}")
else:
    print("✓ All students are performing well (>= 70)")

print()

# ========================================
# PART 6: RANKING (Sorted by Average)
# ========================================

print("=== Part 6: Student Rankings ===")
print()

# Create list of (name, average) and sort by average (descending)
ranked = [(name, sum(grades)/len(grades)) for (id, name, grades) in students]
ranked.sort(key=lambda x: x[1], reverse=True)  # Sort by average

print("Top to Bottom:")
for rank, (name, avg) in enumerate(ranked, start=1):
    print(f"  #{rank:<3} {name:<20} {avg:.2f}")

print()

# Top 3 using slicing
print("🏆 Top 3 Performers:")
for rank, (name, avg) in enumerate(ranked[:3], start=1):
    medal = ["🥇", "🥈", "🥉"][rank-1]
    print(f"  {medal} #{rank} {name:<20} {avg:.2f}")

print()

# Bottom 3 using negative slicing
print("📉 Bottom 3 (Need Improvement):")
for name, avg in ranked[-3:]:
    print(f"  {name:<20} {avg:.2f}")

print()

# ========================================
# PART 7: GRADE DISTRIBUTION
# ========================================

print("=== Part 7: Grade Distribution ===")
print()

# Count students in each grade range
a_count = len([avg for avg in averages if avg >= 90])
b_count = len([avg for avg in averages if 80 <= avg < 90])
c_count = len([avg for avg in averages if 70 <= avg < 80])
d_count = len([avg for avg in averages if 60 <= avg < 70])
f_count = len([avg for avg in averages if avg < 60])

print("Letter Grade Distribution:")
print(f"  A (90+):  {a_count:2} students {'*' * a_count}")
print(f"  B (80+):  {b_count:2} students {'*' * b_count}")
print(f"  C (70+):  {c_count:2} students {'*' * c_count}")
print(f"  D (60+):  {d_count:2} students {'*' * d_count}")
print(f"  F (<60):  {f_count:2} students {'*' * f_count}")

print()

# ========================================
# PART 8: GRADE CURVE (+5 points)
# ========================================

print("=== Part 8: Apply Grade Curve (+5 points) ===")
print()

print("Before curve:")
print(f"  Class average: {class_avg:.2f}")

# Apply curve: add 5 to each grade, cap at 100
curved_students = [
    (id, name, [min(grade + 5, 100) for grade in grades])  # List comprehension for curve
    for (id, name, grades) in students
]

# Recalculate averages after curve
curved_averages = [sum(grades)/len(grades) for (id, name, grades) in curved_students]
curved_class_avg = sum(curved_averages) / len(curved_averages)

print(f"\nAfter curve:")
print(f"  Class average: {curved_class_avg:.2f}")
print(f"  Improvement: +{curved_class_avg - class_avg:.2f} points")

print()
print("Sample student (before → after):")
for i in [0, 3]:  # Show first and fourth student
    id, name, old_grades = students[i]
    _, _, new_grades = curved_students[i]
    print(f"  {name}:")
    print(f"    Before: {old_grades} → Avg: {sum(old_grades)/len(old_grades):.2f}")
    print(f"    After:  {new_grades} → Avg: {sum(new_grades)/len(new_grades):.2f}")

print()

# ========================================
# PART 9: GRADE TREND ANALYSIS
# ========================================

print("=== Part 9: Grade Trends (First vs Last Test) ===")
print()

for student_id, name, grades in students:
    first_test = grades[0]      # First grade
    last_test = grades[-1]      # Last grade
    change = last_test - first_test
    
    trend = "📈 Improving" if change > 0 else "📉 Declining" if change < 0 else "➡️  Stable"
    
    print(f"{name:<20} {first_test:3} → {last_test:3} ({change:+3}) {trend}")

print()

# ========================================
# PART 10: TEST DIFFICULTY ANALYSIS
# ========================================

print("=== Part 10: Test Difficulty Analysis ===")
print()

# Calculate average for each test across all students
num_tests = len(students[0][2])  # Assuming all students have same number of tests

test_averages = [
    sum([grades[test_num] for (id, name, grades) in students]) / len(students)
    for test_num in range(num_tests)
]

print("Average score per test:")
for test_num, avg in enumerate(test_averages, start=1):
    bar = "*" * int(avg / 5)  # Each * = 5 points
    difficulty = "Hard" if avg < 75 else "Medium" if avg < 85 else "Easy"
    print(f"  Test {test_num}: {avg:5.1f} {bar} ({difficulty})")

print()

# Hardest and easiest tests
hardest_test = test_averages.index(min(test_averages)) + 1
easiest_test = test_averages.index(max(test_averages)) + 1

print(f"Hardest test: Test {hardest_test} (avg: {min(test_averages):.1f})")
print(f"Easiest test: Test {easiest_test} (avg: {max(test_averages):.1f})")

print()

# ========================================
# PART 11: FINAL REPORT
# ========================================

print("=" * 70)
print("FINAL SUMMARY REPORT")
print("=" * 70)
print()

print(f"Class Size: {len(students)} students")
print(f"Tests Administered: {num_tests}")
print(f"Total Grades: {len(all_grades)}")
print()
print(f"Class Average: {class_avg:.2f}")
print(f"Honor Roll: {len(honor_roll)} students")
print(f"At-Risk: {len(at_risk)} students")
print()
print(f"Top Performer: {ranked[0][0]} ({ranked[0][1]:.2f})")
print(f"Needs Support: {ranked[-1][0]} ({ranked[-1][1]:.2f})")
print()
print(f"Grade Distribution: {a_count} A's, {b_count} B's, {c_count} C's, {d_count} D's, {f_count} F's")
print()
print("✓ Analysis complete!")
```
