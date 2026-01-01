---
type: "THEORY"
title: "Setting Up Flyway in Spring Boot"
---

STEP 1: Add dependency (build.gradle or pom.xml)

// Gradle
dependencies {
    implementation 'org.flywaydb:flyway-core'
}

// Maven
<dependency>
    <groupId>org.flywaydb</groupId>
    <artifactId>flyway-core</artifactId>
</dependency>

STEP 2: Configure (application.properties)

spring.flyway.enabled=true
spring.flyway.locations=classpath:db/migration

STEP 3: Create migration folder
src/main/resources/db/migration/

That's it! Spring Boot auto-configures Flyway.
Migrations run automatically on startup.