---
type: "THEORY"
title: "Solution 3"
---

**Heroku** (simple):

```bash
# Enable auto-scaling (requires Standard dynos or higher)
heroku ps:autoscale:enable web --min 2 --max 10

# Set scaling target (requests per second)
heroku ps:autoscale:configure web --p95-response-time 500
```

**AWS ECS** - Create scaling-policy.json:

```json
{
  "TargetValue": 70.0,
  "PredefinedMetricSpecification": {
    "PredefinedMetricType": "ECSServiceAverageCPUUtilization"
  },
  "ScaleOutCooldown": 60,
  "ScaleInCooldown": 120
}
```

```bash
aws application-autoscaling register-scalable-target \
  --service-namespace ecs \
  --resource-id service/my-app-cluster/my-ktor-service \
  --scalable-dimension ecs:service:DesiredCount \
  --min-capacity 2 \
  --max-capacity 10

aws application-autoscaling put-scaling-policy \
  --policy-name cpu-tracking \
  --service-namespace ecs \
  --resource-id service/my-app-cluster/my-ktor-service \
  --scalable-dimension ecs:service:DesiredCount \
  --policy-type TargetTrackingScaling \
  --target-tracking-scaling-policy-configuration file://scaling-policy.json
```

**Google Cloud Run** (automatic!):

```bash
# Cloud Run scales automatically based on concurrent requests
gcloud run deploy my-app \
  --min-instances 2 \
  --max-instances 10 \
  --cpu-throttling \
  --concurrency 100 \
  --cpu 1 \
  --memory 512Mi

# View current instances
gcloud run services describe my-app --format="value(status.traffic)"
```
