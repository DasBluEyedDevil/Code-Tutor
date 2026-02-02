---
type: "WARNING"
title: "Common Pitfalls"
---

Common JSX mistakes:

1. **Using `class` instead of `className`**:
   ```jsx
   // Wrong!
   <div class="container">  // Error in JSX
   
   // Correct!
   <div className="container">
   ```

2. **Forgetting to close tags**:
   ```jsx
   // Wrong!
   <img src="logo.png">     // Error!
   <br>                     // Error!
   
   // Correct!
   <img src="logo.png" />
   <br />
   ```

3. **Quotes around JavaScript expressions**:
   ```jsx
   // Wrong!
   <p>{"userName"}</p>       // Renders the string "userName"
   <p>{age + 1}</p>         // Wrong quotes
   
   // Correct!
   <p>{userName}</p>        // Uses the variable
   <p>{age + 1}</p>         // No quotes for expressions
   ```

4. **Multiple root elements**:
   ```jsx
   // Wrong!
   return (
     <h1>Title</h1>
     <p>Text</p>            // Error: Adjacent JSX elements
   );
   
   // Correct!
   return (
     <>
       <h1>Title</h1>
       <p>Text</p>
     </>
   );
   ```

5. **Inline style as string**:
   ```jsx
   // Wrong! (HTML syntax)
   <div style="color: red; font-size: 16px">
   
   // Correct! (JSX object syntax)
   <div style={{ color: 'red', fontSize: 16 }}>
   //          ^^^^ object  ^^^^^ camelCase
   ```

6. **Event handler called immediately**:
   ```jsx
   // Wrong!
   <button onClick={handleClick()}>  // Calls immediately!
   
   // Correct!
   <button onClick={handleClick}>    // Passes function reference
   <button onClick={() => handleClick()}>  // Arrow function wrapper
   ```

7. **Conditional rendering syntax**:
   ```jsx
   // Wrong!
   <div>
     if (isLoggedIn) {      // Can't use if in JSX!
       <p>Welcome</p>
     }
   </div>
   
   // Correct!
   <div>
     {isLoggedIn && <p>Welcome</p>}           // && operator
     {isLoggedIn ? <p>Hi</p> : <p>Login</p>}  // Ternary
   </div>
   ```