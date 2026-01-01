---
type: "THEORY"
title: "Cloud Storage Providers Overview"
---

Serverpod supports multiple cloud storage providers. Understanding their differences helps you choose the right one.

**Amazon S3 (Simple Storage Service):**

The industry standard, used by millions of applications.

Advantages:
- Extremely reliable (99.999999999% durability)
- Extensive ecosystem and tooling
- Flexible storage classes for cost optimization
- Excellent documentation
- Mature and battle-tested

Pricing (approximate):
- Storage: $0.023 per GB/month (Standard)
- Data transfer out: $0.09 per GB (first 10 TB)
- PUT/POST requests: $0.005 per 1,000
- GET requests: $0.0004 per 1,000

**Google Cloud Storage:**

Google's equivalent offering with tight GCP integration.

Advantages:
- Seamless integration with other Google services
- Competitive pricing
- Strong analytics and AI integration
- Good for apps already using Firebase

Pricing (approximate):
- Storage: $0.020 per GB/month (Standard)
- Data transfer out: $0.12 per GB (first 1 TB)
- Similar request pricing to S3

**When to Choose Which:**

Choose S3 if:
- You are already using AWS services
- You need the most mature ecosystem
- You want extensive third-party tool support

Choose Google Cloud Storage if:
- You are using Firebase or other GCP services
- You need integration with Google AI/ML services
- Your team is familiar with Google Cloud

