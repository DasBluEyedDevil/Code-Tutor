---
type: "THEORY"
title: "The pom.xml File - Project Object Model"
---

pom.xml is the heart of a Maven project:

<project>
    <groupId>com.yourcompany</groupId>
    <artifactId>my-app</artifactId>
    <version>1.0.0</version>
    
    <properties>
        <maven.compiler.source>23</maven.compiler.source>
        <maven.compiler.target>23</maven.compiler.target>
    </properties>
    
    <dependencies>
        <dependency>
            <groupId>org.junit.jupiter</groupId>
            <artifactId>junit-jupiter</artifactId>
            <version>5.11.0</version>
            <scope>test</scope>
        </dependency>
    </dependencies>
</project>

KEY SECTIONS:
- groupId: Your organization (like package)
- artifactId: Project name
- version: Your project version
- properties: Configuration (Java version, etc.)
- dependencies: External libraries you need