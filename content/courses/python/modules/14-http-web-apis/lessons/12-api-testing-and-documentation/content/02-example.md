---
type: "EXAMPLE"
title: "Testing Flask APIs with pytest"
---

**Expected Output:**
```
============================= test session starts ==============================
collected 6 items

test_api.py ......                                                       [100%]

============================== 6 passed in 0.15s ===============================
```

```python
# Install: pip install pytest pytest-flask

import pytest
from flask import Flask, jsonify, request

# ========== Sample API to Test ==========
app = Flask(__name__)

# In-memory data store
tasks = [
    {'id': 1, 'title': 'Learn Python', 'completed': False},
    {'id': 2, 'title': 'Build API', 'completed': True}
]

@app.route('/api/tasks', methods=['GET'])
def get_tasks():
    return jsonify({'tasks': tasks})

@app.route('/api/tasks/<int:task_id>', methods=['GET'])
def get_task(task_id):
    task = next((t for t in tasks if t['id'] == task_id), None)
    if task:
        return jsonify(task)
    return jsonify({'error': 'Task not found'}), 404

@app.route('/api/tasks', methods=['POST'])
def create_task():
    data = request.get_json()
    if not data or 'title' not in data:
        return jsonify({'error': 'Title is required'}), 400
    
    new_task = {
        'id': max([t['id'] for t in tasks]) + 1,
        'title': data['title'],
        'completed': False
    }
    tasks.append(new_task)
    return jsonify(new_task), 201

# ========== Test Configuration ==========
# tests/conftest.py

@pytest.fixture
def client():
    """Create test client for the Flask app."""
    app.config['TESTING'] = True
    with app.test_client() as client:
        yield client

# ========== API Tests ==========
# tests/test_api.py

def test_get_all_tasks(client):
    """Test GET /api/tasks returns all tasks."""
    response = client.get('/api/tasks')
    
    assert response.status_code == 200
    data = response.get_json()
    assert 'tasks' in data
    assert len(data['tasks']) >= 2

def test_get_single_task(client):
    """Test GET /api/tasks/<id> returns specific task."""
    response = client.get('/api/tasks/1')
    
    assert response.status_code == 200
    data = response.get_json()
    assert data['id'] == 1
    assert 'title' in data

def test_get_nonexistent_task(client):
    """Test GET /api/tasks/<id> returns 404 for missing task."""
    response = client.get('/api/tasks/999')
    
    assert response.status_code == 404
    data = response.get_json()
    assert 'error' in data

def test_create_task(client):
    """Test POST /api/tasks creates new task."""
    response = client.post(
        '/api/tasks',
        json={'title': 'New Task'}
    )
    
    assert response.status_code == 201
    data = response.get_json()
    assert data['title'] == 'New Task'
    assert data['completed'] == False
    assert 'id' in data

def test_create_task_without_title(client):
    """Test POST /api/tasks returns 400 without title."""
    response = client.post(
        '/api/tasks',
        json={}
    )
    
    assert response.status_code == 400
    assert 'error' in response.get_json()

def test_create_task_empty_body(client):
    """Test POST /api/tasks handles empty request."""
    response = client.post(
        '/api/tasks',
        content_type='application/json'
    )
    
    assert response.status_code == 400

# Run tests: pytest test_api.py -v
print("API tests defined! Run with: pytest -v")
```
