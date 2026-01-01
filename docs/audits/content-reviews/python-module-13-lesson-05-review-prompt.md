# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** HTTP & Web APIs
- **Lesson:** API Testing and Documentation (ID: module-13-lesson-05)
- **Difficulty:** beginner
- **Estimated Time:** 30 minutes

## Current Lesson Content

{
    "id":  "module-13-lesson-05",
    "contentSections":  [
                            {
                                "type":  "THEORY",
                                "title":  "Why Test Your APIs?",
                                "content":  "**API testing ensures your endpoints work correctly before users find bugs.**\n\nThink of it like quality control in a factory - you test products before shipping them to customers. API testing catches issues early when they\u0027re cheaper to fix.\n\n**Types of API tests:**\n\n1. **Unit Tests** - Test individual functions in isolation\n   - Fast (milliseconds)\n   - Test business logic\n   - Mock external dependencies\n\n2. **Integration Tests** - Test API endpoints end-to-end\n   - Test HTTP requests/responses\n   - Verify status codes and JSON\n   - Test with real database (test DB)\n\n3. **Contract Tests** - Verify API matches documentation\n   - Schema validation\n   - Response structure checks\n\n**Why pytest for Flask testing?**\n- Simple, readable syntax\n- Powerful fixtures for setup/teardown\n- Great Flask integration with pytest-flask\n- Excellent assertion messages\n- Used by 70%+ of Python developers\n\n**What to test in APIs:**\n- Correct status codes (200, 201, 400, 404, 500)\n- Response JSON structure\n- Authentication/authorization\n- Input validation\n- Error handling\n- Edge cases (empty lists, invalid IDs)"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Testing Flask APIs with pytest",
                                "content":  "**Expected Output:**\n```\n============================= test session starts ==============================\ncollected 6 items\n\ntest_api.py ......                                                       [100%]\n\n============================== 6 passed in 0.15s ===============================\n```",
                                "code":  "# Install: pip install pytest pytest-flask\n\nimport pytest\nfrom flask import Flask, jsonify, request\n\n# ========== Sample API to Test ==========\napp = Flask(__name__)\n\n# In-memory data store\ntasks = [\n    {\u0027id\u0027: 1, \u0027title\u0027: \u0027Learn Python\u0027, \u0027completed\u0027: False},\n    {\u0027id\u0027: 2, \u0027title\u0027: \u0027Build API\u0027, \u0027completed\u0027: True}\n]\n\n@app.route(\u0027/api/tasks\u0027, methods=[\u0027GET\u0027])\ndef get_tasks():\n    return jsonify({\u0027tasks\u0027: tasks})\n\n@app.route(\u0027/api/tasks/\u003cint:task_id\u003e\u0027, methods=[\u0027GET\u0027])\ndef get_task(task_id):\n    task = next((t for t in tasks if t[\u0027id\u0027] == task_id), None)\n    if task:\n        return jsonify(task)\n    return jsonify({\u0027error\u0027: \u0027Task not found\u0027}), 404\n\n@app.route(\u0027/api/tasks\u0027, methods=[\u0027POST\u0027])\ndef create_task():\n    data = request.get_json()\n    if not data or \u0027title\u0027 not in data:\n        return jsonify({\u0027error\u0027: \u0027Title is required\u0027}), 400\n    \n    new_task = {\n        \u0027id\u0027: max([t[\u0027id\u0027] for t in tasks]) + 1,\n        \u0027title\u0027: data[\u0027title\u0027],\n        \u0027completed\u0027: False\n    }\n    tasks.append(new_task)\n    return jsonify(new_task), 201\n\n# ========== Test Configuration ==========\n# tests/conftest.py\n\n@pytest.fixture\ndef client():\n    \"\"\"Create test client for the Flask app.\"\"\"\n    app.config[\u0027TESTING\u0027] = True\n    with app.test_client() as client:\n        yield client\n\n# ========== API Tests ==========\n# tests/test_api.py\n\ndef test_get_all_tasks(client):\n    \"\"\"Test GET /api/tasks returns all tasks.\"\"\"\n    response = client.get(\u0027/api/tasks\u0027)\n    \n    assert response.status_code == 200\n    data = response.get_json()\n    assert \u0027tasks\u0027 in data\n    assert len(data[\u0027tasks\u0027]) \u003e= 2\n\ndef test_get_single_task(client):\n    \"\"\"Test GET /api/tasks/\u003cid\u003e returns specific task.\"\"\"\n    response = client.get(\u0027/api/tasks/1\u0027)\n    \n    assert response.status_code == 200\n    data = response.get_json()\n    assert data[\u0027id\u0027] == 1\n    assert \u0027title\u0027 in data\n\ndef test_get_nonexistent_task(client):\n    \"\"\"Test GET /api/tasks/\u003cid\u003e returns 404 for missing task.\"\"\"\n    response = client.get(\u0027/api/tasks/999\u0027)\n    \n    assert response.status_code == 404\n    data = response.get_json()\n    assert \u0027error\u0027 in data\n\ndef test_create_task(client):\n    \"\"\"Test POST /api/tasks creates new task.\"\"\"\n    response = client.post(\n        \u0027/api/tasks\u0027,\n        json={\u0027title\u0027: \u0027New Task\u0027}\n    )\n    \n    assert response.status_code == 201\n    data = response.get_json()\n    assert data[\u0027title\u0027] == \u0027New Task\u0027\n    assert data[\u0027completed\u0027] == False\n    assert \u0027id\u0027 in data\n\ndef test_create_task_without_title(client):\n    \"\"\"Test POST /api/tasks returns 400 without title.\"\"\"\n    response = client.post(\n        \u0027/api/tasks\u0027,\n        json={}\n    )\n    \n    assert response.status_code == 400\n    assert \u0027error\u0027 in response.get_json()\n\ndef test_create_task_empty_body(client):\n    \"\"\"Test POST /api/tasks handles empty request.\"\"\"\n    response = client.post(\n        \u0027/api/tasks\u0027,\n        content_type=\u0027application/json\u0027\n    )\n    \n    assert response.status_code == 400\n\n# Run tests: pytest test_api.py -v\nprint(\"API tests defined! Run with: pytest -v\")",
                                "language":  "python"
                            },
                            {
                                "type":  "THEORY",
                                "title":  "API Documentation with OpenAPI/Swagger",
                                "content":  "**Good documentation is essential for any API.**\n\nImagine using an API with no documentation - you\u0027d have to guess endpoints, parameters, and response formats. That\u0027s why the OpenAPI Specification (formerly Swagger) became the industry standard.\n\n**OpenAPI/Swagger provides:**\n\n1. **Machine-readable specification** (YAML/JSON)\n   - Defines all endpoints, parameters, responses\n   - Can generate client SDKs automatically\n   - Enables automated testing\n\n2. **Interactive documentation (Swagger UI)**\n   - Try endpoints directly in browser\n   - See request/response examples\n   - No code needed to test\n\n3. **Validation**\n   - Ensures API matches documentation\n   - Validates request/response schemas\n\n**Popular Flask documentation tools:**\n\n- **Flasgger** - Easy Swagger UI integration\n  - Extracts docs from docstrings\n  - Built-in Swagger UI at /apidocs\n  - OpenAPI 3.0 support\n\n- **flask-smorest** - OpenAPI generation\n  - Auto-generates OpenAPI spec\n  - Integrates with marshmallow schemas\n\n- **Connexion** - Spec-first development\n  - Define API in YAML first\n  - Auto-generates routes from spec\n\n**Documentation best practices:**\n- Document every endpoint\n- Include request/response examples\n- Explain error codes\n- Keep docs in sync with code\n- Version your API"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Adding Swagger Documentation with Flasgger",
                                "content":  "**Expected Output:**\nSwagger UI available at: http://localhost:5000/apidocs\n\nThe interactive documentation lets users try your API directly from the browser!",
                                "code":  "# Install: pip install flasgger\n\nfrom flask import Flask, jsonify, request\nfrom flasgger import Swagger, swag_from\n\napp = Flask(__name__)\n\n# Configure Swagger\napp.config[\u0027SWAGGER\u0027] = {\n    \u0027title\u0027: \u0027Task API\u0027,\n    \u0027uiversion\u0027: 3,\n    \u0027description\u0027: \u0027A simple Task Management API\u0027,\n    \u0027version\u0027: \u00271.0.0\u0027\n}\nswagger = Swagger(app)\n\ntasks = [\n    {\u0027id\u0027: 1, \u0027title\u0027: \u0027Learn Python\u0027, \u0027completed\u0027: False}\n]\n\n@app.route(\u0027/api/tasks\u0027, methods=[\u0027GET\u0027])\ndef get_tasks():\n    \"\"\"\n    Get all tasks\n    ---\n    tags:\n      - Tasks\n    responses:\n      200:\n        description: List of all tasks\n        schema:\n          type: object\n          properties:\n            tasks:\n              type: array\n              items:\n                type: object\n                properties:\n                  id:\n                    type: integer\n                    example: 1\n                  title:\n                    type: string\n                    example: Learn Python\n                  completed:\n                    type: boolean\n                    example: false\n    \"\"\"\n    return jsonify({\u0027tasks\u0027: tasks})\n\n@app.route(\u0027/api/tasks/\u003cint:task_id\u003e\u0027, methods=[\u0027GET\u0027])\ndef get_task(task_id):\n    \"\"\"\n    Get a specific task by ID\n    ---\n    tags:\n      - Tasks\n    parameters:\n      - name: task_id\n        in: path\n        type: integer\n        required: true\n        description: The task ID\n    responses:\n      200:\n        description: Task details\n        schema:\n          type: object\n          properties:\n            id:\n              type: integer\n            title:\n              type: string\n            completed:\n              type: boolean\n      404:\n        description: Task not found\n        schema:\n          type: object\n          properties:\n            error:\n              type: string\n              example: Task not found\n    \"\"\"\n    task = next((t for t in tasks if t[\u0027id\u0027] == task_id), None)\n    if task:\n        return jsonify(task)\n    return jsonify({\u0027error\u0027: \u0027Task not found\u0027}), 404\n\n@app.route(\u0027/api/tasks\u0027, methods=[\u0027POST\u0027])\ndef create_task():\n    \"\"\"\n    Create a new task\n    ---\n    tags:\n      - Tasks\n    parameters:\n      - name: body\n        in: body\n        required: true\n        schema:\n          type: object\n          required:\n            - title\n          properties:\n            title:\n              type: string\n              example: Complete API project\n    responses:\n      201:\n        description: Task created successfully\n      400:\n        description: Invalid request (missing title)\n    \"\"\"\n    data = request.get_json()\n    if not data or \u0027title\u0027 not in data:\n        return jsonify({\u0027error\u0027: \u0027Title is required\u0027}), 400\n    \n    new_task = {\n        \u0027id\u0027: max([t[\u0027id\u0027] for t in tasks], default=0) + 1,\n        \u0027title\u0027: data[\u0027title\u0027],\n        \u0027completed\u0027: False\n    }\n    tasks.append(new_task)\n    return jsonify(new_task), 201\n\nif __name__ == \u0027__main__\u0027:\n    print(\"Swagger UI: http://localhost:5000/apidocs\")\n    app.run(debug=True)",
                                "language":  "python"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **Test your APIs** before deployment - catch bugs early with pytest\n- **Use pytest fixtures** to create reusable test setup (test client, database)\n- **Test all scenarios**: success cases, error cases, edge cases, validation\n- **Check status codes** (200, 201, 400, 404) and response structure\n- **Document with OpenAPI/Swagger** - industry standard for API docs\n- **Flasgger** adds Swagger UI to Flask with minimal setup\n- **Interactive docs** let users try your API without writing code\n- **Keep documentation in sync** - outdated docs are worse than no docs\n- **Run tests in CI/CD** - automated testing on every commit prevents regressions"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "14_11-challenge-1",
                           "title":  "Interactive Exercise: Write API Tests",
                           "description":  "Write pytest tests for a User API with the following endpoints:\n- GET /api/users - Returns list of users\n- GET /api/users/\u003cid\u003e - Returns single user or 404\n- POST /api/users - Creates user (requires name and email)\n- DELETE /api/users/\u003cid\u003e - Deletes user\n\nWrite tests for: success cases, 404 errors, and validation errors.",
                           "instructions":  "Write pytest tests for a User API with the following endpoints:\n- GET /api/users - Returns list of users\n- GET /api/users/\u003cid\u003e - Returns single user or 404\n- POST /api/users - Creates user (requires name and email)\n- DELETE /api/users/\u003cid\u003e - Deletes user\n\nWrite tests for: success cases, 404 errors, and validation errors.",
                           "starterCode":  "import pytest\nfrom flask import Flask, jsonify, request\n\napp = Flask(__name__)\nusers = [{\u0027id\u0027: 1, \u0027name\u0027: \u0027Alice\u0027, \u0027email\u0027: \u0027alice@example.com\u0027}]\n\n# API endpoints (already implemented)\n@app.route(\u0027/api/users\u0027, methods=[\u0027GET\u0027])\ndef get_users():\n    return jsonify({\u0027users\u0027: users})\n\n@app.route(\u0027/api/users/\u003cint:user_id\u003e\u0027, methods=[\u0027GET\u0027])\ndef get_user(user_id):\n    user = next((u for u in users if u[\u0027id\u0027] == user_id), None)\n    if user:\n        return jsonify(user)\n    return jsonify({\u0027error\u0027: \u0027User not found\u0027}), 404\n\n@app.route(\u0027/api/users\u0027, methods=[\u0027POST\u0027])\ndef create_user():\n    data = request.get_json()\n    if not data or \u0027name\u0027 not in data or \u0027email\u0027 not in data:\n        return jsonify({\u0027error\u0027: \u0027Name and email required\u0027}), 400\n    new_user = {\n        \u0027id\u0027: max([u[\u0027id\u0027] for u in users], default=0) + 1,\n        \u0027name\u0027: data[\u0027name\u0027],\n        \u0027email\u0027: data[\u0027email\u0027]\n    }\n    users.append(new_user)\n    return jsonify(new_user), 201\n\n# TODO: Write the test fixture\n@pytest.fixture\ndef client():\n    pass\n\n# TODO: Write test for GET /api/users\ndef test_get_all_users(client):\n    pass\n\n# TODO: Write test for GET /api/users/\u003cid\u003e success\ndef test_get_user_success(client):\n    pass\n\n# TODO: Write test for GET /api/users/\u003cid\u003e not found\ndef test_get_user_not_found(client):\n    pass\n\n# TODO: Write test for POST /api/users success\ndef test_create_user_success(client):\n    pass\n\n# TODO: Write test for POST /api/users validation error\ndef test_create_user_missing_fields(client):\n    pass",
                           "solution":  "import pytest\nfrom flask import Flask, jsonify, request\n\napp = Flask(__name__)\n\n# Store users in a list (will be reset for each test)\nusers = [{\u0027id\u0027: 1, \u0027name\u0027: \u0027Alice\u0027, \u0027email\u0027: \u0027alice@example.com\u0027}]\n\n# API endpoints (already implemented)\n@app.route(\u0027/api/users\u0027, methods=[\u0027GET\u0027])\ndef get_users():\n    return jsonify({\u0027users\u0027: users})\n\n@app.route(\u0027/api/users/\u003cint:user_id\u003e\u0027, methods=[\u0027GET\u0027])\ndef get_user(user_id):\n    user = next((u for u in users if u[\u0027id\u0027] == user_id), None)\n    if user:\n        return jsonify(user)\n    return jsonify({\u0027error\u0027: \u0027User not found\u0027}), 404\n\n@app.route(\u0027/api/users\u0027, methods=[\u0027POST\u0027])\ndef create_user():\n    data = request.get_json()\n    if not data or \u0027name\u0027 not in data or \u0027email\u0027 not in data:\n        return jsonify({\u0027error\u0027: \u0027Name and email required\u0027}), 400\n    new_user = {\n        \u0027id\u0027: max([u[\u0027id\u0027] for u in users], default=0) + 1,\n        \u0027name\u0027: data[\u0027name\u0027],\n        \u0027email\u0027: data[\u0027email\u0027]\n    }\n    users.append(new_user)\n    return jsonify(new_user), 201\n\n@app.route(\u0027/api/users/\u003cint:user_id\u003e\u0027, methods=[\u0027DELETE\u0027])\ndef delete_user(user_id):\n    global users\n    user = next((u for u in users if u[\u0027id\u0027] == user_id), None)\n    if not user:\n        return jsonify({\u0027error\u0027: \u0027User not found\u0027}), 404\n    users = [u for u in users if u[\u0027id\u0027] != user_id]\n    return jsonify({\u0027message\u0027: \u0027User deleted\u0027}), 200\n\n# Test fixture - creates a test client for making requests\n@pytest.fixture\ndef client():\n    \"\"\"Create test client and reset user data before each test.\"\"\"\n    # Set testing mode for better error messages\n    app.config[\u0027TESTING\u0027] = True\n    \n    # Reset users to initial state before each test\n    global users\n    users.clear()\n    users.append({\u0027id\u0027: 1, \u0027name\u0027: \u0027Alice\u0027, \u0027email\u0027: \u0027alice@example.com\u0027})\n    \n    # Create and yield the test client\n    with app.test_client() as test_client:\n        yield test_client\n\n# Test: GET /api/users returns all users\ndef test_get_all_users(client):\n    \"\"\"Test that GET /api/users returns list of all users.\"\"\"\n    # Make GET request to the users endpoint\n    response = client.get(\u0027/api/users\u0027)\n    \n    # Check status code is 200 OK\n    assert response.status_code == 200\n    \n    # Parse JSON response\n    data = response.get_json()\n    \n    # Verify response structure\n    assert \u0027users\u0027 in data\n    assert isinstance(data[\u0027users\u0027], list)\n    assert len(data[\u0027users\u0027]) \u003e= 1\n    \n    # Check first user has expected fields\n    assert data[\u0027users\u0027][0][\u0027name\u0027] == \u0027Alice\u0027\n    assert data[\u0027users\u0027][0][\u0027email\u0027] == \u0027alice@example.com\u0027\n\n# Test: GET /api/users/\u003cid\u003e returns specific user\ndef test_get_user_success(client):\n    \"\"\"Test that GET /api/users/\u003cid\u003e returns user when found.\"\"\"\n    # Request user with ID 1\n    response = client.get(\u0027/api/users/1\u0027)\n    \n    # Check status code is 200 OK\n    assert response.status_code == 200\n    \n    # Parse and verify response\n    data = response.get_json()\n    assert data[\u0027id\u0027] == 1\n    assert data[\u0027name\u0027] == \u0027Alice\u0027\n    assert data[\u0027email\u0027] == \u0027alice@example.com\u0027\n\n# Test: GET /api/users/\u003cid\u003e returns 404 for missing user\ndef test_get_user_not_found(client):\n    \"\"\"Test that GET /api/users/\u003cid\u003e returns 404 when user doesn\u0027t exist.\"\"\"\n    # Request non-existent user\n    response = client.get(\u0027/api/users/999\u0027)\n    \n    # Check status code is 404 Not Found\n    assert response.status_code == 404\n    \n    # Check error message is returned\n    data = response.get_json()\n    assert \u0027error\u0027 in data\n    assert data[\u0027error\u0027] == \u0027User not found\u0027\n\n# Test: POST /api/users creates new user\ndef test_create_user_success(client):\n    \"\"\"Test that POST /api/users creates a new user.\"\"\"\n    # Create new user data\n    new_user_data = {\n        \u0027name\u0027: \u0027Bob\u0027,\n        \u0027email\u0027: \u0027bob@example.com\u0027\n    }\n    \n    # Make POST request with JSON data\n    response = client.post(\n        \u0027/api/users\u0027,\n        json=new_user_data\n    )\n    \n    # Check status code is 201 Created\n    assert response.status_code == 201\n    \n    # Verify response contains created user\n    data = response.get_json()\n    assert data[\u0027name\u0027] == \u0027Bob\u0027\n    assert data[\u0027email\u0027] == \u0027bob@example.com\u0027\n    assert \u0027id\u0027 in data\n    assert data[\u0027id\u0027] == 2  # Should be second user\n\n# Test: POST /api/users returns 400 for missing fields\ndef test_create_user_missing_fields(client):\n    \"\"\"Test that POST /api/users returns 400 when required fields missing.\"\"\"\n    # Test with missing email\n    response = client.post(\n        \u0027/api/users\u0027,\n        json={\u0027name\u0027: \u0027Bob\u0027}  # Missing email\n    )\n    \n    assert response.status_code == 400\n    data = response.get_json()\n    assert \u0027error\u0027 in data\n    \n    # Test with missing name\n    response = client.post(\n        \u0027/api/users\u0027,\n        json={\u0027email\u0027: \u0027bob@example.com\u0027}  # Missing name\n    )\n    \n    assert response.status_code == 400\n    \n    # Test with empty body\n    response = client.post(\n        \u0027/api/users\u0027,\n        json={}\n    )\n    \n    assert response.status_code == 400\n\n# Bonus: Test DELETE endpoint\ndef test_delete_user_success(client):\n    \"\"\"Test that DELETE /api/users/\u003cid\u003e removes a user.\"\"\"\n    response = client.delete(\u0027/api/users/1\u0027)\n    \n    assert response.status_code == 200\n    data = response.get_json()\n    assert data[\u0027message\u0027] == \u0027User deleted\u0027\n    \n    # Verify user is actually deleted\n    response = client.get(\u0027/api/users/1\u0027)\n    assert response.status_code == 404\n\ndef test_delete_user_not_found(client):\n    \"\"\"Test that DELETE /api/users/\u003cid\u003e returns 404 for non-existent user.\"\"\"\n    response = client.delete(\u0027/api/users/999\u0027)\n    \n    assert response.status_code == 404\n    data = response.get_json()\n    assert \u0027error\u0027 in data\n\n# Run with: pytest -v\nprint(\u0027Run tests with: pytest -v\u0027)",
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
                                             "text":  "Use app.test_client() in the fixture. Check response.status_code and response.get_json() in assertions."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Not using app.config[\u0027TESTING\u0027] = True",
                                                      "consequence":  "Tests may behave differently than expected",
                                                      "correction":  "Set TESTING config to True in fixture"
                                                  },
                                                  {
                                                      "mistake":  "Forgetting to use json= for POST data",
                                                      "consequence":  "Request body not sent correctly",
                                                      "correction":  "Use client.post(\u0027/path\u0027, json={\u0027key\u0027: \u0027value\u0027})"
                                                  }
                                              ],
                           "difficulty":  "intermediate"
                       }
                   ],
    "difficulty":  "beginner",
    "title":  "API Testing and Documentation",
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
- Search for "python API Testing and Documentation 2024 2025" to find latest practices
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
  "lessonId": "module-13-lesson-05",
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

