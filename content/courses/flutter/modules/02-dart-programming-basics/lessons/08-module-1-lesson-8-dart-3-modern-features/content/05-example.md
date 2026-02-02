---
type: "EXAMPLE"
title: "Records as Function Return Types"
---

Records elegantly solve the multiple-return-value problem. Declare the record type as the return type, then return values in parentheses. Named fields make the API self-documenting.

```dart
// Return multiple values elegantly!
(String, int) getUserInfo() {
  // Imagine fetching from database
  return ('Alice', 30);
}

// Named fields version - even clearer
({String name, int age, String email}) fetchUser() {
  return (
    name: 'Bob',
    age: 25,
    email: 'bob@example.com',
  );
}

// Return success/error with data
(bool success, String? data, String? error) fetchData() {
  try {
    // Simulate API call
    return (true, 'Data loaded!', null);
  } catch (e) {
    return (false, null, e.toString());
  }
}

void main() {
  // Using positional record
  var info = getUserInfo();
  print('${info.$1} is ${info.$2} years old');
  
  // Using named record
  var user = fetchUser();
  print('${user.name}: ${user.email}');
  
  // Handling result record
  var result = fetchData();
  if (result.$1) {
    print('Success: ${result.$2}');
  } else {
    print('Error: ${result.$3}');
  }
}
```
