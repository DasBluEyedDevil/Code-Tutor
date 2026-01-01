---
type: "THEORY"
title: "Storage Configuration and Optimization"
---

Optimizing your storage configuration reduces costs and improves performance.

**Storage Classes (S3 Example):**

| Class | Use Case | Cost | Retrieval |
|-------|----------|------|----------|
| Standard | Frequently accessed | $0.023/GB | Immediate |
| Intelligent-Tiering | Unknown patterns | $0.023/GB | Immediate |
| Standard-IA | Infrequent access | $0.0125/GB | Immediate |
| Glacier Instant | Archival, rare access | $0.004/GB | Milliseconds |
| Glacier Deep | Long-term archive | $0.00099/GB | 12 hours |

**Lifecycle Policies:**

Automatically transition files to cheaper storage:

```json
{
  "Rules": [{
    "ID": "ArchiveOldFiles",
    "Status": "Enabled",
    "Filter": {"Prefix": "documents/"},
    "Transitions": [
      {"Days": 30, "StorageClass": "STANDARD_IA"},
      {"Days": 90, "StorageClass": "GLACIER_IR"}
    ],
    "Expiration": {"Days": 365}
  }]
}
```

**Content Delivery Network (CDN):**

For public files, use a CDN to reduce latency:

1. **AWS CloudFront**: Works seamlessly with S3
2. **Cloudflare**: Easy setup, generous free tier
3. **Google Cloud CDN**: Integrates with GCS

Benefits:
- Cached copies near users globally
- Reduced S3/GCS egress costs
- HTTPS with custom domains
- DDoS protection

**Compression:**

Enable compression for text-based files:
- JSON, XML, HTML, CSS, JS: 70-90% smaller
- Already compressed: JPEG, PNG, ZIP (skip)

```yaml
# CloudFront configuration
Compress: true
```

**Cost Monitoring:**

- Set up billing alerts
- Review storage analytics monthly
- Delete unused files (orphaned uploads)
- Use lifecycle rules aggressively

