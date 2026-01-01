---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// OLD WAY: Promises with .then()
function getUser() {
  fetchUser(1)
    .then(user => {
      console.log('User:', user);
      return fetchPosts(user.id);
    })
    .then(posts => {
      console.log('Posts:', posts);
    })
    .catch(error => {
      console.log('Error:', error);
    });
}

// NEW WAY: async/await (same thing, cleaner!)
async function getUser() {
  try {
    let user = await fetchUser(1);
    console.log('User:', user);
    
    let posts = await fetchPosts(user.id);
    console.log('Posts:', posts);
  } catch (error) {
    console.log('Error:', error);
  }
}

// async function returns a Promise
async function getData() {
  return 'Hello';  // Automatically wrapped in Promise.resolve()
}

getData().then(result => console.log(result));  // Hello

// await pauses until Promise resolves
async function example() {
  console.log('Start');
  
  let result = await someAsyncOperation();  // Waits here
  console.log('Result:', result);  // Runs after promise resolves
  
  console.log('End');
}

// Multiple awaits in sequence
async function sequential() {
  let user = await fetchUser(1);  // Wait for this
  let posts = await fetchPosts(user.id);  // Then wait for this
  let comments = await fetchComments(posts[0].id);  // Then wait for this
  return { user, posts, comments };
}

// Multiple awaits in parallel (faster!)
async function parallel() {
  // Start all at once
  let userPromise = fetchUser(1);
  let postsPromise = fetchPosts(1);
  let commentsPromise = fetchComments(1);
  
  // Wait for all to finish
  let user = await userPromise;
  let posts = await postsPromise;
  let comments = await commentsPromise;
  
  return { user, posts, comments };
}

// Or use Promise.all
async function parallelAll() {
  let [user, posts, comments] = await Promise.all([
    fetchUser(1),
    fetchPosts(1),
    fetchComments(1)
  ]);
  return { user, posts, comments };
}

// Error handling
async function withErrorHandling() {
  try {
    let data = await riskyOperation();
    return data;
  } catch (error) {
    console.log('Caught error:', error);
    return null;
  }
}

// Can only use await inside async function
// This is WRONG:
function normal() {
  let result = await fetchData();  // ERROR!
}

// This is CORRECT:
async function async() {
  let result = await fetchData();  // Works!
}

// Top-level await (ES2022 - in modules only)
await fetchData();  // Now works at module top level!
```
