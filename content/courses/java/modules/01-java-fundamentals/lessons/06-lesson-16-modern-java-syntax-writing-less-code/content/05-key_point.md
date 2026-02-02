---
type: "KEY_POINT"
title: "JEP 476: Module Imports"
---

Tired of importing dozens of classes?

// OLD WAY:
import java.util.List;
import java.util.ArrayList;
import java.util.Map;
import java.util.HashMap;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
// ... and more

// NEW WAY (Java 23+):
import module java.base;

This single line imports EVERYTHING from java.base:
- All collections (List, Map, Set...)
- All I/O classes (Files, Path, Stream...)
- All utility classes (Optional, Objects...)
- And more!

Perfect for learning and prototyping. In production code, you may prefer explicit imports for clarity.