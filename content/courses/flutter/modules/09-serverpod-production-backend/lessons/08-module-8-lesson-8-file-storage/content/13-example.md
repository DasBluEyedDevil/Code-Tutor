---
type: "EXAMPLE"
title: "Image Thumbnails and Processing"
---

For images, generating thumbnails improves performance and saves bandwidth. Here is how to implement server-side image processing.



```dart
// File: lib/src/services/image_processing_service.dart

import 'dart:typed_data';
import 'package:image/image.dart' as img;
import 'package:serverpod/serverpod.dart';

/// Service for processing and optimizing images.
class ImageProcessingService {
  
  /// Generate thumbnails for an uploaded image.
  /// Creates multiple sizes for different use cases.
  static Future<Map<String, String>> generateThumbnails(
    Session session, {
    required String sourcePath,
    required String storageId,
  }) async {
    // Download original image from storage
    final originalBytes = await session.storage.retrieveFile(
      storageId: storageId,
      path: sourcePath,
    );
    
    if (originalBytes == null) {
      throw Exception('Original image not found');
    }
    
    // Decode image
    final originalImage = img.decodeImage(originalBytes);
    if (originalImage == null) {
      throw Exception('Failed to decode image');
    }
    
    // Define thumbnail sizes
    final sizes = {
      'thumb': 100,   // Small thumbnail for lists
      'medium': 300,  // Medium for previews
      'large': 800,   // Large for detail views
    };
    
    final thumbnailPaths = <String, String>{};
    final basePath = sourcePath.replaceAll(RegExp(r'\.[^.]+$'), '');
    final extension = sourcePath.split('.').last;
    
    for (final entry in sizes.entries) {
      final sizeName = entry.key;
      final maxDimension = entry.value;
      
      // Resize image (maintaining aspect ratio)
      final resized = _resizeImage(originalImage, maxDimension);
      
      // Encode as JPEG with quality optimization
      final encodedBytes = img.encodeJpg(resized, quality: 85);
      
      // Create storage path for thumbnail
      final thumbPath = '${basePath}_$sizeName.jpg';
      
      // Store thumbnail
      await session.storage.storeFile(
        storageId: storageId,
        path: thumbPath,
        byteData: ByteData.view(Uint8List.fromList(encodedBytes).buffer),
      );
      
      thumbnailPaths[sizeName] = thumbPath;
    }
    
    return thumbnailPaths;
  }
  
  /// Resize image maintaining aspect ratio.
  static img.Image _resizeImage(img.Image image, int maxDimension) {
    final width = image.width;
    final height = image.height;
    
    // Calculate new dimensions
    int newWidth, newHeight;
    
    if (width > height) {
      // Landscape: limit by width
      newWidth = maxDimension;
      newHeight = (height * maxDimension / width).round();
    } else {
      // Portrait: limit by height
      newHeight = maxDimension;
      newWidth = (width * maxDimension / height).round();
    }
    
    return img.copyResize(
      image,
      width: newWidth,
      height: newHeight,
      interpolation: img.Interpolation.linear,
    );
  }
  
  /// Validate and optimize an uploaded image.
  /// Returns the optimized image bytes.
  static Future<Uint8List> optimizeImage(
    Uint8List imageBytes, {
    int maxWidth = 2000,
    int maxHeight = 2000,
    int quality = 85,
  }) async {
    // Decode image
    final image = img.decodeImage(imageBytes);
    if (image == null) {
      throw Exception('Invalid image format');
    }
    
    // Check if resize needed
    img.Image processed = image;
    
    if (image.width > maxWidth || image.height > maxHeight) {
      // Calculate scale factor
      final scale = (image.width / maxWidth > image.height / maxHeight)
          ? maxWidth / image.width
          : maxHeight / image.height;
      
      processed = img.copyResize(
        image,
        width: (image.width * scale).round(),
        height: (image.height * scale).round(),
      );
    }
    
    // Auto-orient based on EXIF data
    processed = img.bakeOrientation(processed);
    
    // Encode as JPEG
    return Uint8List.fromList(img.encodeJpg(processed, quality: quality));
  }
  
  /// Strip EXIF metadata for privacy.
  static img.Image stripExifData(img.Image image) {
    // Create a new image without metadata
    return img.Image.from(image);
  }
  
  /// Get image dimensions without fully decoding.
  static Map<String, int>? getImageDimensions(Uint8List bytes) {
    final decoder = img.findDecoderForData(bytes);
    if (decoder == null) return null;
    
    final image = decoder.decode(bytes);
    if (image == null) return null;
    
    return {
      'width': image.width,
      'height': image.height,
    };
  }
}

// Usage in your endpoint:
class ImageEndpoint extends Endpoint {
  /// Process an uploaded image and generate thumbnails.
  Future<ImageInfo> processUploadedImage(
    Session session, {
    required int fileId,
  }) async {
    final fileRecord = await FileRecord.db.findById(session, fileId);
    if (fileRecord == null) {
      throw FileNotFoundException('File not found');
    }
    
    // Generate thumbnails
    final thumbnails = await ImageProcessingService.generateThumbnails(
      session,
      sourcePath: fileRecord.storagePath,
      storageId: 'public',
    );
    
    // Update file record with thumbnail paths
    final updatedRecord = fileRecord.copyWith(
      thumbnailSmall: thumbnails['thumb'],
      thumbnailMedium: thumbnails['medium'],
      thumbnailLarge: thumbnails['large'],
    );
    
    await FileRecord.db.updateRow(session, updatedRecord);
    
    return ImageInfo(
      originalPath: fileRecord.storagePath,
      thumbnails: thumbnails,
    );
  }
}
```
