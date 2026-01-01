/// File validation result.
class ValidationResult {
  final bool isValid;
  final String? error;
  final String? sanitizedFileName;
  final String? detectedContentType;
  
  ValidationResult({
    required this.isValid,
    this.error,
    this.sanitizedFileName,
    this.detectedContentType,
  });
  
  factory ValidationResult.success({
    required String sanitizedFileName,
    required String detectedContentType,
  }) {
    return ValidationResult(
      isValid: true,
      sanitizedFileName: sanitizedFileName,
      detectedContentType: detectedContentType,
    );
  }
  
  factory ValidationResult.failure(String error) {
    return ValidationResult(
      isValid: false,
      error: error,
    );
  }
}

/// Service for validating files before upload.
class FileValidationService {
  // Magic bytes for common file types
  static const Map<String, List<int>> _magicBytes = {
    'image/jpeg': [0xFF, 0xD8, 0xFF],
    'image/png': [0x89, 0x50, 0x4E, 0x47],
    'image/gif': [0x47, 0x49, 0x46],
    'application/pdf': [0x25, 0x50, 0x44, 0x46],
  };
  
  // Configuration per category
  static const Map<String, Map<String, dynamic>> _categoryConfig = {
    'avatar': {
      'allowedExtensions': ['jpg', 'jpeg', 'png'],
      'maxSizeBytes': 5 * 1024 * 1024, // 5 MB
      'allowedMimeTypes': ['image/jpeg', 'image/png'],
    },
    'document': {
      'allowedExtensions': ['pdf', 'doc', 'docx'],
      'maxSizeBytes': 20 * 1024 * 1024, // 20 MB
      'allowedMimeTypes': ['application/pdf'],
    },
    'media': {
      'allowedExtensions': ['jpg', 'jpeg', 'png', 'gif'],
      'maxSizeBytes': 10 * 1024 * 1024, // 10 MB
      'allowedMimeTypes': ['image/jpeg', 'image/png', 'image/gif'],
    },
  };
  
  // Counter for generating unique IDs (simplified for demo)
  static int _idCounter = 0;
  
  /// Validate a file for upload.
  ValidationResult validateFile({
    required String fileName,
    required int fileSize,
    required List<int> fileHeader,
    required String category,
  }) {
    // Get configuration for this category
    final config = _categoryConfig[category];
    if (config == null) {
      return ValidationResult.failure('Unknown file category: $category');
    }
    
    // Extract and validate file extension
    final extension = getExtension(fileName);
    if (extension == null) {
      return ValidationResult.failure('File must have an extension');
    }
    
    final allowedExtensions = config['allowedExtensions'] as List<String>;
    if (!allowedExtensions.contains(extension.toLowerCase())) {
      return ValidationResult.failure(
        'Extension .$extension not allowed for $category. Allowed: ${allowedExtensions.join(", ")}',
      );
    }
    
    // Validate file size
    final maxSize = config['maxSizeBytes'] as int;
    if (fileSize > maxSize) {
      final maxMB = maxSize / (1024 * 1024);
      final actualMB = fileSize / (1024 * 1024);
      return ValidationResult.failure(
        'File size ${actualMB.toStringAsFixed(1)}MB exceeds maximum ${maxMB.toStringAsFixed(0)}MB',
      );
    }
    
    // Detect content type from magic bytes
    final detectedType = detectContentType(fileHeader);
    if (detectedType == null) {
      return ValidationResult.failure(
        'Could not detect file type from content',
      );
    }
    
    // Verify detected content type is allowed
    final allowedMimeTypes = config['allowedMimeTypes'] as List<String>;
    if (!allowedMimeTypes.contains(detectedType)) {
      return ValidationResult.failure(
        'Detected file type $detectedType not allowed for $category',
      );
    }
    
    // Sanitize the file name
    final sanitized = sanitizeFileName(fileName);
    
    return ValidationResult.success(
      sanitizedFileName: sanitized,
      detectedContentType: detectedType,
    );
  }
  
  /// Detect content type from file header bytes.
  String? detectContentType(List<int> header) {
    if (header.isEmpty) return null;
    
    for (final entry in _magicBytes.entries) {
      final mimeType = entry.key;
      final signature = entry.value;
      
      // Check if header starts with this signature
      if (header.length >= signature.length) {
        bool matches = true;
        for (int i = 0; i < signature.length; i++) {
          if (header[i] != signature[i]) {
            matches = false;
            break;
          }
        }
        if (matches) {
          return mimeType;
        }
      }
    }
    
    return null;
  }
  
  /// Sanitize a file name to remove dangerous characters.
  String sanitizeFileName(String fileName) {
    // Replace any character that isn't alphanumeric, dot, hyphen, or underscore
    String sanitized = fileName.replaceAll(RegExp(r'[^a-zA-Z0-9._-]'), '_');
    
    // Replace multiple consecutive underscores with single underscore
    sanitized = sanitized.replaceAll(RegExp(r'_{2,}'), '_');
    
    // Remove leading/trailing underscores
    sanitized = sanitized.replaceAll(RegExp(r'^_+|_+$'), '');
    
    // Ensure we have a valid name
    if (sanitized.isEmpty || sanitized == '.') {
      sanitized = 'unnamed_file';
    }
    
    return sanitized;
  }
  
  /// Extract file extension from file name.
  String? getExtension(String fileName) {
    final lastDot = fileName.lastIndexOf('.');
    
    // No dot found, or dot is first character, or dot is last character
    if (lastDot == -1 || lastDot == 0 || lastDot == fileName.length - 1) {
      return null;
    }
    
    return fileName.substring(lastDot + 1);
  }
  
  /// Generate a safe storage path.
  String generateStoragePath({
    required int userId,
    required String category,
    required String extension,
  }) {
    final now = DateTime.now();
    final year = now.year;
    final month = now.month.toString().padLeft(2, '0');
    
    // Generate a simple unique ID (in production, use UUID)
    _idCounter++;
    final uniqueId = '${now.millisecondsSinceEpoch}_$_idCounter';
    
    return '$category/$year/$month/user_$userId/$uniqueId.$extension';
  }
}

// Test your implementation
void main() {
  final service = FileValidationService();
  
  // Test 1: Valid JPEG image
  print('--- Test 1: Valid JPEG ---');
  final jpegHeader = [0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10];
  final result1 = service.validateFile(
    fileName: 'photo.jpg',
    fileSize: 1024 * 1024, // 1 MB
    fileHeader: jpegHeader,
    category: 'avatar',
  );
  print('Valid: ${result1.isValid}');
  print('Sanitized name: ${result1.sanitizedFileName}');
  print('Detected type: ${result1.detectedContentType}');
  
  // Test 2: File too large
  print('\n--- Test 2: File too large ---');
  final result2 = service.validateFile(
    fileName: 'huge.jpg',
    fileSize: 50 * 1024 * 1024, // 50 MB
    fileHeader: jpegHeader,
    category: 'avatar',
  );
  print('Valid: ${result2.isValid}');
  print('Error: ${result2.error}');
  
  // Test 3: PNG file with jpg extension (content doesn't match)
  print('\n--- Test 3: Extension mismatch ---');
  final pngHeader = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A];
  final result3 = service.validateFile(
    fileName: 'image.jpg',
    fileSize: 1024 * 1024,
    fileHeader: pngHeader,
    category: 'avatar',
  );
  print('Valid: ${result3.isValid}');
  // PNG is allowed in avatar, so this passes despite extension mismatch
  print('Detected type: ${result3.detectedContentType}');
  
  // Test 4: Dangerous file name
  print('\n--- Test 4: Dangerous file name ---');
  final sanitized = service.sanitizeFileName('../../../etc/passwd.jpg');
  print('Sanitized: $sanitized');
  
  // Test 5: Generate storage path
  print('\n--- Test 5: Storage path ---');
  final path = service.generateStoragePath(
    userId: 123,
    category: 'avatar',
    extension: 'jpg',
  );
  print('Path: $path');
  
  // Test 6: Unknown category
  print('\n--- Test 6: Unknown category ---');
  final result6 = service.validateFile(
    fileName: 'file.xyz',
    fileSize: 1024,
    fileHeader: [0x00, 0x00, 0x00],
    category: 'unknown',
  );
  print('Valid: ${result6.isValid}');
  print('Error: ${result6.error}');
}