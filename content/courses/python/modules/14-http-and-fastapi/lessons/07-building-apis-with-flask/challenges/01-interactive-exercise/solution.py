from flask import Flask, jsonify, request

# Flask Blog API
# This solution demonstrates RESTful API endpoints

app = Flask(__name__)

# In-memory storage (would use database in production)
posts = [
    {'id': 1, 'title': 'First Post', 'content': 'Hello World!'},
    {'id': 2, 'title': 'Second Post', 'content': 'More content here.'}
]
next_id = 3

@app.route('/api/posts', methods=['GET'])
def get_all_posts():
    """List all posts."""
    return jsonify({'posts': posts, 'count': len(posts)}), 200

@app.route('/api/posts/<int:post_id>', methods=['GET'])
def get_post(post_id):
    """Get a specific post by ID."""
    post = next((p for p in posts if p['id'] == post_id), None)
    if post:
        return jsonify(post), 200
    return jsonify({'error': 'Post not found'}), 404

@app.route('/api/posts', methods=['POST'])
def create_post():
    """Create a new post."""
    global next_id
    data = request.get_json()
    
    # Validate required fields
    if not data:
        return jsonify({'error': 'No data provided'}), 400
    if 'title' not in data or not data['title'].strip():
        return jsonify({'error': 'Title is required'}), 400
    if 'content' not in data or not data['content'].strip():
        return jsonify({'error': 'Content is required'}), 400
    
    # Create new post
    new_post = {
        'id': next_id,
        'title': data['title'].strip(),
        'content': data['content'].strip()
    }
    posts.append(new_post)
    next_id += 1
    
    return jsonify(new_post), 201

@app.route('/api/posts/<int:post_id>', methods=['DELETE'])
def delete_post(post_id):
    """Delete a post by ID."""
    global posts
    post = next((p for p in posts if p['id'] == post_id), None)
    if not post:
        return jsonify({'error': 'Post not found'}), 404
    
    posts = [p for p in posts if p['id'] != post_id]
    return jsonify({'message': f'Post {post_id} deleted'}), 200

if __name__ == '__main__':
    print("\nFlask Blog API running at http://localhost:5000")
    print("\nEndpoints:")
    print("  GET    /api/posts       - List all posts")
    print("  GET    /api/posts/<id>  - Get specific post")
    print("  POST   /api/posts       - Create post")
    print("  DELETE /api/posts/<id>  - Delete post")
    app.run(debug=True)