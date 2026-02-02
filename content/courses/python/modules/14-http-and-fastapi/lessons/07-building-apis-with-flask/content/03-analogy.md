---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Basic Flask app structure:**
```python
from flask import Flask, jsonify, request

app = Flask(__name__)

@app.route('/path')
def handler():
    return jsonify({'key': 'value'})

if __name__ == '__main__':
    app.run(debug=True)
```

**Route patterns:**
```python
# Static route
@app.route('/users')

# With parameter
@app.route('/users/<user_id>')

# With type converter
@app.route('/users/<int:user_id>')

# Multiple methods
@app.route('/users', methods=['GET', 'POST'])
```

**Request handling:**
```python
# Get JSON data
data = request.get_json()

# Get query parameters
page = request.args.get('page', default=1, type=int)

# Get form data
name = request.form.get('name')

# Get headers
token = request.headers.get('Authorization')
```

**Response patterns:**
```python
# JSON response
return jsonify(data)

# With status code
return jsonify(data), 201

# With headers
return jsonify(data), 200, {'X-Custom': 'value'}
```