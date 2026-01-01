---
type: "THEORY"
title: "Introduction"
---


**Hosting Options for Dart Backends**

When deploying a Serverpod or Dart Frog backend for your Flutter app, you have several excellent hosting options, each with different tradeoffs:

**Platform-as-a-Service (PaaS):**

| Platform | Pros | Cons |
|----------|------|------|
| Railway | Easy setup, generous free tier, PostgreSQL included | Limited regions, newer platform |
| Fly.io | Global edge deployment, competitive pricing | Steeper learning curve |
| Render | Simple GitHub integration, free tier | Cold starts on free tier |
| Google Cloud Run | Scales to zero, pay-per-request | GCP complexity |

**Container Platforms:**

| Platform | Pros | Cons |
|----------|------|------|
| Docker + VPS | Full control, predictable cost | Manual management |
| Kubernetes | Enterprise-scale, auto-healing | Significant complexity |
| AWS ECS/Fargate | AWS integration, managed | Higher cost, AWS lock-in |

**Choosing the Right Platform:**

- **Hobby/MVP**: Railway or Render - fastest to deploy
- **Production with budget**: Fly.io - great performance/price ratio
- **Enterprise**: Docker on managed Kubernetes or cloud-native services
- **Cost-sensitive**: VPS with Docker - most cost-effective at scale

**Key Deployment Considerations:**

1. **Database proximity**: Backend and database should be in the same region
2. **Cold starts**: Serverless platforms may have latency on first request
3. **WebSocket support**: Required for real-time Serverpod features
4. **Persistent storage**: Needed for file uploads and caching

