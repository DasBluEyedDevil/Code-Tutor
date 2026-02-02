---
type: "THEORY"
title: "Breaking Down the Syntax"
---

Components and Props explained:

1. **Function Component** (modern standard):
   ```jsx
   function Welcome(props) {
     return <h1>Hello, {props.name}!</h1>;
   }
   
   // Use it:
   <Welcome name="Alice" />
   ```

2. **Props Object**:
   ```jsx
   function UserCard(props) {
     // props = { name: 'Alice', age: 25, email: 'alice@...' }
     return (
       <div>
         <h2>{props.name}</h2>
         <p>Age: {props.age}</p>
         <p>Email: {props.email}</p>
       </div>
     );
   }
   ```

3. **Destructuring Props** (recommended):
   ```jsx
   // Instead of props.name, props.age...
   function UserCard({ name, age, email }) {
     return (
       <div>
         <h2>{name}</h2>
         <p>Age: {age}</p>
         <p>Email: {email}</p>
       </div>
     );
   }
   ```

4. **Default Props**:
   ```jsx
   function Button({ label, color = 'blue', size = 'medium' }) {
     return <button style={{ backgroundColor: color }}>{label}</button>;
   }
   
   // Uses defaults:
   <Button label="Click" />  // blue, medium
   
   // Override defaults:
   <Button label="Submit" color="green" size="large" />
   ```

5. **Children Prop** (special):
   ```jsx
   function Card({ title, children }) {
     return (
       <div className="card">
         <h3>{title}</h3>
         <div className="card-body">
           {children}
         </div>
       </div>
     );
   }
   
   // Use with children:
   <Card title="My Card">
     <p>This is the content</p>
     <button>Action</button>
   </Card>
   ```

6. **Passing Functions as Props**:
   ```jsx
   function Button({ label, onClick }) {
     return <button onClick={onClick}>{label}</button>;
   }
   
   // Parent passes function:
   function App() {
     function handleClick() {
       console.log('Clicked!');
     }
     
     return <Button label="Click Me" onClick={handleClick} />;
   }
   ```

7. **Component Composition**:
   ```jsx
   function App() {
     return (
       <div>
         <Header />
         <Sidebar />
         <MainContent>
           <Article title="Hello" />
           <Article title="World" />
         </MainContent>
         <Footer />
       </div>
     );
   }
   ```