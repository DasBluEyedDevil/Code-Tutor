# Enhanced Interactive Course Converters

This document describes the 4 enhanced converter scripts that extract **ALL** interactive content from programming courses.

## Overview

Each converter transforms legacy course formats into the unified **interactive course format** defined in `/home/user/Code-Tutor/apps/web/src/types/interactive.ts`.

### What Gets Extracted

- **Content Sections**: Theory, analogies, examples, key points, warnings, experiments
- **Coding Challenges**: FreeCodingChallenge with starter code, solutions, test cases, hints
- **Quizzes**: Multiple choice, true/false, and code output questions
- **Experiments**: Interactive code demonstrations
- **Bonus Challenges**: Additional practice exercises

---

## 1. Python Interactive Converter

**Script**: `/home/user/Code-Tutor/scripts/convert-python-interactive.ts`

### Source Data
- **Location**: `/tmp/Python-Training-Course/`
- **Format**: JSON lesson files + separate quiz files
- **Structure**:
  - `content/modules/module_XX/lesson_YY.json` - 80 lesson files
  - `content/quizzes/quiz_01.json` through `quiz_14.json` - Quiz files

### What It Extracts

**From Lesson Files:**
- Content blocks → ContentSections (Analogy, Theory, Example, Key Points)
- Exercise blocks → FreeCodingChallenge
  - Instructions, starter code, hints, solution
  - Common mistakes → CommonMistake objects
- Code examples with explanations

**From Quiz Files:**
- Multiple choice questions → MultipleChoiceChallenge
- True/false questions → TrueFalseChallenge
- Code output questions → CodeOutputChallenge

### Output Statistics
```
📊 39 lessons
💻 36 coding challenges
📋 14 quizzes
❓ 273 quiz questions
⏱️  ~26 hours of content
```

### Usage
```bash
npx tsx scripts/convert-python-interactive.ts
# or specify paths:
npx tsx scripts/convert-python-interactive.ts /tmp/Python-Training-Course /output/path/course.json
```

---

## 2. JavaScript Interactive Converter

**Script**: `/home/user/Code-Tutor/scripts/convert-javascript-interactive.ts`

### Source Data
- **Location**: `/tmp/JavaScript-TypeScript-Training-Course/src/main/resources/content/`
- **Format**: 14 JSON module files with embedded lessons and challenges
- **Structure**: `module1.json` through `module14.json`

### What It Extracts

**From Each Lesson:**
- `conceptAnalogy` → ANALOGY ContentSection
- `codeExample` → EXAMPLE ContentSection with code extraction
- `syntaxBreakdown` → THEORY ContentSection
- `commonStickingPoints` → WARNING ContentSection
- `challenge` object → FreeCodingChallenge
  - Instructions, starter code, solution
  - Test cases with inputs/expected outputs
  - Hints
  - Common mistakes parsed from sticking points

### Output Statistics
```
📊 62 lessons (across 14 modules)
💻 62 coding challenges
⏱️  ~35 hours of content
```

### Usage
```bash
npx tsx scripts/convert-javascript-interactive.ts
# or specify paths:
npx tsx scripts/convert-javascript-interactive.ts /tmp/JavaScript-TypeScript-Training-Course /output/path/course.json
```

---

## 3. Kotlin Interactive Converter

**Script**: `/home/user/Code-Tutor/scripts/convert-kotlin-interactive.ts`

### Source Data
- **Location**: `/tmp/Kotlin-Training-Course/src/main/resources/`
- **Format**: Markdown lessons + separate quiz/challenge JSON files
- **Structure**:
  - `lessons/part1/` through `lessons/part7/` - 63 markdown files
  - `quizzes/part1-quiz.json` through `part7-quiz.json`
  - `challenges/part1-challenges.json` through `part7-challenges.json`

### What It Extracts

**From Markdown Lessons:**
- Parses markdown structure into ContentSections
- Detects section types by headers:
  - Concept/Analogy → ANALOGY
  - Code Example → EXAMPLE with code extraction
  - Key Points → KEY_POINT
  - Warnings → WARNING
  - Experiments → EXPERIMENT
- Extracts code blocks with language detection
- Estimates reading time from content

**From Challenge Files:**
- Challenge objects → FreeCodingChallenge
  - Starter code, solution, description
  - Test cases with expected output
  - Hints (multiple levels)
  - Difficulty mapping (1-5 → beginner/intermediate/advanced)

**From Quiz Files:**
- Multiple quiz objects per part
- Questions mapped to specific lessons
- Multiple choice and true/false questions
- Detailed explanations

### Output Statistics
```
📊 69 lessons (across 7 parts)
💻 45 coding challenges
📋 26 quizzes
❓ 107 quiz questions
⏱️  ~75 hours of content
```

### Usage
```bash
npx tsx scripts/convert-kotlin-interactive.ts
# or specify paths:
npx tsx scripts/convert-kotlin-interactive.ts /tmp/Kotlin-Training-Course /output/path/course.json
```

---

## 4. Flutter Interactive Converter

**Script**: `/home/user/Code-Tutor/scripts/convert-flutter-interactive.ts`

### Source Data
- **Location**: `/tmp/Flutter-Training-Course/lessons/`
- **Format**: Pure markdown with embedded challenges and experiments
- **Structure**: 87 markdown files across 13 module directories

### What It Extracts

**From Markdown Content:**
- Parses structured sections into ContentSections
- Section type detection by keywords:
  - "Analogy", "Think of", "Imagine" → ANALOGY
  - "Example", "Code" → EXAMPLE
  - "Key", "Remember", "Important" → KEY_POINT
  - "Warning", "Pitfall", "Mistake" → WARNING
  - "Experiment", "Try" → EXPERIMENT

**Challenge Extraction:**
Detects challenge markers:
- `## ✅ YOUR CHALLENGE:`
- `## Challenge`
- `## 🎯 Challenge`

Extracts from challenge sections:
- Requirements (numbered or bulleted lists)
- Hints (multiple levels)
- Bonus challenges
- Starter code and solutions from code blocks

**Experiment Extraction:**
Detects experiment markers:
- `**Try it!**`
- `**Experiment:**`

Extracts:
- Interactive code snippets
- Expected behavior descriptions
- Learning takeaways

### Output Statistics
```
📊 82 lessons (across 13 modules)
💻 46 coding challenges
🧪 Experiments (detected when present)
⭐ Bonus challenges
⏱️  ~77 hours of content
```

### Usage
```bash
npx tsx scripts/convert-flutter-interactive.ts
# or specify paths:
npx tsx scripts/convert-flutter-interactive.ts /tmp/Flutter-Training-Course /output/path/course.json
```

---

## Common Features

All 4 converters share these capabilities:

### 1. Type Safety
- Full TypeScript type definitions from `interactive.ts`
- Proper interfaces for all content types
- Compile-time validation

### 2. Error Handling
- Gracefully handles malformed input files
- Warns about skipped files but continues processing
- Never crashes on bad data

### 3. HTML/Markdown Conversion
- Converts HTML to clean markdown where needed
- Preserves code blocks with proper language tags
- Cleans up whitespace and formatting

### 4. Difficulty Mapping
- Auto-assigns difficulty based on module/part number
- Maps numeric difficulty (1-5) to beginner/intermediate/advanced
- Ensures consistent difficulty progression

### 5. Statistics Tracking
- Counts lessons, challenges, quizzes, questions
- Calculates estimated time
- Reports comprehensive metrics

### 6. Output Format
All converters produce:
```json
{
  "id": "language-name",
  "language": "python|javascript|kotlin|dart",
  "title": "Course Title",
  "description": "...",
  "difficulty": "beginner",
  "estimatedHours": 75,
  "prerequisites": [],
  "modules": [...],
  "metadata": {
    "version": "2.0.0",
    "lastUpdated": "2025-11-14",
    "author": "Code Tutor",
    "interactiveElements": {
      "totalLessons": 69,
      "totalChallenges": 45,
      "totalQuizzes": 26,
      "totalQuestions": 107
    }
  }
}
```

---

## Running All Converters

To convert all 4 courses at once:

```bash
# From the Code-Tutor directory
npx tsx scripts/convert-python-interactive.ts
npx tsx scripts/convert-javascript-interactive.ts
npx tsx scripts/convert-kotlin-interactive.ts
npx tsx scripts/convert-flutter-interactive.ts
```

All output files are generated at:
- `content/courses/python/course.json`
- `content/courses/javascript/course.json`
- `content/courses/kotlin/course.json`
- `content/courses/flutter/course.json`

---

## File Locations

### Converter Scripts
```
/home/user/Code-Tutor/scripts/
├── convert-python-interactive.ts     (16 KB)
├── convert-javascript-interactive.ts (11 KB)
├── convert-kotlin-interactive.ts     (16 KB)
└── convert-flutter-interactive.ts    (18 KB)
```

### Type Definitions
```
/home/user/Code-Tutor/apps/web/src/types/interactive.ts
```

### Source Data
```
/tmp/Python-Training-Course/
/tmp/JavaScript-TypeScript-Training-Course/
/tmp/Kotlin-Training-Course/
/tmp/Flutter-Training-Course/
```

### Output Data
```
/home/user/Code-Tutor/content/courses/
├── python/course.json      (798 KB with full interactive content)
├── javascript/course.json  (792 KB)
├── kotlin/course.json      (1.5 MB)
└── flutter/course.json     (975 KB)
```

---

## Summary of Extracted Content

| Course     | Lessons | Challenges | Quizzes | Questions | Hours |
|------------|---------|------------|---------|-----------|-------|
| Python     | 39      | 36         | 14      | 273       | 26    |
| JavaScript | 62      | 62         | -       | -         | 35    |
| Kotlin     | 69      | 45         | 26      | 107       | 75    |
| Flutter    | 82      | 46         | -       | -         | 77    |
| **TOTAL**  | **252** | **189**    | **40**  | **380**   | **213** |

---

## Next Steps

These converters are ready to use and will extract all available interactive content from the source courses. The generated `course.json` files can be:

1. Loaded into the Code Tutor web platform
2. Used for interactive lesson rendering
3. Connected to code execution engines
4. Enhanced with additional test cases or hints
5. Deployed to production

All converters handle missing or malformed data gracefully and will extract whatever interactive content is available in the source material.
