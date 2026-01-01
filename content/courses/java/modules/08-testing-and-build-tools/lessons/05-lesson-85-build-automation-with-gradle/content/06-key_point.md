---
type: "KEY_POINT"
title: "Gradle Wrapper"
---

ALWAYS USE THE WRAPPER!

CORRECT: ./gradlew build
WRONG: gradle build

WHY?
- Wrapper ensures EVERYONE uses same Gradle version
- No need to install Gradle separately
- Works on any machine with Java

FILES CREATED:
- gradlew (Unix script)
- gradlew.bat (Windows script)
- gradle/wrapper/gradle-wrapper.jar
- gradle/wrapper/gradle-wrapper.properties

COMMIT THESE TO GIT!
Teammates don't need Gradle installed.

UPDATING WRAPPER VERSION:
./gradlew wrapper --gradle-version 8.11