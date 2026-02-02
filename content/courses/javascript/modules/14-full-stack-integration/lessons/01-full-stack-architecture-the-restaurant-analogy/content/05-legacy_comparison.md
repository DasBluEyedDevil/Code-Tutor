---
type: "LEGACY_COMPARISON"
title: "Express Equivalent"
---

If you encounter older codebases using Express, here's how the patterns compare. Express was the dominant Node.js framework before Hono.

```javascript
// Express Full-Stack Setup
import express from 'express';
import cors from 'cors';

const app = express();

// CORS middleware (separate package)
app.use(cors());
app.use(express.json());

// Route handling uses (req, res)
app.get('/api/users', async (req, res) => {
  const users = await prisma.user.findMany();
  res.json(users);
});

// Request data access
app.post('/api/users/:id', (req, res) => {
  req.params.id    // URL params
  req.query.name   // Query string
  req.body.email   // POST body (needs express.json())
});

// Cookies
res.cookie('token', jwtToken, {
  httpOnly: true,
  secure: true,
  sameSite: 'strict'
});
```
