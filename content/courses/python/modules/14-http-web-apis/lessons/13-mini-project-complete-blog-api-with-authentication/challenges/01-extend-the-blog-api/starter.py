# Start with the FastAPI Blog API from the lesson
# Add your new features below

from fastapi import FastAPI, Depends, HTTPException, Query
from pydantic import BaseModel
from typing import Optional, List
from datetime import datetime

app = FastAPI()

# Data stores (from the lesson)
users_db = {}
posts_db = []
comments_db = []
likes_db = []  # TODO: Store likes here

# TODO: Add Pydantic models for responses
class UserProfile(BaseModel):
    username: str
    bio: str
    post_count: int
    joined: str

# TODO: Add helper function to check if user already liked a post
def has_liked(username: str, post_id: int) -> bool:
    pass

# TODO: Add POST /api/posts/{id}/like endpoint
@app.post("/api/posts/{post_id}/like")
async def like_post(post_id: int, current_user: str = Depends(get_current_user)):
    pass

# TODO: Add GET /api/users/{username} endpoint
@app.get("/api/users/{username}", response_model=UserProfile)
async def get_user_profile(username: str):
    pass

# TODO: Modify GET /api/posts to support ?search= parameter
@app.get("/api/posts")
async def get_posts(search: Optional[str] = Query(None)):
    # Filter posts if search parameter provided
    pass

# Run with: uvicorn main:app --reload