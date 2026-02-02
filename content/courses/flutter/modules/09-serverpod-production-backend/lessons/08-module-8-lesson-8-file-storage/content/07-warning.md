---
type: "WARNING"
title: "Storage Security Best Practices"
---

File storage is a common attack vector. Follow these security practices:

**Never Trust User Input:**
- Validate file types on the server, not just the client
- Check file headers (magic bytes), not just extensions
- Limit file sizes to prevent storage attacks
- Sanitize file names to prevent path traversal

**Example of Path Traversal Attack:**
```dart
// DANGEROUS: User could upload with name '../../../etc/passwd'
final userFileName = request.fileName; // NEVER use directly

// SAFE: Generate your own file names
final safeFileName = '${uuid.v4()}.${extension}';
```

**Credential Security:**
- Never commit AWS/GCP credentials to git
- Use IAM roles in production (not access keys)
- Rotate credentials regularly
- Use least-privilege access (only permissions needed)

**Bucket Configuration:**
- Enable versioning for important buckets
- Enable server-side encryption
- Configure lifecycle rules to delete old files
- Enable access logging for audit trails
- Block public access unless explicitly needed

**Content Validation:**
- Scan uploaded files for malware
- Validate image dimensions for profile pictures
- Check file content matches claimed type
- Reject executable files unless specifically needed

