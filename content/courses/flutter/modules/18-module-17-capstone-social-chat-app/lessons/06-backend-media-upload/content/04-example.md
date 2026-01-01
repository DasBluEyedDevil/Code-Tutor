---
type: "EXAMPLE"
title: "Thumbnail Generation"
---


**Server-Side Image Processing**

Thumbnails improve loading performance and reduce bandwidth. We'll generate multiple sizes for different use cases.



```dart
// server/pubspec.yaml additions
dependencies:
  image: ^4.1.0  # Pure Dart image processing

---

// server/lib/src/services/image_processor_service.dart
import 'dart:typed_data';
import 'package:image/image.dart' as img;
import 'package:serverpod/serverpod.dart';
import '../generated/protocol.dart';
import '../util/storage_config.dart';

/// Service for processing images and generating thumbnails
class ImageProcessorService {
  /// Thumbnail size configurations
  static const Map<String, ThumbnailConfig> thumbnailSizes = {
    'thumb': ThumbnailConfig(width: 150, height: 150, quality: 80),
    'medium': ThumbnailConfig(width: 600, height: 600, quality: 85),
    'large': ThumbnailConfig(width: 1200, height: 1200, quality: 90),
  };

  /// Process uploaded image and generate thumbnails
  Future<ImageProcessingResult> processImage(
    Session session, {
    required int mediaFileId,
  }) async {
    // Get media record
    final mediaFile = await MediaFile.db.findById(session, mediaFileId);
    if (mediaFile == null) {
      throw ImageProcessingException(
        code: 'not_found',
        message: 'Media file not found',
      );
    }
    
    try {
      // Download original image from storage
      final imageBytes = await session.storage.retrieveFile(
        storageId: StorageConfig.publicStorage,
        path: mediaFile.storageKey,
      );
      
      if (imageBytes == null) {
        throw ImageProcessingException(
          code: 'file_not_found',
          message: 'Original file not found in storage',
        );
      }
      
      // Decode image
      final originalImage = img.decodeImage(
        Uint8List.view(imageBytes.buffer),
      );
      
      if (originalImage == null) {
        throw ImageProcessingException(
          code: 'decode_failed',
          message: 'Failed to decode image',
        );
      }
      
      // Strip EXIF data for privacy
      originalImage.exif.clear();
      
      // Get original dimensions
      final originalWidth = originalImage.width;
      final originalHeight = originalImage.height;
      
      // Generate thumbnails
      final thumbnailUrls = <String, String>{};
      
      for (final entry in thumbnailSizes.entries) {
        final sizeName = entry.key;
        final config = entry.value;
        
        // Skip if original is smaller than target
        if (originalWidth <= config.width && originalHeight <= config.height) {
          // Use original as thumbnail
          thumbnailUrls[sizeName] = mediaFile.publicUrl ?? '';
          continue;
        }
        
        // Resize image maintaining aspect ratio
        final resized = _resizeImage(
          originalImage,
          config.width,
          config.height,
        );
        
        // Encode to JPEG for consistent format and smaller size
        final thumbnailBytes = img.encodeJpg(
          resized,
          quality: config.quality,
        );
        
        // Generate thumbnail path
        final thumbnailKey = StorageConfig.getThumbnailPath(
          mediaFile.storageKey,
          sizeName,
        ).replaceAll(RegExp(r'\.[^.]+$'), '.jpg');
        
        // Upload thumbnail
        await session.storage.storeFile(
          storageId: StorageConfig.publicStorage,
          path: thumbnailKey,
          byteData: ByteData.view(Uint8List.fromList(thumbnailBytes).buffer),
        );
        
        // Get public URL
        final thumbnailUrl = await session.storage.getPublicUrl(
          storageId: StorageConfig.publicStorage,
          path: thumbnailKey,
        );
        
        thumbnailUrls[sizeName] = thumbnailUrl?.toString() ?? '';
      }
      
      // Update media record with dimensions and thumbnail URLs
      final updatedMedia = await MediaFile.db.updateRow(
        session,
        mediaFile.copyWith(
          width: originalWidth,
          height: originalHeight,
          thumbnailUrl: thumbnailUrls['thumb'],
          mediumUrl: thumbnailUrls['medium'],
          status: 'ready',
          processedAt: DateTime.now(),
        ),
      );
      
      return ImageProcessingResult(
        mediaFile: updatedMedia,
        originalWidth: originalWidth,
        originalHeight: originalHeight,
        thumbnailUrls: thumbnailUrls,
      );
    } catch (e) {
      // Update status to failed
      await MediaFile.db.updateRow(
        session,
        mediaFile.copyWith(
          status: 'failed',
          processingError: e.toString(),
        ),
      );
      
      rethrow;
    }
  }

  /// Resize image maintaining aspect ratio
  img.Image _resizeImage(
    img.Image source,
    int maxWidth,
    int maxHeight,
  ) {
    // Calculate target dimensions maintaining aspect ratio
    double ratio = source.width / source.height;
    int targetWidth, targetHeight;
    
    if (ratio > 1) {
      // Landscape
      targetWidth = maxWidth;
      targetHeight = (maxWidth / ratio).round();
    } else {
      // Portrait or square
      targetHeight = maxHeight;
      targetWidth = (maxHeight * ratio).round();
    }
    
    // Use high-quality interpolation
    return img.copyResize(
      source,
      width: targetWidth,
      height: targetHeight,
      interpolation: img.Interpolation.cubic,
    );
  }

  /// Crop image to exact dimensions (for profile pictures)
  Future<ImageProcessingResult> processProfilePicture(
    Session session, {
    required int mediaFileId,
    int? cropX,
    int? cropY,
    int? cropWidth,
    int? cropHeight,
  }) async {
    final mediaFile = await MediaFile.db.findById(session, mediaFileId);
    if (mediaFile == null) {
      throw ImageProcessingException(
        code: 'not_found',
        message: 'Media file not found',
      );
    }
    
    // Download original
    final imageBytes = await session.storage.retrieveFile(
      storageId: StorageConfig.publicStorage,
      path: mediaFile.storageKey,
    );
    
    if (imageBytes == null) {
      throw ImageProcessingException(
        code: 'file_not_found',
        message: 'Original file not found',
      );
    }
    
    var image = img.decodeImage(Uint8List.view(imageBytes.buffer));
    if (image == null) {
      throw ImageProcessingException(
        code: 'decode_failed',
        message: 'Failed to decode image',
      );
    }
    
    // Apply crop if specified
    if (cropX != null && cropY != null && 
        cropWidth != null && cropHeight != null) {
      image = img.copyCrop(
        image,
        x: cropX,
        y: cropY,
        width: cropWidth,
        height: cropHeight,
      );
    }
    
    // Strip EXIF
    image.exif.clear();
    
    // Generate profile picture sizes
    const profileSizes = {
      'small': 64,
      'medium': 128,
      'large': 256,
      'xlarge': 512,
    };
    
    final thumbnailUrls = <String, String>{};
    
    for (final entry in profileSizes.entries) {
      final sizeName = entry.key;
      final size = entry.value;
      
      // Resize to square
      final resized = img.copyResizeCropSquare(image, size: size);
      
      // Encode
      final bytes = img.encodeJpg(resized, quality: 90);
      
      // Upload
      final key = StorageConfig.getThumbnailPath(
        mediaFile.storageKey,
        'profile_$sizeName',
      ).replaceAll(RegExp(r'\.[^.]+$'), '.jpg');
      
      await session.storage.storeFile(
        storageId: StorageConfig.publicStorage,
        path: key,
        byteData: ByteData.view(Uint8List.fromList(bytes).buffer),
      );
      
      final url = await session.storage.getPublicUrl(
        storageId: StorageConfig.publicStorage,
        path: key,
      );
      
      thumbnailUrls[sizeName] = url?.toString() ?? '';
    }
    
    // Update record
    final updated = await MediaFile.db.updateRow(
      session,
      mediaFile.copyWith(
        width: image.width,
        height: image.height,
        thumbnailUrl: thumbnailUrls['small'],
        mediumUrl: thumbnailUrls['medium'],
        status: 'ready',
        processedAt: DateTime.now(),
        metadata: jsonEncode({
          'profileSizes': thumbnailUrls,
          'cropped': cropX != null,
        }),
      ),
    );
    
    return ImageProcessingResult(
      mediaFile: updated,
      originalWidth: image.width,
      originalHeight: image.height,
      thumbnailUrls: thumbnailUrls,
    );
  }

  /// Clean up files when media is deleted
  Future<void> deleteMediaFiles(
    Session session, {
    required int mediaFileId,
  }) async {
    final mediaFile = await MediaFile.db.findById(session, mediaFileId);
    if (mediaFile == null) return;
    
    // Delete original
    await session.storage.deleteFile(
      storageId: StorageConfig.publicStorage,
      path: mediaFile.storageKey,
    );
    
    // Delete all thumbnails
    for (final sizeName in thumbnailSizes.keys) {
      final thumbKey = StorageConfig.getThumbnailPath(
        mediaFile.storageKey,
        sizeName,
      ).replaceAll(RegExp(r'\.[^.]+$'), '.jpg');
      
      try {
        await session.storage.deleteFile(
          storageId: StorageConfig.publicStorage,
          path: thumbKey,
        );
      } catch (e) {
        // Ignore errors for missing thumbnails
      }
    }
    
    // Hard delete the record
    await MediaFile.db.deleteRow(session, mediaFile);
  }
}

/// Thumbnail configuration
class ThumbnailConfig {
  final int width;
  final int height;
  final int quality;
  
  const ThumbnailConfig({
    required this.width,
    required this.height,
    required this.quality,
  });
}

/// Result of image processing
class ImageProcessingResult {
  final MediaFile mediaFile;
  final int originalWidth;
  final int originalHeight;
  final Map<String, String> thumbnailUrls;
  
  ImageProcessingResult({
    required this.mediaFile,
    required this.originalWidth,
    required this.originalHeight,
    required this.thumbnailUrls,
  });
}

class ImageProcessingException implements Exception {
  final String code;
  final String message;
  
  ImageProcessingException({required this.code, required this.message});
  
  @override
  String toString() => 'ImageProcessingException: [$code] $message';
}
```
