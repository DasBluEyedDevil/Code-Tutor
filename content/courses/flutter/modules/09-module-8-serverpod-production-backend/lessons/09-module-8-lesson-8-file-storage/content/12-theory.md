---
type: "THEORY"
title: "Downloading and Serving Files"
---

After files are uploaded, users need to access them. The download approach depends on whether files are public or private.

**Public Files (Avatars, Public Assets):**

Public files have permanent, stable URLs:
```dart
// Server-side: get public URL
final publicUrl = session.storage.getPublicUrl(
  storageId: 'public',
  path: 'avatars/user_123/avatar.png',
);
// Returns: https://myapp-public.s3.amazonaws.com/avatars/user_123/avatar.png

// Flutter-side: display directly
Image.network(user.avatarUrl)
```

**Private Files (Documents, Sensitive Media):**

Private files require signed URLs with expiration:
```dart
// Server-side: create signed URL
final signedUrl = await session.storage.createDirectFileDownloadUrl(
  storageId: 'private',
  path: fileRecord.storagePath,
  expiration: Duration(hours: 1),
);
// Returns: https://myapp-private.s3.amazonaws.com/docs/...?X-Amz-Signature=...

// Flutter-side: use signed URL (expires in 1 hour)
await launchUrl(Uri.parse(signedUrl));
```

**Caching Considerations:**

Public URLs can be cached indefinitely by CDNs and browsers. For private URLs:
- Cache in app only for the signed URL duration
- Re-fetch URL when it approaches expiration
- Consider caching file content locally for offline access

**Download Implementation:**

```dart
// Flutter-side: download file with progress
Future<File> downloadFile(String url, String localPath) async {
  final response = await http.Client().send(
    http.Request('GET', Uri.parse(url)),
  );
  
  final file = File(localPath);
  final sink = file.openWrite();
  
  int received = 0;
  final total = response.contentLength ?? 0;
  
  await response.stream.listen(
    (chunk) {
      sink.add(chunk);
      received += chunk.length;
      print('Progress: ${received / total * 100}%');
    },
  ).asFuture();
  
  await sink.close();
  return file;
}
```

