---
type: "THEORY"
title: "Multiple Choices: else if"
---


What if you have more than two options?


**Output**: `Grade: B`

The program:
1. Checks if score >= 90 (NO, 85 is not >= 90)
2. Checks if score >= 80 (YES! → runs this block)
3. Stops checking (once one condition is true, it skips the rest)



```dart
void main() {
  var score = 85;

  if (score >= 90) {
    print('Grade: A');
  } else if (score >= 80) {
    print('Grade: B');
  } else if (score >= 70) {
    print('Grade: C');
  } else if (score >= 60) {
    print('Grade: D');
  } else {
    print('Grade: F');
  }
}
```
