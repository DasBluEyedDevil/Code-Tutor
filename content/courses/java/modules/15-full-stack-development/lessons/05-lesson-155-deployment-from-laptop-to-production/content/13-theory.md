---
type: "THEORY"
title: "Stage 1: Build"
---

FROM eclipse-temurin:23-jdk as builder
WORKDIR /app
COPY pom.xml .
COPY src ./src
RUN ./mvnw clean package -DskipTests