---
type: "EXAMPLE"
title: "Promise.catch() Method"
---

When working with Promises directly (without async/await), use .catch() to handle rejections.

```javascript
// Using .catch() with promises
fetch('/api/data')
  .then(response => response.json())
  .then(data => {
    console.log('Data:', data);
  })
  .catch(error => {
    // Catches errors from fetch OR from .json() OR from data processing
    console.error('Error:', error.message);
  });

// Catch at different points in the chain
fetch('/api/data')
  .then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  })
  .then(data => processData(data))
  .then(result => saveResult(result))
  .catch(error => {
    // Catches errors from ANY step above
    console.error('Pipeline failed:', error.message);
  })
  .finally(() => {
    // Runs after success OR failure
    console.log('Request completed');
  });

// Multiple catch handlers for different stages
fetch('/api/data')
  .then(response => response.json())
  .catch(error => {
    // Handle network/parsing errors, provide fallback
    console.log('Network error, using cache');
    return getCachedData();
  })
  .then(data => processData(data))
  .catch(error => {
    // Handle processing errors
    console.log('Processing error:', error.message);
    return null;
  });

// Convert callback-based to promise
function readFilePromise(path) {
  return new Promise((resolve, reject) => {
    fs.readFile(path, 'utf8', (err, data) => {
      if (err) reject(err);
      else resolve(data);
    });
  });
}

readFilePromise('/path/to/file')
  .then(content => console.log('Content:', content))
  .catch(error => console.error('Read failed:', error.message));
```
