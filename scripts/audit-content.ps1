# Content Quality Audit Script
# Finds all content sections < 51 chars and challenges where starter ≈ solution

param(
    [string]$CoursesPath = "..\content\courses",
    [int]$MinContentLength = 51,
    [switch]$FixMode
)

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$coursesFullPath = Join-Path $scriptDir $CoursesPath

if (-not (Test-Path $coursesFullPath)) {
    $coursesFullPath = "C:\Users\dasbl\Downloads\Code-Tutor\content\courses"
}

Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host "CONTENT QUALITY AUDIT" -ForegroundColor Cyan
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host ""

$shortContentIssues = @()
$starterSolutionIssues = @()
$validPlaceholders = @("____", "TODO", "...", "# Your code here", "// Your code here")

foreach ($courseDir in Get-ChildItem -Path $coursesFullPath -Directory) {
    $courseFile = Join-Path $courseDir.FullName "course.json"
    if (-not (Test-Path $courseFile)) { continue }

    $courseId = $courseDir.Name
    Write-Host "Scanning: $courseId" -ForegroundColor Yellow

    try {
        $content = Get-Content $courseFile -Raw -Encoding UTF8
        $course = $content | ConvertFrom-Json

        foreach ($module in $course.modules) {
            foreach ($lesson in $module.lessons) {
                $lessonId = $lesson.id

                # Check content sections
                foreach ($section in $lesson.contentSections) {
                    if ($section.content -and $section.content.Length -lt $MinContentLength) {
                        $shortContentIssues += [PSCustomObject]@{
                            Course = $courseId
                            LessonId = $lessonId
                            SectionType = $section.type
                            SectionTitle = $section.title
                            ContentLength = $section.content.Length
                            Content = if ($section.content.Length -gt 60) { $section.content.Substring(0, 60) + "..." } else { $section.content }
                        }
                    }
                }

                # Check challenges
                foreach ($challenge in $lesson.challenges) {
                    if ($challenge.starterCode -and $challenge.solution) {
                        $starter = $challenge.starterCode
                        $solution = $challenge.solution

                        # Check if starter contains valid placeholder
                        $hasPlaceholder = $false
                        foreach ($placeholder in $validPlaceholders) {
                            if ($starter.Contains($placeholder)) {
                                $hasPlaceholder = $true
                                break
                            }
                        }

                        # Check if starter is too similar to solution
                        $starterLen = $starter.Length
                        $solutionLen = $solution.Length
                        $isTooSimilar = ($starterLen -ge $solutionLen * 0.9) -and (-not $hasPlaceholder)
                        $isIdentical = $starter -eq $solution

                        if ($isIdentical -or $isTooSimilar) {
                            $starterSolutionIssues += [PSCustomObject]@{
                                Course = $courseId
                                LessonId = $lessonId
                                ChallengeId = $challenge.id
                                ChallengeTitle = $challenge.title
                                Issue = if ($isIdentical) { "IDENTICAL" } else { "TOO_SIMILAR" }
                                StarterLength = $starterLen
                                SolutionLength = $solutionLen
                                HasPlaceholder = $hasPlaceholder
                            }
                        }
                    }
                }
            }
        }
    }
    catch {
        Write-Host "  Error parsing $courseFile : $_" -ForegroundColor Red
    }
}

Write-Host ""
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host "SHORT CONTENT SECTIONS (< $MinContentLength chars)" -ForegroundColor Cyan
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host ""

if ($shortContentIssues.Count -eq 0) {
    Write-Host "No short content sections found!" -ForegroundColor Green
} else {
    Write-Host "Found $($shortContentIssues.Count) short content sections:" -ForegroundColor Red
    Write-Host ""

    $grouped = $shortContentIssues | Group-Object Course
    foreach ($group in $grouped) {
        Write-Host "[$($group.Name)] - $($group.Count) issues" -ForegroundColor Yellow
        foreach ($issue in $group.Group) {
            Write-Host "  Lesson: $($issue.LessonId)" -ForegroundColor White
            Write-Host "    Type: $($issue.SectionType) | Title: $($issue.SectionTitle)" -ForegroundColor Gray
            Write-Host "    Length: $($issue.ContentLength) chars | Content: $($issue.Content -replace "`n", " ")" -ForegroundColor Gray
            Write-Host ""
        }
    }
}

Write-Host ""
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host "STARTER = SOLUTION ISSUES" -ForegroundColor Cyan
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host ""

if ($starterSolutionIssues.Count -eq 0) {
    Write-Host "No starter=solution issues found!" -ForegroundColor Green
} else {
    Write-Host "Found $($starterSolutionIssues.Count) starter=solution issues:" -ForegroundColor Red
    Write-Host ""
    Write-Host "Valid placeholders: $($validPlaceholders -join ', ')" -ForegroundColor Gray
    Write-Host ""

    $grouped = $starterSolutionIssues | Group-Object Course
    foreach ($group in $grouped) {
        Write-Host "[$($group.Name)] - $($group.Count) issues" -ForegroundColor Yellow
        foreach ($issue in $group.Group) {
            Write-Host "  Lesson: $($issue.LessonId) | Challenge: $($issue.ChallengeId)" -ForegroundColor White
            Write-Host "    Issue: $($issue.Issue) | Starter: $($issue.StarterLength) chars | Solution: $($issue.SolutionLength) chars" -ForegroundColor Gray
            Write-Host ""
        }
    }
}

Write-Host ""
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host "SUMMARY" -ForegroundColor Cyan
Write-Host "=" * 80 -ForegroundColor Cyan
Write-Host ""
Write-Host "Total short content issues: $($shortContentIssues.Count)" -ForegroundColor $(if ($shortContentIssues.Count -eq 0) { "Green" } else { "Red" })
Write-Host "Total starter=solution issues: $($starterSolutionIssues.Count)" -ForegroundColor $(if ($starterSolutionIssues.Count -eq 0) { "Green" } else { "Red" })
Write-Host ""

# Export to CSV for easier processing
$csvPath = Join-Path $scriptDir "content-audit-results"
if (-not (Test-Path $csvPath)) { New-Item -ItemType Directory -Path $csvPath -Force | Out-Null }

$shortContentIssues | Export-Csv -Path (Join-Path $csvPath "short-content.csv") -NoTypeInformation
$starterSolutionIssues | Export-Csv -Path (Join-Path $csvPath "starter-solution.csv") -NoTypeInformation

Write-Host "Results exported to:" -ForegroundColor Green
Write-Host "  $csvPath\short-content.csv" -ForegroundColor Gray
Write-Host "  $csvPath\starter-solution.csv" -ForegroundColor Gray
