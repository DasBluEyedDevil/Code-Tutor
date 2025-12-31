$filePath = 'C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\course.json'
$content = [System.IO.File]::ReadAllText($filePath, [System.Text.Encoding]::UTF8)

$oldText = 'Professional code ALWAYS uses encapsulation!"
            }
          ],
          "challenges": ['

$newText = 'Professional code ALWAYS uses encapsulation!"
            },
            {
              "type": "WARNING",
              "title": "Common Encapsulation Pitfalls",
              "content": "COMMON MISTAKES TO AVOID:\n\n1. EXPOSING MUTABLE OBJECTS:\n   private List<String> items;\n   public List<String> getItems() { return items; }  // BAD!\n   Caller can modify your internal list!\n   CORRECT: return new ArrayList<>(items);\n\n2. SETTERS FOR EVERYTHING:\n   Auto-generating all getters/setters defeats encapsulation.\n   Only expose what is truly needed.\n\n3. BREAKING IMMUTABILITY WITH ARRAYS:\n   Return copies: return Arrays.copyOf(scores, scores.length);\n\n4. FORGETTING VALIDATION IN CONSTRUCTORS:\n   Validate in constructor too, not just setters.\n\n5. JAVA 16+ RECORDS:\n   record Account(double balance) {}\n   Immutable, auto-generates getters, ideal for DTOs."
            }
          ],
          "challenges": ['

if ($content.Contains($oldText)) {
    $content = $content.Replace($oldText, $newText)
    [System.IO.File]::WriteAllText($filePath, $content, (New-Object System.Text.UTF8Encoding $false))
    Write-Output 'SUCCESS: Encapsulation WARNING added'
} elseif ($content.Contains('Common Encapsulation Pitfalls')) {
    Write-Output 'Already has WARNING section'
} else {
    Write-Output 'Pattern mismatch'
}
