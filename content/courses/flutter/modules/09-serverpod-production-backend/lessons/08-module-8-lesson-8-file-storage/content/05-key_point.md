---
type: "KEY_POINT"
title: "Understanding Storage Architecture"
---

Before configuring storage, understand the key concepts:

**Buckets:**
A bucket is a container for files (objects). Think of it as a top-level folder in cloud storage.
- Each bucket has a globally unique name
- Buckets are region-specific
- You can have multiple buckets for different purposes

Example bucket structure:
- `myapp-user-uploads` - User-uploaded content
- `myapp-public-assets` - Public images and assets
- `myapp-private-documents` - Sensitive files

**Objects:**
Files stored in buckets are called objects. Each object has:
- Key: The file path/name (e.g., `users/123/avatar.png`)
- Value: The file content (binary data)
- Metadata: Content type, size, custom headers
- ACL: Access control list (who can access)

**Signed URLs:**
Temporary URLs that grant time-limited access to private files.
- Include cryptographic signature
- Expire after specified duration
- Allow downloads without exposing credentials
- Can also be used for secure uploads

**Content Delivery Network (CDN):**
A network of servers that cache and serve files from locations near users.
- Reduces latency (faster downloads)
- Reduces load on storage
- Often included with cloud storage (CloudFront for S3, Cloud CDN for GCS)

