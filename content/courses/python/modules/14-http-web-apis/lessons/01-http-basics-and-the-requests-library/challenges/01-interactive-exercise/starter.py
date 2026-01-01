import requests
from typing import List, Dict, Optional

class PostsAPI:
    BASE_URL = 'https://jsonplaceholder.typicode.com'
    
    def get_user_posts(self, user_id: int) -> List[Dict]:
        # TODO: Get all posts for user_id
        pass
    
    def filter_posts_by_keyword(self, posts: List[Dict], keyword: str) -> List[Dict]:
        # TODO: Filter posts where keyword is in title
        pass
    
    def format_post(self, post: Dict) -> str:
        # TODO: Return formatted string "[ID] Title"
        pass

# Test
api = PostsAPI()
posts = api.get_user_posts(1)
filtered = api.filter_posts_by_keyword(posts, 'sunt')
for post in filtered:
    print(api.format_post(post))