$filePath = 'C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\course.json'
$content = [System.IO.File]::ReadAllText($filePath, [System.Text.Encoding]::UTF8)

# Use a simpler pattern that should match
$oldText = '"id": "epoch-2-lesson-4-vehicle",'

$warningSection = @'
            {
              "type": "WARNING",
              "title": "Common Inheritance Pitfalls",
              "content": "COMMON MISTAKES TO AVOID:\n\n1. INHERITANCE FOR CODE REUSE ONLY:\n   Stack extends Vector // BAD! Stack is NOT a Vector\n   Prefer composition: class Stack { private List items; }\n\n2. DEEP INHERITANCE HIERARCHIES:\n   A -> B -> C -> D -> E  // Too deep! Hard to maintain\n   Keep hierarchies shallow (2-3 levels max).\n\n3. BREAKING PARENT CONTRACT:\n   Override methods must honor parent behavior expectations.\n   Violating Liskov Substitution Principle causes bugs.\n\n4. FORGETTING super() CALL:\n   If parent has no default constructor, must call super(...) explicitly.\n   Java 22+ allows statements before super() for validation.\n\n5. JAVA 17+ SEALED CLASSES:\n   sealed class Shape permits Circle, Square {}\n   Controls exactly which classes can extend yours.\n   Enables exhaustive pattern matching in switch.\n\n6. PREFER COMPOSITION OVER INHERITANCE:\n   Composition is more flexible and avoids tight coupling.\n   Use inheritance only for true IS-A relationships."
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-4-vehicle",
'@

# Find the closing of the last KEY_POINT section before challenges
$pattern = '          ],\r?\n          "challenges": \[\r?\n            \{\r?\n              "type": "FREE_CODING",\r?\n              "id": "epoch-2-lesson-4-vehicle",'

if ($content -match $pattern) {
    $match = $Matches[0]
    $replacement = $warningSection
    $content = $content -replace [regex]::Escape($match), $replacement
    [System.IO.File]::WriteAllText($filePath, $content, (New-Object System.Text.UTF8Encoding $false))
    Write-Output 'SUCCESS: Inheritance WARNING added'
} elseif ($content.Contains('Common Inheritance Pitfalls')) {
    Write-Output 'Already has WARNING section'
} else {
    # Try simpler approach - find the exact position
    $searchStr = '"id": "epoch-2-lesson-4-vehicle"'
    $idx = $content.IndexOf($searchStr)
    if ($idx -gt 0) {
        # Look backwards for the challenges section start
        $challengesStart = $content.LastIndexOf('"challenges": [', $idx)
        if ($challengesStart -gt 0) {
            # Look backwards from challengesStart to find the contentSections close
            $sectionClose = $content.LastIndexOf('}', $challengesStart)
            $contentBefore = $content.Substring(0, $sectionClose + 1)
            $contentAfter = $content.Substring($sectionClose + 1)

            $warningToInsert = ',
            {
              "type": "WARNING",
              "title": "Common Inheritance Pitfalls",
              "content": "COMMON MISTAKES TO AVOID:\n\n1. INHERITANCE FOR CODE REUSE ONLY:\n   Stack extends Vector // BAD! Stack is NOT a Vector\n   Prefer composition: class Stack { private List items; }\n\n2. DEEP INHERITANCE HIERARCHIES:\n   A -> B -> C -> D -> E  // Too deep!\n   Keep hierarchies shallow (2-3 levels max).\n\n3. BREAKING PARENT CONTRACT:\n   Override methods must honor parent behavior.\n   Violating Liskov Substitution Principle causes bugs.\n\n4. FORGETTING super() CALL:\n   If parent has no default constructor, must call super(...) explicitly.\n\n5. JAVA 17+ SEALED CLASSES:\n   sealed class Shape permits Circle, Square {}\n   Controls exactly which classes can extend yours.\n\n6. PREFER COMPOSITION OVER INHERITANCE:\n   Composition is more flexible. Use inheritance only for true IS-A relationships."
            }'

            $newContent = $contentBefore + $warningToInsert + $contentAfter
            [System.IO.File]::WriteAllText($filePath, $newContent, (New-Object System.Text.UTF8Encoding $false))
            Write-Output 'SUCCESS: Inheritance WARNING added (method 2)'
        } else {
            Write-Output 'Could not find challenges section'
        }
    } else {
        Write-Output 'Could not find epoch-2-lesson-4-vehicle'
    }
}
