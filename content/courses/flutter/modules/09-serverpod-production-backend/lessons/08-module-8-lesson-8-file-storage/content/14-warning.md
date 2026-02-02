---
type: "WARNING"
title: "Image Processing Considerations"
---

Image processing is resource-intensive. Consider these factors:

**Memory Usage:**
- A 4000x3000 pixel image uses ~48 MB of RAM when decoded (4000 * 3000 * 4 bytes)
- Processing multiple images simultaneously can exhaust server memory
- Use streaming or queue systems for batch processing

**Processing Time:**
- Large images can take several seconds to process
- Do not process synchronously in API handlers
- Use background jobs or dedicated worker processes

**Recommended Architecture:**

1. **Synchronous (Small Scale):**
   - Process during upload confirmation
   - Acceptable for apps with few uploads
   - Limits: ~10 concurrent uploads

2. **Background Jobs (Medium Scale):**
   - Return immediately from upload
   - Process in background worker
   - Update record when thumbnails ready
   - Flutter app polls or uses WebSocket for status

3. **Dedicated Service (Large Scale):**
   - Use AWS Lambda or Cloud Functions
   - Trigger on S3/GCS upload event
   - Scales automatically
   - Process without impacting main server

**Security:**
- Validate images thoroughly (malformed images can crash decoders)
- Limit maximum file size before processing
- Consider sandboxing image processing
- Malicious images can contain embedded code

