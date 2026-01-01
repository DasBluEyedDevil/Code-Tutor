---
type: "EXAMPLE"
title: "File Storage Configuration"
---


**Setting Up Serverpod Cloud Storage**

Serverpod supports multiple storage backends through its unified storage API. We'll configure S3-compatible storage that works with AWS S3, Cloudflare R2, or MinIO for local development.



```yaml
# config/development.yaml
# Local development storage configuration

apiServer:
  port: 8080
  publicHost: localhost
  publicPort: 8080
  publicScheme: http

insightsServer:
  port: 8081
  publicHost: localhost
  publicPort: 8081
  publicScheme: http

database:
  host: localhost
  port: 5432
  name: social_app
  user: postgres

redis:
  enabled: false
  host: localhost
  port: 6379

# Storage configuration for development (MinIO)
storage:
  public:
    type: s3
    bucket: social-app-public
    region: us-east-1
    endpoint: http://localhost:9000
    accessKeyId: minioadmin
    secretAccessKey: minioadmin
    publicHost: http://localhost:9000/social-app-public
  private:
    type: s3
    bucket: social-app-private
    region: us-east-1
    endpoint: http://localhost:9000
    accessKeyId: minioadmin
    secretAccessKey: minioadmin

---

# config/production.yaml
# Production storage with Cloudflare R2

apiServer:
  port: 8080
  publicHost: api.socialapp.com
  publicPort: 443
  publicScheme: https

database:
  host: ${DB_HOST}
  port: 5432
  name: ${DB_NAME}
  user: ${DB_USER}

redis:
  enabled: true
  host: ${REDIS_HOST}
  port: 6379

storage:
  public:
    type: s3
    bucket: ${R2_PUBLIC_BUCKET}
    region: auto
    endpoint: https://${R2_ACCOUNT_ID}.r2.cloudflarestorage.com
    accessKeyId: ${R2_ACCESS_KEY_ID}
    secretAccessKey: ${R2_SECRET_ACCESS_KEY}
    # CDN URL for public files
    publicHost: https://media.socialapp.com
  private:
    type: s3
    bucket: ${R2_PRIVATE_BUCKET}
    region: auto
    endpoint: https://${R2_ACCOUNT_ID}.r2.cloudflarestorage.com
    accessKeyId: ${R2_ACCESS_KEY_ID}
    secretAccessKey: ${R2_SECRET_ACCESS_KEY}

---

// server/lib/src/util/storage_config.dart
import 'package:serverpod/serverpod.dart';

/// Storage utility for managing file uploads
class StorageConfig {
  /// Storage identifier for public media files (images, videos)
  static const String publicStorage = 'public';
  
  /// Storage identifier for private files (documents, backups)
  static const String privateStorage = 'private';
  
  /// Maximum file sizes by type (in bytes)
  static const Map<String, int> maxFileSizes = {
    'image': 10 * 1024 * 1024,    // 10 MB
    'video': 100 * 1024 * 1024,   // 100 MB
    'audio': 20 * 1024 * 1024,    // 20 MB
    'document': 25 * 1024 * 1024, // 25 MB
  };
  
  /// Allowed MIME types by category
  static const Map<String, List<String>> allowedMimeTypes = {
    'image': [
      'image/jpeg',
      'image/png',
      'image/gif',
      'image/webp',
      'image/heic',
      'image/heif',
    ],
    'video': [
      'video/mp4',
      'video/quicktime',
      'video/webm',
      'video/x-msvideo',
    ],
    'audio': [
      'audio/mpeg',
      'audio/wav',
      'audio/ogg',
      'audio/aac',
    ],
  };
  
  /// Get the storage path for a media file
  static String getMediaPath({
    required String contentType,
    required int userId,
    required String fileId,
    required String extension,
  }) {
    final now = DateTime.now();
    final yearMonth = '${now.year}-${now.month.toString().padLeft(2, '0')}';
    final category = _getCategoryFromMimeType(contentType);
    
    return '$category/user_$userId/$yearMonth/${fileId}.$extension';
  }
  
  /// Get thumbnail path from original path
  static String getThumbnailPath(String originalPath, String size) {
    final lastDot = originalPath.lastIndexOf('.');
    if (lastDot == -1) return '${originalPath}_$size';
    
    return '${originalPath.substring(0, lastDot)}_$size${originalPath.substring(lastDot)}';
  }
  
  /// Validate file size
  static bool isValidFileSize(String category, int sizeBytes) {
    final maxSize = maxFileSizes[category];
    if (maxSize == null) return false;
    return sizeBytes <= maxSize;
  }
  
  /// Validate MIME type
  static bool isValidMimeType(String mimeType) {
    return allowedMimeTypes.values.any((types) => types.contains(mimeType));
  }
  
  /// Get category from MIME type
  static String _getCategoryFromMimeType(String mimeType) {
    for (final entry in allowedMimeTypes.entries) {
      if (entry.value.contains(mimeType)) {
        return entry.key;
      }
    }
    return 'other';
  }
  
  /// Get file extension from MIME type
  static String getExtensionFromMimeType(String mimeType) {
    const mimeToExt = {
      'image/jpeg': 'jpg',
      'image/png': 'png',
      'image/gif': 'gif',
      'image/webp': 'webp',
      'image/heic': 'heic',
      'image/heif': 'heif',
      'video/mp4': 'mp4',
      'video/quicktime': 'mov',
      'video/webm': 'webm',
      'audio/mpeg': 'mp3',
      'audio/wav': 'wav',
    };
    return mimeToExt[mimeType] ?? 'bin';
  }
  
  /// Generate signed URL expiration (in seconds)
  static const int uploadUrlExpiration = 3600;  // 1 hour
  static const int downloadUrlExpiration = 86400;  // 24 hours
}

---

// docker-compose.yml (for local MinIO)
version: '3.8'

services:
  minio:
    image: minio/minio:latest
    command: server /data --console-address ":9001"
    ports:
      - "9000:9000"   # API
      - "9001:9001"   # Console
    environment:
      MINIO_ROOT_USER: minioadmin
      MINIO_ROOT_PASSWORD: minioadmin
    volumes:
      - minio_data:/data
    healthcheck:
      test: ["CMD", "curl", "-f", "http://localhost:9000/minio/health/live"]
      interval: 30s
      timeout: 20s
      retries: 3

  # Create buckets on startup
  minio-init:
    image: minio/mc:latest
    depends_on:
      minio:
        condition: service_healthy
    entrypoint: >
      /bin/sh -c "
      mc alias set local http://minio:9000 minioadmin minioadmin;
      mc mb local/social-app-public --ignore-existing;
      mc mb local/social-app-private --ignore-existing;
      mc anonymous set download local/social-app-public;
      "

volumes:
  minio_data:
```
