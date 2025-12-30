# Lesson Content Quality Review - Master Plan

> **For Claude:** REQUIRED SUB-SKILL: Use superpowers:executing-plans to implement this plan task-by-task.

**Goal:** Systematically review every lesson in all 6 courses for accuracy, completeness, freshness, and pedagogical gaps.

**Architecture:** Each lesson will be processed by an AI agent that reads the current content, performs web searches to verify accuracy against current documentation/best practices, identifies gaps, and outputs a structured review report with recommended improvements.

**Tech Stack:** PowerShell/Bash scripting, AI agents with web search capability, JSON content files, Markdown review reports

---

## Individual Course Plans

Each course has its own detailed review plan:

| Course | Lessons | Plan File |
|--------|---------|-----------|
| **C#** | 104 | [2025-01-01-csharp-content-review.md](./2025-01-01-csharp-content-review.md) |
| **JavaScript** | 95 | [2025-01-01-javascript-content-review.md](./2025-01-01-javascript-content-review.md) |
| **Python** | 108 | [2025-01-01-python-content-review.md](./2025-01-01-python-content-review.md) |
| **Kotlin** | 87 | [2025-01-01-kotlin-content-review.md](./2025-01-01-kotlin-content-review.md) |
| **Flutter** | 109 | [2025-01-01-flutter-content-review.md](./2025-01-01-flutter-content-review.md) |
| **Java** | 63 | [2025-01-01-java-content-review.md](./2025-01-01-java-content-review.md) |
| **TOTAL** | **566** | |

---

## Overview

This plan establishes a systematic process for reviewing all lesson content across 6 courses:
- **csharp** - C# programming
- **flutter** - Flutter/Dart mobile development
- **java** - Java programming
- **javascript** - JavaScript & TypeScript
- **kotlin** - Kotlin programming
- **python** - Python programming

### Review Criteria (Per Lesson)

1. **Accuracy** - Is the technical content correct? Does it match current language/framework versions?
2. **Completeness** - Are all necessary concepts covered to understand the topic?
3. **Freshness** - Is the content up-to-date with the latest language version/best practices?
4. **Pedagogical Gaps** - What's missing that a learner would need to properly understand this topic?

### Content Structure Reference

Each lesson (`course.json`) contains:
```json
{
  "id": "lesson-id",
  "title": "Lesson Title",
  "contentSections": [
    { "type": "ANALOGY|THEORY|EXAMPLE|WARNING|KEY_POINT|EXPERIMENT", "title": "...", "content": "..." }
  ],
  "challenges": [...]
}
```

---

## Task 1: Create Lesson Extractor Script

**Files:**
- Create: `scripts/extract-lesson.ps1`

**Step 1: Write the lesson extractor script**

```powershell
# scripts/extract-lesson.ps1
# Extracts a single lesson from a course for AI review

param(
    [Parameter(Mandatory=$true)]
    [string]$Course,

    [Parameter(Mandatory=$true)]
    [string]$LessonId,

    [string]$OutputPath = "."
)

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$coursePath = Join-Path $scriptDir "..\content\courses\$Course\course.json"

if (-not (Test-Path $coursePath)) {
    Write-Error "Course not found: $Course"
    exit 1
}

$courseJson = Get-Content $coursePath -Raw -Encoding UTF8 | ConvertFrom-Json

$lesson = $null
$moduleTitle = ""
$lessonOrder = 0

foreach ($module in $courseJson.modules) {
    foreach ($l in $module.lessons) {
        if ($l.id -eq $LessonId) {
            $lesson = $l
            $moduleTitle = $module.title
            break
        }
    }
    if ($lesson) { break }
}

if (-not $lesson) {
    Write-Error "Lesson not found: $LessonId in course $Course"
    exit 1
}

# Build review context
$reviewContext = @{
    course = @{
        id = $courseJson.id
        language = $courseJson.language
        title = $courseJson.title
    }
    module = @{
        title = $moduleTitle
    }
    lesson = @{
        id = $lesson.id
        title = $lesson.title
        difficulty = $lesson.difficulty
        estimatedMinutes = $lesson.estimatedMinutes
        contentSections = $lesson.contentSections
        challenges = $lesson.challenges
    }
}

$outputFile = Join-Path $OutputPath "$Course-$LessonId-review-input.json"
$reviewContext | ConvertTo-Json -Depth 10 | Out-File $outputFile -Encoding UTF8

Write-Host "Extracted lesson to: $outputFile"
Write-Output $outputFile
```

**Step 2: Test the extractor**

Run: `powershell -File scripts/extract-lesson.ps1 -Course javascript -LessonId 1.1 -OutputPath ./temp`
Expected: Creates `temp/javascript-1.1-review-input.json`

**Step 3: Commit**

```bash
git add scripts/extract-lesson.ps1
git commit -m "feat: add lesson extractor script for content review"
```

---

## Task 2: Create Review Prompt Template

**Files:**
- Create: `scripts/review-templates/lesson-review-prompt.md`

**Step 1: Write the review prompt template**

```markdown
# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** {{COURSE_TITLE}} ({{COURSE_LANGUAGE}})
- **Module:** {{MODULE_TITLE}}
- **Lesson:** {{LESSON_TITLE}} (ID: {{LESSON_ID}})
- **Difficulty:** {{DIFFICULTY}}
- **Estimated Time:** {{ESTIMATED_MINUTES}} minutes

## Current Lesson Content

{{LESSON_CONTENT_JSON}}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current {{COURSE_LANGUAGE}} documentation
- Search the web for the latest {{COURSE_LANGUAGE}} version and verify examples work with it
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
- Search for "{{COURSE_LANGUAGE}} {{LESSON_TITLE}} 2024 2025" to find latest practices
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
  "lessonId": "{{LESSON_ID}}",
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
```

**Step 2: Create the templates directory and save**

```bash
mkdir -p scripts/review-templates
```

**Step 3: Commit**

```bash
git add scripts/review-templates/lesson-review-prompt.md
git commit -m "feat: add lesson review prompt template"
```

---

## Task 3: Create Lesson List Generator

**Files:**
- Create: `scripts/list-all-lessons.ps1`

**Step 1: Write the lesson list generator**

```powershell
# scripts/list-all-lessons.ps1
# Generates a complete list of all lessons across all courses

param(
    [string]$OutputPath = "scripts/content-audit-results",
    [switch]$JsonOutput
)

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$coursesPath = Join-Path $scriptDir "..\content\courses"

$allLessons = @()

foreach ($courseDir in Get-ChildItem -Path $coursesPath -Directory) {
    $courseFile = Join-Path $courseDir.FullName "course.json"
    if (-not (Test-Path $courseFile)) { continue }

    $courseId = $courseDir.Name
    $course = Get-Content $courseFile -Raw -Encoding UTF8 | ConvertFrom-Json

    foreach ($module in $course.modules) {
        foreach ($lesson in $module.lessons) {
            $contentLength = 0
            foreach ($section in $lesson.contentSections) {
                if ($section.content) {
                    $contentLength += $section.content.Length
                }
            }

            $allLessons += [PSCustomObject]@{
                Course = $courseId
                CourseTitle = $course.title
                ModuleId = $module.id
                ModuleTitle = $module.title
                LessonId = $lesson.id
                LessonTitle = $lesson.title
                Difficulty = $lesson.difficulty
                EstimatedMinutes = $lesson.estimatedMinutes
                ContentSectionCount = $lesson.contentSections.Count
                ChallengeCount = $lesson.challenges.Count
                TotalContentLength = $contentLength
            }
        }
    }
}

if (-not (Test-Path $OutputPath)) {
    New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
}

if ($JsonOutput) {
    $outputFile = Join-Path $OutputPath "all-lessons.json"
    $allLessons | ConvertTo-Json -Depth 5 | Out-File $outputFile -Encoding UTF8
} else {
    $outputFile = Join-Path $OutputPath "all-lessons.csv"
    $allLessons | Export-Csv -Path $outputFile -NoTypeInformation
}

Write-Host "=" * 60
Write-Host "LESSON INVENTORY SUMMARY"
Write-Host "=" * 60
Write-Host ""

$grouped = $allLessons | Group-Object Course
foreach ($group in $grouped) {
    Write-Host "[$($group.Name)] - $($group.Count) lessons"
}

Write-Host ""
Write-Host "Total lessons: $($allLessons.Count)"
Write-Host "Output saved to: $outputFile"

Write-Output $allLessons.Count
```

**Step 2: Run to generate lesson inventory**

Run: `powershell -File scripts/list-all-lessons.ps1`
Expected: Creates `scripts/content-audit-results/all-lessons.csv` and displays count

**Step 3: Commit**

```bash
git add scripts/list-all-lessons.ps1
git commit -m "feat: add lesson inventory generator"
```

---

## Task 4: Create Review Orchestrator Script

**Files:**
- Create: `scripts/review-lesson.ps1`

**Step 1: Write the review orchestrator**

```powershell
# scripts/review-lesson.ps1
# Orchestrates the review of a single lesson using AI

param(
    [Parameter(Mandatory=$true)]
    [string]$Course,

    [Parameter(Mandatory=$true)]
    [string]$LessonId,

    [string]$OutputPath = "docs/audits/content-reviews"
)

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

# Step 1: Extract the lesson
$tempDir = Join-Path $scriptDir "temp"
if (-not (Test-Path $tempDir)) { New-Item -ItemType Directory -Path $tempDir -Force | Out-Null }

$extractScript = Join-Path $scriptDir "extract-lesson.ps1"
$lessonFile = & powershell -File $extractScript -Course $Course -LessonId $LessonId -OutputPath $tempDir

if (-not (Test-Path $lessonFile)) {
    Write-Error "Failed to extract lesson"
    exit 1
}

$lessonData = Get-Content $lessonFile -Raw | ConvertFrom-Json

# Step 2: Load and populate the prompt template
$templatePath = Join-Path $scriptDir "review-templates\lesson-review-prompt.md"
$template = Get-Content $templatePath -Raw

$prompt = $template `
    -replace '{{COURSE_TITLE}}', $lessonData.course.title `
    -replace '{{COURSE_LANGUAGE}}', $lessonData.course.language `
    -replace '{{MODULE_TITLE}}', $lessonData.module.title `
    -replace '{{LESSON_TITLE}}', $lessonData.lesson.title `
    -replace '{{LESSON_ID}}', $lessonData.lesson.id `
    -replace '{{DIFFICULTY}}', $lessonData.lesson.difficulty `
    -replace '{{ESTIMATED_MINUTES}}', $lessonData.lesson.estimatedMinutes `
    -replace '{{LESSON_CONTENT_JSON}}', ($lessonData.lesson | ConvertTo-Json -Depth 10)

# Step 3: Save the populated prompt for AI processing
if (-not (Test-Path $OutputPath)) {
    New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
}

$promptFile = Join-Path $OutputPath "$Course-$LessonId-review-prompt.md"
$prompt | Out-File $promptFile -Encoding UTF8

Write-Host "Review prompt generated: $promptFile"
Write-Host ""
Write-Host "Next steps:"
Write-Host "1. Feed this prompt to an AI agent with web search capability"
Write-Host "2. Save the AI's JSON response to: $OutputPath\$Course-$LessonId-review-result.json"
Write-Host ""

# Clean up temp
Remove-Item $lessonFile -Force

Write-Output $promptFile
```

**Step 2: Test the orchestrator**

Run: `powershell -File scripts/review-lesson.ps1 -Course javascript -LessonId 1.1`
Expected: Creates review prompt in `docs/audits/content-reviews/`

**Step 3: Commit**

```bash
git add scripts/review-lesson.ps1
git commit -m "feat: add lesson review orchestrator"
```

---

## Task 5: Create Batch Review Script

**Files:**
- Create: `scripts/batch-review-lessons.ps1`

**Step 1: Write the batch processor**

```powershell
# scripts/batch-review-lessons.ps1
# Generates review prompts for all lessons in a course (or all courses)

param(
    [string]$Course = "",  # Empty = all courses
    [string]$OutputPath = "docs/audits/content-reviews",
    [int]$StartFrom = 0,   # Resume from this lesson index
    [int]$Limit = 0        # 0 = no limit
)

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$coursesPath = Join-Path $scriptDir "..\content\courses"

# Get course list
if ($Course -ne "") {
    $courseList = @($Course)
} else {
    $courseList = Get-ChildItem -Path $coursesPath -Directory | Select-Object -ExpandProperty Name
}

$allPrompts = @()
$lessonIndex = 0

foreach ($courseName in $courseList) {
    $courseFile = Join-Path $coursesPath "$courseName\course.json"
    if (-not (Test-Path $courseFile)) { continue }

    $courseData = Get-Content $courseFile -Raw -Encoding UTF8 | ConvertFrom-Json

    foreach ($module in $courseData.modules) {
        foreach ($lesson in $module.lessons) {
            $lessonIndex++

            if ($lessonIndex -le $StartFrom) { continue }
            if ($Limit -gt 0 -and ($lessonIndex - $StartFrom) -gt $Limit) { break }

            Write-Host "[$lessonIndex] Generating review for: $courseName / $($lesson.id) - $($lesson.title)"

            $reviewScript = Join-Path $scriptDir "review-lesson.ps1"
            $promptFile = & powershell -File $reviewScript -Course $courseName -LessonId $lesson.id -OutputPath $OutputPath

            $allPrompts += [PSCustomObject]@{
                Index = $lessonIndex
                Course = $courseName
                LessonId = $lesson.id
                LessonTitle = $lesson.title
                PromptFile = $promptFile
                Status = "pending"
            }
        }
    }
}

# Save batch manifest
$manifestPath = Join-Path $OutputPath "batch-manifest.json"
$manifest = @{
    generatedAt = (Get-Date -Format "yyyy-MM-dd HH:mm:ss")
    totalLessons = $allPrompts.Count
    prompts = $allPrompts
}
$manifest | ConvertTo-Json -Depth 5 | Out-File $manifestPath -Encoding UTF8

Write-Host ""
Write-Host "=" * 60
Write-Host "BATCH REVIEW PROMPTS GENERATED"
Write-Host "=" * 60
Write-Host "Total prompts: $($allPrompts.Count)"
Write-Host "Manifest: $manifestPath"
Write-Host ""
Write-Host "Next: Process each prompt file with an AI agent that has web search"
```

**Step 2: Run for a single course (test)**

Run: `powershell -File scripts/batch-review-lessons.ps1 -Course javascript -Limit 3`
Expected: Generates 3 review prompts for JavaScript lessons

**Step 3: Commit**

```bash
git add scripts/batch-review-lessons.ps1
git commit -m "feat: add batch review generator for all lessons"
```

---

## Task 6: Create Review Result Aggregator

**Files:**
- Create: `scripts/aggregate-reviews.ps1`

**Step 1: Write the aggregator**

```powershell
# scripts/aggregate-reviews.ps1
# Aggregates all review results into a summary report

param(
    [string]$ReviewsPath = "docs/audits/content-reviews",
    [string]$OutputPath = "docs/audits"
)

$reviewFiles = Get-ChildItem -Path $ReviewsPath -Filter "*-review-result.json"

$summary = @{
    totalReviewed = 0
    averageScore = 0
    byCourse = @{}
    highPriority = @()
    mediumPriority = @()
    lowPriority = @()
    shortContentIssues = @()
    accuracyIssues = @()
    freshnessIssues = @()
}

$totalScore = 0

foreach ($file in $reviewFiles) {
    $review = Get-Content $file.FullName -Raw | ConvertFrom-Json

    $summary.totalReviewed++
    $totalScore += $review.overallScore

    # Extract course from filename
    $course = $file.Name -replace '-.*', ''

    if (-not $summary.byCourse.ContainsKey($course)) {
        $summary.byCourse[$course] = @{
            count = 0
            totalScore = 0
            issues = @()
        }
    }
    $summary.byCourse[$course].count++
    $summary.byCourse[$course].totalScore += $review.overallScore

    # Categorize by priority
    $reviewSummary = @{
        lessonId = $review.lessonId
        course = $course
        overallScore = $review.overallScore
        file = $file.Name
    }

    switch ($review.priority) {
        "HIGH" { $summary.highPriority += $reviewSummary }
        "MEDIUM" { $summary.mediumPriority += $reviewSummary }
        "LOW" { $summary.lowPriority += $reviewSummary }
    }

    # Collect specific issues
    if ($review.contentLengthIssues.shortSections.Count -gt 0) {
        foreach ($section in $review.contentLengthIssues.shortSections) {
            $summary.shortContentIssues += @{
                course = $course
                lessonId = $review.lessonId
                section = $section.sectionTitle
                length = $section.currentLength
            }
        }
    }

    if ($review.accuracy.issues.Count -gt 0) {
        $summary.accuracyIssues += @{
            course = $course
            lessonId = $review.lessonId
            issues = $review.accuracy.issues
        }
    }

    if ($review.freshness.outdatedItems.Count -gt 0) {
        $summary.freshnessIssues += @{
            course = $course
            lessonId = $review.lessonId
            items = $review.freshness.outdatedItems
        }
    }
}

if ($summary.totalReviewed -gt 0) {
    $summary.averageScore = [math]::Round($totalScore / $summary.totalReviewed, 2)
}

# Save summary
$summaryPath = Join-Path $OutputPath "content-review-summary.json"
$summary | ConvertTo-Json -Depth 10 | Out-File $summaryPath -Encoding UTF8

# Generate markdown report
$mdReport = @"
# Content Quality Review Summary

Generated: $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")

## Overview

- **Total Lessons Reviewed:** $($summary.totalReviewed)
- **Average Score:** $($summary.averageScore) / 10

## Priority Breakdown

| Priority | Count |
|----------|-------|
| HIGH | $($summary.highPriority.Count) |
| MEDIUM | $($summary.mediumPriority.Count) |
| LOW | $($summary.lowPriority.Count) |

## High Priority Lessons (Fix First)

$($summary.highPriority | ForEach-Object { "- [$($_.course)] $($_.lessonId) (Score: $($_.overallScore))" } | Out-String)

## Issues Summary

### Short Content Sections: $($summary.shortContentIssues.Count)
### Accuracy Issues: $($summary.accuracyIssues.Count)
### Freshness Issues: $($summary.freshnessIssues.Count)

## By Course

$($summary.byCourse.GetEnumerator() | ForEach-Object {
    $avg = if ($_.Value.count -gt 0) { [math]::Round($_.Value.totalScore / $_.Value.count, 2) } else { 0 }
    "### $($_.Key)`n- Lessons: $($_.Value.count)`n- Average Score: $avg`n"
} | Out-String)
"@

$mdPath = Join-Path $OutputPath "content-review-summary.md"
$mdReport | Out-File $mdPath -Encoding UTF8

Write-Host "Summary saved to:"
Write-Host "  JSON: $summaryPath"
Write-Host "  Markdown: $mdPath"
```

**Step 2: Commit**

```bash
git add scripts/aggregate-reviews.ps1
git commit -m "feat: add review result aggregator"
```

---

## Task 7: Create Content Update Script

**Files:**
- Create: `scripts/apply-review-fix.ps1`

**Step 1: Write the content updater**

```powershell
# scripts/apply-review-fix.ps1
# Applies a specific fix from a review to the course content

param(
    [Parameter(Mandatory=$true)]
    [string]$Course,

    [Parameter(Mandatory=$true)]
    [string]$LessonId,

    [Parameter(Mandatory=$true)]
    [string]$SectionTitle,

    [Parameter(Mandatory=$true)]
    [string]$NewContent,

    [switch]$DryRun
)

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$coursePath = Join-Path $scriptDir "..\content\courses\$Course\course.json"

if (-not (Test-Path $coursePath)) {
    Write-Error "Course not found: $Course"
    exit 1
}

$courseJson = Get-Content $coursePath -Raw -Encoding UTF8 | ConvertFrom-Json

$found = $false

foreach ($module in $courseJson.modules) {
    foreach ($lesson in $module.lessons) {
        if ($lesson.id -eq $LessonId) {
            foreach ($section in $lesson.contentSections) {
                if ($section.title -eq $SectionTitle) {
                    Write-Host "Found section: $SectionTitle in $LessonId"
                    Write-Host "Current content length: $($section.content.Length)"
                    Write-Host "New content length: $($NewContent.Length)"

                    if ($DryRun) {
                        Write-Host "[DRY RUN] Would update section"
                    } else {
                        $section.content = $NewContent
                        Write-Host "Section updated"
                    }
                    $found = $true
                    break
                }
            }
        }
    }
}

if (-not $found) {
    Write-Error "Section not found: $SectionTitle in lesson $LessonId"
    exit 1
}

if (-not $DryRun) {
    $courseJson | ConvertTo-Json -Depth 20 | Out-File $coursePath -Encoding UTF8
    Write-Host "Course file saved: $coursePath"
}
```

**Step 2: Commit**

```bash
git add scripts/apply-review-fix.ps1
git commit -m "feat: add content fix applicator script"
```

---

## Execution Workflow Summary

### Phase 1: Generate Review Prompts (Automated)

```bash
# Generate lesson inventory
powershell -File scripts/list-all-lessons.ps1

# Generate all review prompts (creates ~500 prompt files)
powershell -File scripts/batch-review-lessons.ps1
```

### Phase 2: Execute Reviews (AI Agent Work)

For each prompt file in `docs/audits/content-reviews/`:

1. Load the prompt file
2. Send to AI agent with web search capability
3. AI performs:
   - Web searches for current {{language}} documentation
   - Accuracy verification against official docs
   - Freshness check against 2024/2025 best practices
   - Gap analysis for pedagogy
4. Save AI response as `*-review-result.json`

### Phase 3: Aggregate & Prioritize

```bash
# Generate summary report
powershell -File scripts/aggregate-reviews.ps1
```

### Phase 4: Apply Fixes

```bash
# For each high-priority issue:
powershell -File scripts/apply-review-fix.ps1 -Course javascript -LessonId 1.1 -SectionTitle "Code Example" -NewContent "expanded content here"
```

---

## File Structure After Implementation

```
scripts/
  extract-lesson.ps1           # Extract single lesson
  list-all-lessons.ps1         # Generate lesson inventory
  review-lesson.ps1            # Generate single review prompt
  batch-review-lessons.ps1     # Generate all review prompts
  aggregate-reviews.ps1        # Aggregate review results
  apply-review-fix.ps1         # Apply content fixes
  review-templates/
    lesson-review-prompt.md    # AI prompt template

docs/audits/
  content-reviews/
    javascript-1.1-review-prompt.md
    javascript-1.1-review-result.json
    ...
    batch-manifest.json
  content-review-summary.json
  content-review-summary.md
```
