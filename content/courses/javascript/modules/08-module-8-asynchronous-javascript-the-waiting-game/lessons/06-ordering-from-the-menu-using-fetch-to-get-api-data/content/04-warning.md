---
type: "WARNING"
title: "Common Pitfalls"
---

Common mistakes:

1. Not awaiting response.json():
   let response = await fetch(url);
   let data = response.json();  // WRONG - data is a Promise!
   
   Must await:
   let data = await response.json();

2. Forgetting to check response.ok:
   let response = await fetch(url);
   let data = await response.json();  // Might fail!
   
   Always check:
   if (!response.ok) {
     throw new Error('HTTP error');
   }

3. Not stringifying request body:
   fetch(url, {
     body: {name: 'Alice'}  // WRONG - object!
   });
   
   Must stringify:
   fetch(url, {
     body: JSON.stringify({name: 'Alice'})
   });

4. CORS errors (Cross-Origin Request Blocked):
   // Can't fetch from different domain without server permission
   fetch('https://other-site.com/api')  // Might be blocked
   // Server must send CORS headers to allow

5. Forgetting Content-Type header:
   fetch(url, {
     method: 'POST',
     body: JSON.stringify(data)  // Server might not parse it!
   });
   
   Need header:
   fetch(url, {
     method: 'POST',
     headers: {
       'Content-Type': 'application/json'
     },
     body: JSON.stringify(data)
   });

6. Not handling network errors:
   // If internet is down, fetch throws
   try {
     await fetch(url);
   } catch (error) {
     // Handle network error
   }