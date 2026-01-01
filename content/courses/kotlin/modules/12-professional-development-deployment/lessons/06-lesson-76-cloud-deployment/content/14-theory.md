---
type: "THEORY"
title: "Solution 2"
---

**1. Create Dockerfile**:

```dockerfile
FROM gradle:8.5-jdk17 AS builder
WORKDIR /app
COPY . .
RUN gradle shadowJar --no-daemon

FROM eclipse-temurin:17-jre-alpine
WORKDIR /app
COPY --from=builder /app/build/libs/*-all.jar app.jar
EXPOSE 8080
CMD ["java", "-jar", "app.jar"]
```

**2. Push to ECR**:

```bash
# Create repository
aws ecr create-repository --repository-name my-ktor-app

# Login to ECR
aws ecr get-login-password | docker login --username AWS --password-stdin ACCOUNT_ID.dkr.ecr.REGION.amazonaws.com

# Build and push
docker build -t my-ktor-app .
docker tag my-ktor-app:latest ACCOUNT_ID.dkr.ecr.REGION.amazonaws.com/my-ktor-app:latest
docker push ACCOUNT_ID.dkr.ecr.REGION.amazonaws.com/my-ktor-app:latest
```

**3. Create ECS task definition** (task-definition.json):

```json
{
  "family": "my-ktor-app",
  "networkMode": "awsvpc",
  "requiresCompatibilities": ["FARGATE"],
  "cpu": "256",
  "memory": "512",
  "containerDefinitions": [{
    "name": "my-ktor-app",
    "image": "ACCOUNT_ID.dkr.ecr.REGION.amazonaws.com/my-ktor-app:latest",
    "portMappings": [{"containerPort": 8080}]
  }]
}
```

**4. Deploy to ECS**:

```bash
# Create cluster
aws ecs create-cluster --cluster-name my-app-cluster

# Register task definition
aws ecs register-task-definition --cli-input-json file://task-definition.json

# Create service with load balancer
aws ecs create-service \
  --cluster my-app-cluster \
  --service-name my-ktor-service \
  --task-definition my-ktor-app \
  --desired-count 2 \
  --launch-type FARGATE \
  --network-configuration "awsvpcConfiguration={subnets=[subnet-xxxxx],securityGroups=[sg-xxxxx],assignPublicIp=ENABLED}" \
  --load-balancers "targetGroupArn=arn:aws:elasticloadbalancing:...,containerName=my-ktor-app,containerPort=8080"
```
