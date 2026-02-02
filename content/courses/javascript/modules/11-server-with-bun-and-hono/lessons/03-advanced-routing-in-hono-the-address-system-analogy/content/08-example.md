---
type: "EXAMPLE"
title: "Request Body Parsing (JSON and FormData)"
---

Hono provides simple methods to parse different types of request bodies. While c.req.json() handles JSON data, you will also need c.req.formData() for HTML form submissions and file uploads. Understanding both is essential for building real-world APIs that accept data from web forms, mobile apps, and other services.

```javascript
// Hono Request Body Parsing (2025)
// Handle JSON, Form Data, and other body types

import { Hono } from 'hono';

const app = new Hono();

// 1. JSON BODY PARSING
// Most common for APIs - data sent as application/json
app.post('/api/users', async (c) => {
  // c.req.json() parses JSON body (async!)
  const body = await c.req.json();
  
  // Validate required fields
  if (!body.name || !body.email) {
    return c.json({ 
      error: 'Validation failed',
      required: ['name', 'email'] 
    }, 400);
  }
  
  return c.json({
    message: 'User created',
    user: {
      id: crypto.randomUUID(),
      name: body.name,
      email: body.email,
      role: body.role || 'user'
    }
  }, 201);
});

// 2. FORM DATA PARSING
// HTML forms send data as application/x-www-form-urlencoded or multipart/form-data
app.post('/contact', async (c) => {
  // c.req.formData() parses form submissions
  const formData = await c.req.formData();
  
  // Get individual fields
  const name = formData.get('name');
  const email = formData.get('email');
  const message = formData.get('message');
  
  // Validate
  if (!name || !email || !message) {
    return c.json({ error: 'All fields are required' }, 400);
  }
  
  return c.json({
    success: true,
    message: 'Contact form submitted',
    data: { name, email, message }
  });
});

// 3. FILE UPLOADS with FormData
app.post('/upload', async (c) => {
  const formData = await c.req.formData();
  
  // Get the uploaded file
  const file = formData.get('file');
  const description = formData.get('description');
  
  if (!file || !(file instanceof File)) {
    return c.json({ error: 'No file uploaded' }, 400);
  }
  
  // File properties
  const fileInfo = {
    name: file.name,
    size: file.size,
    type: file.type,
    description: description || 'No description'
  };
  
  // In real code, save the file:
  // const buffer = await file.arrayBuffer();
  // await Bun.write(`./uploads/${file.name}`, buffer);
  
  return c.json({
    message: 'File uploaded successfully',
    file: fileInfo
  }, 201);
});

// 4. RAW TEXT BODY
app.post('/webhook', async (c) => {
  // c.req.text() gets raw body as string
  const rawBody = await c.req.text();
  
  // Useful for webhooks that send raw data
  // or when you need to verify signatures
  console.log('Raw webhook payload:', rawBody);
  
  return c.json({ received: true, length: rawBody.length });
});

// 5. BLOB/BINARY DATA
app.post('/binary', async (c) => {
  // c.req.blob() gets body as Blob
  const blob = await c.req.blob();
  
  return c.json({
    type: blob.type,
    size: blob.size
  });
});

// 6. ARRAY BUFFER (for binary processing)
app.post('/process-binary', async (c) => {
  // c.req.arrayBuffer() for low-level binary data
  const buffer = await c.req.arrayBuffer();
  const bytes = new Uint8Array(buffer);
  
  return c.json({
    byteLength: buffer.byteLength,
    firstBytes: Array.from(bytes.slice(0, 10))
  });
});

// 7. MULTIPLE FILES
app.post('/upload-multiple', async (c) => {
  const formData = await c.req.formData();
  
  // getAll() returns all files with the same field name
  const files = formData.getAll('files');
  
  const fileInfos = files
    .filter(f => f instanceof File)
    .map(file => ({
      name: file.name,
      size: file.size,
      type: file.type
    }));
  
  return c.json({
    message: `${fileInfos.length} files uploaded`,
    files: fileInfos
  });
});

// CONTENT-TYPE DETECTION HELPER
app.post('/smart-parse', async (c) => {
  const contentType = c.req.header('Content-Type') || '';
  
  if (contentType.includes('application/json')) {
    const data = await c.req.json();
    return c.json({ type: 'json', data });
  }
  
  if (contentType.includes('form-data') || contentType.includes('x-www-form-urlencoded')) {
    const formData = await c.req.formData();
    const data = Object.fromEntries(formData.entries());
    return c.json({ type: 'form', data });
  }
  
  const text = await c.req.text();
  return c.json({ type: 'text', data: text });
});

export default app;
```
