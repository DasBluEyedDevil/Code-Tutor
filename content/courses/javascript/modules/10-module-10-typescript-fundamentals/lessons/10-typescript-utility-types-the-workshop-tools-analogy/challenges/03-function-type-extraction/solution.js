// External functions (imagine these come from a library)
async function fetchUser(id: number) {
  console.log(`Fetching user ${id}...`);
  return { id, name: 'Alice', email: 'alice@test.com' };
}

async function fetchPosts(userId: number, limit: number) {
  console.log(`Fetching ${limit} posts for user ${userId}...`);
  return [
    { id: 1, title: 'First Post', authorId: userId },
    { id: 2, title: 'Second Post', authorId: userId }
  ].slice(0, limit);
}

async function fetchComments(postId: number) {
  console.log(`Fetching comments for post ${postId}...`);
  return [
    { id: 1, postId, text: 'Great post!' },
    { id: 2, postId, text: 'Thanks for sharing' }
  ];
}

// 2. Extract types from the functions
type User = Awaited<ReturnType<typeof fetchUser>>;
type Post = Awaited<ReturnType<typeof fetchPosts>>[number];
type Comment = Awaited<ReturnType<typeof fetchComments>>[number];

// 3. Cache entry stores data with timestamp
interface CacheEntry<T> {
  data: T;
  cachedAt: number;
  ttlMs: number;
}

// 4. Create a generic cached version of any async function
function createCached<T extends (...args: any[]) => Promise<any>>(
  fn: T,
  keyGenerator: (...args: Parameters<T>) => string,
  ttlMs: number = 60000
): (...args: Parameters<T>) => Promise<Awaited<ReturnType<T>>> {
  const cache = new Map<string, CacheEntry<Awaited<ReturnType<T>>>>();
  
  return async (...args: Parameters<T>): Promise<Awaited<ReturnType<T>>> => {
    const key = keyGenerator(...args);
    const cached = cache.get(key);
    
    // Check if valid cache entry exists
    if (cached && Date.now() - cached.cachedAt < cached.ttlMs) {
      console.log(`Cache hit for ${key}`);
      return cached.data;
    }
    
    // Call original function and cache result
    const result = await fn(...args);
    cache.set(key, {
      data: result,
      cachedAt: Date.now(),
      ttlMs
    });
    
    return result;
  };
}

// 5. Create cached versions
const cachedFetchUser = createCached(
  fetchUser,
  (id) => `user:${id}`,
  30000
);

const cachedFetchPosts = createCached(
  fetchPosts,
  (userId, limit) => `posts:${userId}:${limit}`,
  30000
);

// 6. Test the cache
async function testCache() {
  // First call - should fetch
  const user1 = await cachedFetchUser(1);
  console.log('User:', user1);
  // Fetching user 1...
  // User: { id: 1, name: 'Alice', email: 'alice@test.com' }
  
  // Second call - should use cache
  const user2 = await cachedFetchUser(1);
  console.log('User (cached):', user2);
  // Cache hit for user:1
  // User (cached): { id: 1, name: 'Alice', email: 'alice@test.com' }
  
  // Different ID - should fetch
  const user3 = await cachedFetchUser(2);
  console.log('User 2:', user3);
  // Fetching user 2...
  // User 2: { id: 2, name: 'Alice', email: 'alice@test.com' }
  
  // Posts test
  const posts = await cachedFetchPosts(1, 2);
  console.log('Posts:', posts);
  // Fetching 2 posts for user 1...
  // Posts: [{ id: 1, title: 'First Post', authorId: 1 }, { id: 2, title: 'Second Post', authorId: 1 }]
}

testCache();