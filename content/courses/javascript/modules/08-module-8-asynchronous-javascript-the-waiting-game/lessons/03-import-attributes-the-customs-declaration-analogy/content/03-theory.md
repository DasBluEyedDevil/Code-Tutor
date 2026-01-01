---
type: "THEORY"
title: "The Evolution of Imports"
---

As JavaScript has moved away from simple scripts to complex module systems, the way we handle non-code files (like JSON and CSS) has evolved.

### 1. The Security Problem
In the past, if you imported a file, the browser looked at the file extension or the "MIME type" sent by the server. If a hacker managed to compromise a server and change a `.json` file to a `.js` file, your code might accidentally execute malicious script thinking it was just reading data.

### 2. The `with` Keyword (ES2025)
Import Attributes solve this by moving the validation to the **source code**. By adding `with { type: "json" }`, you are hard-coding a security check. If the browser receives anything other than JSON, it throws an error and stops the script.

### 3. Syntax Breakdown
```javascript
import data from "./file.json" with { type: "json" };
```
*   `import data`: The name you give the imported content.
*   `from "./file.json"`: The path to the file.
*   `with { ... }`: The attributes block.
*   `type: "json"`: The specific validation rule.

### 4. Beyond JSON
While JSON is currently the most common use case, the Import Attributes specification is designed to be extensible. In the future, we will see:
*   `type: "css"` — For CSS Module Scripts.
*   `type: "html"` — For importing HTML fragments.
*   Custom attributes for specific tools or bundlers.
