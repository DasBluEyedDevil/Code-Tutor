# Task 3: Database Lessons Challenges - Spec Compliance Review

## Task Overview
Add challenges to 7 database lessons (epoch-5-lesson-1 through epoch-5-lesson-7) with specific topics and correct answer indices.

## Verification Results

### Lesson 9.1: Variables vs Database Persistence
- **File**: `content/courses/java/course.json` (line 6124)
- **Lesson ID**: `epoch-5-lesson-1`
- **Challenge ID**: `lesson-9-1-persistence`
- **Topic**: "Why Do We Need Databases?"
- **Question**: "Why can't we just use variables to store user data permanently?"
- **Correct Answer Index**: 1 ✓
- **Answer**: "Variables only exist while the program runs"
- **Status**: ✓ COMPLIANT

### Lesson 9.2: NOT NULL Constraint
- **File**: `content/courses/java/course.json` (line 6170)
- **Lesson ID**: `epoch-5-lesson-2`
- **Challenge ID**: `lesson-9-2-create-table`
- **Topic**: "SQL Basics - Your First Database"
- **Question**: "Which SQL keyword defines a column that cannot contain NULL values?"
- **Correct Answer Index**: 1 ✓
- **Answer**: "NOT NULL"
- **Status**: ✓ COMPLIANT

### Lesson 9.3: SELECT WHERE ORDER BY DESC
- **File**: `content/courses/java/course.json` (line 6231)
- **Lesson ID**: `epoch-5-lesson-3`
- **Challenge ID**: `lesson-9-3-select-where`
- **Topic**: "SQL Queries - Filtering, Sorting, and Aggregating"
- **Question**: "What does 'SELECT * FROM students WHERE gpa > 3.5 ORDER BY gpa DESC' return?"
- **Correct Answer Index**: 1 ✓
- **Answer**: "Students with GPA above 3.5, sorted highest first"
- **Status**: ✓ COMPLIANT

### Lesson 9.4: LEFT JOIN Keeps All Left Rows
- **File**: `content/courses/java/course.json` (line 6287)
- **Lesson ID**: `epoch-5-lesson-4`
- **Challenge ID**: `lesson-9-4-join-types`
- **Topic**: "JOINs - Connecting Tables"
- **Question**: "You want ALL students, even those not enrolled in any course. Which JOIN should you use?"
- **Correct Answer Index**: 1 ✓
- **Answer**: "LEFT JOIN from students"
- **Status**: ✓ COMPLIANT

### Lesson 9.5: PreparedStatement Prevents SQL Injection
- **File**: `content/courses/java/course.json` (line 6348)
- **Lesson ID**: `epoch-5-lesson-5`
- **Challenge ID**: `lesson-9-5-prepared-stmt`
- **Topic**: "JDBC - Databases + Java"
- **Question**: "Why should you use PreparedStatement instead of concatenating SQL strings?"
- **Correct Answer Index**: 1 ✓
- **Answer**: "PreparedStatement prevents SQL injection attacks"
- **Status**: ✓ COMPLIANT

### Lesson 9.6: @Entity Annotation
- **File**: `content/courses/java/course.json` (line 6414)
- **Lesson ID**: `epoch-5-lesson-6`
- **Challenge ID**: `lesson-9-6-jpa-entity`
- **Topic**: "JPA and Hibernate - The ORM Solution"
- **Question**: "Which annotation marks a class as a database entity in JPA?"
- **Correct Answer Index**: 1 ✓
- **Answer**: "@Entity"
- **Status**: ✓ COMPLIANT

### Lesson 9.7: Flyway V{version}__ Naming
- **File**: `content/courses/java/course.json` (line 6480)
- **Lesson ID**: `epoch-5-lesson-7`
- **Challenge ID**: `lesson-9-7-flyway`
- **Topic**: "Database Migrations with Flyway"
- **Question**: "What is the correct Flyway migration file naming convention?"
- **Correct Answer Index**: 1 ✓
- **Answer**: "V1__create_users.sql"
- **Status**: ✓ COMPLIANT

## Summary

| Lesson | Topic | Challenge ID | Correct Index | Status |
|--------|-------|--------------|---------------|--------|
| 9.1 | Variables vs database persistence | lesson-9-1-persistence | 1 | ✓ |
| 9.2 | NOT NULL constraint | lesson-9-2-create-table | 1 | ✓ |
| 9.3 | SELECT WHERE ORDER BY DESC | lesson-9-3-select-where | 1 | ✓ |
| 9.4 | LEFT JOIN keeps all left rows | lesson-9-4-join-types | 1 | ✓ |
| 9.5 | PreparedStatement prevents SQL injection | lesson-9-5-prepared-stmt | 1 | ✓ |
| 9.6 | @Entity annotation | lesson-9-6-jpa-entity | 1 | ✓ |
| 9.7 | Flyway V{version}__ naming | lesson-9-7-flyway | 1 | ✓ |

## Verification Details

All challenges follow the proper structure:
- **Type**: MULTIPLE_CHOICE
- **Title**: Descriptive title matching lesson topic
- **Description**: Clear question for the learner
- **Options**: Array of 4 answer choices
- **correctAnswer**: Integer index (0-3), all set to 1 per spec
- **Explanation**: Detailed explanation of why the answer is correct

## Compliance Status

✓ **ALL 7 LESSONS FULLY COMPLIANT WITH SPECIFICATION**

All database lessons (epoch-5-lesson-1 through epoch-5-lesson-7) have been successfully verified:
- Each lesson contains exactly one challenge
- All challenges are MULTIPLE_CHOICE type
- All correct answer indices are set to 1 as specified
- All challenges have proper structure and explanations
- Questions are relevant to lesson content

