---
type: "EXAMPLE"
title: "Complete CRUD Operations with Dio"
---


Let us implement all CRUD operations using the free JSONPlaceholder API, which provides a realistic REST API for testing.



```dart
// lib/features/posts/data/post_api_service.dart
import 'package:dio/dio.dart';

/// Represents a blog post from the JSONPlaceholder API.
class Post {
  final int? id;
  final int userId;
  final String title;
  final String body;

  Post({
    this.id,
    required this.userId,
    required this.title,
    required this.body,
  });

  /// Creates a Post from JSON response data.
  factory Post.fromJson(Map<String, dynamic> json) {
    return Post(
      id: json['id'] as int?,
      userId: json['userId'] as int,
      title: json['title'] as String,
      body: json['body'] as String,
    );
  }

  /// Converts Post to JSON for request bodies.
  Map<String, dynamic> toJson() {
    return {
      if (id != null) 'id': id,
      'userId': userId,
      'title': title,
      'body': body,
    };
  }
  
  @override
  String toString() => 'Post(id: $id, title: $title)';
}

/// Service class that demonstrates all HTTP methods with Dio.
/// 
/// Uses JSONPlaceholder API: https://jsonplaceholder.typicode.com
/// This is a free fake API for testing - changes are not persisted.
class PostApiService {
  final Dio _dio;

  PostApiService(this._dio);

  // ============================================================
  // GET REQUESTS - Fetching Data
  // ============================================================

  /// Fetches all posts.
  /// 
  /// GET /posts
  /// Returns: List of all 100 posts from the API.
  Future<List<Post>> getAllPosts() async {
    final response = await _dio.get('/posts');
    
    // response.data is automatically parsed as JSON (List<dynamic>)
    final List<dynamic> jsonList = response.data;
    
    // Convert each JSON object to a Post
    return jsonList.map((json) => Post.fromJson(json)).toList();
  }

  /// Fetches a single post by ID.
  /// 
  /// GET /posts/{id}
  /// Returns: The post with the given ID, or throws if not found.
  Future<Post> getPostById(int id) async {
    final response = await _dio.get('/posts/$id');
    return Post.fromJson(response.data);
  }

  /// Fetches posts with query parameters.
  /// 
  /// GET /posts?userId=1
  /// Demonstrates filtering with query parameters.
  Future<List<Post>> getPostsByUser(int userId) async {
    final response = await _dio.get(
      '/posts',
      queryParameters: {'userId': userId},
    );
    
    final List<dynamic> jsonList = response.data;
    return jsonList.map((json) => Post.fromJson(json)).toList();
  }

  /// Fetches posts with pagination.
  /// 
  /// GET /posts?_page=1&_limit=10
  /// JSONPlaceholder supports pagination with _page and _limit.
  Future<List<Post>> getPostsPaginated({int page = 1, int limit = 10}) async {
    final response = await _dio.get(
      '/posts',
      queryParameters: {
        '_page': page,
        '_limit': limit,
      },
    );
    
    final List<dynamic> jsonList = response.data;
    return jsonList.map((json) => Post.fromJson(json)).toList();
  }

  // ============================================================
  // POST REQUEST - Creating Data
  // ============================================================

  /// Creates a new post.
  /// 
  /// POST /posts
  /// Body: {"userId": 1, "title": "...", "body": "..."}
  /// Returns: The created post with server-assigned ID.
  Future<Post> createPost(Post post) async {
    final response = await _dio.post(
      '/posts',
      data: post.toJson(), // Dio automatically serializes to JSON
    );
    
    // Server returns the created post with an ID
    // Note: JSONPlaceholder always returns id: 101 for new posts
    return Post.fromJson(response.data);
  }

  // ============================================================
  // PUT REQUEST - Full Update
  // ============================================================

  /// Replaces an entire post.
  /// 
  /// PUT /posts/{id}
  /// Body: Complete post object
  /// The entire resource is replaced with the provided data.
  Future<Post> updatePostFull(Post post) async {
    if (post.id == null) {
      throw ArgumentError('Post ID is required for update');
    }
    
    final response = await _dio.put(
      '/posts/${post.id}',
      data: post.toJson(),
    );
    
    return Post.fromJson(response.data);
  }

  // ============================================================
  // PATCH REQUEST - Partial Update
  // ============================================================

  /// Updates only specific fields of a post.
  /// 
  /// PATCH /posts/{id}
  /// Body: Only the fields to update
  /// More efficient than PUT when changing few fields.
  Future<Post> updatePostPartial(int id, {String? title, String? body}) async {
    final Map<String, dynamic> updates = {};
    if (title != null) updates['title'] = title;
    if (body != null) updates['body'] = body;
    
    if (updates.isEmpty) {
      throw ArgumentError('At least one field must be provided for update');
    }
    
    final response = await _dio.patch(
      '/posts/$id',
      data: updates,
    );
    
    return Post.fromJson(response.data);
  }

  // ============================================================
  // DELETE REQUEST - Removing Data
  // ============================================================

  /// Deletes a post.
  /// 
  /// DELETE /posts/{id}
  /// Returns: void (success) or throws on error.
  Future<void> deletePost(int id) async {
    await _dio.delete('/posts/$id');
    // No return value - success means the post was deleted
    // JSONPlaceholder returns {} for successful deletes
  }
}

// ============================================================
// Usage Example
// ============================================================

/// Example demonstrating all CRUD operations.
Future<void> demonstrateCrudOperations() async {
  // Create Dio instance
  final dio = Dio(BaseOptions(
    baseUrl: 'https://jsonplaceholder.typicode.com',
    headers: {'Content-Type': 'application/json'},
  ));
  
  final postService = PostApiService(dio);
  
  try {
    // CREATE - Add a new post
    print('Creating a new post...');
    final newPost = await postService.createPost(Post(
      userId: 1,
      title: 'My First Post',
      body: 'This is the content of my post.',
    ));
    print('Created: $newPost');
    
    // READ - Fetch posts
    print('\nFetching all posts by user 1...');
    final userPosts = await postService.getPostsByUser(1);
    print('Found ${userPosts.length} posts');
    
    // READ - Fetch single post
    print('\nFetching post #1...');
    final post = await postService.getPostById(1);
    print('Title: ${post.title}');
    
    // UPDATE (PATCH) - Modify title only
    print('\nUpdating post #1 title...');
    final updated = await postService.updatePostPartial(
      1,
      title: 'Updated Title',
    );
    print('New title: ${updated.title}');
    
    // DELETE - Remove post
    print('\nDeleting post #1...');
    await postService.deletePost(1);
    print('Deleted successfully!');
    
  } on DioException catch (e) {
    print('API Error: ${e.message}');
  }
}
```
