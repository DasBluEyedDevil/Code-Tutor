import pytest
from flask import Flask, jsonify, request

app = Flask(__name__)
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

# TODO: Write the test fixture
@pytest.fixture
def client():
    pass

# TODO: Write test for GET /api/users
def test_get_all_users(client):
    pass

# TODO: Write test for GET /api/users/<id> success
def test_get_user_success(client):
    pass

# TODO: Write test for GET /api/users/<id> not found
def test_get_user_not_found(client):
    pass

# TODO: Write test for POST /api/users success
def test_create_user_success(client):
    pass

# TODO: Write test for POST /api/users validation error
def test_create_user_missing_fields(client):
    pass