---
type: "EXAMPLE"
title: "Understanding Scope"
---

```javascript
// 1. Global Scope
// Defined outside of any function
const buildingName = "Central Plaza";

function showInfo() {
    // A function can see global variables
    console.log(`Welcome to ${buildingName}`);
    
    // 2. Local (Function) Scope
    const roomNumber = 201;
    console.log(`You are in room ${roomNumber}`);
}

showInfo();
// console.log(roomNumber); // ERROR! roomNumber only exists inside the function

// 3. Block Scope (let and const)
// if, for, and while blocks have their own scope
if (true) {
    const secretCode = "XY-123";
    console.log(secretCode); // Works
}
// console.log(secretCode); // ERROR! secretCode only exists inside the if-block

// 4. Shadowing
// Using the same name in different scopes
const city = "New York";

function travel() {
    const city = "London"; // This "shadows" the global city
    console.log(`Current city: ${city}`); // London
}

travel();
console.log(`Home city: ${city}`); // New York (global was never changed)
```