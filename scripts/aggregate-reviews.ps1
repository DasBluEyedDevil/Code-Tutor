# scripts/aggregate-reviews.ps1
# Consolidates AI-generated review results into a summary report

[CmdletBinding()]
param(
    [string]$InputPath = "docs/audits/content-reviews",
    [string]$OutputFile = "docs/audits/review-summary.md"
)

Set-StrictMode -Version Latest

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$projectRoot = Split-Path $scriptDir -Parent

# Resolve paths relative to project root
if (-not [System.IO.Path]::IsPathRooted($InputPath)) {
    $InputPath = Join-Path $projectRoot $InputPath
}
if (-not [System.IO.Path]::IsPathRooted($OutputFile)) {
    $OutputFile = Join-Path $projectRoot $OutputFile
}

# Verify input directory exists
if (-not (Test-Path $InputPath)) {
    Write-Error "Input directory not found: $InputPath"
    exit 1
}

# Find all review result JSON files
$reviewFiles = Get-ChildItem -Path $InputPath -Filter "*-review-result.json" -ErrorAction SilentlyContinue

if ($reviewFiles.Count -eq 0) {
    Write-Warning "No review result files found matching '*-review-result.json' in: $InputPath"
    exit 0
}

Write-Host "Found $($reviewFiles.Count) review result file(s)"

# Parse all review results
$reviews = [System.Collections.ArrayList]::new()
$parseErrors = [System.Collections.ArrayList]::new()

foreach ($file in $reviewFiles) {
    try {
        $content = Get-Content $file.FullName -Raw -Encoding UTF8
        $review = $content | ConvertFrom-Json

        # Validate required fields
        if (-not $review.lessonId) {
            throw "Missing required field: lessonId"
        }
        if (-not $review.overallScore) {
            throw "Missing required field: overallScore"
        }

        # Add source file info for reference
        $review | Add-Member -NotePropertyName "_sourceFile" -NotePropertyValue $file.Name -Force

        $null = $reviews.Add($review)
        Write-Host "  Parsed: $($file.Name)"
    }
    catch {
        $null = $parseErrors.Add([PSCustomObject]@{
            File = $file.Name
            Error = $_.Exception.Message
        })
        Write-Warning "Failed to parse $($file.Name): $($_.Exception.Message)"
    }
}

if ($reviews.Count -eq 0) {
    Write-Error "No valid review results found"
    exit 1
}

# Calculate statistics
$totalReviews = $reviews.Count

# Average scores by category
$avgOverall = ($reviews | Measure-Object -Property overallScore -Average).Average
$avgAccuracy = ($reviews | Where-Object { $_.accuracy.score } | ForEach-Object { $_.accuracy.score } | Measure-Object -Average).Average
$avgCompleteness = ($reviews | Where-Object { $_.completeness.score } | ForEach-Object { $_.completeness.score } | Measure-Object -Average).Average
$avgFreshness = ($reviews | Where-Object { $_.freshness.score } | ForEach-Object { $_.freshness.score } | Measure-Object -Average).Average
$avgPedagogical = ($reviews | Where-Object { $_.pedagogicalGaps.score } | ForEach-Object { $_.pedagogicalGaps.score } | Measure-Object -Average).Average

# Count by priority
$highPriority = @($reviews | Where-Object { $_.priority -eq "HIGH" })
$mediumPriority = @($reviews | Where-Object { $_.priority -eq "MEDIUM" })
$lowPriority = @($reviews | Where-Object { $_.priority -eq "LOW" })
$unknownPriority = @($reviews | Where-Object { $_.priority -notin @("HIGH", "MEDIUM", "LOW") })

# Sort reviews by overall score (lowest first for attention)
$sortedReviews = $reviews | Sort-Object -Property overallScore

# Generate Markdown report
$report = @"
# Lesson Content Review Summary

**Generated:** $(Get-Date -Format "yyyy-MM-dd HH:mm:ss")
**Source:** $InputPath

---

## Overview

| Metric | Value |
|--------|-------|
| Total Lessons Reviewed | $totalReviews |
| Parse Errors | $($parseErrors.Count) |

---

## Average Scores by Category

| Category | Average Score |
|----------|---------------|
| **Overall** | $([math]::Round($avgOverall, 1)) / 10 |
| Accuracy | $(if ($avgAccuracy) { [math]::Round($avgAccuracy, 1) } else { "N/A" }) / 10 |
| Completeness | $(if ($avgCompleteness) { [math]::Round($avgCompleteness, 1) } else { "N/A" }) / 10 |
| Freshness | $(if ($avgFreshness) { [math]::Round($avgFreshness, 1) } else { "N/A" }) / 10 |
| Pedagogical | $(if ($avgPedagogical) { [math]::Round($avgPedagogical, 1) } else { "N/A" }) / 10 |

---

## Priority Distribution

| Priority | Count | Percentage |
|----------|-------|------------|
| HIGH | $($highPriority.Count) | $(if ($totalReviews -gt 0) { [math]::Round(($highPriority.Count / $totalReviews) * 100, 1) } else { 0 })% |
| MEDIUM | $($mediumPriority.Count) | $(if ($totalReviews -gt 0) { [math]::Round(($mediumPriority.Count / $totalReviews) * 100, 1) } else { 0 })% |
| LOW | $($lowPriority.Count) | $(if ($totalReviews -gt 0) { [math]::Round(($lowPriority.Count / $totalReviews) * 100, 1) } else { 0 })% |
$(if ($unknownPriority.Count -gt 0) { "| Unknown | $($unknownPriority.Count) | $(if ($totalReviews -gt 0) { [math]::Round(($unknownPriority.Count / $totalReviews) * 100, 1) } else { 0 })% |" } else { "" })

---

## HIGH Priority Lessons (Needs Immediate Attention)

"@

if ($highPriority.Count -gt 0) {
    foreach ($review in $highPriority) {
        $report += @"

### $($review.lessonId)

- **Overall Score:** $($review.overallScore) / 10
- **Review Date:** $(if ($review.reviewDate) { $review.reviewDate } else { "Not specified" })

**Top Issues:**

"@
        # Collect top issues from all categories
        $topIssues = @()

        if ($review.accuracy.issues) {
            $topIssues += $review.accuracy.issues | Select-Object -First 2
        }
        if ($review.completeness.gaps) {
            $topIssues += $review.completeness.gaps | Select-Object -First 2
        }
        if ($review.freshness.outdatedItems) {
            $topIssues += $review.freshness.outdatedItems | Select-Object -First 2
        }
        if ($review.pedagogicalGaps.missingPrerequisites) {
            $topIssues += $review.pedagogicalGaps.missingPrerequisites | Select-Object -First 2
        }

        if ($topIssues.Count -gt 0) {
            foreach ($issue in ($topIssues | Select-Object -First 5)) {
                $report += "- $issue`n"
            }
        } else {
            $report += "- No specific issues listed`n"
        }
    }
} else {
    $report += "`n*No HIGH priority lessons found.*`n"
}

$report += @"

---

## All Lessons by Score (Lowest First)

| Lesson ID | Overall | Accuracy | Completeness | Freshness | Pedagogical | Priority |
|-----------|---------|----------|--------------|-----------|-------------|----------|

"@

foreach ($review in $sortedReviews) {
    $accuracyScore = if ($review.accuracy.score) { $review.accuracy.score } else { "-" }
    $completenessScore = if ($review.completeness.score) { $review.completeness.score } else { "-" }
    $freshnessScore = if ($review.freshness.score) { $review.freshness.score } else { "-" }
    $pedagogicalScore = if ($review.pedagogicalGaps.score) { $review.pedagogicalGaps.score } else { "-" }
    $priority = if ($review.priority) { $review.priority } else { "-" }

    $report += "| $($review.lessonId) | $($review.overallScore) | $accuracyScore | $completenessScore | $freshnessScore | $pedagogicalScore | $priority |`n"
}

# Add parse errors section if any
if ($parseErrors.Count -gt 0) {
    $report += @"

---

## Parse Errors

The following files could not be parsed:

| File | Error |
|------|-------|

"@
    foreach ($err in $parseErrors) {
        # Sanitize error message for markdown table (remove newlines and pipes)
        $sanitizedError = $err.Error -replace '[\r\n]+', ' ' -replace '\|', '-'
        $report += "| $($err.File) | $sanitizedError |`n"
    }
}

$report += @"

---

*Report generated by aggregate-reviews.ps1*
"@

# Ensure output directory exists
$outputDir = Split-Path $OutputFile -Parent
if (-not (Test-Path $outputDir)) {
    New-Item -ItemType Directory -Path $outputDir -Force | Out-Null
}

# Write report
$report | Out-File $OutputFile -Encoding UTF8

Write-Host ""
Write-Host "============================================================"
Write-Host "REVIEW SUMMARY GENERATED"
Write-Host "============================================================"
Write-Host "Total reviews processed: $totalReviews"
Write-Host "Parse errors: $($parseErrors.Count)"
Write-Host "HIGH priority: $($highPriority.Count)"
Write-Host "MEDIUM priority: $($mediumPriority.Count)"
Write-Host "LOW priority: $($lowPriority.Count)"
if ($unknownPriority.Count -gt 0) {
    Write-Host "Unknown priority: $($unknownPriority.Count)"
}
Write-Host ""
Write-Host "Report saved to: $OutputFile"

Write-Output $OutputFile
