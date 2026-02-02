---
type: "EXAMPLE"
title: "Named Records (Named Fields)"
---

Named records use field names instead of positions for clarity. Access fields by name (person.name) rather than index. You can mix positional and named fields in the same record.

```dart
void main() {
  // Named fields for clarity
  ({String name, int age}) person = (name: 'Alice', age: 30);
  
  // Access by name - much more readable!
  print('Name: ${person.name}');
  print('Age: ${person.age}');
  
  // Mix positional and named fields
  (String, {int age, String city}) profile = (
    'Charlie',
    age: 25,
    city: 'New York',
  );
  
  print('${profile.$1} is ${profile.age} years old');
  print('Lives in ${profile.city}');
}
```
