---
type: "EXAMPLE"
title: "Code Example"
---

See the code example above demonstrating Code Example.

```javascript
// Global scope - accessible everywhere
let globalVar = 'I am global';

function testScope() {
  // Function scope - only accessible inside this function
  let localVar = 'I am local';
  
  console.log(globalVar);  // Works - can access global
  console.log(localVar);   // Works - we're inside the function
}

testScope();
console.log(globalVar);  // Works - global is accessible
// console.log(localVar);   // ERROR - localVar doesn't exist here!

// Block scope (let and const)
if (true) {
  let blockVar = 'I am in a block';
  const alsoBlock = 'Me too';
  var notBlock = 'I escape!';
  
  console.log(blockVar);  // Works
}

// console.log(blockVar);  // ERROR - blockVar is block-scoped
console.log(notBlock);   // Works - var ignores block scope (bad!)

// Nested scopes
let outer = 'outer';

function outerFunc() {
  let middle = 'middle';
  
  function innerFunc() {
    let inner = 'inner';
    
    console.log(outer);   // Works - can see outer
    console.log(middle);  // Works - can see parent
    console.log(inner);   // Works - own scope
  }
  
  innerFunc();
  // console.log(inner);  // ERROR - can't see child scope
}

outerFunc();

// Shadowing (be careful!)
let name = 'Global Alice';

function greet() {
  let name = 'Local Bob';  // Different variable!
  console.log(name);  // Local Bob
}

greet();
console.log(name);  // Global Alice
```
