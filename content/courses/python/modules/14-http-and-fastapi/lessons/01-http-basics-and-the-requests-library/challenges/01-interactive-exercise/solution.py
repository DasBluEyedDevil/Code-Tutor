import requests
from typing import List, Dict, Optional

# JSONPlaceholder API Client
# This solution demonstrates API consumption with error handling

class PostsAPI:
    """API client for JSONPlaceholder posts."""
    
    BASE_URL = 'https://jsonplaceholder.typicode.com'
    
    def get_user_posts(self, user_id: int) -> List[Dict]:
        """Get all posts for a specific user."""
        try:
            url = f"{self.BASE_URL}/posts"
            response = requests.get(url, params={'userId': user_id})
            response.raise_for_status()  # Raise exception for HTTP errors
            return response.json()
        except requests.RequestException as e:
            print(f"Error fetching posts: {e}")
            return []
    
    def filter_posts_by_keyword(self, posts: List[Dict], keyword: str) -> List[Dict]:
        """Filter posts where keyword appears in title (case-insensitive)."""
        keyword_lower = keyword.lower()
        return [
            post for post in posts
            if keyword_lower in post.get('title', '').lower()
        ]
    
    def format_post(self, post: Dict) -> str:
        """Return formatted string for a post."""
        post_id = post.get('id', '?')
        title = post.get('title', 'No title')
        return f"[{post_id}] {title}"
    
    def get_post_summary(self, user_id: int) -> Dict:
        """Get summary of user's posts."""
        posts = self.get_user_posts(user_id)
        return {
            'user_id': user_id,
            'total_posts': len(posts),
            'posts': posts
        }

# Test the API client
print("=== Posts API Client Demo ===")

api = PostsAPI()

# Get posts for user 1
print("\nFetching posts for user 1...")
posts = api.get_user_posts(1)
print(f"Found {len(posts)} posts")

# Filter by keyword
print("\nFiltering posts with 'sunt' in title:")
filtered = api.filter_posts_by_keyword(posts, 'sunt')
for post in filtered:
    print(f"  {api.format_post(post)}")

# Show first 3 posts
print("\nFirst 3 posts:")
for post in posts[:3]:
    print(f"  {api.format_post(post)}")