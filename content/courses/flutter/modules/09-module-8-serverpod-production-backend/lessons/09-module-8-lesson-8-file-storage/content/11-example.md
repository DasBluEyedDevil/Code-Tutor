---
type: "EXAMPLE"
title: "Flutter UI: File Upload Widget"
---

Let us create a reusable upload widget with progress indication.



```dart
// File: lib/widgets/file_upload_widget.dart

import 'dart:io';
import 'package:flutter/material.dart';
import 'package:image_picker/image_picker.dart';
import '../services/file_upload_service.dart';

enum UploadState { idle, picking, uploading, success, error }

class FileUploadWidget extends StatefulWidget {
  final FileUploadService uploadService;
  final String category;
  final void Function(FileRecord)? onUploadComplete;
  final void Function(String)? onError;
  
  const FileUploadWidget({
    super.key,
    required this.uploadService,
    required this.category,
    this.onUploadComplete,
    this.onError,
  });
  
  @override
  State<FileUploadWidget> createState() => _FileUploadWidgetState();
}

class _FileUploadWidgetState extends State<FileUploadWidget> {
  UploadState _state = UploadState.idle;
  double _progress = 0.0;
  String? _errorMessage;
  File? _selectedFile;
  
  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisSize: MainAxisSize.min,
          children: [
            _buildPreview(),
            const SizedBox(height: 16),
            _buildControls(),
            if (_state == UploadState.uploading) ...
              _buildProgress(),
            if (_state == UploadState.error)
              _buildError(),
          ],
        ),
      ),
    );
  }
  
  Widget _buildPreview() {
    if (_selectedFile == null) {
      return Container(
        width: 150,
        height: 150,
        decoration: BoxDecoration(
          color: Colors.grey[200],
          borderRadius: BorderRadius.circular(8),
        ),
        child: const Icon(
          Icons.add_photo_alternate_outlined,
          size: 50,
          color: Colors.grey,
        ),
      );
    }
    
    return ClipRRect(
      borderRadius: BorderRadius.circular(8),
      child: Image.file(
        _selectedFile!,
        width: 150,
        height: 150,
        fit: BoxFit.cover,
      ),
    );
  }
  
  Widget _buildControls() {
    switch (_state) {
      case UploadState.idle:
        return Row(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            ElevatedButton.icon(
              onPressed: _pickFile,
              icon: const Icon(Icons.photo_library),
              label: const Text('Choose File'),
            ),
          ],
        );
      
      case UploadState.picking:
        return const CircularProgressIndicator();
      
      case UploadState.uploading:
        return ElevatedButton(
          onPressed: null, // Disable during upload
          child: const Text('Uploading...'),
        );
      
      case UploadState.success:
        return Column(
          children: [
            const Icon(Icons.check_circle, color: Colors.green, size: 40),
            const SizedBox(height: 8),
            TextButton(
              onPressed: _reset,
              child: const Text('Upload Another'),
            ),
          ],
        );
      
      case UploadState.error:
        return ElevatedButton(
          onPressed: _selectedFile != null ? _uploadFile : _pickFile,
          child: Text(_selectedFile != null ? 'Retry Upload' : 'Try Again'),
        );
    }
  }
  
  List<Widget> _buildProgress() {
    return [
      const SizedBox(height: 16),
      LinearProgressIndicator(value: _progress),
      const SizedBox(height: 8),
      Text('${(_progress * 100).toInt()}%'),
    ];
  }
  
  Widget _buildError() {
    return Padding(
      padding: const EdgeInsets.only(top: 8),
      child: Text(
        _errorMessage ?? 'Upload failed',
        style: const TextStyle(color: Colors.red),
      ),
    );
  }
  
  Future<void> _pickFile() async {
    setState(() {
      _state = UploadState.picking;
      _errorMessage = null;
    });
    
    try {
      final picker = ImagePicker();
      final XFile? image = await picker.pickImage(
        source: ImageSource.gallery,
        maxWidth: 1024,
        maxHeight: 1024,
        imageQuality: 85,
      );
      
      if (image == null) {
        setState(() => _state = UploadState.idle);
        return;
      }
      
      setState(() {
        _selectedFile = File(image.path);
        _state = UploadState.idle;
      });
      
      // Optionally auto-upload after selection
      await _uploadFile();
      
    } catch (e) {
      setState(() {
        _state = UploadState.error;
        _errorMessage = 'Failed to pick file: $e';
      });
      widget.onError?.call(_errorMessage!);
    }
  }
  
  Future<void> _uploadFile() async {
    if (_selectedFile == null) return;
    
    setState(() {
      _state = UploadState.uploading;
      _progress = 0.0;
      _errorMessage = null;
    });
    
    try {
      final result = await widget.uploadService.uploadWithProgress(
        file: _selectedFile!,
        category: widget.category,
        onProgress: (progress) {
          setState(() => _progress = progress);
        },
      );
      
      setState(() => _state = UploadState.success);
      widget.onUploadComplete?.call(result);
      
    } catch (e) {
      setState(() {
        _state = UploadState.error;
        _errorMessage = e.toString();
      });
      widget.onError?.call(_errorMessage!);
    }
  }
  
  void _reset() {
    setState(() {
      _state = UploadState.idle;
      _selectedFile = null;
      _progress = 0.0;
      _errorMessage = null;
    });
  }
}
```
