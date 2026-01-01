---
type: "EXAMPLE"
title: "The try/await/catch Pattern"
---

The most important pattern for modern async error handling. This is what you'll use 90% of the time.

```javascript
// The try/await/catch pattern - your go-to for async errors
async function fetchUserData(userId) {
  try {
    // await pauses until the promise resolves or rejects
    const response = await fetch(`/api/users/${userId}`);
    
    // Check for HTTP errors (fetch doesn't throw on 404/500)
    if (!response.ok) {
      throw new Error(`HTTP error! status: ${response.status}`);
    }
    
    const data = await response.json();
    console.log('User data:', data);
    return data;
    
  } catch (error) {
    // This catches BOTH network errors AND HTTP errors
    console.error('Failed to fetch user:', error.message);
    return null;
  }
}

// Calling async functions
async function main() {
  const user = await fetchUserData(123);
  if (user) {
    console.log('Got user:', user.name);
  } else {
    console.log('Could not load user');
  }
}

main();

// Multiple await calls in one try block
async function getFullProfile(userId) {
  try {
    const user = await fetchUser(userId);
    const posts = await fetchUserPosts(userId);
    const friends = await fetchUserFriends(userId);
    
    return { user, posts, friends };
    
  } catch (error) {
    // Any of the three awaits can trigger this catch
    console.error('Failed to load profile:', error.message);
    return null;
  }
}

// With finally for cleanup
async function loadDataWithLoading() {
  showLoadingSpinner();
  
  try {
    const data = await fetchData();
    displayData(data);
  } catch (error) {
    showError(error.message);
  } finally {
    // Always hide spinner, success or failure
    hideLoadingSpinner();
  }
}
```
