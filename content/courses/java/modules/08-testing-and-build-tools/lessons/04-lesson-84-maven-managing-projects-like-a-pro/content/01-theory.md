---
type: "THEORY"
title: "The Problem: Managing Dependencies Manually is a Nightmare"
---

Imagine building a real project:

You need external libraries:
- JUnit for testing
- Gson for JSON parsing
- Apache Commons for utilities

Manual approach:
1. Download each JAR file from the internet
2. Put them in your project folder
3. Add to classpath manually
4. What if library needs OTHER libraries?
5. What if versions conflict?
6. How do teammates get same libraries?

This is CHAOS! Welcome to dependency hell.

Solution: BUILD TOOLS like Maven