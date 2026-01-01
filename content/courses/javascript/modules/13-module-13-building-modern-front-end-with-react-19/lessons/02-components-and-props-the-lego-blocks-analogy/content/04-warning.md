---
type: "WARNING"
title: "Common Pitfalls"
---

Common component/props mistakes:

1. **Modifying props (forbidden!)**:
   ```jsx
   function UserCard(props) {
     props.name = 'Different';  // ERROR! Props are read-only!
     return <h1>{props.name}</h1>;
   }
   ```
   Props flow down (parent → child) and cannot be changed by child.

2. **Forgetting to pass props**:
   ```jsx
   function Greeting({ name }) {
     return <h1>Hello, {name}!</h1>;
   }
   
   // Wrong!
   <Greeting />  // name is undefined!
   
   // Correct!
   <Greeting name="Alice" />
   ```

3. **Component name not capitalized**:
   ```jsx
   // Wrong!
   function greeting() {  // lowercase!
     return <h1>Hello</h1>;
   }
   
   // Correct!
   function Greeting() {  // PascalCase!
     return <h1>Hello</h1>;
   }
   ```
   React treats lowercase as HTML tags, uppercase as components.

4. **Not destructuring (verbose)**:
   ```jsx
   // Works but verbose:
   function UserCard(props) {
     return <div>{props.name} - {props.email} - {props.age}</div>;
   }
   
   // Better (destructured):
   function UserCard({ name, email, age }) {
     return <div>{name} - {email} - {age}</div>;
   }
   ```

5. **Missing key in lists**:
   ```jsx
   // Wrong!
   {users.map(user => <UserCard {...user} />)}
   
   // Correct!
   {users.map(user => <UserCard key={user.id} {...user} />)}
   ```
   React needs keys to track which items changed.

6. **Passing strings incorrectly**:
   ```jsx
   // Wrong!
   <UserCard age="25" />     // age is string "25", not number!
   
   // Correct!
   <UserCard age={25} />     // age is number 25
   
   // Strings don't need braces:
   <UserCard name="Alice" /> // OK
   <UserCard name={'Alice'} /> // Also OK but unnecessary
   ```

7. **Inline object props (causes re-renders)**:
   ```jsx
   // Avoid (creates new object every render):
   <UserCard style={{ color: 'red' }} />
   
   // Better (define outside):
   const cardStyle = { color: 'red' };
   <UserCard style={cardStyle} />
   ```