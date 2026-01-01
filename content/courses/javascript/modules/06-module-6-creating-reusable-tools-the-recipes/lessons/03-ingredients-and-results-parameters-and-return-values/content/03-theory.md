---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding parameters and returns:

**Parameters** (Function Definition):
function doSomething(param1, param2, param3) {
                     │      │      │
                     └──────┴──────┴─ Placeholders for values
}

**Arguments** (Function Call):
doSomething(value1, value2, value3);
            │       │       │
            └───────┴───────┴─ Actual values passed in

Parameter patterns:

1. **No parameters**:
   function sayHello() {
     return 'Hello!';
   }

2. **One parameter**:
   function double(x) {
     return x * 2;
   }

3. **Multiple parameters**:
   function add(a, b) {
     return a + b;
   }

4. **Default parameters** (ES2015):
   function greet(name = 'Guest', greeting = 'Hello') {
     return greeting + ', ' + name;
   }
   greet();  // Hello, Guest
   greet('Alice');  // Hello, Alice
   greet('Bob', 'Hi');  // Hi, Bob

5. **Rest parameters** (collect all remaining args):
   function sum(...numbers) {
     return numbers.reduce((total, n) => total + n, 0);
   }
   sum(1, 2, 3, 4);  // 10

Return value patterns:

1. **Return a value**:
   return 42;
   return 'text';
   return true;

2. **Return early** (exit function immediately):
   if (error) {
     return 'Error';
   }
   // Rest of function

3. **No return** (implicitly returns undefined):
   function doSomething() {
     console.log('Done');
   }

4. **Return object** (use parentheses with arrow functions):
   const getUser = () => ({name: 'Alice', age: 25});

5. **Return another function**:
   function outer() {
     return function inner() {
       return 'Hello';
     };
   }