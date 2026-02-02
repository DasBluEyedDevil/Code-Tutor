---
type: "EXAMPLE"
title: "Decorator Syntax"
---

Decorators use the @ symbol and can wrap classes, methods, and fields:

```javascript
// Method decorator - add logging
function logged(target, context) {
  return function (...args) {
    console.log(`Calling ${context.name} with`, args);
    const result = target.apply(this, args);
    console.log(`${context.name} returned`, result);
    return result;
  };
}

class Calculator {
  @logged
  add(a, b) {
    return a + b;
  }
}

const calc = new Calculator();
calc.add(2, 3);
// Logs: Calling add with [2, 3]
// Logs: add returned 5
```
