---
type: "EXAMPLE"
title: "The finally Block - Runs No Matter What"
---

The finally block executes regardless of whether an error occurred or not. It even runs after a return statement! This is perfect for cleanup operations.

```javascript
// The finally block ALWAYS runs
function fetchData() {
  console.log('Starting fetch...');
  
  try {
    // Simulating a risky operation
    let data = JSON.parse('{"status": "ok"}');
    console.log('Data fetched successfully');
    return data; // Note: we're returning here!
  } catch (error) {
    console.log('Error occurred:', error.message);
    return null;
  } finally {
    // This runs EVEN AFTER the return statement!
    console.log('Cleanup: Closing connection...');
  }
}

let result = fetchData();
console.log('Result:', result);
// Output:
// Starting fetch...
// Data fetched successfully
// Cleanup: Closing connection... (finally runs after return!)
// Result: { status: 'ok' }

// Practical example: File handling pattern
function readConfigFile(filename) {
  let file = null;
  
  try {
    console.log('Opening file:', filename);
    file = openFile(filename); // Imagine this opens a file
    let content = file.read();
    return JSON.parse(content);
  } catch (error) {
    console.log('Failed to read config:', error.message);
    return getDefaultConfig();
  } finally {
    // ALWAYS close the file, even if reading failed
    if (file) {
      console.log('Closing file...');
      file.close();
    }
  }
}

// The finally block is guaranteed to run for:
// - Successful execution
// - After catching an error
// - Even after return statements
// - Even after throw statements (before propagating)
```
