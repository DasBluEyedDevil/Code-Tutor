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
type User = /* Use Awaited<ReturnType<...>> */;
type Post = /* Get single post type from array */;
type Comment = /* Get single comment type from array */;

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
): /* What should return type be? */ {
  const cache = new Map<string, CacheEntry<Awaited<ReturnType<T>>>>();
  
  return async (...args: Parameters<T>): Promise<Awaited<ReturnType<T>>> => {
    const key = keyGenerator(...args);
    const cached = cache.get(key);
    
    // Check if valid cache entry exists
    // If not, call original function and cache result
    // Return the data
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
  
  // Second call - should use cache
  const user2 = await cachedFetchUser(1);
  console.log('User (cached):', user2);
  
  // Different ID - should fetch
  const user3 = await cachedFetchUser(2);
  console.log('User 2:', user3);
  
  // Posts test
  const posts = await cachedFetchPosts(1, 2);
  console.log('Posts:', posts);
}

testCache();