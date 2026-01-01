---
type: "THEORY"
title: "Event Handling"
---

React events are camelCase and take function references:

BASIC EVENTS:

// Click
<button onClick={handleClick}>Click me</button>

// With parameter (use arrow function)
<button onClick={() => handleDelete(item.id)}>Delete</button>

// Event object
<button onClick={(e) => {
    e.preventDefault();  // Prevent default behavior
    console.log(e.target);  // The clicked element
}}>Submit</button>

COMMON EVENTS:

// Mouse
onClick, onDoubleClick, onMouseEnter, onMouseLeave

// Keyboard
onKeyDown, onKeyUp, onKeyPress

// Form
onChange, onSubmit, onFocus, onBlur

// Touch
onTouchStart, onTouchEnd, onTouchMove

FORM EXAMPLE:

function LoginForm() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    
    const handleSubmit = (e) => {
        e.preventDefault();  // Prevent page reload!
        console.log('Login:', email, password);
        // Call API here
    };
    
    return (
        <form onSubmit={handleSubmit}>
            <input 
                type="email" 
                value={email} 
                onChange={(e) => setEmail(e.target.value)} 
            />
            <input 
                type="password" 
                value={password} 
                onChange={(e) => setPassword(e.target.value)} 
            />
            <button type="submit">Login</button>
        </form>
    );
}