from fastapi import FastAPI
from pydantic import BaseModel
from typing import List, Optional
from datetime import datetime

app = FastAPI()

# TODO: Create models:
# - PostCreate: title, content
# - PostResponse: id, title, content, created_at, comments
# - CommentCreate: text, author
# - CommentResponse: id, text, author, created_at

# TODO: Create endpoints:
# POST /posts - create post
# GET /posts/{post_id} - get post with comments
# POST /posts/{post_id}/comments - add comment

posts = {}
next_id = 1

print("Blog API structure created")