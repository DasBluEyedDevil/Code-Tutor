---
type: "THEORY"
title: "Load Balancing and Scaling"
---

### Horizontal Scaling

**AWS Auto Scaling**:

```bash
# Create auto-scaling group
aws autoscaling create-auto-scaling-group \
  --auto-scaling-group-name my-app-asg \
  --launch-template LaunchTemplateId=lt-xxxxx \
  --min-size 2 \
  --max-size 10 \
  --desired-capacity 2 \
  --target-group-arns arn:aws:elasticloadbalancing:...

# Add scaling policy (scale up when CPU > 70%)
aws autoscaling put-scaling-policy \
  --auto-scaling-group-name my-app-asg \
  --policy-name cpu-scale-up \
  --policy-type TargetTrackingScaling \
  --target-tracking-configuration file://scaling-config.json
```

**Google Cloud Run** (automatic):

```bash
# Cloud Run auto-scales automatically (0 to N instances)
gcloud run deploy my-app \
  --min-instances 1 \
  --max-instances 100 \
  --concurrency 80
```

### Vertical Scaling

**Change instance size** (requires restart):

```bash
# AWS EC2 - Stop instance first
aws ec2 stop-instances --instance-ids i-xxxxx
aws ec2 modify-instance-attribute \
  --instance-id i-xxxxx \
  --instance-type "{\"Value\": \"t3.large\"}"
aws ec2 start-instances --instance-ids i-xxxxx

# GCP Compute Engine
gcloud compute instances stop INSTANCE_NAME
gcloud compute instances set-machine-type INSTANCE_NAME \
  --machine-type n1-standard-2
gcloud compute instances start INSTANCE_NAME

# Heroku - Just change dyno type
heroku ps:type web=standard-2x
```
