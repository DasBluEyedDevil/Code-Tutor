---
type: "THEORY"
title: "Changing Variable Contents"
---


Variables aren't permanent - you can change what's inside the box:


Notice: The second time, we don't use `var` - we already created the variable!



```dart
void main() {
  var mood = 'happy';
  print('I am feeling $mood');  // Output: I am feeling happy

  mood = 'excited';  // Change the contents
  print('Now I am feeling $mood');  // Output: Now I am feeling excited
}
```
