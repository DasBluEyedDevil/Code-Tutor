---
type: "EXAMPLE"
title: "Code Example: Basic Flask API"
---

**Flask API key concepts:**

**1. Routes:**
```python
@app.route('/path', methods=['GET', 'POST'])
def function():
    return jsonify(data)
```

**2. URL parameters:**
```python
@app.route('/users/<int:user_id>')
def get_user(user_id):
    # user_id is extracted from URL
```

**3. Request data:**
```python
data = request.get_json()  # Parse JSON body
params = request.args      # Query parameters
```

**4. Responses:**
```python
return jsonify(data)           # 200 OK
return jsonify(data), 201      # Created
return jsonify(error), 404     # Not found
```

**5. HTTP status codes:**
- 200: Success
- 201: Created
- 400: Bad request
- 404: Not found
- 500: Server error

```python
from flask import Flask, jsonify, request

app = Flask(__name__)

# In-memory data store
users = [
    {'id': 1, 'name': 'Alice', 'email': 'alice@example.com'},
    {'id': 2, 'name': 'Bob', 'email': 'bob@example.com'}
]

print("=== Basic Routes ===")

@app.route('/')
def home():
    """Root endpoint"""
    return jsonify({
        'message': 'Welcome to the API',
        'version': '1.0',
        'endpoints': {
            'users': '/api/users',
            'health': '/api/health'
        }
    })

@app.route('/api/health')
def health():
    """Health check endpoint"""
    return jsonify({'status': 'healthy', 'service': 'user-api'})

print("\n=== GET - Read Resources ===")

@app.route('/api/users', methods=['GET'])
def get_users():
    """Get all users"""
    return jsonify({
        'users': users,
        'count': len(users)
    })

@app.route('/api/users/<int:user_id>', methods=['GET'])
def get_user(user_id):
    """Get specific user by ID"""
    user = next((u for u in users if u['id'] == user_id), None)
    
    if user:
        return jsonify(user)
    return jsonify({'error': 'User not found'}), 404

print("\n=== POST - Create Resources ===")

@app.route('/api/users', methods=['POST'])
def create_user():
    """Create new user"""
    data = request.get_json()
    
    # Validation
    if not data or 'name' not in data or 'email' not in data:
        return jsonify({'error': 'Name and email required'}), 400
    
    # Create new user
    new_user = {
        'id': max([u['id'] for u in users]) + 1 if users else 1,
        'name': data['name'],
        'email': data['email']
    }
    
    users.append(new_user)
    
    return jsonify(new_user), 201

print("\n=== PUT - Update Resources ===")

@app.route('/api/users/<int:user_id>', methods=['PUT'])
def update_user(user_id):
    """Update existing user"""
    user = next((u for u in users if u['id'] == user_id), None)
    
    if not user:
        return jsonify({'error': 'User not found'}), 404
    
    data = request.get_json()
    
    # Update fields if provided
    if 'name' in data:
        user['name'] = data['name']
    if 'email' in data:
        user['email'] = data['email']
    
    return jsonify(user)

print("\n=== DELETE - Remove Resources ===")

@app.route('/api/users/<int:user_id>', methods=['DELETE'])
def delete_user(user_id):
    """Delete user"""
    global users
    user = next((u for u in users if u['id'] == user_id), None)
    
    if not user:
        return jsonify({'error': 'User not found'}), 404
    
    users = [u for u in users if u['id'] != user_id]
    
    return jsonify({'message': 'User deleted', 'id': user_id})

print("\n=== Error Handling ===")

@app.errorhandler(404)
def not_found(error):
    """Handle 404 errors"""
    return jsonify({'error': 'Resource not found'}), 404

@app.errorhandler(500)
def internal_error(error):
    """Handle 500 errors"""
    return jsonify({'error': 'Internal server error'}), 500

if __name__ == '__main__':
    print("\n=== Starting Flask API ===")
    print("API will run on http://localhost:5000")
    print("\nEndpoints:")
    print("  GET    /api/users       - List all users")
    print("  GET    /api/users/<id>  - Get specific user")
    print("  POST   /api/users       - Create user")
    print("  PUT    /api/users/<id>  - Update user")
    print("  DELETE /api/users/<id>  - Delete user")
    print("\nNote: Run with 'python app.py' to start server")
    
    # For demonstration, we'll show the structure
    # In real use: app.run(debug=True)
```
