---
type: "THEORY"
title: "Serverpod File Storage Configuration"
---

Serverpod provides a unified file storage API that works with multiple cloud providers. Let us configure it step by step.

**Step 1: Add Dependencies**

In your server's `pubspec.yaml`, the storage packages are already included with Serverpod:

```yaml
dependencies:
  serverpod: ^2.0.0
  # Storage is included in serverpod package
```

**Step 2: Configure Storage in config/production.yaml**

Serverpod uses configuration files to set up storage providers:

```yaml
# config/production.yaml

# Database configuration
database:
  host: your-db-host
  port: 5432
  name: your_database
  user: your_user

# Storage configuration
storage:
  # Public storage - files accessible without authentication
  public:
    type: s3
    bucket: myapp-public-assets
    region: us-east-1
    publicHost: https://myapp-public-assets.s3.amazonaws.com
  
  # Private storage - files require signed URLs
  private:
    type: s3
    bucket: myapp-private-files
    region: us-east-1
```

**Step 3: Set Environment Variables**

Never commit credentials to your repository. Use environment variables:

```bash
# AWS credentials
export AWS_ACCESS_KEY_ID=your_access_key
export AWS_SECRET_ACCESS_KEY=your_secret_key

# Or for Google Cloud
export GOOGLE_APPLICATION_CREDENTIALS=/path/to/service-account.json
```

**Step 4: Create Storage Buckets**

In AWS Console or using AWS CLI:

```bash
# Create public bucket
aws s3 mb s3://myapp-public-assets --region us-east-1

# Create private bucket
aws s3 mb s3://myapp-private-files --region us-east-1

# Configure public bucket for public read access
aws s3api put-bucket-policy --bucket myapp-public-assets --policy '{
  "Version": "2012-10-17",
  "Statement": [{
    "Effect": "Allow",
    "Principal": "*",
    "Action": "s3:GetObject",
    "Resource": "arn:aws:s3:::myapp-public-assets/*"
  }]
}'
```

