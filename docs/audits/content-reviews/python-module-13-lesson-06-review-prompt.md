# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** HTTP & Web APIs
- **Lesson:** Mini-Project: Complete Blog API with Authentication (ID: module-13-lesson-06)
- **Difficulty:** beginner
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "module-13-lesson-06",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Building a Complete Blog API",
                                "content":  "**Congratulations! You\u0027re ready to build a real-world API project.**\n\nThis mini-project combines everything you\u0027ve learned about FastAPI and Pydantic into a complete blog system with user authentication.\n\n**What we\u0027re building:**\n\nA RESTful Blog API with:\n- **User registration and login** (JWT authentication)\n- **Blog post CRUD** (Create, Read, Update, Delete)\n- **Comments system**\n- **Protected routes** (only authors can edit their posts)\n- **Input validation** with Pydantic models\n\n**Project structure:**\n```\nblog_api/\n    main.py             # FastAPI application\n    models.py           # Pydantic models\n    auth.py             # OAuth2 + JWT logic\n    routers/\n        users.py        # User endpoints\n        posts.py        # Post endpoints\n        comments.py     # Comment endpoints\n    tests/\n        test_auth.py    # Auth tests\n        test_posts.py   # Post tests\n```\n\n**API Endpoints:**\n\n| Method | Endpoint | Description | Auth |\n|--------|----------|-------------|------|\n| POST | /api/auth/register | Register new user | No |\n| POST | /api/auth/login | Login, get token | No |\n| GET | /api/posts | List all posts | No |\n| GET | /api/posts/{id} | Get single post | No |\n| POST | /api/posts | Create post | Yes |\n| PUT | /api/posts/{id} | Update post | Yes |\n| DELETE | /api/posts/{id} | Delete post | Yes |\n| POST | /api/posts/{id}/comments | Add comment | Yes |\n\n**Technologies used:**\n- FastAPI (web framework)\n- python-jose + OAuth2 (authentication)\n- Pydantic (data validation)\n- In-memory storage (can upgrade to SQLite/PostgreSQL)\n- pytest + httpx (testing)"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Complete Blog API Implementation",
                                "content":  "**This is a complete, working Blog API with FastAPI!**\n\nTest it with curl or Postman:\n```bash\n# Register\ncurl -X POST http://localhost:8000/api/auth/register \\\n  -H \"Content-Type: application/json\" \\\n  -d \u0027{\"username\": \"alice\", \"email\": \"alice@example.com\", \"password\": \"secret123\"}\u0027\n\n# Login (save the token)\ncurl -X POST http://localhost:8000/api/auth/login \\\n  -H \"Content-Type: application/json\" \\\n  -d \u0027{\"username\": \"alice\", \"password\": \"secret123\"}\u0027\n\n# Create post (use token from login)\ncurl -X POST http://localhost:8000/api/posts \\\n  -H \"Content-Type: application/json\" \\\n  -H \"Authorization: Bearer YOUR_TOKEN\" \\\n  -d \u0027{\"title\": \"My First Post\", \"content\": \"Hello World!\"}\u0027\n```\n\n**Interactive API docs at:** http://localhost:8000/docs",
                                "code":  "# Complete Blog API with FastAPI + OAuth2 Authentication\n# Install: pip install fastapi uvicorn python-jose passlib\n\nfrom fastapi import FastAPI, Depends, HTTPException, status\nfrom fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm\nfrom pydantic import BaseModel, EmailStr, Field\nfrom jose import jwt\nfrom passlib.context import CryptContext\nfrom datetime import datetime, timedelta\nfrom typing import Optional, List\n\napp = FastAPI(title=\"Blog API\", version=\"1.0.0\")\n\n# ========== Security Config ==========\nSECRET_KEY = \"your-secret-key-change-in-production\"\nALGORITHM = \"HS256\"\nACCESS_TOKEN_EXPIRE_MINUTES = 30\n\npwd_context = CryptContext(schemes=[\"bcrypt\"], deprecated=\"auto\")\noauth2_scheme = OAuth2PasswordBearer(tokenUrl=\"api/auth/login\")\n\n# ========== Pydantic Models ==========\nclass UserCreate(BaseModel):\n    username: str = Field(min_length=3, max_length=50)\n    email: EmailStr\n    password: str = Field(min_length=6)\n\nclass UserResponse(BaseModel):\n    username: str\n    email: str\n\nclass PostCreate(BaseModel):\n    title: str = Field(min_length=5, max_length=200)\n    content: str = Field(min_length=1)\n\nclass PostResponse(BaseModel):\n    id: int\n    title: str\n    content: str\n    author: str\n    created_at: str\n    comment_count: int = 0\n\nclass CommentCreate(BaseModel):\n    content: str = Field(min_length=1)\n\nclass Token(BaseModel):\n    access_token: str\n    token_type: str\n\n# ========== In-Memory Database ==========\nusers_db = {}  # username -\u003e user data\nposts_db = []  # list of posts\ncomments_db = []  # list of comments\n\n# ========== Auth Helpers ==========\ndef hash_password(password: str) -\u003e str:\n    return pwd_context.hash(password)\n\ndef verify_password(plain: str, hashed: str) -\u003e bool:\n    return pwd_context.verify(plain, hashed)\n\ndef create_access_token(data: dict) -\u003e str:\n    to_encode = data.copy()\n    expire = datetime.utcnow() + timedelta(minutes=ACCESS_TOKEN_EXPIRE_MINUTES)\n    to_encode.update({\"exp\": expire})\n    return jwt.encode(to_encode, SECRET_KEY, algorithm=ALGORITHM)\n\nasync def get_current_user(token: str = Depends(oauth2_scheme)) -\u003e str:\n    credentials_exception = HTTPException(\n        status_code=status.HTTP_401_UNAUTHORIZED,\n        detail=\"Invalid authentication credentials\",\n        headers={\"WWW-Authenticate\": \"Bearer\"},\n    )\n    try:\n        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])\n        username: str = payload.get(\"sub\")\n        if username is None:\n            raise credentials_exception\n    except jwt.JWTError:\n        raise credentials_exception\n    if username not in users_db:\n        raise credentials_exception\n    return username\n\n# ========== Auth Endpoints ==========\n@app.post(\"/api/auth/register\", response_model=dict, status_code=201)\nasync def register(user: UserCreate):\n    if user.username in users_db:\n        raise HTTPException(status_code=400, detail=\"Username already exists\")\n    \n    users_db[user.username] = {\n        \"username\": user.username,\n        \"email\": user.email,\n        \"password_hash\": hash_password(user.password),\n        \"created_at\": datetime.now().isoformat()\n    }\n    return {\"message\": \"User registered\", \"user\": UserResponse(username=user.username, email=user.email).model_dump()}\n\n@app.post(\"/api/auth/login\", response_model=Token)\nasync def login(form_data: OAuth2PasswordRequestForm = Depends()):\n    user = users_db.get(form_data.username)\n    if not user or not verify_password(form_data.password, user[\"password_hash\"]):\n        raise HTTPException(status_code=401, detail=\"Invalid credentials\")\n    \n    access_token = create_access_token(data={\"sub\": form_data.username})\n    return {\"access_token\": access_token, \"token_type\": \"bearer\"}\n\n# ========== Post Endpoints ==========\n@app.get(\"/api/posts\", response_model=List[PostResponse])\nasync def get_posts():\n    return [\n        PostResponse(\n            id=i,\n            title=p[\"title\"],\n            content=p[\"content\"][:100] + \"...\" if len(p[\"content\"]) \u003e 100 else p[\"content\"],\n            author=p[\"author\"],\n            created_at=p[\"created_at\"],\n            comment_count=len([c for c in comments_db if c[\"post_id\"] == i])\n        )\n        for i, p in enumerate(posts_db) if not p.get(\"deleted\")\n    ]\n\n@app.get(\"/api/posts/{post_id}\", response_model=dict)\nasync def get_post(post_id: int):\n    if post_id \u003e= len(posts_db) or posts_db[post_id].get(\"deleted\"):\n        raise HTTPException(status_code=404, detail=\"Post not found\")\n    \n    post = posts_db[post_id]\n    post_comments = [c for c in comments_db if c[\"post_id\"] == post_id]\n    return {**post, \"id\": post_id, \"comments\": post_comments}\n\n@app.post(\"/api/posts\", response_model=dict, status_code=201)\nasync def create_post(post: PostCreate, current_user: str = Depends(get_current_user)):\n    new_post = {\n        \"title\": post.title,\n        \"content\": post.content,\n        \"author\": current_user,\n        \"created_at\": datetime.now().isoformat()\n    }\n    posts_db.append(new_post)\n    return {\"message\": \"Post created\", \"post\": {**new_post, \"id\": len(posts_db) - 1}}\n\n@app.put(\"/api/posts/{post_id}\", response_model=dict)\nasync def update_post(post_id: int, post: PostCreate, current_user: str = Depends(get_current_user)):\n    if post_id \u003e= len(posts_db):\n        raise HTTPException(status_code=404, detail=\"Post not found\")\n    \n    existing_post = posts_db[post_id]\n    if existing_post[\"author\"] != current_user:\n        raise HTTPException(status_code=403, detail=\"Not authorized\")\n    \n    existing_post[\"title\"] = post.title\n    existing_post[\"content\"] = post.content\n    existing_post[\"updated_at\"] = datetime.now().isoformat()\n    return {\"message\": \"Post updated\", \"post\": {**existing_post, \"id\": post_id}}\n\n@app.delete(\"/api/posts/{post_id}\", response_model=dict)\nasync def delete_post(post_id: int, current_user: str = Depends(get_current_user)):\n    if post_id \u003e= len(posts_db):\n        raise HTTPException(status_code=404, detail=\"Post not found\")\n    \n    post = posts_db[post_id]\n    if post[\"author\"] != current_user:\n        raise HTTPException(status_code=403, detail=\"Not authorized\")\n    \n    post[\"deleted\"] = True\n    return {\"message\": \"Post deleted\"}\n\n# ========== Comment Endpoints ==========\n@app.post(\"/api/posts/{post_id}/comments\", response_model=dict, status_code=201)\nasync def add_comment(post_id: int, comment: CommentCreate, current_user: str = Depends(get_current_user)):\n    if post_id \u003e= len(posts_db) or posts_db[post_id].get(\"deleted\"):\n        raise HTTPException(status_code=404, detail=\"Post not found\")\n    \n    new_comment = {\n        \"post_id\": post_id,\n        \"content\": comment.content,\n        \"author\": current_user,\n        \"created_at\": datetime.now().isoformat()\n    }\n    comments_db.append(new_comment)\n    return {\"message\": \"Comment added\", \"comment\": new_comment}\n\nif __name__ == \"__main__\":\n    import uvicorn\n    print(\"Blog API running at http://localhost:8000\")\n    print(\"Interactive docs at http://localhost:8000/docs\")\n    uvicorn.run(app, host=\"0.0.0.0\", port=8000)",
                                "language":  "python"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "Understanding the Authentication Flow",
                                "content":  "**How OAuth2 + JWT token authentication works in FastAPI:**\n\n**1. Registration Flow:**\n```\nClient                          Server\n  |                               |\n  |-- POST /register ------------\u003e|\n  |   {username, email, pass}     |\n  |                               |-- Validate with Pydantic\n  |                               |-- Hash password (bcrypt)\n  |                               |-- Store user\n  |\u003c------------ 201 Created -----|   \n  |   {message: \u0027registered\u0027}     |\n```\n\n**2. Login Flow (OAuth2PasswordRequestForm):**\n```\nClient                          Server\n  |                               |\n  |-- POST /login ---------------\u003e|\n  |   (form data: username, pwd)  |\n  |                               |-- Verify credentials\n  |                               |-- Create JWT token\n  |\u003c------------ 200 OK ---------|   \n  |   {access_token, token_type}  |\n```\n\n**3. Authenticated Request (Depends):**\n```\nClient                          Server\n  |                               |\n  |-- POST /posts ---------------\u003e|\n  |   Header: Bearer \u003cjwt\u003e        |\n  |   {title, content}            |\n  |                               |-- Depends(get_current_user)\n  |                               |-- Decode JWT, get user\n  |                               |-- Create post\n  |\u003c------------ 201 Created -----|   \n  |   {post: {...}}               |\n```\n\n**FastAPI security features:**\n- **Pydantic validation** - Automatic input validation\n- **OAuth2PasswordBearer** - Standard OAuth2 flow\n- **Depends()** - Dependency injection for auth\n- **HTTPException** - Proper error responses\n- **Auto-generated docs** - /docs shows auth requirements\n\n**Authorization levels:**\n- **Public**: Anyone can access (GET /api/posts)\n- **Authenticated**: Requires Depends(get_current_user)\n- **Owner only**: Check current_user == resource.author"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Testing the Blog API",
                                "content":  "**Test your API thoroughly before deployment!**\n\nThese tests verify:\n- User registration and login\n- Token authentication\n- Post creation with auth\n- Authorization (can\u0027t edit others\u0027 posts)",
                                "code":  "# tests/test_blog_api.py\n# Install: pip install pytest httpx pytest-asyncio\nimport pytest\nfrom httpx import AsyncClient, ASGITransport\nfrom main import app, users_db, posts_db, comments_db\n\n@pytest.fixture(autouse=True)\ndef reset_data():\n    \"\"\"Reset database before each test.\"\"\"\n    users_db.clear()\n    posts_db.clear()\n    comments_db.clear()\n    yield\n\n@pytest.fixture\nasync def client():\n    \"\"\"Create async test client.\"\"\"\n    async with AsyncClient(transport=ASGITransport(app=app), base_url=\"http://test\") as ac:\n        yield ac\n\n@pytest.fixture\nasync def auth_headers(client):\n    \"\"\"Create user and return auth headers.\"\"\"\n    # Register user\n    await client.post(\"/api/auth/register\", json={\n        \"username\": \"testuser\",\n        \"email\": \"test@example.com\",\n        \"password\": \"password123\"\n    })\n    \n    # Login (OAuth2 uses form data)\n    response = await client.post(\"/api/auth/login\", data={\n        \"username\": \"testuser\",\n        \"password\": \"password123\"\n    })\n    token = response.json()[\"access_token\"]\n    return {\"Authorization\": f\"Bearer {token}\"}\n\n# ========== Auth Tests ==========\n@pytest.mark.asyncio\nasync def test_register_success(client):\n    \"\"\"Test successful user registration.\"\"\"\n    response = await client.post(\"/api/auth/register\", json={\n        \"username\": \"alice\",\n        \"email\": \"alice@example.com\",\n        \"password\": \"secret123\"\n    })\n    \n    assert response.status_code == 201\n    assert response.json()[\"user\"][\"username\"] == \"alice\"\n\n@pytest.mark.asyncio\nasync def test_register_duplicate(client):\n    \"\"\"Test duplicate registration fails.\"\"\"\n    await client.post(\"/api/auth/register\", json={\n        \"username\": \"alice\", \"email\": \"a@b.com\", \"password\": \"secret123\"\n    })\n    response = await client.post(\"/api/auth/register\", json={\n        \"username\": \"alice\", \"email\": \"c@d.com\", \"password\": \"secret123\"\n    })\n    \n    assert response.status_code == 400\n\n@pytest.mark.asyncio\nasync def test_login_success(client):\n    \"\"\"Test login returns token.\"\"\"\n    await client.post(\"/api/auth/register\", json={\n        \"username\": \"alice\", \"email\": \"a@b.com\", \"password\": \"secret123\"\n    })\n    response = await client.post(\"/api/auth/login\", data={\n        \"username\": \"alice\", \"password\": \"secret123\"\n    })\n    \n    assert response.status_code == 200\n    assert \"access_token\" in response.json()\n\n# ========== Post Tests ==========\n@pytest.mark.asyncio\nasync def test_create_post_authenticated(client, auth_headers):\n    \"\"\"Test creating post with valid token.\"\"\"\n    response = await client.post(\n        \"/api/posts\",\n        json={\"title\": \"Test Post Title\", \"content\": \"Content here\"},\n        headers=auth_headers\n    )\n    \n    assert response.status_code == 201\n    assert response.json()[\"post\"][\"title\"] == \"Test Post Title\"\n\n@pytest.mark.asyncio\nasync def test_create_post_unauthenticated(client):\n    \"\"\"Test creating post without token fails.\"\"\"\n    response = await client.post(\n        \"/api/posts\",\n        json={\"title\": \"Test Post\", \"content\": \"Content\"}\n    )\n    \n    assert response.status_code == 401\n\n@pytest.mark.asyncio\nasync def test_update_own_post(client, auth_headers):\n    \"\"\"Test updating own post.\"\"\"\n    await client.post(\n        \"/api/posts\",\n        json={\"title\": \"Original Title\", \"content\": \"Original\"},\n        headers=auth_headers\n    )\n    response = await client.put(\n        \"/api/posts/0\",\n        json={\"title\": \"Updated Title\", \"content\": \"Updated\"},\n        headers=auth_headers\n    )\n    \n    assert response.status_code == 200\n    assert response.json()[\"post\"][\"title\"] == \"Updated Title\"\n\n# Run: pytest tests/test_blog_api.py -v --asyncio-mode=auto",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **FastAPI combines many concepts**: routes, Pydantic validation, OAuth2 auth, error handling\n- **Depends()** injects authentication into routes cleanly\n- **Pydantic models** validate input automatically with clear errors\n- **OAuth2PasswordBearer** implements standard authentication flow\n- **HTTPException** returns proper error responses (400, 401, 403, 404)\n- **Test with httpx** + pytest-asyncio for async endpoints\n- **Authorization** ensures users can only modify their own data\n- **Auto-generated docs** at /docs show all endpoints and auth requirements\n- **In production**: use bcrypt (passlib), PostgreSQL, HTTPS, and environment variables\n- **You\u0027ve built a real API!** This is the foundation for web and mobile apps"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "14_12-challenge-1",
                           "title":  "Extend the Blog API",
                           "description":  "Extend the FastAPI Blog API with these features:\n\n1. Add a \u0027likes\u0027 system:\n   - POST /api/posts/{id}/like - Like a post (authenticated)\n   - Each user can only like a post once\n   - GET /api/posts/{id} should include like_count\n\n2. Add user profiles:\n   - GET /api/users/{username} - Get user\u0027s public profile\n   - Include: username, bio, post_count, joined date\n\n3. Add search:\n   - GET /api/posts?search=keyword - Filter posts by title/content",
                           "instructions":  "Extend the FastAPI Blog API with these features:\n\n1. Add a \u0027likes\u0027 system:\n   - POST /api/posts/{id}/like - Like a post (authenticated)\n   - Each user can only like a post once\n   - GET /api/posts/{id} should include like_count\n\n2. Add user profiles:\n   - GET /api/users/{username} - Get user\u0027s public profile\n   - Include: username, bio, post_count, joined date\n\n3. Add search:\n   - GET /api/posts?search=keyword - Filter posts by title/content",
                           "starterCode":  "# Start with the FastAPI Blog API from the lesson\n# Add your new features below\n\nfrom fastapi import FastAPI, Depends, HTTPException, Query\nfrom pydantic import BaseModel\nfrom typing import Optional, List\nfrom datetime import datetime\n\napp = FastAPI()\n\n# Data stores (from the lesson)\nusers_db = {}\nposts_db = []\ncomments_db = []\nlikes_db = []  # TODO: Store likes here\n\n# TODO: Add Pydantic models for responses\nclass UserProfile(BaseModel):\n    username: str\n    bio: str\n    post_count: int\n    joined: str\n\n# TODO: Add helper function to check if user already liked a post\ndef has_liked(username: str, post_id: int) -\u003e bool:\n    pass\n\n# TODO: Add POST /api/posts/{id}/like endpoint\n@app.post(\"/api/posts/{post_id}/like\")\nasync def like_post(post_id: int, current_user: str = Depends(get_current_user)):\n    pass\n\n# TODO: Add GET /api/users/{username} endpoint\n@app.get(\"/api/users/{username}\", response_model=UserProfile)\nasync def get_user_profile(username: str):\n    pass\n\n# TODO: Modify GET /api/posts to support ?search= parameter\n@app.get(\"/api/posts\")\nasync def get_posts(search: Optional[str] = Query(None)):\n    # Filter posts if search parameter provided\n    pass\n\n# Run with: uvicorn main:app --reload",
                           "solution":  "from fastapi import FastAPI, Depends, HTTPException, Query\nfrom fastapi.security import OAuth2PasswordBearer\nfrom pydantic import BaseModel\nfrom jose import jwt\nfrom passlib.context import CryptContext\nfrom datetime import datetime, timedelta\nfrom typing import Optional, List\n\napp = FastAPI(title=\"Extended Blog API\")\n\n# Security config\nSECRET_KEY = \"your-secret-key\"\nALGORITHM = \"HS256\"\npwd_context = CryptContext(schemes=[\"bcrypt\"], deprecated=\"auto\")\noauth2_scheme = OAuth2PasswordBearer(tokenUrl=\"api/auth/login\")\n\n# Pydantic models\nclass UserProfile(BaseModel):\n    username: str\n    bio: str\n    post_count: int\n    joined: str\n\nclass PostResponse(BaseModel):\n    id: int\n    title: str\n    content: str\n    author: str\n    created_at: str\n    like_count: int\n    comment_count: int\n\n# Data stores\nusers_db = {}\nposts_db = []\ncomments_db = []\nlikes_db = []\n\n# Auth helpers\nasync def get_current_user(token: str = Depends(oauth2_scheme)) -\u003e str:\n    try:\n        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])\n        return payload.get(\"sub\")\n    except:\n        raise HTTPException(status_code=401, detail=\"Invalid token\")\n\ndef has_liked(username: str, post_id: int) -\u003e bool:\n    return any(l[\"username\"] == username and l[\"post_id\"] == post_id for l in likes_db)\n\ndef get_like_count(post_id: int) -\u003e int:\n    return sum(1 for l in likes_db if l[\"post_id\"] == post_id)\n\n# Feature 1: Likes System\n@app.post(\"/api/posts/{post_id}/like\", status_code=201)\nasync def like_post(post_id: int, current_user: str = Depends(get_current_user)):\n    if post_id \u003e= len(posts_db) or posts_db[post_id].get(\"deleted\"):\n        raise HTTPException(status_code=404, detail=\"Post not found\")\n    \n    if has_liked(current_user, post_id):\n        raise HTTPException(status_code=400, detail=\"Already liked\")\n    \n    likes_db.append({\"username\": current_user, \"post_id\": post_id, \"created_at\": datetime.now().isoformat()})\n    return {\"message\": \"Post liked\", \"like_count\": get_like_count(post_id)}\n\n# Feature 2: User Profiles\n@app.get(\"/api/users/{username}\", response_model=UserProfile)\nasync def get_user_profile(username: str):\n    if username not in users_db:\n        raise HTTPException(status_code=404, detail=\"User not found\")\n    \n    user = users_db[username]\n    post_count = sum(1 for p in posts_db if p[\"author\"] == username and not p.get(\"deleted\"))\n    \n    return UserProfile(\n        username=user[\"username\"],\n        bio=user.get(\"bio\", \"\"),\n        post_count=post_count,\n        joined=user[\"created_at\"]\n    )\n\n# Feature 3: Search Posts\n@app.get(\"/api/posts\", response_model=List[PostResponse])\nasync def get_posts(search: Optional[str] = Query(None)):\n    result = []\n    search_lower = search.lower().strip() if search else None\n    \n    for i, post in enumerate(posts_db):\n        if post.get(\"deleted\"):\n            continue\n        \n        if search_lower:\n            if search_lower not in post[\"title\"].lower() and search_lower not in post[\"content\"].lower():\n                continue\n        \n        result.append(PostResponse(\n            id=i,\n            title=post[\"title\"],\n            content=post[\"content\"][:100] + \"...\" if len(post[\"content\"]) \u003e 100 else post[\"content\"],\n            author=post[\"author\"],\n            created_at=post[\"created_at\"],\n            like_count=get_like_count(i),\n            comment_count=sum(1 for c in comments_db if c[\"post_id\"] == i)\n        ))\n    \n    return result\n\n# Get single post with like_count\n@app.get(\"/api/posts/{post_id}\")\nasync def get_post(post_id: int):\n    if post_id \u003e= len(posts_db) or posts_db[post_id].get(\"deleted\"):\n        raise HTTPException(status_code=404, detail=\"Post not found\")\n    \n    post = posts_db[post_id]\n    post_comments = [c for c in comments_db if c[\"post_id\"] == post_id]\n    \n    return {\n        **post,\n        \"id\": post_id,\n        \"like_count\": get_like_count(post_id),\n        \"comments\": post_comments\n    }\n\nif __name__ == \"__main__\":\n    import uvicorn\n    print(\"Extended Blog API - http://localhost:8000/docs\")\n    uvicorn.run(app, host=\"0.0.0.0\", port=8000)",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use Depends(get_current_user) for auth. Use Query(None) for optional search parameter. Use HTTPException for errors. Store likes as {\u0027post_id\u0027: id, \u0027username\u0027: user}."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Allowing users to like the same post multiple times",
                                                      "consequence":  "Inflated like counts",
                                                      "correction":  "Check if user already liked before adding new like"
                                                  },
                                                  {
                                                      "mistake":  "Not handling case-insensitive search",
                                                      "consequence":  "Users may not find posts with different capitalization",
                                                      "correction":  "Use .lower() on both search term and post content"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "beginner",
    "title":  "Mini-Project: Complete Blog API with Authentication",
    "estimatedMinutes":  30
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "python Mini-Project: Complete Blog API with Authentication 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "module-13-lesson-06",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

