---
type: "WARNING"
title: "Common Maven Pitfalls"
---

AVOID THESE MISTAKES:

1. NOT USING DEPENDENCY MANAGEMENT
   In multi-module projects, use <dependencyManagement> to centralize versions.

2. USING SNAPSHOT VERSIONS IN PRODUCTION
   SNAPSHOT versions can change unexpectedly. Use release versions for production.

3. NOT SPECIFYING ENCODING
   Add <project.build.sourceEncoding>UTF-8</project.build.sourceEncoding> to properties.

4. FORGETTING MAVEN WRAPPER
   Use mvnw (Maven Wrapper) so all developers use the same Maven version.

5. IGNORING DEPENDENCY CONFLICTS
   Run 'mvn dependency:tree' to check for version conflicts.