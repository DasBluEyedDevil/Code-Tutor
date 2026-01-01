---
type: "EXAMPLE"
title: "Object Destructuring Basics"
---

Object destructuring lets you extract properties from an object into standalone variables. The variable names must match the property names (unless you rename them).

```javascript
// === THE OLD WAY: Repetitive property access ===
let user = {
  name: 'Alice',
  age: 28,
  email: 'alice@example.com',
  city: 'New York'
};

// Tedious - we write 'user.' over and over
let name = user.name;
let age = user.age;
let email = user.email;
console.log(name, age, email);  // Alice 28 alice@example.com

// === THE NEW WAY: Destructuring ===
let user2 = {
  name: 'Bob',
  age: 32,
  email: 'bob@example.com',
  city: 'Los Angeles'
};

// Extract multiple properties in one line!
let { name: userName, age: userAge, email: userEmail } = user2;
console.log(userName, userAge, userEmail);  // Bob 32 bob@example.com

// Simpler: if variable names match property names
let { name: n, age: a, email: e, city: c } = user2;
// Wait, let's use matching names (most common):
let person = { name: 'Charlie', age: 25, city: 'Chicago' };
let { name: personName, age: personAge, city: personCity } = person;
console.log(personName, personAge);  // Charlie 25

// === RENAMING: When you want different variable names ===
let product = { title: 'Laptop', price: 999 };

// Rename 'title' to 'productName' and 'price' to 'cost'
let { title: productName, price: cost } = product;
console.log(productName);  // 'Laptop'
console.log(cost);         // 999
// Note: 'title' and 'price' are NOT created as variables!

// === DEFAULT VALUES: When property might not exist ===
let config = { theme: 'dark' };

// 'language' doesn't exist, so use default 'en'
let { theme, language = 'en', fontSize = 16 } = config;
console.log(theme);     // 'dark' (from object)
console.log(language);  // 'en' (default - property was undefined)
console.log(fontSize);  // 16 (default - property was undefined)

// Default only applies if property is undefined, not null!
let settings = { volume: null };
let { volume = 50 } = settings;
console.log(volume);  // null (not 50! null is a value, not undefined)

// === COMBINING RENAME AND DEFAULT ===
let response = { status: 200 };
let { status: httpStatus = 500, message: errorMsg = 'Unknown' } = response;
console.log(httpStatus);  // 200
console.log(errorMsg);    // 'Unknown' (default)
```
