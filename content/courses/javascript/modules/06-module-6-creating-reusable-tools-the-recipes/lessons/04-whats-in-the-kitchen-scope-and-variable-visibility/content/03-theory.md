---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Understanding scope:

**1. Global Scope**
- Variables declared outside any function
- Accessible everywhere in your code
- Use sparingly - can cause naming conflicts

let globalVar = 'accessible everywhere';

function anywhere() {
  console.log(globalVar);  // Can access
}

**2. Function Scope**
- Variables declared inside a function
- Only accessible inside that function
- Includes parameters

function myFunc(param) {  // param has function scope
  let localVar = 'only here';  // localVar has function scope
}

**3. Block Scope** (let and const only)
- Variables declared inside { }
- Only accessible inside that block
- if, for, while, etc. create blocks

if (true) {
  let x = 5;  // Block scoped
  const y = 10;  // Block scoped
  var z = 15;  // Function scoped (escapes block!)
}

**Scope Chain** (looking up variables):

let a = 'global';

function outer() {
  let b = 'outer';
  
  function inner() {
    let c = 'inner';
    
    // JavaScript looks for variables in this order:
    // 1. Current scope (inner) - c
    // 2. Parent scope (outer) - b
    // 3. Grandparent scope (global) - a
  }
}

**Best Practices**:

1. Use let and const (not var)
   - They respect block scope
   - Prevent accidental global variables

2. Keep variables in smallest scope needed
   - Declare inside functions/blocks when possible
   - Reduces naming conflicts

3. Avoid global variables
   - Hard to track who modifies them
   - Can cause bugs

4. Don't shadow variables (same name in nested scopes)
   - Confusing to read
   - Use different names