---
type: "EXAMPLE"
title: "Code Example: Advanced Flask Features"
---

**Advanced Flask patterns:**

**1. Query parameters:**
```python
page = request.args.get('page', 1, type=int)
sort = request.args.get('sort', 'name')
```

**2. Validation:**
- Check required fields
- Validate data types
- Return 400 for invalid data

**3. Decorators:**
```python
@require_auth
def protected_route():
    ...
```

**4. Custom error handlers:**
```python
@app.errorhandler(CustomError)
def handle_error(e):
    return jsonify(...), 400
```

**5. Response helpers:**
- Consistent response format
- Easier to maintain
- Better client experience

**6. API versioning:**
- /api/v1/... for version 1
- /api/v2/... for version 2
- Allows breaking changes

```python
from flask import Flask, jsonify, request, abort
from functools import wraps
import re

app = Flask(__name__)

# Sample data
tasks = [
    {'id': 1, 'title': 'Learn Python', 'completed': True},
    {'id': 2, 'title': 'Build API', 'completed': False}
]

print("=== Query Parameters ===")

@app.route('/api/tasks')
def get_tasks():
    """Get tasks with optional filtering"""
    # Get query parameters
    completed = request.args.get('completed')
    limit = request.args.get('limit', default=10, type=int)
    
    filtered_tasks = tasks
    
    # Filter by completed status
    if completed is not None:
        is_completed = completed.lower() == 'true'
        filtered_tasks = [t for t in tasks if t['completed'] == is_completed]
    
    # Limit results
    filtered_tasks = filtered_tasks[:limit]
    
    return jsonify({
        'tasks': filtered_tasks,
        'count': len(filtered_tasks),
        'filters': {'completed': completed, 'limit': limit}
    })

print("\n=== Request Validation ===")

def validate_task(data):
    """Validate task data"""
    errors = []
    
    if not data:
        return ['No data provided']
    
    if 'title' not in data:
        errors.append('Title is required')
    elif len(data['title'].strip()) < 3:
        errors.append('Title must be at least 3 characters')
    
    if 'completed' in data and not isinstance(data['completed'], bool):
        errors.append('Completed must be boolean')
    
    return errors

@app.route('/api/tasks', methods=['POST'])
def create_task():
    """Create new task with validation"""
    data = request.get_json()
    
    # Validate
    errors = validate_task(data)
    if errors:
        return jsonify({'errors': errors}), 400
    
    # Create task
    new_task = {
        'id': max([t['id'] for t in tasks]) + 1 if tasks else 1,
        'title': data['title'].strip(),
        'completed': data.get('completed', False)
    }
    
    tasks.append(new_task)
    
    return jsonify(new_task), 201

print("\n=== Decorators for Authentication ===")

def require_api_key(f):
    """Decorator to require API key"""
    @wraps(f)
    def decorated_function(*args, **kwargs):
        api_key = request.headers.get('X-API-Key')
        
        if not api_key or api_key != 'secret-key-123':
            return jsonify({'error': 'Invalid or missing API key'}), 401
        
        return f(*args, **kwargs)
    return decorated_function

@app.route('/api/admin/tasks', methods=['DELETE'])
@require_api_key
def delete_all_tasks():
    """Delete all tasks (requires API key)"""
    global tasks
    count = len(tasks)
    tasks = []
    
    return jsonify({'message': f'Deleted {count} tasks'})

print("\n=== Error Handlers ===")

class ValidationError(Exception):
    """Custom validation error"""
    pass

@app.errorhandler(ValidationError)
def handle_validation_error(error):
    return jsonify({'error': str(error)}), 400

@app.errorhandler(404)
def not_found(error):
    return jsonify({
        'error': 'Resource not found',
        'status': 404
    }), 404

print("\n=== Response Helpers ===")

def success_response(data, message=None, status=200):
    """Create success response"""
    response = {'success': True, 'data': data}
    if message:
        response['message'] = message
    return jsonify(response), status

def error_response(message, status=400):
    """Create error response"""
    return jsonify({
        'success': False,
        'error': message
    }), status

@app.route('/api/tasks/<int:task_id>', methods=['PATCH'])
def update_task_partial(task_id):
    """Partially update task"""
    task = next((t for t in tasks if t['id'] == task_id), None)
    
    if not task:
        return error_response('Task not found', 404)
    
    data = request.get_json()
    
    if 'title' in data:
        task['title'] = data['title']
    if 'completed' in data:
        task['completed'] = data['completed']
    
    return success_response(task, 'Task updated')

print("\n=== API Versioning ===")

@app.route('/api/v1/tasks')
def get_tasks_v1():
    """Version 1 of tasks endpoint"""
    return jsonify({'version': 1, 'tasks': tasks})

@app.route('/api/v2/tasks')
def get_tasks_v2():
    """Version 2 with enhanced response"""
    return jsonify({
        'version': 2,
        'data': {
            'tasks': tasks,
            'total': len(tasks),
            'completed': len([t for t in tasks if t['completed']]),
            'pending': len([t for t in tasks if not t['completed']])
        }
    })

if __name__ == '__main__':
    print("\n=== Flask API with Advanced Features ===")
    print("\nFeatures demonstrated:")
    print("  ✓ Query parameters")
    print("  ✓ Request validation")
    print("  ✓ Authentication decorator")
    print("  ✓ Custom error handlers")
    print("  ✓ Response helpers")
    print("  ✓ API versioning")
    print("\nExample requests:")
    print("  GET /api/tasks?completed=true&limit=5")
    print("  POST /api/tasks (with JSON body)")
    print("  DELETE /api/admin/tasks (requires X-API-Key header)")
```
