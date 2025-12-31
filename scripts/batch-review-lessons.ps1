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

# Resolve OutputPath relative to project root
if (-not [System.IO.Path]::IsPathRooted($OutputPath)) {
    $OutputPath = Join-Path (Split-Path $scriptDir -Parent) $OutputPath
}

# Validate course parameter if provided
if ($Course -ne "") {
    $courseDir = Join-Path $coursesPath $Course
    if (-not (Test-Path $courseDir)) {
        Write-Error "Course not found: $Course (expected at $courseDir)"
        exit 1
    }
    $courseList = @($Course)
} else {
    $courseList = Get-ChildItem -Path $coursesPath -Directory | Select-Object -ExpandProperty Name
}

$allPrompts = @()
$lessonIndex = 0
$failureCount = 0

foreach ($courseName in $courseList) {
    $courseFile = Join-Path $coursesPath "$courseName\course.json"
    if (-not (Test-Path $courseFile)) { continue }

    $courseData = Get-Content $courseFile -Raw -Encoding UTF8 | ConvertFrom-Json

    foreach ($module in $courseData.modules) {
        foreach ($lesson in $module.lessons) {
            $lessonIndex++

            if ($lessonIndex -le $StartFrom) { continue }
            if ($Limit -gt 0 -and ($lessonIndex - $StartFrom) -ge $Limit) { break }

            Write-Host "[$lessonIndex] Generating review for: $courseName / $($lesson.id) - $($lesson.title)"

            $reviewScript = Join-Path $scriptDir "review-lesson.ps1"
            $promptFile = & powershell -File $reviewScript -Course $courseName -LessonId $lesson.id -OutputPath $OutputPath | Select-Object -Last 1
            $exitCode = $LASTEXITCODE

            if ($exitCode -ne 0) {
                $status = "failed"
                $failureCount++
                Write-Warning "Failed to generate review for $($lesson.id) (exit code: $exitCode)"
            } else {
                $status = "pending"
            }

            $allPrompts += [PSCustomObject]@{
                Index = $lessonIndex
                Course = $courseName
                LessonId = $lesson.id
                LessonTitle = $lesson.title
                PromptFile = $promptFile
                Status = $status
            }
        }

        # Check limit at module level too
        if ($Limit -gt 0 -and ($lessonIndex - $StartFrom) -ge $Limit) { break }
    }

    # Check limit at course level
    if ($Limit -gt 0 -and ($lessonIndex - $StartFrom) -ge $Limit) { break }
}

# Save batch manifest
if (-not (Test-Path $OutputPath)) {
    New-Item -ItemType Directory -Path $OutputPath -Force | Out-Null
}

$manifestPath = Join-Path $OutputPath "batch-manifest.json"
$manifest = @{
    generatedAt = (Get-Date -Format "yyyy-MM-dd HH:mm:ss")
    totalLessons = $allPrompts.Count
    prompts = $allPrompts
}
$manifest | ConvertTo-Json -Depth 5 | Out-File $manifestPath -Encoding UTF8

Write-Host ""
Write-Host "============================================================"
Write-Host "BATCH REVIEW PROMPTS GENERATED"
Write-Host "============================================================"
Write-Host "Total prompts: $($allPrompts.Count)"
if ($failureCount -gt 0) {
    Write-Host "Failures: $failureCount" -ForegroundColor Red
}
Write-Host "Manifest: $manifestPath"
Write-Host ""
Write-Host "Next: Process each prompt file with an AI agent that has web search"
