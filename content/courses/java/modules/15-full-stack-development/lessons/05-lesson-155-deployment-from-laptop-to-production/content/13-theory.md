---
type: "THEORY"
title: "Stage 1: Build"
---

FROM eclipse-temurin:25-jdk as builder
WORKDIR /app
COPY pom.xml .
COPY src ./src
RUN ./mvnw clean package -DskipTests