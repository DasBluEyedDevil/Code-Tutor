---
type: "THEORY"
title: "Building a Production JAR"
---

Spring Boot creates a 'fat JAR' - everything in one file:

Build with Maven:
./mvnw clean package

This creates:
target/myapp-0.0.1-SNAPSHOT.jar

Run the JAR:
java -jar target/myapp-0.0.1-SNAPSHOT.jar

What's inside the JAR?
- Your compiled code (.class files)
- All dependencies (Spring, database drivers, etc.)
- Embedded web server (Tomcat)
- Configuration files (application.yml)

RESULT: One file that runs anywhere with Java installed!

Skip tests during build (faster, but risky):
./mvnw clean package -DskipTests

Build with specific profile:
./mvnw clean package -Pprod

Rename JAR to something simpler:
In pom.xml:
<build>
    <finalName>myapp</finalName>
</build>

Now creates: target/myapp.jar