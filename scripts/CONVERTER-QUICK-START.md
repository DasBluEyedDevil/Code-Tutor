# Interactive Course Converters - Quick Start Guide

## ✅ All 4 Converters Ready to Use

### Quick Run Commands

```bash
# Run from /home/user/Code-Tutor directory

# Python Course (39 lessons, 36 challenges, 14 quizzes, 273 questions)
npx tsx scripts/convert-python-interactive.ts

# JavaScript Course (62 lessons, 62 challenges)
npx tsx scripts/convert-javascript-interactive.ts

# Kotlin Course (69 lessons, 45 challenges, 26 quizzes, 107 questions)
npx tsx scripts/convert-kotlin-interactive.ts

# Flutter Course (82 lessons, 46 challenges)
npx tsx scripts/convert-flutter-interactive.ts
```

---

## 📦 What Each Converter Extracts

### 1. Python (`convert-python-interactive.ts`)
**Source**: `/tmp/Python-Training-Course/`

✅ **Extracts**:
- 39 lessons from JSON files
- 36 coding challenges (FreeCodingChallenge)
  - Instructions, starter code, hints, solutions
  - Test cases
  - Common mistakes
- 14 quizzes with 273 questions
  - Multiple choice
  - True/false
  - Code output questions
- Content sections (Analogy, Theory, Examples, Key Points)

**Output**: `content/courses/python/course.json` (725 KB)

---

### 2. JavaScript (`convert-javascript-interactive.ts`)
**Source**: `/tmp/JavaScript-TypeScript-Training-Course/src/main/resources/content/`

✅ **Extracts**:
- 62 lessons from 14 module JSON files
- 62 coding challenges (one per lesson)
  - Concept analogies
  - Code examples with syntax breakdown
  - Test cases with inputs/outputs
  - Hints
- Content sections parsed from embedded markdown
- Common pitfalls as warnings

**Output**: `content/courses/javascript/course.json` (792 KB)

---

### 3. Kotlin (`convert-kotlin-interactive.ts`)
**Source**: `/tmp/Kotlin-Training-Course/src/main/resources/`

✅ **Extracts**:
- 69 lessons from markdown files (7 parts)
- 45 coding challenges from separate JSON files
  - Starter code, solutions, descriptions
  - Test cases
  - Multi-level hints (1-5)
  - Difficulty mapping
- 26 quizzes with 107 questions
  - Linked to specific lessons
  - Multiple choice and true/false
  - Detailed explanations
- Content sections parsed from markdown structure

**Output**: `content/courses/kotlin/course.json` (1.5 MB)

---

### 4. Flutter (`convert-flutter-interactive.ts`)
**Source**: `/tmp/Flutter-Training-Course/lessons/`

✅ **Extracts**:
- 82 lessons from markdown files (13 modules)
- 46 coding challenges detected by markers:
  - `## ✅ YOUR CHALLENGE:`
  - `## Challenge`
  - Requirements, hints, bonus challenges
  - Starter code and solutions
- Experiments (when present)
  - `**Try it!**` sections
  - Interactive code snippets
  - Expected behaviors
- Content sections with smart type detection

**Output**: `content/courses/flutter/course.json` (975 KB)

---

## 🎯 Key Features

### All Converters Include:

✅ **Type Safety**
- Full TypeScript types from `apps/web/src/types/interactive.ts`
- ContentSection, FreeCodingChallenge, Quiz, etc.

✅ **Error Handling**
- Gracefully handles malformed files
- Warns but continues processing
- Never crashes on bad data

✅ **Smart Parsing**
- HTML to Markdown conversion
- Code block extraction with language detection
- Section type detection (Analogy, Example, Warning, etc.)

✅ **Comprehensive Output**
- Modules with difficulty progression
- Estimated time calculations
- Interactive element statistics
- Metadata tracking

---

## 📊 Total Extracted Content

| Course     | Lessons | Challenges | Quizzes | Questions | Hours | File Size |
|------------|---------|------------|---------|-----------|-------|-----------|
| Python     | 39      | 36         | 14      | 273       | 26    | 725 KB    |
| JavaScript | 62      | 62         | -       | -         | 35    | 792 KB    |
| Kotlin     | 69      | 45         | 26      | 107       | 75    | 1.5 MB    |
| Flutter    | 82      | 46         | -       | -         | 77    | 975 KB    |
| **TOTAL**  | **252** | **189**    | **40**  | **380**   | **213** | **4 MB** |

---

## 🔧 Customizing Converters

### Change Source Path
```bash
npx tsx scripts/convert-python-interactive.ts /custom/source/path
```

### Change Output Path
```bash
npx tsx scripts/convert-python-interactive.ts /tmp/Python-Training-Course /custom/output/course.json
```

---

## 📝 Output Format

All converters produce unified JSON:

```json
{
  "id": "python",
  "language": "python",
  "title": "Python Full-Stack Development",
  "description": "...",
  "difficulty": "beginner",
  "estimatedHours": 26,
  "modules": [
    {
      "id": "module-08",
      "title": "Module 8",
      "lessons": [
        {
          "id": "08_01",
          "title": "Introduction to Exceptions",
          "contentSections": [
            {
              "type": "ANALOGY",
              "title": "Understanding the Concept",
              "content": "..."
            },
            {
              "type": "EXAMPLE",
              "title": "Code Example",
              "content": "...",
              "code": "try:\n    ...",
              "language": "python"
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "08_01-challenge-0",
              "title": "Practice Exercise",
              "instructions": "...",
              "starterCode": "...",
              "solution": "...",
              "language": "python",
              "testCases": [...],
              "hints": [...],
              "commonMistakes": [...]
            }
          ]
        }
      ],
      "quizzes": [...]
    }
  ],
  "metadata": {
    "version": "2.0.0",
    "interactiveElements": {
      "totalLessons": 39,
      "totalChallenges": 36,
      "totalQuizzes": 14,
      "totalQuestions": 273
    }
  }
}
```

---

## 🚀 Running All Converters

```bash
#!/bin/bash
# Run all converters sequentially

cd /home/user/Code-Tutor

echo "Converting Python..."
npx tsx scripts/convert-python-interactive.ts

echo "Converting JavaScript..."
npx tsx scripts/convert-javascript-interactive.ts

echo "Converting Kotlin..."
npx tsx scripts/convert-kotlin-interactive.ts

echo "Converting Flutter..."
npx tsx scripts/convert-flutter-interactive.ts

echo "✅ All courses converted!"
```

---

## 📚 Documentation

- **Full Documentation**: `INTERACTIVE-CONVERTERS.md`
- **Type Definitions**: `apps/web/src/types/interactive.ts`
- **Source Code**: `scripts/convert-*-interactive.ts`

---

## ✨ Ready to Use!

All 4 converters are production-ready and tested. They successfully extract:
- ✅ 252 interactive lessons
- ✅ 189 coding challenges with solutions
- ✅ 40 quizzes with 380 questions
- ✅ ~213 hours of learning content

Simply run the converters and the interactive course JSON will be generated at:
- `content/courses/python/course.json`
- `content/courses/javascript/course.json`
- `content/courses/kotlin/course.json`
- `content/courses/flutter/course.json`
