---
type: "THEORY"
title: "Breaking Down the Syntax"
---

JSX syntax guide:

1. **JSX is JavaScript, not HTML**:
   ```jsx
   // This JSX:
   const element = <h1>Hello</h1>;
   
   // Compiles to:
   const element = React.createElement('h1', null, 'Hello');
   ```

2. **Embedding Expressions** with {}:
   ```jsx
   const name = 'Alice';
   const element = <h1>Hello, {name}!</h1>;
   
   // Any JavaScript expression works:
   <p>{2 + 2}</p>                    // 4
   <p>{user.name.toUpperCase()}</p>  // ALICE
   <p>{isLoggedIn ? 'Hi' : 'Please login'}</p>
   ```

3. **Attributes in JSX**:
   ```jsx
   // className not class
   <div className="container"></div>
   
   // htmlFor not for
   <label htmlFor="email">Email:</label>
   
   // camelCase event handlers
   <button onClick={handleClick}>Click</button>
   
   // Style as object
   <div style={{ color: 'red', fontSize: 16 }}></div>
   ```

4. **Self-Closing Tags**:
   ```jsx
   // Must include /
   <img src="logo.png" />
   <br />
   <input type="text" />
   ```

5. **Children**:
   ```jsx
   // String children
   <h1>Title</h1>
   
   // Expression children
   <p>{user.name}</p>
   
   // Component children
   <div>
     <Header />
     <Main />
     <Footer />
   </div>
   ```

6. **Fragments** (avoid extra divs):
   ```jsx
   // Shorthand
   return (
     <>
       <h1>Title</h1>
       <p>Text</p>
     </>
   );
   
   // Full syntax (needed for keys)
   return (
     <React.Fragment>
       <h1>Title</h1>
       <p>Text</p>
     </React.Fragment>
   );
   ```

7. **Comments in JSX**:
   ```jsx
   return (
     <div>
       {/* This is a comment */}
       <h1>Title</h1>
     </div>
   );
   ```