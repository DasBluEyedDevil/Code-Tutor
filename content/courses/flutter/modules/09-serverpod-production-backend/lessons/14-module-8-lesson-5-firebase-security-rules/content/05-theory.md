---
type: "THEORY"
title: "Part 1: Firestore Security Rules"
---


### Basic Structure


### The Four Operations




```javascript
allow read;   // = get + list
allow write;  // = create + update + delete

// Or be specific:
allow get;      // Read single document
allow list;     // Read multiple documents (query)
allow create;   // Create new document
allow update;   // Update existing document
allow delete;   // Delete document
```
