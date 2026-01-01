---
type: "KEY_POINT"
title: "Routing Rules Summary"
---


**Master These 4 Rules**:

1. **`index.dart` = folder's root route**
   - `routes/users/index.dart` handles `/users`
   - Without it, `/users` returns 404

2. **`[param].dart` = dynamic segment**
   - Square brackets capture URL parts as parameters
   - The parameter name becomes the function argument

3. **Folders create path nesting**
   - `routes/api/users/profile.dart` = `/api/users/profile`
   - Each folder adds a segment to the URL

4. **File name = final URL segment**
   - `routes/hello.dart` = `/hello`
   - `routes/api/status.dart` = `/api/status`

**Pro Tip**: Think of your `routes/` folder as a mirror of your API documentation. When someone asks "what endpoints do you have?", just show them the folder structure!

