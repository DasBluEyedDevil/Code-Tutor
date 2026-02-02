---
type: "THEORY"
title: "Part 2: Storage Security Rules"
---


### Basic Structure


### Common Storage Patterns

#### 1. User-Specific Files


#### 2. File Size Limits


#### 3. File Type Validation


#### 4. Public Read, Authenticated Write




```javascript
match /public/{allPaths=**} {
  allow read: if true;
  allow write: if request.auth != null;
}
```
