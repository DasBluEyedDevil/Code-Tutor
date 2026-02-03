---
type: "WARNING"
title: "File I/O and Data Storage Pitfalls"
---

## Watch Out For These Issues!

**File path injection**: Never build file paths from user input without validation! `File.ReadAllText(userInput)` can read ANY file on disk. Always sanitize and restrict to known directories using `Path.Combine()` with a base directory, then verify the result stays within bounds.

**Not disposing streams**: `StreamReader` and `StreamWriter` implement `IDisposable`. Forgetting to dispose them leaks file handles! Always use `using var reader = new StreamReader(path);` or the simpler `File.ReadAllText()` / `File.WriteAllText()` which handle disposal internally.

**Encoding issues**: `File.ReadAllText()` defaults to UTF-8, but files may use other encodings. Garbled text? Specify encoding explicitly: `File.ReadAllText(path, Encoding.Latin1)`. When writing, always use UTF-8 unless you have a specific reason not to.

**Text files for structured data**: CSV and text files seem simple but break when data contains commas, newlines, or special characters. For anything beyond simple configuration, use a database -- that is what the rest of this module teaches!
