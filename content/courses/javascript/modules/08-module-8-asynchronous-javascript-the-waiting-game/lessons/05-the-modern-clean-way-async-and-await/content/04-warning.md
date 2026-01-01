---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Forgetting 'async' keyword:
   function getData() {
     let result = await fetch();  // ERROR!
   }
   
   Must be:
   async function getData() {
     let result = await fetch();
   }

2. Not awaiting Promises:
   async function getData() {
     let data = fetchData();  // data is a Promise!
     console.log(data.name);  // undefined!
   }
   
   Must await:
   async function getData() {
     let data = await fetchData();
     console.log(data.name);  // Works!
   }

3. Sequential when could be parallel:
   // SLOW (3 seconds total):
   async function slow() {
     let a = await fetchA();  // 1s
     let b = await fetchB();  // 1s
     let c = await fetchC();  // 1s
   }
   
   // FAST (1 second total):
   async function fast() {
     let [a, b, c] = await Promise.all([
       fetchA(),
       fetchB(),
       fetchC()
     ]);  // All at once!
   }

4. Not handling errors:
   async function getData() {
     let data = await fetch('/api/data');  // What if it fails?
   }
   
   Always try/catch:
   async function getData() {
     try {
       let data = await fetch('/api/data');
     } catch (error) {
       console.log('Error:', error);
     }
   }

5. Mixing async/await with .then():
   // Pick one style, don't mix:
   async function mixed() {
     let data = await fetchData();
     data.then(result => { });  // Confusing!
   }
   
   // Use async/await consistently:
   async function clean() {
     let data = await fetchData();
     let result = await processData(data);
   }