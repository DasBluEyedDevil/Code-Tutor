---
type: "THEORY"
title: "Input Validation"
---


### Never Trust User Input

**Golden Rule**: All input is malicious until proven otherwise.

### SQL Injection Prevention

❌ **DANGER** (SQL Injection vulnerable):

✅ **Safe** (Parameterized queries):

### XSS Prevention

❌ **Bad** (XSS vulnerable):

✅ **Good** (Sanitized):

### Email Validation

❌ **Bad** (Weak validation):

✅ **Good** (Robust validation):

### Path Traversal Prevention

❌ **DANGER** (Path traversal):

✅ **Safe** (Validated path):

---



```kotlin
fun getFile(filename: String): File? {
    // Validate filename
    if (filename.contains("..") || filename.contains("/")) {
        logger.warn("Path traversal attempt: $filename")
        return null
    }

    val file = File("/uploads", filename).canonicalFile
    val uploadDir = File("/uploads").canonicalFile

    // Ensure file is within upload directory
    if (!file.path.startsWith(uploadDir.path)) {
        logger.warn("Path traversal detected: $filename")
        return null
    }

    return file
}
```
