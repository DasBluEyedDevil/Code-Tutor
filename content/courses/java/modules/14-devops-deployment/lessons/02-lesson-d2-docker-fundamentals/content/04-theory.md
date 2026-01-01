---
type: "THEORY"
title: "Dockerfile Instructions Explained"
---

ESSENTIAL INSTRUCTIONS:

FROM <image>:<tag>
  Base image to build on
  eclipse-temurin:21-jdk-alpine = Java 21 on Alpine Linux
  Alpine = lightweight Linux (5MB vs 200MB)

WORKDIR /path
  Set working directory for subsequent commands
  Like 'cd' but creates if doesn't exist

COPY <source> <destination>
  Copy files from host to container
  COPY . . = copy everything (avoid - include .git, etc.)
  COPY src/ src/ = copy specific directories

RUN <command>
  Execute command during image BUILD
  Each RUN creates a new layer
  Combine commands with && to reduce layers

EXPOSE <port>
  Document which port the app uses
  Doesn't actually publish the port
  Publishing done with -p when running

ENTRYPOINT ["command", "arg1"]
  Command to run when container STARTS
  Use exec form ["..."] not shell form

MULTI-STAGE BUILDS:

FROM eclipse-temurin:21-jdk-alpine AS build
  # Full JDK for compilation
  # ~300MB image

FROM eclipse-temurin:21-jre-alpine
  # Only JRE for runtime
  # ~100MB image
  COPY --from=build /app/target/*.jar app.jar

Result: Production image is 1/3 the size!