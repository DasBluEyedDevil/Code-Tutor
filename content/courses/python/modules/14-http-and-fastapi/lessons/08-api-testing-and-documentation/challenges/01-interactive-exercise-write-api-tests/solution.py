import pytest
from flask import Flask, jsonify, request

app = Flask(__name__)

# Store users in a list (will be reset for each test)
users = [{'id': 1, 'name': 'Alice', 'email': 'alice@example.com'}]

# API endpoints (already implemented)
@app.route('/api/users', methods=['GET'])
def get_users():
    return jsonify({'users': users})

@app.route('/api/users/<int:user_id>', methods=['GET'])
def get_user(user_id):
    user = next((u for u in users if u['id'] == user_id), None)
    if user:
        return jsonify(user)
    return jsonify({'error': 'User not found'}), 404

@app.route('/api/users', methods=['POST'])
def create_user():
    data = request.get_json()
    if not data or 'name' not in data or 'email' not in data:
        return jsonify({'error': 'Name and email required'}), 400
    new_user = {
        'id': max([u['id'] for u in users], default=0) + 1,
        'name': data['name'],
        'email': data['email']
    }
    users.append(new_user)
    return jsonify(new_user), 201

@app.route('/api/users/<int:user_id>', methods=['DELETE'])
def delete_user(user_id):
    global users
    user = next((u for u in users if u['id'] == user_id), None)
    if not user:
        return jsonify({'error': 'User not found'}), 404
    users = [u for u in users if u['id'] != user_id]
    return jsonify({'message': 'User deleted'}), 200

# Test fixture - creates a test client for making requests
@pytest.fixture
def client():
    """Create test client and reset user data before each test."""
    # Set testing mode for better error messages
    app.config['TESTING'] = True
    
    # Reset users to initial state before each test
    global users
    users.clear()
    users.append({'id': 1, 'name': 'Alice', 'email': 'alice@example.com'})
    
    # Create and yield the test client
    with app.test_client() as test_client:
        yield test_client

# Test: GET /api/users returns all users
def test_get_all_users(client):
    """Test that GET /api/users returns list of all users."""
    # Make GET request to the users endpoint
    response = client.get('/api/users')
    
    # Check status code is 200 OK
    assert response.status_code == 200
    
    # Parse JSON response
    data = response.get_json()
    
    # Verify response structure
    assert 'users' in data
    assert isinstance(data['users'], list)
    assert len(data['users']) >= 1
    
    # Check first user has expected fields
    assert data['users'][0]['name'] == 'Alice'
    assert data['users'][0]['email'] == 'alice@example.com'

# Test: GET /api/users/<id> returns specific user
def test_get_user_success(client):
    """Test that GET /api/users/<id> returns user when found."""
    # Request user with ID 1
    response = client.get('/api/users/1')
    
    # Check status code is 200 OK
    assert response.status_code == 200
    
    # Parse and verify response
    data = response.get_json()
    assert data['id'] == 1
    assert data['name'] == 'Alice'
    assert data['email'] == 'alice@example.com'

# Test: GET /api/users/<id> returns 404 for missing user
def test_get_user_not_found(client):
    """Test that GET /api/users/<id> returns 404 when user doesn't exist."""
    # Request non-existent user
    response = client.get('/api/users/999')
    
    # Check status code is 404 Not Found
    assert response.status_code == 404
    
    # Check error message is returned
    data = response.get_json()
    assert 'error' in data
    assert data['error'] == 'User not found'

# Test: POST /api/users creates new user
def test_create_user_success(client):
    """Test that POST /api/users creates a new user."""
    # Create new user data
    new_user_data = {
        'name': 'Bob',
        'email': 'bob@example.com'
    }
    
    # Make POST request with JSON data
    response = client.post(
        '/api/users',
        json=new_user_data
    )
    
    # Check status code is 201 Created
    assert response.status_code == 201
    
    # Verify response contains created user
    data = response.get_json()
    assert data['name'] == 'Bob'
    assert data['email'] == 'bob@example.com'
    assert 'id' in data
    assert data['id'] == 2  # Should be second user

# Test: POST /api/users returns 400 for missing fields
def test_create_user_missing_fields(client):
    """Test that POST /api/users returns 400 when required fields missing."""
    # Test with missing email
    response = client.post(
        '/api/users',
        json={'name': 'Bob'}  # Missing email
    )
    
    assert response.status_code == 400
    data = response.get_json()
    assert 'error' in data
    
    # Test with missing name
    response = client.post(
        '/api/users',
        json={'email': 'bob@example.com'}  # Missing name
    )
    
    assert response.status_code == 400
    
    # Test with empty body
    response = client.post(
        '/api/users',
        json={}
    )
    
    assert response.status_code == 400

# Bonus: Test DELETE endpoint
def test_delete_user_success(client):
    """Test that DELETE /api/users/<id> removes a user."""
    response = client.delete('/api/users/1')
    
    assert response.status_code == 200
    data = response.get_json()
    assert data['message'] == 'User deleted'
    
    # Verify user is actually deleted
    response = client.get('/api/users/1')
    assert response.status_code == 404

def test_delete_user_not_found(client):
    """Test that DELETE /api/users/<id> returns 404 for non-existent user."""
    response = client.delete('/api/users/999')
    
    assert response.status_code == 404
    data = response.get_json()
    assert 'error' in data

# Run with: pytest -v
print('Run tests with: pytest -v')