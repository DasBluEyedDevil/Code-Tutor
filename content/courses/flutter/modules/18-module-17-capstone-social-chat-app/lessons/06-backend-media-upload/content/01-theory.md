---
type: "THEORY"
title: "Media Storage Architecture"
---


**Cloud Storage for Social Media Applications**

Media files like images and videos require specialized storage solutions. Unlike database records, media files are large, binary, and benefit from CDN distribution. In this lesson, we'll implement a robust media upload system using cloud storage with Serverpod.

**Cloud Storage Options Comparison**

| Provider | Strengths | Pricing Model | Best For |
|----------|-----------|---------------|----------|
| **AWS S3** | Industry standard, extensive tooling | Pay per GB + requests | Enterprise, high volume |
| **Cloudflare R2** | Zero egress fees, S3-compatible | Pay per GB stored only | Cost-sensitive, global delivery |
| **Google Cloud Storage** | ML integration, strong consistency | Pay per GB + operations | GCP ecosystem, AI workloads |
| **DigitalOcean Spaces** | Simple pricing, S3-compatible | $5/mo for 250GB | Startups, predictable costs |

**Serverpod File Storage Architecture**

Serverpod provides built-in cloud storage abstraction:

```
┌─────────────────────────────────────────────────────────┐
│                    Client Upload Flow                    │
├─────────────────────────────────────────────────────────┤
│                                                          │
│   Mobile App                    Serverpod Server         │
│       │                              │                   │
│       │─── 1. Request Upload URL ───>│                   │
│       │                              │                   │
│       │<── 2. Signed URL + ID ───────│                   │
│       │                              │                   │
│       │                         Cloud Storage            │
│       │                              │                   │
│       │─── 3. Direct Upload ────────>│                   │
│       │                              │                   │
│       │<── 4. Upload Complete ───────│                   │
│       │                              │                   │
│       │─── 5. Confirm Upload ───────>│  (Serverpod)     │
│       │                              │                   │
│       │<── 6. Media Record ─────────│                   │
│                                                          │
└─────────────────────────────────────────────────────────┘
```

**Why Signed URLs?**

Direct client-to-storage uploads avoid:
- Server bandwidth bottlenecks
- Memory pressure from large files
- Timeout issues with slow connections

Signed URLs provide:
- Time-limited access (typically 15-60 minutes)
- Path restrictions (specific file key only)
- Size limits enforced by storage provider
- Content-type validation

**File Organization Patterns**

| Pattern | Structure | Pros | Cons |
|---------|-----------|------|------|
| **User-based** | `users/{userId}/media/{fileId}` | Easy user cleanup | Hot partitions for active users |
| **Date-based** | `media/{year}/{month}/{day}/{fileId}` | Balanced distribution | Harder to find user files |
| **Content-type** | `images/{hash}`, `videos/{hash}` | Type-specific processing | Cross-type queries harder |
| **Hybrid** | `{contentType}/{userId}/{year-month}/{fileId}` | Best of all worlds | More complex paths |

We'll use the hybrid approach for optimal organization and query patterns.

**Content Addressing with Hashes**

Using content hashes as part of file identifiers provides:
- **Deduplication**: Same file uploaded twice uses same storage
- **Integrity verification**: Detect corruption or tampering
- **Cache efficiency**: Identical content = identical URL = cached

```
Original filename: vacation_photo.jpg
Content hash: a1b2c3d4e5f6...
Storage key: images/user_42/2024-01/a1b2c3d4e5f6_vacation_photo.jpg
```

**Security Considerations**

1. **Never trust client metadata**: Verify file type server-side
2. **Scan for malware**: Integrate virus scanning for uploads
3. **Strip EXIF data**: Remove location/personal metadata from images
4. **Validate dimensions**: Reject excessively large images
5. **Rate limit uploads**: Prevent storage abuse

