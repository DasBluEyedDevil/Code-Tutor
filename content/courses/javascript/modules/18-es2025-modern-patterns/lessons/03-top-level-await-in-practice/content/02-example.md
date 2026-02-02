---
type: "EXAMPLE"
title: "Modern Server Initialization"
---

Top-level await makes initialization clean and readable:

```javascript
// server.js - Bun + Hono
import { Hono } from 'hono';

// Top-level await: Load everything before starting
const config = await Bun.file('config.json').json();
const secrets = await loadSecrets(config.vaultUrl);
const db = await connectDatabase(secrets.databaseUrl);

console.log(`Environment: ${config.env}`);
console.log(`Database connected: ${db.isConnected}`);

const app = new Hono();

app.get('/health', (c) => c.json({ status: 'ok', db: db.isConnected }));

app.get('/users', async (c) => {
  const users = await db.query('SELECT * FROM users');
  return c.json(users);
});

// Export for Bun to serve
export default {
  port: config.port,
  fetch: app.fetch,
};
```
