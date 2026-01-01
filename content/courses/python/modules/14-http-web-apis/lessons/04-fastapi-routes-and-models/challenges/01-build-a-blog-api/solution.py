from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, Field
from typing import List, Optional
from datetime import datetime

app = FastAPI(title="Blog API")

# Models
class CommentCreate(BaseModel):
    text: str = Field(min_length=1)
    author: str

class CommentResponse(BaseModel):
    id: int
    text: str
    author: str
    created_at: datetime

class PostCreate(BaseModel):
    title: str = Field(min_length=1, max_length=200)
    content: str

class PostResponse(BaseModel):
    id: int
    title: str
    content: str
    created_at: datetime
    comments: List[CommentResponse] = []

# Storage
posts = {}
next_post_id = 1
next_comment_id = 1

@app.post("/posts", response_model=PostResponse, status_code=201)
def create_post(post: PostCreate):
    global next_post_id
    new_post = {
        "id": next_post_id,
        "title": post.title,
        "content": post.content,
        "created_at": datetime.now(),
        "comments": []
    }
    posts[next_post_id] = new_post
    next_post_id += 1
    return new_post

@app.get("/posts/{post_id}", response_model=PostResponse)
def get_post(post_id: int):
    if post_id not in posts:
        raise HTTPException(404, "Post not found")
    return posts[post_id]

@app.post("/posts/{post_id}/comments", response_model=CommentResponse)
def add_comment(post_id: int, comment: CommentCreate):
    global next_comment_id
    if post_id not in posts:
        raise HTTPException(404, "Post not found")
    new_comment = {
        "id": next_comment_id,
        "text": comment.text,
        "author": comment.author,
        "created_at": datetime.now()
    }
    posts[post_id]["comments"].append(new_comment)
    next_comment_id += 1
    return new_comment

print("Blog API ready!")