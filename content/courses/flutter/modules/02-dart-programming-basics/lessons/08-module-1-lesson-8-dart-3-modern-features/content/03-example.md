---
type: "EXAMPLE"
title: "Anonymous Records (Positional Fields)"
---

Anonymous records group multiple values in parentheses. Access fields using $1, $2, etc. (1-indexed). Records are immutable - once created, values cannot be changed.

```dart
void main() {
  // Create a record with two values
  (String, int) person = ('Alice', 30);
  
  // Access by position (1-indexed with $ prefix)
  print('Name: ${person.$1}');  // Alice
  print('Age: ${person.$2}');   // 30
  
  // Records with more values
  (String, String, int, bool) employee = ('Bob', 'Engineering', 5, true);
  print('${employee.$1} works in ${employee.$2}');
  print('Years: ${employee.$3}, Active: ${employee.$4}');
  
  // Records are immutable - this won't compile:
  // person.$1 = 'Charlie';  // Error!
}
```
