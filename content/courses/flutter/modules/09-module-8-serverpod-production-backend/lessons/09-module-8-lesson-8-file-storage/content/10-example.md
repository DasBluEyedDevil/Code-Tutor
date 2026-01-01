---
type: "EXAMPLE"
title: "Flutter Client: Uploading Files"
---

Now let us implement the Flutter client that uploads files using the signed URL approach.



```dart
// File: lib/services/file_upload_service.dart

import 'dart:io';
import 'dart:typed_data';
import 'package:http/http.dart' as http;
import 'package:image_picker/image_picker.dart';
import 'package:file_picker/file_picker.dart';
import 'package:mime/mime.dart';
import 'package:my_app_client/my_app_client.dart';

/// Service for handling file uploads to the server.
class FileUploadService {
  final Client _client;
  final _picker = ImagePicker();
  
  FileUploadService(this._client);
  
  /// Pick and upload a profile avatar image.
  Future<FileRecord?> uploadAvatar() async {
    // Pick image from gallery
    final XFile? image = await _picker.pickImage(
      source: ImageSource.gallery,
      maxWidth: 500, // Resize to save bandwidth
      maxHeight: 500,
      imageQuality: 85, // Compress slightly
    );
    
    if (image == null) return null; // User cancelled
    
    return _uploadFile(
      file: File(image.path),
      category: 'avatar',
    );
  }
  
  /// Pick and upload from camera.
  Future<FileRecord?> uploadFromCamera() async {
    final XFile? image = await _picker.pickImage(
      source: ImageSource.camera,
      maxWidth: 1024,
      maxHeight: 1024,
      imageQuality: 85,
    );
    
    if (image == null) return null;
    
    return _uploadFile(
      file: File(image.path),
      category: 'media',
    );
  }
  
  /// Pick and upload a document.
  Future<FileRecord?> uploadDocument() async {
    final result = await FilePicker.platform.pickFiles(
      type: FileType.custom,
      allowedExtensions: ['pdf', 'doc', 'docx'],
      withData: false, // We will read the file ourselves
    );
    
    if (result == null || result.files.isEmpty) return null;
    
    final platformFile = result.files.first;
    if (platformFile.path == null) return null;
    
    return _uploadFile(
      file: File(platformFile.path!),
      category: 'document',
    );
  }
  
  /// Core upload logic using signed URLs.
  Future<FileRecord> _uploadFile({
    required File file,
    required String category,
  }) async {
    // Get file info
    final fileName = file.path.split('/').last;
    final fileBytes = await file.readAsBytes();
    final fileSize = fileBytes.length;
    final contentType = lookupMimeType(fileName) ?? 'application/octet-stream';
    
    // Step 1: Get signed upload URL from server
    final uploadInfo = await _client.file.getUploadUrl(
      fileName: fileName,
      contentType: contentType,
      fileSize: fileSize,
      category: category,
    );
    
    // Step 2: Upload directly to cloud storage
    final uploadResponse = await http.put(
      Uri.parse(uploadInfo.uploadUrl),
      headers: {
        'Content-Type': contentType,
        ...uploadInfo.uploadHeaders,
      },
      body: fileBytes,
    );
    
    if (uploadResponse.statusCode != 200 && uploadResponse.statusCode != 201) {
      throw UploadFailedException(
        'Upload failed with status ${uploadResponse.statusCode}',
      );
    }
    
    // Step 3: Confirm upload with server
    final confirmedFile = await _client.file.confirmUpload(
      fileId: uploadInfo.fileId,
    );
    
    return confirmedFile;
  }
  
  /// Upload with progress tracking.
  Future<FileRecord> uploadWithProgress({
    required File file,
    required String category,
    required void Function(double progress) onProgress,
  }) async {
    final fileName = file.path.split('/').last;
    final fileSize = await file.length();
    final contentType = lookupMimeType(fileName) ?? 'application/octet-stream';
    
    // Get upload URL
    final uploadInfo = await _client.file.getUploadUrl(
      fileName: fileName,
      contentType: contentType,
      fileSize: fileSize,
      category: category,
    );
    
    // Create upload request with progress tracking
    final request = http.StreamedRequest(
      uploadInfo.uploadMethod,
      Uri.parse(uploadInfo.uploadUrl),
    );
    
    request.headers.addAll({
      'Content-Type': contentType,
      'Content-Length': fileSize.toString(),
      ...uploadInfo.uploadHeaders,
    });
    
    // Track bytes sent
    int bytesSent = 0;
    final fileStream = file.openRead();
    
    // Add file content with progress tracking
    final transformedStream = fileStream.transform(
      StreamTransformer.fromHandlers(
        handleData: (data, sink) {
          bytesSent += data.length;
          onProgress(bytesSent / fileSize);
          sink.add(data);
        },
      ),
    );
    
    request.sink.addStream(transformedStream).then((_) {
      request.sink.close();
    });
    
    // Send the request
    final response = await request.send();
    final responseBody = await response.stream.bytesToString();
    
    if (response.statusCode != 200 && response.statusCode != 201) {
      throw UploadFailedException('Upload failed: $responseBody');
    }
    
    // Confirm upload
    return _client.file.confirmUpload(fileId: uploadInfo.fileId);
  }
}

/// Exception thrown when upload fails.
class UploadFailedException implements Exception {
  final String message;
  UploadFailedException(this.message);
  
  @override
  String toString() => message;
}
```
