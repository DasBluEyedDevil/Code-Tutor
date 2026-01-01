// server/lib/src/services/feed_cache.dart
import 'dart:async';

class FeedCache {
  // In production, use Redis or Memcached
  final Map<String, CacheEntry> _cache = {};
  
  // For cache stampede prevention
  final Map<String, Completer<List<EnrichedPost>>> _pendingComputes = {};
  
  // Statistics
  int _hits = 0;
  int _misses = 0;
  
  static const Duration defaultTTL = Duration(minutes: 5);
  static const int maxCacheSize = 10000;
  
  /// Generate cache key for user feed
  String userFeedKey(int userId, String feedType, String? cursor) {
    return 'feed:$userId:$feedType:${cursor ?? 'first'}';
  }
  
  /// Generate cache key for explore feed
  String exploreFeedKey(String? cursor) {
    return 'feed:explore:${cursor ?? 'first'}';
  }

  /// Get cached feed or null if not cached/expired
  Future<List<EnrichedPost>?> getCachedFeed(
    String cacheKey,
  ) async {
    final entry = _cache[cacheKey];
    
    if (entry == null) {
      _misses++;
      return null;
    }
    
    if (entry.isExpired) {
      _cache.remove(cacheKey);
      _misses++;
      return null;
    }
    
    _hits++;
    return entry.data as List<EnrichedPost>;
  }
  
  /// Cache feed results
  Future<void> cacheFeed(
    String cacheKey,
    List<EnrichedPost> posts, {
    Duration? ttl,
  }) async {
    // Evict old entries if cache is too large
    if (_cache.length >= maxCacheSize) {
      _evictOldEntries();
    }
    
    final effectiveTTL = ttl ?? defaultTTL;
    
    _cache[cacheKey] = CacheEntry(
      data: posts,
      expiresAt: DateTime.now().add(effectiveTTL),
      createdAt: DateTime.now(),
    );
  }
  
  /// Invalidate cache for a user's feed
  Future<void> invalidateUserFeed(int userId) async {
    final keysToRemove = _cache.keys
        .where((key) => key.startsWith('feed:$userId:'))
        .toList();
    
    for (final key in keysToRemove) {
      _cache.remove(key);
    }
  }
  
  /// Invalidate feeds affected by a new post
  Future<void> onNewPost(int authorId, List<int> followerIds) async {
    // Invalidate author's own feed
    await invalidateUserFeed(authorId);
    
    // Invalidate explore feed (new public posts affect it)
    final exploreKeys = _cache.keys
        .where((key) => key.startsWith('feed:explore:'))
        .toList();
    for (final key in exploreKeys) {
      _cache.remove(key);
    }
    
    // Invalidate followers' home feeds
    for (final followerId in followerIds) {
      final keysToRemove = _cache.keys
          .where((key) => key.startsWith('feed:$followerId:home:'))
          .toList();
      for (final key in keysToRemove) {
        _cache.remove(key);
      }
    }
  }
  
  /// Invalidate cache when a post is updated or deleted
  Future<void> onPostModified(int postId, int authorId) async {
    // For simplicity, invalidate author's feeds
    await invalidateUserFeed(authorId);
    
    // In production, track which cache entries contain this post
    // and invalidate only those
  }
  
  /// Get or compute feed with cache stampede prevention
  /// 
  /// When multiple requests ask for the same uncached feed,
  /// only one will compute it and others will wait for the result.
  Future<List<EnrichedPost>> getOrCompute(
    String cacheKey,
    Future<List<EnrichedPost>> Function() compute, {
    Duration? ttl,
  }) async {
    // Check cache first
    final cached = await getCachedFeed(cacheKey);
    if (cached != null) {
      return cached;
    }
    
    // Check if someone else is already computing this
    if (_pendingComputes.containsKey(cacheKey)) {
      // Wait for the other computation to complete
      return _pendingComputes[cacheKey]!.future;
    }
    
    // We're the first - create a completer and compute
    final completer = Completer<List<EnrichedPost>>();
    _pendingComputes[cacheKey] = completer;
    
    try {
      // Compute the feed
      final result = await compute();
      
      // Cache the result
      await cacheFeed(cacheKey, result, ttl: ttl);
      
      // Complete the future for any waiters
      completer.complete(result);
      
      return result;
    } catch (e) {
      // Complete with error for waiters
      completer.completeError(e);
      rethrow;
    } finally {
      // Remove from pending
      _pendingComputes.remove(cacheKey);
    }
  }
  
  /// Get cache statistics
  CacheStats getStats() {
    final total = _hits + _misses;
    return CacheStats(
      hits: _hits,
      misses: _misses,
      size: _cache.length,
      hitRate: total > 0 ? _hits / total : 0.0,
    );
  }
  
  /// Reset statistics
  void resetStats() {
    _hits = 0;
    _misses = 0;
  }
  
  /// Clear entire cache
  void clear() {
    _cache.clear();
    _pendingComputes.clear();
  }
  
  /// Evict expired and old entries
  void _evictOldEntries() {
    // First, remove all expired entries
    _cache.removeWhere((key, entry) => entry.isExpired);
    
    // If still too large, remove oldest entries
    if (_cache.length >= maxCacheSize) {
      final entries = _cache.entries.toList()
        ..sort((a, b) => a.value.createdAt.compareTo(b.value.createdAt));
      
      // Remove oldest 20%
      final toRemove = (entries.length * 0.2).ceil();
      for (var i = 0; i < toRemove; i++) {
        _cache.remove(entries[i].key);
      }
    }
  }
  
  /// Warm up cache for a user (called on login)
  Future<void> warmupUserCache(
    int userId,
    Future<List<EnrichedPost>> Function(String feedType) getFeed,
  ) async {
    // Pre-cache home and following feeds
    final feedTypes = ['home', 'following'];
    
    for (final feedType in feedTypes) {
      final key = userFeedKey(userId, feedType, null);
      if (!_cache.containsKey(key)) {
        try {
          final posts = await getFeed(feedType);
          await cacheFeed(key, posts);
        } catch (e) {
          // Ignore warmup failures
        }
      }
    }
  }
}

class CacheEntry {
  final dynamic data;
  final DateTime expiresAt;
  final DateTime createdAt;
  
  CacheEntry({
    required this.data,
    required this.expiresAt,
    required this.createdAt,
  });
  
  bool get isExpired => DateTime.now().isAfter(expiresAt);
  
  Duration get timeToLive => expiresAt.difference(DateTime.now());
}

class CacheStats {
  final int hits;
  final int misses;
  final int size;
  final double hitRate;
  
  CacheStats({
    required this.hits,
    required this.misses,
    required this.size,
    required this.hitRate,
  });
  
  @override
  String toString() {
    return 'CacheStats(hits: $hits, misses: $misses, '
           'size: $size, hitRate: ${(hitRate * 100).toStringAsFixed(1)}%)';
  }
}

// Placeholder for EnrichedPost type
class EnrichedPost {
  final int id;
  EnrichedPost({required this.id});
}