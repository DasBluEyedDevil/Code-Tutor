---
type: "THEORY"
title: "Run with layers"
---

FROM eclipse-temurin:25-jre
WORKDIR /app
COPY --from=builder /app/dependencies/ ./
COPY --from=builder /app/spring-boot-loader/ ./
COPY --from=builder /app/snapshot-dependencies/ ./
COPY --from=builder /app/application/ ./
ENTRYPOINT ["java", "org.springframework.boot.loader.JarLauncher"]

Benefit: Dependencies cached, only your code layer changes!