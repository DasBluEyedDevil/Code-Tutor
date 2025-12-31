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

# Resolve OutputPath relative to script directory if relative
if (-not [System.IO.Path]::IsPathRooted($OutputPath)) {
    $OutputPath = Join-Path (Split-Path $scriptDir -Parent) $OutputPath
}

# Step 1: Extract the lesson
$tempDir = Join-Path $scriptDir "temp"
if (-not (Test-Path $tempDir)) { New-Item -ItemType Directory -Path $tempDir -Force | Out-Null }

$extractScript = Join-Path $scriptDir "extract-lesson.ps1"
$extractOutput = & powershell -File $extractScript -Course $Course -LessonId $LessonId -OutputPath $tempDir

# The extract script outputs multiple lines; the last line is the file path
$lessonFile = ($extractOutput | Select-Object -Last 1).Trim()

if (-not (Test-Path $lessonFile)) {
    Write-Error "Failed to extract lesson: $lessonFile"
    exit 1
}

try {
    $lessonData = Get-Content $lessonFile -Raw -Encoding UTF8 | ConvertFrom-Json
} catch {
    Write-Error "Failed to parse lesson data: $_"
    Remove-Item $lessonFile -Force -ErrorAction SilentlyContinue
    exit 1
}

# Step 2: Load and populate the prompt template
$templatePath = Join-Path $scriptDir "review-templates\lesson-review-prompt.md"
if (-not (Test-Path $templatePath)) {
    Write-Error "Template not found: $templatePath"
    exit 1
}
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
