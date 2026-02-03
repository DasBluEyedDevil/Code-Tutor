---
type: "THEORY"
title: "Creating the Spring Boot Project"
---

Let us set up our backend project using Spring Initializr. We will configure all the dependencies we need for a production-ready application.

Using Spring Initializr (start.spring.io):

Project Configuration:
- Project: Maven
- Language: Java
- Spring Boot: 4.0.x
- Group: com.taskmanager
- Artifact: taskmanager
- Packaging: Jar
- Java: 25

Dependencies to Add:
1. Spring Web - REST API endpoints, embedded Tomcat
2. Spring Data JPA - Database access, repository pattern
3. Spring Security - Authentication, authorization
4. PostgreSQL Driver - Database connectivity
5. Validation - Input validation (@NotBlank, @Email)
6. Flyway Migration - Database schema versioning
7. Spring Boot DevTools - Hot reload during development
8. Lombok - Reduce boilerplate (optional but recommended)

After generating the project, your pom.xml will include all these dependencies. The project structure follows Maven conventions with src/main/java for code and src/main/resources for configuration.

Important: Before running the application, we need to configure the database connection in application.yml. Without this, Spring Boot cannot start because Spring Data JPA requires a datasource.