---
type: "KEY_POINT"
title: "Session is NOT the HTTP Request"
---

**Important Distinction:**

In many web frameworks, you work with raw HTTP requests:
```javascript
// Express.js style
app.get('/users/:id', (req, res) => {
  const userId = req.params.id;  // Parse from URL
  const authToken = req.headers.authorization;  // Parse from headers
  // ... lots of manual parsing
});
```

**In Serverpod, Session abstracts all of this away:**

```dart
// Serverpod style
Future<User?> getUser(Session session, int userId) async {
  // userId is already parsed and typed!
  // Authentication is already verified!
  // Database is ready to use!
  return await User.db.findById(session, userId);
}
```

**What Session Handles For You:**
- Connection management
- Authentication state
- Database transactions
- Logging
- Error tracking
- Message queues
- Caching

**You focus on business logic. Serverpod handles infrastructure.**

