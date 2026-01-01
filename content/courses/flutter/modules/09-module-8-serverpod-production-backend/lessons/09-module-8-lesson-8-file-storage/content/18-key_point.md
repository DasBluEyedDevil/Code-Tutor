---
type: "KEY_POINT"
title: "Summary: File Storage Best Practices"
---

After completing this lesson, remember these key principles:

**Architecture:**
- Use direct uploads with signed URLs for efficiency
- Separate public and private storage buckets
- Generate server-controlled file paths (never trust client paths)
- Store file metadata in your database

**Security:**
- Validate file types on the server using magic bytes
- Limit file sizes to prevent abuse
- Use signed URLs with short expiration for private files
- Never commit cloud credentials to version control
- Implement proper access control for all files

**Performance:**
- Generate thumbnails for images
- Use CDN for frequently accessed public files
- Consider lazy loading for large file lists
- Implement upload progress for better UX

**Cost Optimization:**
- Use lifecycle policies to move old files to cheaper storage
- Delete orphaned uploads (pending but never confirmed)
- Monitor storage usage and set billing alerts
- Compress text-based files

**Reliability:**
- Handle upload failures gracefully
- Implement retry logic in clients
- Use cloud storage for durability (not local disk)
- Keep file records in database for tracking

**In the Next Lesson:**
You will learn about deploying your Serverpod application to production, including server setup, domain configuration, and monitoring.

