---
type: "THEORY"
title: "Stage 2: Run"
---

FROM eclipse-temurin:25-jre
WORKDIR /app
COPY --from=builder /app/target/myapp.jar app.jar
EXPOSE 8080
ENTRYPOINT ["java", "-jar", "app.jar"]

Why multi-stage?
✓ Smaller image (no Maven, no source code)
✓ Faster downloads
✓ More secure (fewer tools = less attack surface)

Image sizes:
- Single stage with JDK: ~500 MB
- Multi-stage with JRE: ~250 MB

Layer optimization (built into Spring Boot):
Separate layers for dependencies vs your code: