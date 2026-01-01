---
type: "THEORY"
title: "AWS Deployment"
---


### Option 1: AWS Elastic Beanstalk (Easiest AWS)

**1. Install AWS CLI**:

**2. Install Elastic Beanstalk CLI**:

**3. Initialize EB**:

**4. Create Dockerfile** (if not exists):

**5. Create .ebextensions/options.config**:

**6. Deploy**:

### Option 2: AWS ECS (Container Service)

**1. Create ECR repository**:

**2. Build and push Docker image**:

**3. Create task definition** (task-definition.json):

**4. Create ECS service**:

---



```bash
# Create cluster
aws ecs create-cluster --cluster-name my-app-cluster

# Register task definition
aws ecs register-task-definition --cli-input-json file://task-definition.json

# Create service
aws ecs create-service \
  --cluster my-app-cluster \
  --service-name my-app-service \
  --task-definition my-app \
  --desired-count 2 \
  --launch-type FARGATE
```
