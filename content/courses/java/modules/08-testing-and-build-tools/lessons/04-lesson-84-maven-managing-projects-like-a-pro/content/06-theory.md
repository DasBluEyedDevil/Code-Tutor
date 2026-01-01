---
type: "THEORY"
title: "Common Maven Commands"
---

Run these from command line in project folder:

mvn clean
  → Deletes target/ folder (compiled output)

mvn compile
  → Compiles your main source code

mvn test
  → Compiles and runs all tests

mvn package
  → Compiles, tests, and creates JAR file

mvn install
  → Package and install to local repository

mvn clean test
  → Clean then test (combines commands)

MOST COMMON WORKFLOW:
1. Write code
2. mvn clean test (verify tests pass)
3. mvn package (create JAR)
4. Deploy JAR to production