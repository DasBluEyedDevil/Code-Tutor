// lib/features/posts/data/offline_post_queue.dart
import 'dart:async';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../../../core/database/app_database.dart';

final offlinePostQueueProvider = Provider<OfflinePostQueue>((ref) {
  return OfflinePostQueue(ref);
});

class OfflinePostQueue {
  final Ref _ref;

  OfflinePostQueue(this._ref);

  // Queue a new post for upload
  Future<String> queuePost({
    required String content,
    List<String>? localImagePaths,
  }) async {
    // TODO: Create local post entry with pending status
    // TODO: Queue image uploads if present
    // TODO: Return local post ID
    throw UnimplementedError();
  }

  // Process pending posts when online
  Future<void> processPendingPosts() async {
    // TODO: Get pending posts from database
    // TODO: Upload images first
    // TODO: Create post on server with image URLs
    // TODO: Update local post with server ID
    throw UnimplementedError();
  }
}

// lib/features/posts/data/image_upload_service.dart
class ImageUploadService {
  // Upload images with progress tracking
  Stream<ImageUploadProgress> uploadImages(List<String> paths) {
    // TODO: Upload each image
    // TODO: Yield progress updates
    // TODO: Handle individual failures
    throw UnimplementedError();
  }

  // Retry failed uploads with exponential backoff
  Future<String> uploadWithRetry(String path, {int maxRetries = 3}) async {
    // TODO: Implement retry logic with backoff
    throw UnimplementedError();
  }
}

class ImageUploadProgress {
  final int current;
  final int total;
  final double currentProgress;
  final String? currentPath;
  final String? error;
  final List<String> completedUrls;

  ImageUploadProgress({
    required this.current,
    required this.total,
    this.currentProgress = 0,
    this.currentPath,
    this.error,
    this.completedUrls = const [],
  });
}