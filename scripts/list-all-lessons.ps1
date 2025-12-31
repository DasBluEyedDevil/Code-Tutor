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
