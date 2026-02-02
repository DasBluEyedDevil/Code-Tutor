---
type: "KEY_POINT"
title: "Media Upload Checklist"
---


**Production Media Upload Checklist**

**Storage Setup**
- [ ] Configure cloud storage buckets (public/private)
- [ ] Set up CDN for public media delivery
- [ ] Configure CORS for direct browser uploads
- [ ] Set lifecycle policies for cleanup

**Upload Security**
- [ ] Validate MIME types server-side
- [ ] Verify file magic bytes match claimed type
- [ ] Enforce file size limits per type
- [ ] Generate signed URLs with expiration
- [ ] Implement upload quotas per user

**File Processing**
- [ ] Generate thumbnails in multiple sizes
- [ ] Strip EXIF/metadata for privacy
- [ ] Compress images appropriately
- [ ] Extract video thumbnails and metadata
- [ ] Queue processing for background execution

**Large File Handling**
- [ ] Implement chunked uploads for videos
- [ ] Track upload progress per session
- [ ] Handle upload resumption
- [ ] Clean up incomplete uploads
- [ ] Set session expiration times

**Deduplication**
- [ ] Hash content for duplicate detection
- [ ] Reuse existing files when possible
- [ ] Consider storage savings vs. compute cost

