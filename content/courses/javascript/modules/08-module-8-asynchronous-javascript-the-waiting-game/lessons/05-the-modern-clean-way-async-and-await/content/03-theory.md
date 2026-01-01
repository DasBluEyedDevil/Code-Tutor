---
type: "THEORY"
title: "Breaking Down the Syntax"
---

async/await syntax:

**async function:**

async function functionName() {
│     │
│     └─ Makes function asynchronous
└─────── async keyword required
  // Can use 'await' inside
}

// async function automatically returns a Promise
async function getName() {
  return 'Alice';  // Becomes Promise.resolve('Alice')
}

// These are equivalent:
async function a() { return 'Hi'; }
function b() { return Promise.resolve('Hi'); }

**await keyword:**

let result = await promise;
│            │     │
│            │     └─ A Promise
│            └─────── await keyword (pauses until resolved)
└──────────────────── Result of the promise

Rules for await:
1. Can ONLY be used inside async functions (or module top-level)
2. Pauses function execution until Promise resolves
3. Returns the resolved value
4. Throws if Promise rejects (use try/catch)

**Error Handling:**

// Promises:
promise
  .then(result => { })
  .catch(error => { });

// async/await:
try {
  let result = await promise;
} catch (error) {
  // Handle error
}

**Sequential vs Parallel:**

// Sequential (one after another - SLOW)
async function sequential() {
  let a = await fetchA();  // Wait 1 second
  let b = await fetchB();  // Wait 1 second
  // Total: 2 seconds
}

// Parallel (at same time - FAST)
async function parallel() {
  let [a, b] = await Promise.all([
    fetchA(),  // Both start at same time
    fetchB()
  ]);
  // Total: 1 second (whichever is slower)
}

**Common Patterns:**

1. Simple fetch:
   async function getData() {
     let response = await fetch('/api/data');
     let data = await response.json();
     return data;
   }

2. With error handling:
   async function getData() {
     try {
       let response = await fetch('/api/data');
       if (!response.ok) throw new Error('Failed');
       return await response.json();
     } catch (error) {
       console.log('Error:', error);
       return null;
     }
   }

3. Multiple parallel requests:
   async function getAll() {
     let [users, posts, comments] = await Promise.all([
       fetch('/api/users').then(r => r.json()),
       fetch('/api/posts').then(r => r.json()),
       fetch('/api/comments').then(r => r.json())
     ]);
     return { users, posts, comments };
   }