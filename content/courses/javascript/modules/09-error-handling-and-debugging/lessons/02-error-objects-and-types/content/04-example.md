---
type: "EXAMPLE"
title: "Error Cause Chaining (ES2022)"
---

The error cause feature lets you create a chain of errors, preserving the full history of what went wrong.

```javascript
// Error cause chaining - ES2022 feature
// This creates a traceable chain of errors

function connectToDatabase() {
  throw new Error('Connection refused: port 5432');
}

function initializeApp() {
  try {
    connectToDatabase();
  } catch (dbError) {
    throw new Error('Database initialization failed', {
      cause: dbError
    });
  }
}

function startServer() {
  try {
    initializeApp();
  } catch (initError) {
    throw new Error('Server failed to start', {
      cause: initError
    });
  }
}

// Now let's catch and trace the full error chain
try {
  startServer();
} catch (error) {
  console.log('=== Error Chain ===');
  
  let currentError = error;
  let depth = 0;
  
  while (currentError) {
    console.log(`${'  '.repeat(depth)}${currentError.message}`);
    currentError = currentError.cause;
    depth++;
  }
}
// Output:
// === Error Chain ===
// Server failed to start
//   Database initialization failed
//     Connection refused: port 5432

// Utility function to get full error chain
function getErrorChain(error) {
  const chain = [];
  let current = error;
  
  while (current) {
    chain.push({
      name: current.name,
      message: current.message
    });
    current = current.cause;
  }
  
  return chain;
}

try {
  startServer();
} catch (error) {
  const chain = getErrorChain(error);
  console.log('Error chain:', JSON.stringify(chain, null, 2));
}
```
