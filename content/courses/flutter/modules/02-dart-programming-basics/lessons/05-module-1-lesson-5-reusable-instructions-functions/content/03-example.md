---
type: "EXAMPLE"
title: "Creating Your First Function"
---


**Conceptual Explanation**:
A function is like creating your own command. Once you define it, you can use it anywhere!

**The Pattern**:

```dart
// Define a function
void functionName() {
  // Code that runs when you call this function
}

// Call (use) the function
functionName();
```

**Real Example**:

```dart
// Define the function
void greet() {
  print('Hello!');
  print('Welcome to Flutter!');
  print('Have a great day!');
}

void main() {
  // Call it twice - no need to repeat the 3 print statements!
  greet();
  greet();
}
```

**Output**:



```dart
Hello!
Welcome to Flutter!
Have a great day!
Hello!
Welcome to Flutter!
Have a great day!
```
