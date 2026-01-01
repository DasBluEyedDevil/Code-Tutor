from django.db import models
from django.contrib.auth.models import User

class Post(models.Model):
    """Blog post model."""
    title = models.CharField(max_length=200)
    content = models.TextField()
    author = models.ForeignKey(User, on_delete=models.CASCADE)
    published_at = models.DateTimeField(null=True, blank=True)
    is_published = models.BooleanField(default=False)
    created_at = models.DateTimeField(auto_now_add=True)
    
    class Meta:
        ordering = ['-published_at']
    
    def __str__(self):
        return self.title

class Comment(models.Model):
    """Comment on a blog post."""
    post = models.ForeignKey(Post, on_delete=models.CASCADE, related_name='comments')
    author_name = models.CharField(max_length=100)
    content = models.TextField()
    created_at = models.DateTimeField(auto_now_add=True)
    
    def __str__(self):
        return f"Comment by {self.author_name} on {self.post.title}"

# Admin configuration (in admin.py)
from django.contrib import admin

@admin.register(Post)
class PostAdmin(admin.ModelAdmin):
    list_display = ['title', 'author', 'is_published', 'published_at']
    list_filter = ['is_published', 'published_at', 'author']
    search_fields = ['title', 'content']
    date_hierarchy = 'published_at'

@admin.register(Comment)
class CommentAdmin(admin.ModelAdmin):
    list_display = ['author_name', 'post', 'created_at']
    list_filter = ['created_at']
    search_fields = ['author_name', 'content']

# Query: Get all published posts, newest first
published_posts = Post.objects.filter(is_published=True).order_by('-published_at')

print("=== Blog Models Created ===")
print("\nPost model fields:")
print("  - title: CharField(max_length=200)")
print("  - content: TextField")
print("  - author: ForeignKey(User)")
print("  - published_at: DateTimeField")
print("  - is_published: BooleanField")

print("\nComment model fields:")
print("  - post: ForeignKey(Post)")
print("  - author_name: CharField")
print("  - content: TextField")
print("  - created_at: DateTimeField")

print("\nQuery for published posts:")
print("  Post.objects.filter(is_published=True).order_by('-published_at')")