---
type: "WARNING"
title: "Common Mistakes"
---

**1. Shallow Copy Confusion (Most Common!)**
```javascript
let user = { name: 'Alice', address: { city: 'NYC' } };
let copy = { ...user };
copy.address.city = 'LA';  // ALSO changes user.address.city!

// Fix: Deep copy for nested objects
let deepCopy = JSON.parse(JSON.stringify(user));
// Or: let deepCopy = structuredClone(user);
```

**2. Destructuring undefined/null**
```javascript
let user = null;
let { name } = user;  // TypeError!

// Fix: Provide default empty object
let { name } = user || {};
// Or use optional chaining first:
let name = user?.name;
```

**3. Forgetting Default for Missing Parameter**
```javascript
function greet({ name }) {
  console.log(name);
}
greet();  // TypeError: Cannot destructure 'name' of undefined

// Fix: Add = {} default
function greet({ name } = {}) {
  console.log(name);  // undefined, but no crash
}
```

**4. Order Matters for Spread**
```javascript
// These produce DIFFERENT results!
let result1 = { ...defaults, ...userPrefs };  // userPrefs wins
let result2 = { ...userPrefs, ...defaults };  // defaults wins

// Later properties override earlier ones!
```

**5. Rest Must Be Last**
```javascript
let { ...rest, name } = obj;  // SYNTAX ERROR!
let { name, ...rest } = obj;  // Correct

let [...rest, last] = arr;    // SYNTAX ERROR!
let [first, ...rest] = arr;   // Correct
```

**6. Expecting Destructuring to Create Missing Properties**
```javascript
let { name } = {};  // name is undefined, not an error
let { name } = { title: 'Hi' };  // name is undefined

// Destructuring extracts what exists
// It doesn't create properties that don't exist
```

**7. Confusing Rename Syntax**
```javascript
// { oldName: newName } extracts oldName AS newName
let { title: name } = { title: 'Alice' };
console.log(name);   // 'Alice'
console.log(title);  // ReferenceError: title is not defined

// The LEFT side is the property name
// The RIGHT side is the new variable name
```

**8. Mutating Spread Copies (Still References for Nested)**
```javascript
let original = [{ id: 1 }, { id: 2 }];
let copy = [...original];

// The array is copied, but objects inside are same references!
copy[0].id = 999;
console.log(original[0].id);  // 999 - CHANGED!
```