---
type: "THEORY"
title: "The Logic Gates"
---

Logical operators act like gates. Depending on whether the inputs are `true` or `false`, the gate opens or stays closed.

### 1. AND (`&&`)
Think of this as a strict filter. 
*   `true && true` is `true`
*   `true && false` is `false`
*   `false && false` is `false`

### 2. OR (`||`)
Think of this as a flexible gate. It only stays closed if **everything** is false.
*   `true || false` is `true`
*   `false || true` is `true`
*   `false || false` is `false`

### 3. NOT (`!`)
This operator is a "toggle." It is always placed **before** the value it is modifying.
*   `!true` is `false`
*   `!false` is `true`

### 4. Short-Circuiting (Advanced)
JavaScript is efficient. If it's using an `&&` and the first condition is `false`, it doesn't even look at the second one because it knows the whole thing must be `false` anyway. 
Similarly, with `||`, if the first part is `true`, it stops right there. This is called **Short-Circuit Evaluation**.

### 5. Operator Precedence
Just like multiplication happens before addition, `&&` happens before `||`.
`true || false && false` results in `true`.
(Because the `false && false` is calculated first).
**Always use parentheses `( )` to make your intentions clear!**
