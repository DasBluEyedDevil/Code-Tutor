---
type: "KEY_POINT"
title: "Testing the API with curl or Postman"
---

Before building the frontend, test your API thoroughly using curl or Postman.

Create a Task:
```bash
curl -X POST http://localhost:8080/api/tasks \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <your-jwt-token>" \
  -d '{
    "title": "Learn Spring Boot",
    "description": "Complete the capstone project",
    "priority": "HIGH",
    "dueDate": "2024-12-31"
  }'
```

Expected Response (201 Created):
```json
{
  "id": 1,
  "title": "Learn Spring Boot",
  "description": "Complete the capstone project",
  "status": "PENDING",
  "priority": "HIGH",
  "dueDate": "2024-12-31",
  "category": null,
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": "2024-01-15T10:30:00"
}
```

Get All Tasks (Paginated):
```bash
curl http://localhost:8080/api/tasks?page=0&size=5 \
  -H "Authorization: Bearer <your-jwt-token>"
```

Update a Task:
```bash
curl -X PUT http://localhost:8080/api/tasks/1 \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer <your-jwt-token>" \
  -d '{"title": "Updated Title", "status": "COMPLETED"}'
```

Delete a Task:
```bash
curl -X DELETE http://localhost:8080/api/tasks/1 \
  -H "Authorization: Bearer <your-jwt-token>"
```

Expected Response: 204 No Content (empty body)