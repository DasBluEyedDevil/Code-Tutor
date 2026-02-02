---
type: "EXAMPLE"
title: "Adding Swagger Documentation with Flasgger"
---

**Expected Output:**
Swagger UI available at: http://localhost:5000/apidocs

The interactive documentation lets users try your API directly from the browser!

```python
# Install: pip install flasgger

from flask import Flask, jsonify, request
from flasgger import Swagger, swag_from

app = Flask(__name__)

# Configure Swagger
app.config['SWAGGER'] = {
    'title': 'Task API',
    'uiversion': 3,
    'description': 'A simple Task Management API',
    'version': '1.0.0'
}
swagger = Swagger(app)

tasks = [
    {'id': 1, 'title': 'Learn Python', 'completed': False}
]

@app.route('/api/tasks', methods=['GET'])
def get_tasks():
    """
    Get all tasks
    ---
    tags:
      - Tasks
    responses:
      200:
        description: List of all tasks
        schema:
          type: object
          properties:
            tasks:
              type: array
              items:
                type: object
                properties:
                  id:
                    type: integer
                    example: 1
                  title:
                    type: string
                    example: Learn Python
                  completed:
                    type: boolean
                    example: false
    """
    return jsonify({'tasks': tasks})

@app.route('/api/tasks/<int:task_id>', methods=['GET'])
def get_task(task_id):
    """
    Get a specific task by ID
    ---
    tags:
      - Tasks
    parameters:
      - name: task_id
        in: path
        type: integer
        required: true
        description: The task ID
    responses:
      200:
        description: Task details
        schema:
          type: object
          properties:
            id:
              type: integer
            title:
              type: string
            completed:
              type: boolean
      404:
        description: Task not found
        schema:
          type: object
          properties:
            error:
              type: string
              example: Task not found
    """
    task = next((t for t in tasks if t['id'] == task_id), None)
    if task:
        return jsonify(task)
    return jsonify({'error': 'Task not found'}), 404

@app.route('/api/tasks', methods=['POST'])
def create_task():
    """
    Create a new task
    ---
    tags:
      - Tasks
    parameters:
      - name: body
        in: body
        required: true
        schema:
          type: object
          required:
            - title
          properties:
            title:
              type: string
              example: Complete API project
    responses:
      201:
        description: Task created successfully
      400:
        description: Invalid request (missing title)
    """
    data = request.get_json()
    if not data or 'title' not in data:
        return jsonify({'error': 'Title is required'}), 400
    
    new_task = {
        'id': max([t['id'] for t in tasks], default=0) + 1,
        'title': data['title'],
        'completed': False
    }
    tasks.append(new_task)
    return jsonify(new_task), 201

if __name__ == '__main__':
    print("Swagger UI: http://localhost:5000/apidocs")
    app.run(debug=True)
```
