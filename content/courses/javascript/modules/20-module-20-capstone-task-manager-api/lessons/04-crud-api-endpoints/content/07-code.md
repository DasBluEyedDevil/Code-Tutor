---
type: "CODE"
title: "Test the Complete API"
---

Test all endpoints with curl:

```bash
# Start the server
bun run dev

# 1. Register and save the token
TOKEN=$(curl -s -X POST http://localhost:3000/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email": "test@example.com", "password": "Password123"}' \
  | jq -r '.token')

echo "Token: $TOKEN"

# 2. Create a category
curl -X POST http://localhost:3000/api/categories \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"name": "Work", "color": "#EF4444"}'

# 3. Create a task (use the category ID from above)
curl -X POST http://localhost:3000/api/tasks \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Complete capstone project",
    "description": "Build the Task Manager API",
    "priority": "high",
    "categoryId": "YOUR_CATEGORY_ID"
  }'

# 4. List tasks with filters
curl "http://localhost:3000/api/tasks?status=pending&page=1&limit=10" \
  -H "Authorization: Bearer $TOKEN"

# 5. Update a task
curl -X PUT http://localhost:3000/api/tasks/YOUR_TASK_ID \
  -H "Authorization: Bearer $TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"status": "completed"}'

# 6. Delete a task
curl -X DELETE http://localhost:3000/api/tasks/YOUR_TASK_ID \
  -H "Authorization: Bearer $TOKEN"
```
