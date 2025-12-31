$filePath = 'C:\Users\dasbl\Downloads\Code-Tutor\content\courses\java\course.json'
$content = [System.IO.File]::ReadAllText($filePath, [System.Text.Encoding]::UTF8)

# Lesson 1: Classes and Objects
$lesson1Old = 'You use ONE cookie cutter to make MANY cookies."
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-1-car",'

$lesson1New = 'You use ONE cookie cutter to make MANY cookies."
            },
            {
              "type": "WARNING",
              "title": "Common Classes and Objects Pitfalls",
              "content": "COMMON MISTAKES TO AVOID:\n\n1. FORGETTING new KEYWORD:\n   Student alice;  // Only declares a variable (null)\n   alice.name = \"Alice\";  // NullPointerException!\n   CORRECT: Student alice = new Student();\n\n2. CONFUSING CLASS WITH OBJECT:\n   Student.name = \"Alice\";  // ERROR! Cannot access fields on the class\n   CORRECT: Student s = new Student(); s.name = \"Alice\";\n\n3. PUBLIC FIELDS (BAD PRACTICE):\n   Directly accessing fields like myCar.speed = -500 allows invalid data.\n   Use encapsulation (private fields + getters/setters) instead.\n\n4. FORGETTING TO INITIALIZE FIELDS:\n   Fields have default values (0, null, false), but relying on them causes bugs.\n   Always initialize fields explicitly via constructors.\n\n5. JAVA 16+ ALTERNATIVE - RECORDS:\n   For simple data classes, consider using records:\n   record Student(String name, int age) {}\n   Records auto-generate constructor, getters, equals, hashCode, toString."
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-1-car",'

if ($content.Contains($lesson1Old)) {
    $content = $content.Replace($lesson1Old, $lesson1New)
    Write-Output "Added WARNING to Lesson 1: Classes and Objects"
} else {
    Write-Output "Lesson 1: Pattern not found or already modified"
}

# Lesson 2: Constructors
$lesson2Old = 'Without ''this'', Java gets confused about which ''name'' you mean!"
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-2-constructor",'

$lesson2New = 'Without ''this'', Java gets confused about which ''name'' you mean!"
            },
            {
              "type": "WARNING",
              "title": "Common Constructor Pitfalls",
              "content": "COMMON MISTAKES TO AVOID:\n\n1. ADDING RETURN TYPE:\n   public void Student(String name) { }  // This is a METHOD, not constructor!\n   CORRECT: public Student(String name) { }  // No return type\n\n2. FORGETTING this WITH SAME PARAMETER NAMES:\n   public Student(String name) {\n       name = name;  // Does NOTHING! Assigns parameter to itself\n   }\n   CORRECT: this.name = name;\n\n3. NOT CALLING super() IN SUBCLASS:\n   If parent has no default constructor, you MUST call super(...) first.\n\n4. CONSTRUCTOR CHAINING MISTAKES:\n   this(...) or super(...) must be the FIRST statement in constructor.\n\n5. JAVA 22+ FEATURE - STATEMENTS BEFORE super():\n   Java 22 allows statements before super() for validation/transformation.\n   Pre-Java 22: super() must always be first line.\n\n6. CONSIDER RECORDS FOR DATA CLASSES:\n   record Student(String name, int age) {}\n   Records generate canonical constructor, compact constructor validation."
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-2-constructor",'

if ($content.Contains($lesson2Old)) {
    $content = $content.Replace($lesson2Old, $lesson2New)
    Write-Output "Added WARNING to Lesson 2: Constructors"
} else {
    Write-Output "Lesson 2: Pattern not found or already modified"
}

# Lesson 3: Encapsulation
$lesson3Old = 'Professional code ALWAYS uses encapsulation!"
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-3-bank",'

$lesson3New = 'Professional code ALWAYS uses encapsulation!"
            },
            {
              "type": "WARNING",
              "title": "Common Encapsulation Pitfalls",
              "content": "COMMON MISTAKES TO AVOID:\n\n1. EXPOSING MUTABLE OBJECTS:\n   private List<String> items;\n   public List<String> getItems() { return items; }  // BAD!\n   Caller can modify your internal list!\n   CORRECT: return new ArrayList<>(items);  // Return a copy\n\n2. SETTERS FOR EVERYTHING (ANEMIC CLASSES):\n   Auto-generating all getters/setters defeats encapsulation.\n   Only expose what is truly needed. Prefer behavior methods.\n\n3. BREAKING IMMUTABILITY WITH ARRAYS:\n   private int[] scores;\n   public int[] getScores() { return scores; }  // BAD!\n   CORRECT: return Arrays.copyOf(scores, scores.length);\n\n4. FORGETTING VALIDATION IN CONSTRUCTORS:\n   Validate in constructor too, not just setters.\n   new BankAccount(-1000);  // Should this work?\n\n5. JAVA 16+ ALTERNATIVE - RECORDS:\n   Records are immutable by default with automatic encapsulation:\n   record Account(double balance) {}\n   Fields are final, only getters (no setters), perfect for DTOs.\n\n6. FRAMEWORK REQUIREMENTS:\n   Some frameworks (Hibernate, Spring) need getters/setters.\n   Use records for DTOs, traditional classes for JPA entities."
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-3-bank",'

if ($content.Contains($lesson3Old)) {
    $content = $content.Replace($lesson3Old, $lesson3New)
    Write-Output "Added WARNING to Lesson 3: Encapsulation"
} else {
    Write-Output "Lesson 3: Pattern not found or already modified"
}

# Lesson 4: Inheritance
$lesson4Old = 'Use interfaces for multiple \"contracts\" (later lesson)"
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-4-vehicle",'

$lesson4New = 'Use interfaces for multiple \"contracts\" (later lesson)"
            },
            {
              "type": "WARNING",
              "title": "Common Inheritance Pitfalls",
              "content": "COMMON MISTAKES TO AVOID:\n\n1. INHERITANCE FOR CODE REUSE ONLY:\n   Stack extends Vector // BAD! Stack is NOT a Vector\n   Prefer composition: class Stack { private List items; }\n\n2. DEEP INHERITANCE HIERARCHIES:\n   A -> B -> C -> D -> E  // Too deep! Hard to maintain\n   Keep hierarchies shallow (2-3 levels max).\n\n3. BREAKING PARENT CONTRACT:\n   Override methods must honor parent behavior expectations.\n   Violating Liskov Substitution Principle causes bugs.\n\n4. FORGETTING super() CALL:\n   If parent has no default constructor, must call super(...) explicitly.\n   Java 22+ allows statements before super() for validation.\n\n5. JAVA 17+ SEALED CLASSES:\n   sealed class Shape permits Circle, Square {}\n   Controls exactly which classes can extend yours.\n   Enables exhaustive pattern matching in switch.\n\n6. PREFER COMPOSITION OVER INHERITANCE:\n   Composition is more flexible and avoids tight coupling.\n   Use inheritance only for true IS-A relationships."
            }
          ],
          "challenges": [
            {
              "type": "FREE_CODING",
              "id": "epoch-2-lesson-4-vehicle",'

if ($content.Contains($lesson4Old)) {
    $content = $content.Replace($lesson4Old, $lesson4New)
    Write-Output "Added WARNING to Lesson 4: Inheritance"
} else {
    Write-Output "Lesson 4: Pattern not found or already modified"
}

# Save the file
[System.IO.File]::WriteAllText($filePath, $content, (New-Object System.Text.UTF8Encoding $false))
Write-Output "File saved successfully"
