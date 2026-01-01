---
type: "KEY_POINT"
title: "Maven Dependency Scopes"
---

SCOPE controls WHEN a dependency is available:

<scope>compile</scope> (default)
  - Available everywhere (main code, tests, runtime)
  - Example: Gson JSON library

<scope>test</scope>
  - Only for tests, not in final JAR
  - Example: JUnit

<scope>provided</scope>
  - Available at compile-time, but server provides it at runtime
  - Example: Servlet API (server like Tomcat has it)

<scope>runtime</scope>
  - Not needed for compilation, only at runtime
  - Example: JDBC database driver

RULE: Use test scope for testing libraries (saves space in final JAR)