---
type: "EXAMPLE"
title: "More Return Examples"
---


### Calculate Area of Rectangle


### Check if Adult


### Get Greeting Based on Time




```dart
String getGreeting(int hour) {
  if (hour < 12) {
    return 'Good morning!';
  } else if (hour < 18) {
    return 'Good afternoon!';
  } else {
    return 'Good evening!';
  }
}

void main() {
  print(getGreeting(9));   // Output: Good morning!
  print(getGreeting(14));  // Output: Good afternoon!
  print(getGreeting(20));  // Output: Good evening!
}
```
