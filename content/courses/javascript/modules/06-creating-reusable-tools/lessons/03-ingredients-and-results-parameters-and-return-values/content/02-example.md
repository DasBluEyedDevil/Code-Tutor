---
type: "EXAMPLE"
title: "Input and Output"
---

```javascript
// 1. Basic Return Value
function calculateTotal(price, quantity) {
    const total = price * quantity;
    return total; // Hands the value back to whoever called the function
}

// We capture the "handed back" value in a variable
const myBill = calculateTotal(19.99, 3);
console.log(`Your bill is: $${myBill}`);

// 2. Default Parameters
// If 'name' is missing, it defaults to 'Guest'
function welcomeUser(name = 'Guest') {
    return `Welcome, ${name}!`;
}

console.log(welcomeUser('Alice')); // "Welcome, Alice!"
console.log(welcomeUser());        // "Welcome, Guest!"

// 3. Early Return (The "Guard Clause")
function checkAge(age) {
    if (age < 0) {
        return "Invalid age!"; // Stops the function immediately
    }
    
    if (age >= 18) {
        return "You are an adult.";
    } else {
        return "You are a minor.";
    }
}

console.log(checkAge(-5)); // "Invalid age!"
```