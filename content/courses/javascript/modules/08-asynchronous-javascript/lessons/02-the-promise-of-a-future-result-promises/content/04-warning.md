---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting to return a promise from a function:
   function getData() {
     new Promise((resolve) => { ... });  // WRONG - not returned!
   }
   
   function getData() {
     return new Promise((resolve) => { ... });  // CORRECT
   }

2. Not returning in .then() for chaining:
   promise
     .then(data => {
       processData(data);  // Returns undefined!
     })
     .then(result => {
       console.log(result);  // undefined
     });
   
   Must return:
   .then(data => {
     return processData(data);
   })

3. Calling resolve/reject multiple times:
   new Promise((resolve) => {
     resolve('first');
     resolve('second');  // Ignored!
   });
   // Only first resolve/reject counts

4. Mixing callbacks and promises:
   // Don't do this:
   getData(function(result) {
     // Old callback style mixed with promises
   });
   
   // Use promises consistently

5. Not catching errors:
   fetch('/api/data')
     .then(response => response.json());
   // If error occurs, it's unhandled!
   
   Always add .catch():
   fetch('/api/data')
     .then(response => response.json())
     .catch(error => console.log(error));