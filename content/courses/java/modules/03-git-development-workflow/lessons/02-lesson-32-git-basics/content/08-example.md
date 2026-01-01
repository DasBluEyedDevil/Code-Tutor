---
type: "EXAMPLE"
title: "The .gitignore File"
---

The .gitignore file tells Git which files to ignore (never track).

CREATING A .gitignore FOR JAVA PROJECTS:

Create a file named '.gitignore' in your project root:

```gitignore
# .gitignore for Java projects

# Compiled class files
*.class

# Package files
*.jar
*.war
*.ear

# Build directories
/target/
/build/
/out/
/bin/

# IDE settings
.idea/
*.iml
.vscode/
*.swp
.project
.classpath
.settings/

# Maven
/target/
pom.xml.tag
pom.xml.releaseBackup

# Gradle
.gradle/
build/

# Logs
*.log

# OS files
.DS_Store
Thumbs.db

# Environment files (NEVER commit these!)
.env
*.env.local
application-secret.properties

# Sensitive data
**/secrets/
*.pem
*.key
```
