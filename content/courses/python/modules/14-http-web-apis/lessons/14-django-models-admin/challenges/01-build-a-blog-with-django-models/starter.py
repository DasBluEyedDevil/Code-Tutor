from django.db import models
from django.contrib.auth.models import User

# TODO: Create Post model
class Post(models.Model):
    # Add fields: title, content, author (FK), published_at, is_published
    pass

# TODO: Create Comment model  
class Comment(models.Model):
    # Add fields: post (FK), author_name, content, created_at
    pass

# TODO: Admin configuration
# from django.contrib import admin
# @admin.register(Post)
# class PostAdmin(admin.ModelAdmin):
#     pass

# Query: Get all published posts, newest first
# published_posts = ???