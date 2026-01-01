---
type: "THEORY"
title: "Extract layers"
---

FROM eclipse-temurin:23-jdk as builder
WORKDIR /app
COPY target/myapp.jar app.jar
RUN java -Djarmode=layertools -jar app.jar extract