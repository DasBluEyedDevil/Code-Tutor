---
type: "THEORY"
title: "Run application"
---

ENTRYPOINT ["java", "-jar", "app.jar"]

Build Docker image:
docker build -t myapp:1.0 .

Run container:
docker run -p 8080:8080 \
  -e DATABASE_URL=jdbc:postgresql://db:5432/myapp \
  -e DATABASE_PASSWORD=secret \
  myapp:1.0

Access app: http://localhost:8080