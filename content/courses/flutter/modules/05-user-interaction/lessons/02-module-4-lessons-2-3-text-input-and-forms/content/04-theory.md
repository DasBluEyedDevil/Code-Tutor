---
type: "THEORY"
title: "Forms and Validation"
---

For complex inputs with multiple fields, use the `Form` and `TextFormField` widgets. This allows you to validate all fields at once (e.g., check if an email is valid before submitting).

1.  **`Form`**: The container for all fields.
2.  **`GlobalKey<FormState>`**: A unique key used to trigger validation.
3.  **`TextFormField`**: A `TextField` with built-in validation support.

```dart
final _formKey = GlobalKey<FormState>();

Form(
  key: _formKey,
  child: Column(
    children: [
      TextFormField(
        validator: (value) {
          if (value == null || value.isEmpty) {
            return 'Please enter some text';
          }
          return null; // Valid
        },
      ),
      ElevatedButton(
        onPressed: () {
          // Trigger validation for all fields!
          if (_formKey.currentState!.validate()) {
            print('Process data');
          }
        },
        child: Text('Submit'),
      ),
    ],
  ),
)
```
