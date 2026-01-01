// server/lib/src/endpoints/media_gallery_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class MediaGalleryEndpoint extends Endpoint {
  /// Get user's media gallery with filters
  Future<MediaGalleryResponse> getGallery(
    Session session, {
    String? mediaType,
    DateTime? fromDate,
    DateTime? toDate,
    String? sortBy,
    bool sortDescending = true,
    String? cursor,
    int limit = 20,
  }) async {
    // 1. Authenticate
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw GalleryException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      return MediaGalleryResponse(
        items: [],
        hasMore: false,
        totalCount: 0,
      );
    }
    
    // Validate inputs
    if (limit < 1 || limit > 100) {
      limit = 20;
    }
    
    final validMediaTypes = {'image', 'video', 'audio'};
    if (mediaType != null && !validMediaTypes.contains(mediaType)) {
      throw GalleryException(
        code: 'invalid_type',
        message: 'Invalid media type. Use: image, video, or audio',
      );
    }
    
    final validSortFields = {'date', 'size'};
    sortBy = validSortFields.contains(sortBy) ? sortBy : 'date';
    
    // 2. Build base query
    var whereClause = MediaFile.t.userId.equals(user.id!) &
        MediaFile.t.deletedAt.equals(null) &
        MediaFile.t.status.equals('ready');
    
    // Apply media type filter
    if (mediaType != null) {
      whereClause = whereClause & MediaFile.t.mediaType.equals(mediaType);
    }
    
    // Apply date range filters
    if (fromDate != null) {
      whereClause = whereClause & 
          MediaFile.t.createdAt.greaterOrEqualThan(fromDate);
    }
    
    if (toDate != null) {
      whereClause = whereClause & 
          MediaFile.t.createdAt.lessOrEqualThan(toDate);
    }
    
    // Parse cursor for pagination
    if (cursor != null) {
      final cursorData = _parseCursor(cursor);
      if (cursorData != null) {
        if (sortBy == 'size') {
          if (sortDescending) {
            whereClause = whereClause & (
              MediaFile.t.fileSize.lessThan(cursorData.value) |
              (MediaFile.t.fileSize.equals(cursorData.value) & 
               MediaFile.t.id.lessThan(cursorData.id))
            );
          } else {
            whereClause = whereClause & (
              MediaFile.t.fileSize.greaterThan(cursorData.value) |
              (MediaFile.t.fileSize.equals(cursorData.value) & 
               MediaFile.t.id.greaterThan(cursorData.id))
            );
          }
        } else {
          // Date sorting
          final cursorDate = DateTime.fromMillisecondsSinceEpoch(
            cursorData.value,
          );
          if (sortDescending) {
            whereClause = whereClause & (
              MediaFile.t.createdAt.lessThan(cursorDate) |
              (MediaFile.t.createdAt.equals(cursorDate) & 
               MediaFile.t.id.lessThan(cursorData.id))
            );
          } else {
            whereClause = whereClause & (
              MediaFile.t.createdAt.greaterThan(cursorDate) |
              (MediaFile.t.createdAt.equals(cursorDate) & 
               MediaFile.t.id.greaterThan(cursorData.id))
            );
          }
        }
      }
    }
    
    // 3. Get total count (without pagination)
    final totalCount = await MediaFile.db.count(
      session,
      where: (t) => MediaFile.t.userId.equals(user.id!) &
          MediaFile.t.deletedAt.equals(null) &
          MediaFile.t.status.equals('ready') &
          (mediaType != null 
              ? MediaFile.t.mediaType.equals(mediaType) 
              : MediaFile.t.id.notEquals(-1)) &
          (fromDate != null 
              ? MediaFile.t.createdAt.greaterOrEqualThan(fromDate) 
              : MediaFile.t.id.notEquals(-1)) &
          (toDate != null 
              ? MediaFile.t.createdAt.lessOrEqualThan(toDate) 
              : MediaFile.t.id.notEquals(-1)),
    );
    
    // 4. Execute query with sorting
    final media = await MediaFile.db.find(
      session,
      where: (t) => whereClause,
      orderBy: (t) => sortBy == 'size' ? t.fileSize : t.createdAt,
      orderDescending: sortDescending,
      limit: limit + 1,
    );
    
    // 5. Process results
    final hasMore = media.length > limit;
    final items = hasMore ? media.take(limit).toList() : media;
    
    final galleryItems = items.map((m) => MediaGalleryItem(
      id: m.id!,
      mediaType: m.mediaType,
      thumbnailUrl: m.thumbnailUrl ?? m.publicUrl ?? '',
      publicUrl: m.publicUrl,
      fileSize: m.fileSize,
      width: m.width,
      height: m.height,
      duration: m.duration,
      createdAt: m.createdAt,
    )).toList();
    
    // Generate next cursor
    String? nextCursor;
    if (hasMore && items.isNotEmpty) {
      final lastItem = items.last;
      final cursorValue = sortBy == 'size' 
          ? lastItem.fileSize 
          : lastItem.createdAt.millisecondsSinceEpoch;
      nextCursor = _encodeCursor(lastItem.id!, cursorValue);
    }
    
    return MediaGalleryResponse(
      items: galleryItems,
      nextCursor: nextCursor,
      hasMore: hasMore,
      totalCount: totalCount,
    );
  }

  /// Get counts by media type
  Future<MediaTypeCounts> getMediaCounts(
    Session session,
  ) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw GalleryException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      return MediaTypeCounts(
        images: 0,
        videos: 0,
        audio: 0,
        total: 0,
      );
    }
    
    // Query counts by type
    final result = await session.db.unsafeQuery(
      '''
      SELECT 
        media_type,
        COUNT(*) as count
      FROM media_files
      WHERE user_id = @userId 
        AND deleted_at IS NULL 
        AND status = 'ready'
      GROUP BY media_type
      ''',
      parameters: QueryParameters.named({'userId': user.id}),
    );
    
    int images = 0, videos = 0, audio = 0;
    
    for (final row in result) {
      final type = row['media_type'] as String;
      final count = row['count'] as int;
      
      switch (type) {
        case 'image':
          images = count;
          break;
        case 'video':
          videos = count;
          break;
        case 'audio':
          audio = count;
          break;
      }
    }
    
    return MediaTypeCounts(
      images: images,
      videos: videos,
      audio: audio,
      total: images + videos + audio,
    );
  }

  /// Get storage usage statistics
  Future<StorageUsage> getStorageUsage(
    Session session,
  ) async {
    final userId = await session.auth.authenticatedUserId;
    if (userId == null) {
      throw GalleryException(
        code: 'unauthenticated',
        message: 'Please log in',
      );
    }
    
    final user = await UserProfile.db.findFirstRow(
      session,
      where: (t) => t.userInfoId.equals(userId),
    );
    
    if (user == null) {
      return StorageUsage(
        usedBytes: 0,
        usedByType: {},
        limitBytes: 1024 * 1024 * 1024, // 1 GB default
        percentUsed: 0,
      );
    }
    
    // Query storage by type
    final result = await session.db.unsafeQuery(
      '''
      SELECT 
        media_type,
        COALESCE(SUM(file_size), 0) as total_size,
        COUNT(*) as count
      FROM media_files
      WHERE user_id = @userId 
        AND deleted_at IS NULL
      GROUP BY media_type
      ''',
      parameters: QueryParameters.named({'userId': user.id}),
    );
    
    final usedByType = <String, int>{};
    int totalUsed = 0;
    
    for (final row in result) {
      final type = row['media_type'] as String;
      final size = row['total_size'] as int;
      usedByType[type] = size;
      totalUsed += size;
    }
    
    // Get user's storage limit (could be from subscription tier)
    const storageLimit = 5 * 1024 * 1024 * 1024; // 5 GB
    final percentUsed = (totalUsed / storageLimit * 100).clamp(0, 100);
    
    return StorageUsage(
      usedBytes: totalUsed,
      usedByType: usedByType,
      limitBytes: storageLimit,
      percentUsed: percentUsed,
    );
  }
  
  // Cursor encoding/decoding helpers
  
  String _encodeCursor(int id, int value) {
    return '${id}_$value';
  }
  
  CursorData? _parseCursor(String cursor) {
    final parts = cursor.split('_');
    if (parts.length != 2) return null;
    
    final id = int.tryParse(parts[0]);
    final value = int.tryParse(parts[1]);
    
    if (id == null || value == null) return null;
    
    return CursorData(id: id, value: value);
  }
}

class CursorData {
  final int id;
  final int value;
  
  CursorData({required this.id, required this.value});
}

class MediaGalleryResponse {
  final List<MediaGalleryItem> items;
  final String? nextCursor;
  final bool hasMore;
  final int totalCount;
  
  MediaGalleryResponse({
    required this.items,
    this.nextCursor,
    required this.hasMore,
    required this.totalCount,
  });
}

class MediaGalleryItem {
  final int id;
  final String mediaType;
  final String thumbnailUrl;
  final String? publicUrl;
  final int fileSize;
  final int? width;
  final int? height;
  final int? duration;
  final DateTime createdAt;
  
  MediaGalleryItem({
    required this.id,
    required this.mediaType,
    required this.thumbnailUrl,
    this.publicUrl,
    required this.fileSize,
    this.width,
    this.height,
    this.duration,
    required this.createdAt,
  });
  
  String get fileSizeDisplay {
    if (fileSize < 1024) return '$fileSize B';
    if (fileSize < 1024 * 1024) return '${(fileSize / 1024).toStringAsFixed(1)} KB';
    return '${(fileSize / (1024 * 1024)).toStringAsFixed(1)} MB';
  }
}

class MediaTypeCounts {
  final int images;
  final int videos;
  final int audio;
  final int total;
  
  MediaTypeCounts({
    required this.images,
    required this.videos,
    required this.audio,
    required this.total,
  });
}

class StorageUsage {
  final int usedBytes;
  final Map<String, int> usedByType;
  final int limitBytes;
  final double percentUsed;
  
  StorageUsage({
    required this.usedBytes,
    required this.usedByType,
    required this.limitBytes,
    required this.percentUsed,
  });
  
  String get usedDisplay {
    if (usedBytes < 1024 * 1024) {
      return '${(usedBytes / 1024).toStringAsFixed(1)} KB';
    }
    if (usedBytes < 1024 * 1024 * 1024) {
      return '${(usedBytes / (1024 * 1024)).toStringAsFixed(1)} MB';
    }
    return '${(usedBytes / (1024 * 1024 * 1024)).toStringAsFixed(2)} GB';
  }
  
  String get limitDisplay {
    return '${(limitBytes / (1024 * 1024 * 1024)).toStringAsFixed(0)} GB';
  }
  
  int get remainingBytes => limitBytes - usedBytes;
}

class GalleryException implements Exception {
  final String code;
  final String message;
  
  GalleryException({required this.code, required this.message});
  
  @override
  String toString() => 'GalleryException: [$code] $message';
}