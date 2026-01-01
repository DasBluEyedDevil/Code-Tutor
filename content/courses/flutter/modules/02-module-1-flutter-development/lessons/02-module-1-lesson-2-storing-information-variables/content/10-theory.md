---
type: "THEORY"
title: "Math with Variables"
---


You can do math with number variables:


**Note**: To print an actual `$` symbol, you need to escape it with `\$`.



```dart
void main() {
  var apples = 5;
  var oranges = 3;
  var totalFruit = apples + oranges;

  print('Total fruit: $totalFruit');  // Output: Total fruit: 8

  var price = 10.50;
  var tax = 2.15;
  var total = price + tax;

  print('Total with tax: \$${total}');  // Output: Total with tax: $12.65
}
```
