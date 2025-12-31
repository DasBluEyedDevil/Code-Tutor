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
