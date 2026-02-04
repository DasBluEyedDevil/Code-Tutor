---
type: KEY_POINT
---

- `TextEditingController` reads and sets text field values; always `dispose()` it in `State.dispose()` to prevent memory leaks
- `Form` with `GlobalKey<FormState>` groups multiple fields and validates them all at once via `formKey.currentState!.validate()`
- `TextFormField` accepts a `validator` function that returns an error string on failure or `null` on success
- `InputDecoration` customizes labels, hints, prefixes, suffixes, and error styling for a polished input experience
- `keyboardType` (e.g., `TextInputType.emailAddress`) shows the right keyboard layout and `obscureText: true` hides password input
