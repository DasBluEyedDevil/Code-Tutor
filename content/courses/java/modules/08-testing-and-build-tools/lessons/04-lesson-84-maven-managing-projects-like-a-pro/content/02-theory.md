---
type: "THEORY"
title: "What is Maven?"
---

MAVEN is a build automation tool that:

✓ Manages dependencies (downloads libraries)
✓ Compiles your code
✓ Runs tests
✓ Packages your app (creates JAR file)
✓ Enforces standard project structure

Instead of:
- "Download JUnit JAR"
- "Add to classpath"
- "Hope it works"

You write in pom.xml:
<dependency>
    <groupId>junit</groupId>
    <artifactId>junit</artifactId>
    <version>5.11.0</version>
</dependency>

Maven downloads it automatically! ✨