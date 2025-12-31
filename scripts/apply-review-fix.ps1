# scripts/apply-review-fix.ps1
# Applies a specific fix from an AI review result to the actual course content

[CmdletBinding()]
param(
    [Parameter(Mandatory=$true)]
    [string]$Course,

    [Parameter(Mandatory=$true)]
    [string]$LessonId,

    [Parameter(Mandatory=$true)]
    [ValidateSet("addSection", "updateSection", "addChallenge", "updateChallenge")]
    [string]$FixType,

    [int]$SectionIndex = -1,

    [int]$ChallengeIndex = -1,

    [Parameter(Mandatory=$true)]
    [string]$Content
)

$ErrorActionPreference = "Stop"

# Resolve paths
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$coursePath = Join-Path $scriptDir "..\content\courses\$Course\course.json"
$backupPath = "$coursePath.bak"

# Validate course exists
if (-not (Test-Path $coursePath)) {
    Write-Error "Course not found: $Course (expected at $coursePath)"
    exit 1
}

# Parse the content JSON
try {
    $contentObj = $Content | ConvertFrom-Json
} catch {
    Write-Error "Invalid JSON in -Content parameter: $_"
    exit 1
}

# Load course JSON
$courseJson = Get-Content $coursePath -Raw -Encoding UTF8 | ConvertFrom-Json

# Find the lesson
$lesson = $null
$moduleIndex = -1
$lessonIndex = -1

for ($mi = 0; $mi -lt $courseJson.modules.Count; $mi++) {
    $module = $courseJson.modules[$mi]
    for ($li = 0; $li -lt $module.lessons.Count; $li++) {
        if ($module.lessons[$li].id -eq $LessonId) {
            $lesson = $module.lessons[$li]
            $moduleIndex = $mi
            $lessonIndex = $li
            break
        }
    }
    if ($lesson) { break }
}

if (-not $lesson) {
    Write-Error "Lesson not found: $LessonId in course $Course"
    exit 1
}

Write-Host "Found lesson '$($lesson.title)' at module[$moduleIndex].lessons[$lessonIndex]"

# Validate index parameters for update operations
switch ($FixType) {
    "updateSection" {
        if ($SectionIndex -lt 0) {
            Write-Error "updateSection requires -SectionIndex parameter"
            exit 1
        }
        if (-not $lesson.contentSections -or $SectionIndex -ge $lesson.contentSections.Count) {
            Write-Error "SectionIndex $SectionIndex is out of range (lesson has $($lesson.contentSections.Count) sections)"
            exit 1
        }
    }
    "updateChallenge" {
        if ($ChallengeIndex -lt 0) {
            Write-Error "updateChallenge requires -ChallengeIndex parameter"
            exit 1
        }
        if (-not $lesson.challenges -or $ChallengeIndex -ge $lesson.challenges.Count) {
            Write-Error "ChallengeIndex $ChallengeIndex is out of range (lesson has $($lesson.challenges.Count) challenges)"
            exit 1
        }
    }
}

# Create backup before modifying
Write-Host "Creating backup at: $backupPath"
try {
    Copy-Item $coursePath $backupPath -Force -ErrorAction Stop
} catch {
    Write-Error "Failed to create backup: $_"
    exit 1
}

# Apply the fix
switch ($FixType) {
    "addSection" {
        Write-Host "Adding new content section..."

        # Ensure contentSections is an array
        if (-not $lesson.contentSections) {
            $lesson.contentSections = @()
        }

        # Add the new section
        $lesson.contentSections += $contentObj
        Write-Host "Added section with type '$($contentObj.type)' and title '$($contentObj.title)'"
    }

    "updateSection" {
        Write-Host "Updating content section at index $SectionIndex..."

        $existingSection = $lesson.contentSections[$SectionIndex]

        # Merge properties from contentObj into existing section
        foreach ($prop in $contentObj.PSObject.Properties) {
            $existingSection | Add-Member -MemberType NoteProperty -Name $prop.Name -Value $prop.Value -Force
        }

        Write-Host "Updated section '$($existingSection.title)'"
    }

    "addChallenge" {
        Write-Host "Adding new challenge..."

        # Ensure challenges is an array
        if (-not $lesson.challenges) {
            $lesson.challenges = @()
        }

        # Add the new challenge
        $lesson.challenges += $contentObj
        Write-Host "Added challenge with title '$($contentObj.title)'"
    }

    "updateChallenge" {
        Write-Host "Updating challenge at index $ChallengeIndex..."

        $existingChallenge = $lesson.challenges[$ChallengeIndex]

        # Merge properties from contentObj into existing challenge
        foreach ($prop in $contentObj.PSObject.Properties) {
            $existingChallenge | Add-Member -MemberType NoteProperty -Name $prop.Name -Value $prop.Value -Force
        }

        Write-Host "Updated challenge '$($existingChallenge.title)'"
    }
}

# Update the lesson in the course structure
$courseJson.modules[$moduleIndex].lessons[$lessonIndex] = $lesson

# Write the updated course JSON with pretty formatting
$jsonOutput = $courseJson | ConvertTo-Json -Depth 20
$jsonOutput | Out-File $coursePath -Encoding UTF8

Write-Host ""
Write-Host "Successfully applied $FixType to lesson $LessonId in course $Course"
Write-Host "Backup saved at: $backupPath"
Write-Host ""
Write-Host "To restore from backup, run:"
Write-Host "  Copy-Item '$backupPath' '$coursePath' -Force"
