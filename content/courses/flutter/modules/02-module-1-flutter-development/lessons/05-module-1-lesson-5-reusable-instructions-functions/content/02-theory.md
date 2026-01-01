---
type: "THEORY"
title: "Why Do We Need Functions?"
---


Look at this repetitive code:


We're printing those equals signs multiple times. With a function:


**Same output, cleaner code!**



```dart
void printBorder() {
  print('==========');
}

void main() {
  printBorder();
  print('Welcome!');
  printBorder();

  print('Processing...');

  printBorder();
  print('Done!');
  printBorder();
}
```
