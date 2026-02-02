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
  
  /// Validate a file for upload.
  ValidationResult validateFile({
    required String fileName,
    required int fileSize,
    required List<int> fileHeader, // First few bytes of file
    required String category,
  }) {
    // TODO: Get configuration for this category
    // Return failure if category is unknown
    
    // TODO: Extract and validate file extension
    // Check if extension is in allowed list
    
    // TODO: Validate file size
    // Check against maxSizeBytes for the category
    
    // TODO: Detect content type from magic bytes
    // Compare first bytes of file against known signatures
    
    // TODO: Verify detected content type is allowed
    // Check against allowedMimeTypes for the category
    
    // TODO: Sanitize the file name
    // Remove dangerous characters, keep only safe ones
    
    // TODO: Return success result with sanitized name and detected type
    return ValidationResult.failure('Not implemented');
  }
  
  /// Detect content type from file header bytes.
  String? detectContentType(List<int> header) {
    // TODO: Check header against known magic bytes
    // Return the matching MIME type or null
    return null;
  }
  
  /// Sanitize a file name to remove dangerous characters.
  String sanitizeFileName(String fileName) {
    // TODO: Remove or replace dangerous characters
    // Keep only alphanumeric, dots, hyphens, underscores
    // Handle multiple consecutive underscores
    return fileName;
  }
  
  /// Extract file extension from file name.
  String? getExtension(String fileName) {
    // TODO: Extract extension (without the dot)
    // Return null if no valid extension
    return null;
  }
  
  /// Generate a safe storage path.
  String generateStoragePath({
    required int userId,
    required String category,
    required String extension,
  }) {
    // TODO: Generate a unique, organized path
    // Format: category/YYYY/MM/user_ID/uniqueId.extension
    // Use current date and a simple unique ID
    return '';
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
  
  // Test 3: Wrong extension for content
  print('\n--- Test 3: Extension mismatch ---');
  final pngHeader = [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A];
  final result3 = service.validateFile(
    fileName: 'image.jpg', // Claims to be JPEG
    fileSize: 1024 * 1024,
    fileHeader: pngHeader, // But is actually PNG
    category: 'avatar',
  );
  print('Valid: ${result3.isValid}');
  // This could be valid or invalid depending on your validation strategy
  
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