---
type: "KEY_POINT"
title: "Upload Flow: Direct vs Server-Proxied"
---

There are two ways to handle file uploads. Understanding the difference is crucial for building efficient systems.

**Server-Proxied Upload (Not Recommended for Large Files):**
```
Client -> Server -> Cloud Storage
```
- Client uploads file to your Serverpod server
- Server receives entire file into memory
- Server then uploads to cloud storage

Problems:
- Doubles bandwidth (client-to-server, server-to-storage)
- Server memory spikes with large files
- Slow for users (file travels twice)
- Limits concurrent uploads

**Direct Upload with Signed URLs (Recommended):**
```
1. Client asks server for upload URL
2. Server creates signed URL, returns it
3. Client uploads directly to cloud storage
4. Client notifies server of completion
```

Benefits:
- File goes directly from client to cloud
- Server only handles metadata
- Fast for users
- Scales to many concurrent uploads
- Reduces server load

**When to Use Server-Proxied:**
- Very small files (under 100 KB)
- When you must process files before storing
- When cloud storage is not available

**Our Implementation:**
The code example uses direct uploads. The `getUploadUrl` endpoint returns a signed URL, and the client uploads directly to S3/GCS.

