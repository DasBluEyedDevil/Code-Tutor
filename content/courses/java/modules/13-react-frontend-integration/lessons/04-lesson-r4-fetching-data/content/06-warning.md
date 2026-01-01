---
type: "WARNING"
title: "Common Fetch Pitfalls"
---

PITFALL 1: Forgetting error handling

// BAD - no error handling
const data = await fetch(url).then(r => r.json());

// GOOD - handle errors
const response = await fetch(url);
if (!response.ok) {
    const error = await response.text();
    throw new Error(error || `HTTP ${response.status}`);
}

PITFALL 2: Missing Content-Type header

// BAD - server may reject
fetch(url, { method: 'POST', body: JSON.stringify(data) });

// GOOD - specify content type
fetch(url, {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(data)
});

PITFALL 3: Forgetting to stringify body

// BAD - sends [object Object]
fetch(url, { method: 'POST', body: data });

// GOOD - stringify objects
fetch(url, { method: 'POST', body: JSON.stringify(data) });

PITFALL 4: CORS misconfiguration

// React shows: CORS error, blocked by policy
// Check:
// 1. Spring Boot has CORS config for http://localhost:5173
// 2. allowCredentials matches your fetch config
// 3. allowedMethods includes your HTTP method