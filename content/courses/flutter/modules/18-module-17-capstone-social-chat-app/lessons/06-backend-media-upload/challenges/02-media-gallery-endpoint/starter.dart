// server/lib/src/endpoints/media_gallery_endpoint.dart
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';

class MediaGalleryEndpoint extends Endpoint {
  /// Get user's media gallery with filters
  Future<MediaGalleryResponse> getGallery(
    Session session, {
    String? mediaType,        // Filter by type: 'image', 'video', 'audio'
    DateTime? fromDate,       // Filter by date range
    DateTime? toDate,
    String? sortBy,           // 'date' or 'size'
    bool sortDescending = true,
    String? cursor,
    int limit = 20,
  }) async {
    // TODO: Implement
    // 1. Authenticate user
    // 2. Build query with filters
    // 3. Apply sorting
    // 4. Execute paginated query
    // 5. Return results with metadata
    throw UnimplementedError();
  }

  /// Get counts by media type for the user
  Future<MediaTypeCounts> getMediaCounts(
    Session session,
  ) async {
    // TODO: Implement
    // Return counts for each media type
    throw UnimplementedError();
  }

  /// Get storage usage statistics
  Future<StorageUsage> getStorageUsage(
    Session session,
  ) async {
    // TODO: Implement
    // Return storage usage breakdown by type
    throw UnimplementedError();
  }
}

/// Gallery response with pagination
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

/// Gallery item with display info
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
}