---
type: "LEGACY_COMPARISON"
title: "Express Equivalent"
---

Here's how the same deployment would look with Express + Node.js. Express requires more setup and is platform-specific.

```javascript
// Express + Node.js deployment (traditional approach)

// 1. Install dependencies
// npm install express cors dotenv

// 2. server.js
const express = require('express');
const cors = require('cors');
require('dotenv').config();

const app = express();
const PORT = process.env.PORT || 3000;

// Middleware
app.use(cors({
  origin: ['https://my-app.vercel.app', 'http://localhost:5173']
}));
app.use(express.json());

// Routes
app.get('/', (req, res) => {
  res.send('Hello from Express!');
});

app.get('/api/users', (req, res) => {
  res.json([{ id: 1, name: 'Alice' }]);
});

app.get('/health', (req, res) => {
  res.json({ status: 'ok' });
});

// Error handler
app.use((err, req, res, next) => {
  console.error(err.stack);
  res.status(500).json({ error: 'Internal error' });
});

app.listen(PORT, () => {
  console.log(`Server on port ${PORT}`);
});

// 3. package.json
// {
//   "scripts": {
//     "start": "node server.js",
//     "dev": "nodemon server.js"
//   }
// }

// Key Differences:
// - Express is Node.js only (no Workers, no Deno)
// - Requires dotenv package for .env
// - More verbose syntax
// - No built-in TypeScript
// - Callback-based (req, res) vs Hono's context (c)
```
