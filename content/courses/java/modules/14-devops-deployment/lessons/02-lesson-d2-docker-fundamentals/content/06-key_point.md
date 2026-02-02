---
type: "KEY_POINT"
title: "Docker Layer Caching"
---

Docker builds images in LAYERS. Each instruction creates a layer:

FROM eclipse-temurin:25-jdk-alpine  # Layer 1: Base image
COPY pom.xml .                      # Layer 2: pom.xml
RUN mvn dependency:go-offline       # Layer 3: Dependencies
COPY src/ src/                      # Layer 4: Source code
RUN mvn package                     # Layer 5: Build

CALLING STRATEGY:
- Docker caches each layer
- If a layer changes, all subsequent layers rebuild
- Order matters!

BAD ORDER:
COPY . .                    # Copy everything first
RUN mvn package             # Download deps + build

Problem: Any file change (even README) busts the cache
and re-downloads all dependencies.

GOOD ORDER:
COPY pom.xml .              # pom.xml rarely changes
RUN mvn dependency:go-offline  # Cache dependencies
COPY src/ src/              # Source changes often
RUN mvn package -o          # Build (use offline mode)

Result: Change source code? Only layers 4-5 rebuild.
Dependencies stay cached. Builds go from 5 min to 30 sec.

.dockerignore FILE:
target/
.git/
.idea/
*.md

Exclude files that shouldn't go in the image.