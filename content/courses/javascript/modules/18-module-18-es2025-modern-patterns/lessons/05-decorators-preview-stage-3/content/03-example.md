---
type: "EXAMPLE"
title: "Practical Decorators"
---

Common real-world decorator patterns:

```javascript
// Rate limiting decorator
function rateLimit(callsPerSecond) {
  let lastCall = 0;
  return function(target, context) {
    return function(...args) {
      const now = Date.now();
      const minInterval = 1000 / callsPerSecond;
      if (now - lastCall < minInterval) {
        throw new Error('Rate limit exceeded');
      }
      lastCall = now;
      return target.apply(this, args);
    };
  };
}

class ApiClient {
  @rateLimit(10)  // Max 10 calls per second
  async fetchData(id) {
    return await fetch(`/api/${id}`);
  }
}

// Validation decorator
function validate(schema) {
  return function(target, context) {
    return function(...args) {
      for (let i = 0; i < args.length; i++) {
        if (!schema[i](args[i])) {
          throw new TypeError(`Invalid argument at position ${i}`);
        }
      }
      return target.apply(this, args);
    };
  };
}

class UserService {
  @validate([x => typeof x === 'number', x => typeof x === 'string'])
  createUser(id, name) {
    return { id, name };
  }
}
```
