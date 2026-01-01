---
type: "KEY_POINT"
title: "Controlled Components"
---

In React, form inputs should be CONTROLLED by state:

UNCONTROLLED (avoid):
<input type="text" />  // React doesn't know the value

CONTROLLED (preferred):
const [value, setValue] = useState('');
<input 
    type="text" 
    value={value}                          // Display state
    onChange={(e) => setValue(e.target.value)}  // Update state
/>

WHY CONTROLLED?
- Single source of truth (state)
- Can validate/transform input
- Can programmatically change value
- React controls the input

FORM WITH VALIDATION:

function SignupForm() {
    const [email, setEmail] = useState('');
    const [error, setError] = useState('');
    
    const handleEmailChange = (e) => {
        const value = e.target.value;
        setEmail(value);
        
        // Validate as user types
        if (value && !value.includes('@')) {
            setError('Invalid email address');
        } else {
            setError('');
        }
    };
    
    return (
        <div>
            <input 
                type="email" 
                value={email} 
                onChange={handleEmailChange}
                className={error ? 'error' : ''}
            />
            {error && <span className="error-text">{error}</span>}
        </div>
    );
}