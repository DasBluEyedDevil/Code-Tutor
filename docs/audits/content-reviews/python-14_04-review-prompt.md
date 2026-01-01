# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** HTTP & Web APIs
- **Lesson:** Authentication and API Security (ID: 14_04)
- **Difficulty:** advanced
- **Estimated Time:** 45 minutes

## Current Lesson Content

{
    "id":  "14_04",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: Securing Your API",
                                "content":  "**API Security = Protecting your data and endpoints**\n\n**Think of it like a building:**\n- **No Security:** Anyone can walk in and do anything\n- **API Key:** Guests need a key card to enter\n- **JWT Token:** Temporary access badge with permissions\n- **Password Hashing:** Storing passwords safely (not plain text)\n\n**Authentication Methods:**\n\n**1. API Keys** 🔑\n- Simple key in header\n- Good for: Server-to-server communication\n- Example: `X-API-Key: secret-key-123`\n\n**2. JWT (JSON Web Tokens)** 🎫\n- Encoded token with user info\n- Good for: User authentication\n- Expires after time limit\n- Contains: user ID, permissions, expiration\n\n**3. Basic Auth** 🔒\n- Username:password in header\n- Simple but less secure\n- Should use HTTPS\n\n**Security Best Practices:**\n\n1. **Never store plain passwords** - Always hash\n2. **Use HTTPS** - Encrypt data in transit\n3. **Validate all input** - Prevent injection attacks\n4. **Rate limiting** - Prevent abuse\n5. **CORS** - Control which domains can access API\n6. **Token expiration** - Tokens should expire\n7. **Least privilege** - Give minimum permissions needed"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: API Key Authentication with FastAPI",
                                "content":  "**Security implementation patterns with FastAPI:**\n\n**1. API Key Authentication with Depends:**\n```python\nfrom fastapi.security import APIKeyHeader\n\napi_key_header = APIKeyHeader(name=\u0027X-API-Key\u0027)\n\nasync def verify_api_key(api_key: str = Depends(api_key_header)):\n    if api_key not in API_KEYS:\n        raise HTTPException(status_code=401)\n    return API_KEYS[api_key]\n```\n\n**2. Password Hashing:**\n```python\n# NEVER store plain passwords!\nhashed = hash_password(password)  # Store this\nif verify_password(stored, provided):\n    # Password correct\n```\n\n**3. Rate Limiting:**\n- Track requests per time window\n- Return 429 when limit exceeded\n- Use slowapi package for production\n\n**4. CORS with FastAPI:**\n```python\nfrom fastapi.middleware.cors import CORSMiddleware\napp.add_middleware(CORSMiddleware, allow_origins=[\u0027*\u0027])\n```\n\n**5. Input Validation with Pydantic:**\n- Automatic validation with type hints\n- EmailStr for email validation\n- Field() for constraints",
                                "code":  "from fastapi import FastAPI, Depends, HTTPException, Header, Request\nfrom fastapi.security import APIKeyHeader\nfrom fastapi.middleware.cors import CORSMiddleware\nfrom pydantic import BaseModel, EmailStr, Field\nimport secrets\nimport hashlib\nimport os\nfrom datetime import datetime, timedelta\nfrom collections import defaultdict\nimport time\n\napp = FastAPI()\n\n# CORS configuration\napp.add_middleware(\n    CORSMiddleware,\n    allow_origins=[\u0027*\u0027],  # Configure for production\n    allow_methods=[\u0027*\u0027],\n    allow_headers=[\u0027*\u0027],\n)\n\n# Simulated API keys database\nAPI_KEYS = {\n    \u0027dev-key-123\u0027: {\u0027name\u0027: \u0027Development\u0027, \u0027permissions\u0027: [\u0027read\u0027, \u0027write\u0027]},\n    \u0027readonly-456\u0027: {\u0027name\u0027: \u0027ReadOnly\u0027, \u0027permissions\u0027: [\u0027read\u0027]}\n}\n\nprint(\"=== API Key Authentication ===\")\n\n# FastAPI\u0027s built-in API key header security\napi_key_header = APIKeyHeader(name=\u0027X-API-Key\u0027, auto_error=False)\n\nasync def verify_api_key(api_key: str = Depends(api_key_header)):\n    \"\"\"Dependency to verify API key\"\"\"\n    if not api_key:\n        raise HTTPException(status_code=401, detail=\u0027API key required\u0027)\n    if api_key not in API_KEYS:\n        raise HTTPException(status_code=401, detail=\u0027Invalid API key\u0027)\n    return API_KEYS[api_key]\n\ndef require_permission(permission: str):\n    \"\"\"Factory for permission-checking dependency\"\"\"\n    async def check_permission(api_key_info: dict = Depends(verify_api_key)):\n        if permission not in api_key_info[\u0027permissions\u0027]:\n            raise HTTPException(\n                status_code=403,\n                detail=f\u0027Permission denied: {permission} required\u0027\n            )\n        return api_key_info\n    return check_permission\n\n@app.get(\u0027/api/public\u0027)\ndef public():\n    \"\"\"Public endpoint - no auth required\"\"\"\n    return {\u0027message\u0027: \u0027This is public\u0027}\n\n@app.get(\u0027/api/protected\u0027)\ndef protected(api_key_info: dict = Depends(verify_api_key)):\n    \"\"\"Protected endpoint - requires API key\"\"\"\n    return {\n        \u0027message\u0027: \u0027You have access!\u0027,\n        \u0027key_name\u0027: api_key_info[\u0027name\u0027]\n    }\n\n@app.get(\u0027/api/admin\u0027)\ndef admin(api_key_info: dict = Depends(require_permission(\u0027write\u0027))):\n    \"\"\"Admin endpoint - requires write permission\"\"\"\n    return {\n        \u0027message\u0027: \u0027Admin access granted\u0027,\n        \u0027permissions\u0027: api_key_info[\u0027permissions\u0027]\n    }\n\nprint(\"\\n=== Password Hashing ===\")\n\ndef hash_password(password: str) -\u003e bytes:\n    \"\"\"Hash password with salt\"\"\"\n    salt = os.urandom(32)\n    pwdhash = hashlib.pbkdf2_hmac(\n        \u0027sha256\u0027,\n        password.encode(\u0027utf-8\u0027),\n        salt,\n        100000\n    )\n    return salt + pwdhash\n\ndef verify_password(stored_password: bytes, provided_password: str) -\u003e bool:\n    \"\"\"Verify password against hash\"\"\"\n    salt = stored_password[:32]\n    stored_hash = stored_password[32:]\n    pwdhash = hashlib.pbkdf2_hmac(\n        \u0027sha256\u0027,\n        provided_password.encode(\u0027utf-8\u0027),\n        salt,\n        100000\n    )\n    return pwdhash == stored_hash\n\n# Demo password hashing\noriginal_password = \"MySecurePassword123!\"\nprint(f\"Original password: {original_password}\")\n\nhashed = hash_password(original_password)\nprint(f\"Hashed password length: {len(hashed)} bytes\")\n\nif verify_password(hashed, original_password):\n    print(\"Correct password verified\")\n\nif not verify_password(hashed, \"WrongPassword\"):\n    print(\"Wrong password rejected\")\n\nprint(\"\\n=== Rate Limiting ===\")\n\nclass RateLimiter:\n    \"\"\"Simple rate limiter\"\"\"\n    \n    def __init__(self, max_requests: int = 10, window: int = 60):\n        self.max_requests = max_requests\n        self.window = window\n        self.requests = defaultdict(list)\n    \n    def is_allowed(self, key: str) -\u003e bool:\n        \"\"\"Check if request is allowed\"\"\"\n        now = time.time()\n        self.requests[key] = [\n            req_time for req_time in self.requests[key]\n            if now - req_time \u003c self.window\n        ]\n        if len(self.requests[key]) \u003e= self.max_requests:\n            return False\n        self.requests[key].append(now)\n        return True\n\nrate_limiter = RateLimiter(max_requests=5, window=60)\n\nasync def check_rate_limit(request: Request):\n    \"\"\"Dependency for rate limiting\"\"\"\n    client_ip = request.client.host if request.client else \u0027unknown\u0027\n    if not rate_limiter.is_allowed(client_ip):\n        raise HTTPException(\n            status_code=429,\n            detail=\u0027Rate limit exceeded\u0027,\n            headers={\u0027Retry-After\u0027: \u002760\u0027}\n        )\n    return True\n\n@app.get(\u0027/api/limited\u0027)\ndef limited(_: bool = Depends(check_rate_limit)):\n    \"\"\"Rate limited endpoint\"\"\"\n    return {\u0027message\u0027: \u0027Request successful\u0027}\n\nprint(\"\\n=== Input Validation with Pydantic ===\")\n\nclass UserCreate(BaseModel):\n    \"\"\"User creation with automatic validation\"\"\"\n    email: EmailStr  # Automatically validates email format\n    name: str = Field(..., min_length=1, max_length=100)\n    \n    class Config:\n        str_strip_whitespace = True  # Auto-strip whitespace\n\n@app.post(\u0027/api/users\u0027, status_code=201)\ndef create_user(\n    user: UserCreate,\n    api_key_info: dict = Depends(verify_api_key)\n):\n    \"\"\"Create user - Pydantic validates automatically\"\"\"\n    return {\n        \u0027message\u0027: \u0027User created\u0027,\n        \u0027user\u0027: {\u0027name\u0027: user.name, \u0027email\u0027: user.email}\n    }\n\nif __name__ == \u0027__main__\u0027:\n    print(\"\\n=== FastAPI Security Features ===\")\n    print(\"\\nFeatures implemented:\")\n    print(\"  API key authentication with Depends()\")\n    print(\"  Permission-based access control\")\n    print(\"  Password hashing (PBKDF2)\")\n    print(\"  Rate limiting as dependency\")\n    print(\"  CORS middleware\")\n    print(\"  Input validation with Pydantic\")\n    print(\"\\nRun with: uvicorn main:app --reload\")\n    print(\"API docs at: http://localhost:8000/docs\")",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Syntax Breakdown",
                                "content":  "**FastAPI API Key Authentication Pattern:**\n\n```python\nfrom fastapi.security import APIKeyHeader\n\napi_key_header = APIKeyHeader(name=\u0027X-API-Key\u0027)\n\nasync def verify_api_key(api_key: str = Depends(api_key_header)):\n    if api_key not in VALID_KEYS:\n        raise HTTPException(status_code=401)\n    return VALID_KEYS[api_key]\n\n@app.get(\u0027/protected\u0027)\ndef protected(key_info = Depends(verify_api_key)):\n    return {\u0027data\u0027: \u0027secret\u0027}\n```\n\n**Password Hashing:**\n\n```python\nimport hashlib\nimport os\n\n# Hash password with salt\nsalt = os.urandom(32)\nhashed = hashlib.pbkdf2_hmac(\u0027sha256\u0027, password.encode(), salt, 100000)\nstored = salt + hashed  # Store this\n\n# Verify password\nsalt = stored[:32]\nstored_hash = stored[32:]\ntest_hash = hashlib.pbkdf2_hmac(\u0027sha256\u0027, provided.encode(), salt, 100000)\nif test_hash == stored_hash:\n    # Password correct\n```\n\n**Rate Limiting as Dependency:**\n\n```python\nasync def check_rate_limit(request: Request):\n    client_ip = request.client.host\n    if not rate_limiter.is_allowed(client_ip):\n        raise HTTPException(status_code=429)\n    return True\n\n@app.get(\u0027/limited\u0027)\ndef limited(_: bool = Depends(check_rate_limit)):\n    return {\u0027message\u0027: \u0027OK\u0027}\n```\n\n**CORS with FastAPI Middleware:**\n\n```python\nfrom fastapi.middleware.cors import CORSMiddleware\n\napp.add_middleware(\n    CORSMiddleware,\n    allow_origins=[\u0027https://yourdomain.com\u0027],\n    allow_methods=[\u0027*\u0027],\n    allow_headers=[\u0027*\u0027],\n)\n```"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: FastAPI OAuth2 with JWT",
                                "content":  "**JWT Authentication Flow with FastAPI:**\n\n**1. Login (OAuth2 Password Flow):**\n```\nUser → POST /token (form: username, password)\nAPI → Verify credentials\nAPI → Create JWT token\nAPI → Return access_token\n```\n\n**2. Authenticated Request:**\n```\nUser → GET /api/protected\nHeader: Authorization: Bearer \u003ctoken\u003e\nAPI → OAuth2PasswordBearer extracts token\nAPI → Verify signature and expiration\nAPI → Process request\n```\n\n**3. Token Structure:**\n```json\n{\n  \"sub\": \"alice@example.com\",\n  \"role\": \"admin\",\n  \"exp\": 1234567890\n}\n```\n\n**FastAPI Security Benefits:**\n- Built-in OAuth2 support\n- Automatic Swagger UI auth button\n- Dependency injection for auth\n- Type-safe with Pydantic\n- Auto-generated API docs\n\n**Best Practices:**\n- Use OAuth2PasswordBearer for token extraction\n- python-jose for JWT encoding/decoding\n- passlib for password hashing",
                                "code":  "from fastapi import FastAPI, Depends, HTTPException, status\nfrom fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm\nfrom pydantic import BaseModel, EmailStr\nfrom jose import JWTError, jwt\nfrom datetime import datetime, timedelta\nimport hashlib\nimport os\nfrom typing import Optional\n\napp = FastAPI()\n\n# Configuration\nSECRET_KEY = \u0027your-secret-key-change-this-in-production\u0027\nALGORITHM = \u0027HS256\u0027\nACCESS_TOKEN_EXPIRE_MINUTES = 30\n\n# OAuth2 scheme - tells FastAPI where to find the token\noauth2_scheme = OAuth2PasswordBearer(tokenUrl=\u0027token\u0027)\n\n# Simulated user database\nUSERS = {\n    \u0027alice@example.com\u0027: {\n        \u0027id\u0027: 1,\n        \u0027name\u0027: \u0027Alice\u0027,\n        \u0027email\u0027: \u0027alice@example.com\u0027,\n        \u0027password_hash\u0027: None,\n        \u0027role\u0027: \u0027admin\u0027\n    },\n    \u0027bob@example.com\u0027: {\n        \u0027id\u0027: 2,\n        \u0027name\u0027: \u0027Bob\u0027,\n        \u0027email\u0027: \u0027bob@example.com\u0027,\n        \u0027password_hash\u0027: None,\n        \u0027role\u0027: \u0027user\u0027\n    }\n}\n\nprint(\"=== FastAPI OAuth2 + JWT Authentication ===\")\n\n# Pydantic models\nclass Token(BaseModel):\n    access_token: str\n    token_type: str\n\nclass TokenData(BaseModel):\n    email: Optional[str] = None\n    role: Optional[str] = None\n\nclass User(BaseModel):\n    id: int\n    name: str\n    email: EmailStr\n    role: str\n\ndef hash_password(password: str) -\u003e bytes:\n    \"\"\"Hash password with salt\"\"\"\n    salt = b\u0027demo-salt-change-in-production\u0027\n    return hashlib.pbkdf2_hmac(\u0027sha256\u0027, password.encode(), salt, 100000)\n\ndef verify_password(stored_hash: bytes, password: str) -\u003e bool:\n    \"\"\"Verify password against hash\"\"\"\n    return stored_hash == hash_password(password)\n\n# Set passwords\nUSERS[\u0027alice@example.com\u0027][\u0027password_hash\u0027] = hash_password(\u0027password123\u0027)\nUSERS[\u0027bob@example.com\u0027][\u0027password_hash\u0027] = hash_password(\u0027password456\u0027)\n\ndef create_access_token(data: dict, expires_delta: Optional[timedelta] = None):\n    \"\"\"Create JWT access token\"\"\"\n    to_encode = data.copy()\n    expire = datetime.utcnow() + (expires_delta or timedelta(minutes=15))\n    to_encode.update({\u0027exp\u0027: expire})\n    return jwt.encode(to_encode, SECRET_KEY, algorithm=ALGORITHM)\n\nasync def get_current_user(token: str = Depends(oauth2_scheme)) -\u003e User:\n    \"\"\"Dependency to get current user from token\"\"\"\n    credentials_exception = HTTPException(\n        status_code=status.HTTP_401_UNAUTHORIZED,\n        detail=\u0027Could not validate credentials\u0027,\n        headers={\u0027WWW-Authenticate\u0027: \u0027Bearer\u0027},\n    )\n    try:\n        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])\n        email: str = payload.get(\u0027sub\u0027)\n        if email is None:\n            raise credentials_exception\n    except JWTError:\n        raise credentials_exception\n    \n    user = USERS.get(email)\n    if user is None:\n        raise credentials_exception\n    \n    return User(\n        id=user[\u0027id\u0027],\n        name=user[\u0027name\u0027],\n        email=user[\u0027email\u0027],\n        role=user[\u0027role\u0027]\n    )\n\ndef require_role(required_role: str):\n    \"\"\"Factory for role-checking dependency\"\"\"\n    async def role_checker(current_user: User = Depends(get_current_user)):\n        if current_user.role != required_role:\n            raise HTTPException(\n                status_code=status.HTTP_403_FORBIDDEN,\n                detail=f\u0027{required_role} role required\u0027\n            )\n        return current_user\n    return role_checker\n\n@app.post(\u0027/token\u0027, response_model=Token)\nasync def login(form_data: OAuth2PasswordRequestForm = Depends()):\n    \"\"\"OAuth2 login endpoint - returns JWT token\"\"\"\n    user = USERS.get(form_data.username)\n    if not user:\n        raise HTTPException(\n            status_code=status.HTTP_401_UNAUTHORIZED,\n            detail=\u0027Incorrect email or password\u0027,\n            headers={\u0027WWW-Authenticate\u0027: \u0027Bearer\u0027},\n        )\n    \n    if not verify_password(user[\u0027password_hash\u0027], form_data.password):\n        raise HTTPException(\n            status_code=status.HTTP_401_UNAUTHORIZED,\n            detail=\u0027Incorrect email or password\u0027,\n            headers={\u0027WWW-Authenticate\u0027: \u0027Bearer\u0027},\n        )\n    \n    access_token = create_access_token(\n        data={\u0027sub\u0027: user[\u0027email\u0027], \u0027role\u0027: user[\u0027role\u0027]},\n        expires_delta=timedelta(minutes=ACCESS_TOKEN_EXPIRE_MINUTES)\n    )\n    \n    return {\u0027access_token\u0027: access_token, \u0027token_type\u0027: \u0027bearer\u0027}\n\n@app.get(\u0027/api/me\u0027, response_model=User)\nasync def get_me(current_user: User = Depends(get_current_user)):\n    \"\"\"Get current user info from token\"\"\"\n    return current_user\n\n@app.get(\u0027/api/admin/users\u0027)\nasync def admin_get_users(current_user: User = Depends(require_role(\u0027admin\u0027))):\n    \"\"\"Admin only endpoint\"\"\"\n    return {\n        \u0027users\u0027: [\n            {\u0027id\u0027: u[\u0027id\u0027], \u0027name\u0027: u[\u0027name\u0027], \u0027role\u0027: u[\u0027role\u0027]}\n            for u in USERS.values()\n        ]\n    }\n\n@app.get(\u0027/api/protected\u0027)\nasync def protected_route(current_user: User = Depends(get_current_user)):\n    \"\"\"Any authenticated user can access\"\"\"\n    return {\n        \u0027message\u0027: f\u0027Hello {current_user.name}!\u0027,\n        \u0027your_role\u0027: current_user.role\n    }\n\nif __name__ == \u0027__main__\u0027:\n    print(\"\\n=== FastAPI OAuth2 + JWT API ===\")\n    print(\"\\nEndpoints:\")\n    print(\"  POST /token          - Login (OAuth2 form)\")\n    print(\"  GET  /api/me         - Get current user\")\n    print(\"  GET  /api/protected  - Protected route\")\n    print(\"  GET  /api/admin/users - Admin only\")\n    print(\"\\nRun with: uvicorn main:app --reload\")\n    print(\"\\nSwagger UI: http://localhost:8000/docs\")\n    print(\"  Click \u0027Authorize\u0027 button to login!\")\n    print(\"\\nTest credentials:\")\n    print(\"  alice@example.com / password123 (admin)\")\n    print(\"  bob@example.com / password456 (user)\")",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **Never store plain passwords** - Always hash with salt (use passlib in production)\n- **Use OAuth2PasswordBearer** - FastAPI\u0027s built-in security for token extraction\n- **python-jose for JWT** - `pip install python-jose[cryptography]` for token encoding/decoding\n- **Dependency injection for auth** - Use Depends(get_current_user) for protected routes\n- **Role-based access** - Create dependency factories like require_role(\u0027admin\u0027)\n- **Pydantic for validation** - EmailStr, Field() constraints handle input validation\n- **Rate limiting as dependency** - Implement as async dependency function\n- **CORSMiddleware** - FastAPI\u0027s built-in CORS handling\n- **HTTPS in production** - Encrypt data in transit\n- **Swagger UI auth** - FastAPI automatically adds Authorize button for OAuth2"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "13_04-challenge-4",
                           "title":  "Interactive Exercise",
                           "description":  "Create a secure FastAPI user registration and login system:\n- POST /api/register - Create user with hashed password (use Pydantic for validation)\n- POST /token - OAuth2 login endpoint, return JWT token\n- GET /api/profile - Get user profile (requires OAuth2 token)\n- Use Pydantic EmailStr for email validation\n- Use Field() for password strength (min 8 chars)\n- Use OAuth2PasswordBearer for token extraction",
                           "instructions":  "Create a secure FastAPI user registration and login system:\n- POST /api/register - Create user with hashed password\n- POST /token - OAuth2 login endpoint, return JWT token\n- GET /api/profile - Get user profile (requires OAuth2 token)\n- Use Pydantic for validation\n- Use FastAPI\u0027s OAuth2PasswordBearer",
                           "starterCode":  "from fastapi import FastAPI, Depends, HTTPException, status\nfrom fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm\nfrom pydantic import BaseModel, EmailStr, Field\nfrom jose import jwt\nimport hashlib\nimport os\nfrom datetime import datetime, timedelta\nfrom typing import Optional\n\napp = FastAPI()\nSECRET_KEY = \u0027change-this-secret-key\u0027\nALGORITHM = \u0027HS256\u0027\n\n# OAuth2 scheme\noauth2_scheme = OAuth2PasswordBearer(tokenUrl=\u0027token\u0027)\n\nusers = {}  # In-memory user storage\n\n# TODO: Create Pydantic models for UserCreate and User\n\n# TODO: Implement password hashing\ndef hash_password(password: str) -\u003e bytes:\n    pass\n\n# TODO: Implement password verification\ndef verify_password(stored_hash: bytes, password: str) -\u003e bool:\n    pass\n\n# TODO: Implement JWT token creation\ndef create_access_token(data: dict) -\u003e str:\n    pass\n\n# TODO: Implement get_current_user dependency\n\n# TODO: Implement POST /api/register endpoint\n\n# TODO: Implement POST /token endpoint (OAuth2)\n\n# TODO: Implement GET /api/profile endpoint\n\n# Run with: uvicorn main:app --reload",
                           "solution":  "from fastapi import FastAPI, Depends, HTTPException, status\nfrom fastapi.security import OAuth2PasswordBearer, OAuth2PasswordRequestForm\nfrom pydantic import BaseModel, EmailStr, Field\nfrom jose import JWTError, jwt\nimport hashlib\nimport os\nfrom datetime import datetime, timedelta\nfrom typing import Optional\n\napp = FastAPI()\nSECRET_KEY = \u0027change-this-secret-key\u0027\nALGORITHM = \u0027HS256\u0027\nACCESS_TOKEN_EXPIRE_MINUTES = 30\n\noauth2_scheme = OAuth2PasswordBearer(tokenUrl=\u0027token\u0027)\n\nusers = {}  # email -\u003e user_data\n\n# Pydantic models\nclass UserCreate(BaseModel):\n    email: EmailStr\n    password: str = Field(..., min_length=8)\n\nclass User(BaseModel):\n    id: int\n    email: EmailStr\n\nclass Token(BaseModel):\n    access_token: str\n    token_type: str\n\ndef hash_password(password: str) -\u003e bytes:\n    \"\"\"Hash password with salt.\"\"\"\n    salt = os.urandom(32)\n    pwdhash = hashlib.pbkdf2_hmac(\u0027sha256\u0027, password.encode(), salt, 100000)\n    return salt + pwdhash\n\ndef verify_password(stored_hash: bytes, password: str) -\u003e bool:\n    \"\"\"Verify password against stored hash.\"\"\"\n    salt = stored_hash[:32]\n    stored_pw = stored_hash[32:]\n    test_hash = hashlib.pbkdf2_hmac(\u0027sha256\u0027, password.encode(), salt, 100000)\n    return test_hash == stored_pw\n\ndef create_access_token(data: dict) -\u003e str:\n    \"\"\"Create JWT access token.\"\"\"\n    to_encode = data.copy()\n    expire = datetime.utcnow() + timedelta(minutes=ACCESS_TOKEN_EXPIRE_MINUTES)\n    to_encode.update({\u0027exp\u0027: expire})\n    return jwt.encode(to_encode, SECRET_KEY, algorithm=ALGORITHM)\n\nasync def get_current_user(token: str = Depends(oauth2_scheme)) -\u003e User:\n    \"\"\"Dependency to get current user from token.\"\"\"\n    credentials_exception = HTTPException(\n        status_code=status.HTTP_401_UNAUTHORIZED,\n        detail=\u0027Could not validate credentials\u0027,\n        headers={\u0027WWW-Authenticate\u0027: \u0027Bearer\u0027},\n    )\n    try:\n        payload = jwt.decode(token, SECRET_KEY, algorithms=[ALGORITHM])\n        email: str = payload.get(\u0027sub\u0027)\n        if email is None:\n            raise credentials_exception\n    except JWTError:\n        raise credentials_exception\n    \n    user = users.get(email)\n    if user is None:\n        raise credentials_exception\n    return User(id=user[\u0027id\u0027], email=user[\u0027email\u0027])\n\n@app.post(\u0027/api/register\u0027, response_model=User, status_code=201)\nasync def register(user_data: UserCreate):\n    \"\"\"Register new user with validated email and password.\"\"\"\n    email = user_data.email.lower()\n    if email in users:\n        raise HTTPException(status_code=400, detail=\u0027User already exists\u0027)\n    \n    users[email] = {\n        \u0027id\u0027: len(users) + 1,\n        \u0027email\u0027: email,\n        \u0027password_hash\u0027: hash_password(user_data.password)\n    }\n    return User(id=users[email][\u0027id\u0027], email=email)\n\n@app.post(\u0027/token\u0027, response_model=Token)\nasync def login(form_data: OAuth2PasswordRequestForm = Depends()):\n    \"\"\"OAuth2 login - returns JWT token.\"\"\"\n    user = users.get(form_data.username.lower())\n    if not user or not verify_password(user[\u0027password_hash\u0027], form_data.password):\n        raise HTTPException(\n            status_code=status.HTTP_401_UNAUTHORIZED,\n            detail=\u0027Incorrect email or password\u0027,\n            headers={\u0027WWW-Authenticate\u0027: \u0027Bearer\u0027},\n        )\n    \n    access_token = create_access_token(data={\u0027sub\u0027: user[\u0027email\u0027]})\n    return {\u0027access_token\u0027: access_token, \u0027token_type\u0027: \u0027bearer\u0027}\n\n@app.get(\u0027/api/profile\u0027, response_model=User)\nasync def get_profile(current_user: User = Depends(get_current_user)):\n    \"\"\"Get current user profile (requires token).\"\"\"\n    return current_user\n\nif __name__ == \u0027__main__\u0027:\n    print(\u0027FastAPI OAuth2 Authentication\u0027)\n    print(\u0027Run with: uvicorn main:app --reload\u0027)\n    print(\u0027Swagger UI: http://localhost:8000/docs\u0027)",
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
                                             "text":  "Use OAuth2PasswordBearer(tokenUrl=\u0027token\u0027) for token extraction. Use OAuth2PasswordRequestForm for login form data. Use Depends(get_current_user) for protected routes."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting to use Depends() for OAuth2PasswordRequestForm",
                                                      "consequence":  "Form data not parsed correctly",
                                                      "correction":  "Use form_data: OAuth2PasswordRequestForm = Depends()"
                                                  },
                                                  {
                                                      "mistake":  "Not raising HTTPException for auth errors",
                                                      "consequence":  "Wrong status codes returned",
                                                      "correction":  "Use raise HTTPException(status_code=401, ...)"
                                                  },
                                                  {
                                                      "mistake":  "Missing WWW-Authenticate header in 401 responses",
                                                      "consequence":  "Not OAuth2 compliant",
                                                      "correction":  "Add headers={\u0027WWW-Authenticate\u0027: \u0027Bearer\u0027}"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Authentication and API Security",
    "estimatedMinutes":  45
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
- Search for "python Authentication and API Security 2024 2025" to find latest practices
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
  "lessonId": "14_04",
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

