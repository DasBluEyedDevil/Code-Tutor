# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** HTTP & Web APIs
- **Lesson:** FastAPI Advanced Patterns (ID: 14_10)
- **Difficulty:** advanced
- **Estimated Time:** 40 minutes

## Current Lesson Content

{
    "id":  "14_10",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Dependency Injection",
                                "content":  "**What is Dependency Injection?**\n\nInstead of creating dependencies inside functions, you *inject* them:\n\n```python\nfrom fastapi import Depends\n\n# Dependency function\ndef get_db():\n    db = DatabaseSession()\n    try:\n        yield db  # Provide to endpoint\n    finally:\n        db.close()  # Cleanup after\n\n# Use the dependency\n@app.get(\"/users/\")\ndef get_users(db: Session = Depends(get_db)):\n    return db.query(User).all()\n```\n\n**Benefits:**\n- Reusable logic (auth, db, logging)\n- Automatic cleanup\n- Easy testing (swap dependencies)\n- Clear separation of concerns"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Authentication Dependency",
                                "content":  "Dependencies with Depends() inject shared logic like authentication. The verify_token function extracts and validates JWT tokens from headers, and can be reused across multiple endpoints. Invalid tokens raise HTTPException(401).",
                                "code":  "from fastapi import Depends, HTTPException, Header\n\nasync def verify_token(authorization: str = Header(...)):\n    \"\"\"Verify JWT token from Authorization header\"\"\"\n    if not authorization.startswith(\"Bearer \"):\n        raise HTTPException(401, \"Invalid auth header\")\n    \n    token = authorization.split(\" \")[1]\n    try:\n        payload = jwt.decode(token, SECRET_KEY)\n        return payload[\"user_id\"]\n    except jwt.JWTError:\n        raise HTTPException(401, \"Invalid token\")\n\n@app.get(\"/me/\")\ndef get_current_user(user_id: int = Depends(verify_token)):\n    # Only runs if token is valid!\n    return {\"user_id\": user_id}\n\n@app.get(\"/admin/\")\ndef admin_only(\n    user_id: int = Depends(verify_token),\n    is_admin: bool = Depends(check_admin)\n):\n    # Multiple dependencies!\n    return {\"admin\": True}",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Background Tasks and Middleware",
                                "content":  "**Background tasks** - run after response:\n```python\nfrom fastapi import BackgroundTasks\n\ndef send_email(email: str, message: str):\n    # Slow operation\n    time.sleep(5)\n    print(f\"Sent to {email}\")\n\n@app.post(\"/signup/\")\ndef signup(email: str, background: BackgroundTasks):\n    create_user(email)\n    background.add_task(send_email, email, \"Welcome!\")\n    return {\"status\": \"created\"}  # Returns immediately\n```\n\n**Middleware** - runs on every request:\n```python\nfrom fastapi.middleware.cors import CORSMiddleware\n\napp.add_middleware(\n    CORSMiddleware,\n    allow_origins=[\"http://localhost:3000\"],\n    allow_methods=[\"*\"],\n    allow_headers=[\"*\"]\n)\n\n@app.middleware(\"http\")\nasync def log_requests(request, call_next):\n    start = time.time()\n    response = await call_next(request)\n    duration = time.time() - start\n    print(f\"{request.method} {request.url} - {duration:.2f}s\")\n    return response\n```"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "14_10-challenge",
                           "title":  "Create Auth Dependency",
                           "description":  "Build a dependency that validates API keys.",
                           "instructions":  "Create a dependency function that checks for a valid API key in the X-API-Key header.",
                           "starterCode":  "from fastapi import FastAPI, Depends, HTTPException, Header\n\napp = FastAPI()\n\n# Valid API keys (in real app, store in database)\nVALID_API_KEYS = {\"key123\", \"key456\"}\n\n# TODO: Create dependency function\ndef verify_api_key(x_api_key: str = Header(...)):\n    # Check if key is valid\n    # Raise HTTPException(403) if invalid\n    # Return the key if valid\n    pass\n\n@app.get(\"/public/\")\ndef public_endpoint():\n    return {\"message\": \"Anyone can see this\"}\n\n# TODO: Add protected endpoint that uses verify_api_key\n@app.get(\"/protected/\")\ndef protected_endpoint():\n    pass\n\nprint(\"API Key auth demo\")",
                           "solution":  "from fastapi import FastAPI, Depends, HTTPException, Header\n\napp = FastAPI()\n\nVALID_API_KEYS = {\"key123\", \"key456\"}\n\ndef verify_api_key(x_api_key: str = Header(...)):\n    \"\"\"Dependency to verify API key\"\"\"\n    if x_api_key not in VALID_API_KEYS:\n        raise HTTPException(\n            status_code=403,\n            detail=\"Invalid API key\"\n        )\n    return x_api_key\n\n@app.get(\"/public/\")\ndef public_endpoint():\n    return {\"message\": \"Anyone can see this\"}\n\n@app.get(\"/protected/\")\ndef protected_endpoint(api_key: str = Depends(verify_api_key)):\n    return {\n        \"message\": \"Secret data!\",\n        \"authenticated_with\": api_key[:4] + \"...\"\n    }\n\n@app.get(\"/admin/\")\ndef admin_endpoint(api_key: str = Depends(verify_api_key)):\n    # Can reuse the same dependency\n    return {\"message\": \"Admin area\", \"key\": api_key}\n\nprint(\"API Key auth ready!\")\nprint(\"Test: curl -H \u0027X-API-Key: key123\u0027 localhost:8000/protected/\")",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Dependency validates API keys",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Use Header(...) to require the header. Check if key is in VALID_API_KEYS set."
                                         }
                                     ],
                           "commonMistakes":  [

                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "FastAPI Advanced Patterns",
    "estimatedMinutes":  40
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
- Search for "python FastAPI Advanced Patterns 2024 2025" to find latest practices
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
  "lessonId": "14_10",
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

