---
type: "KEY_POINT"
title: "Props Rules"
---

PROPS ARE READ-ONLY:

// WRONG - never modify props!
function BadComponent(props) {
    props.name = "Changed";  // ERROR!
    return <h1>{props.name}</h1>;
}

// RIGHT - props are immutable
function GoodComponent({ name }) {
    return <h1>{name}</h1>;
}

DATA FLOWS DOWN:

App (has user data)
  ↓ passes via props
UserProfile (receives user)
  ↓ passes via props  
UserAvatar (receives name, image)

Parent → Child only. Never upward directly (we'll learn how in next lesson).

DEFAULT PROPS:

function Button({ label = "Click me", color = "blue" }) {
    return (
        <button style={{ backgroundColor: color }}>
            {label}
        </button>
    );
}

<Button />                    // Uses defaults
<Button label="Submit" />     // Custom label, default color
<Button color="red" />        // Default label, custom color

CHILDREN PROP:

function Card({ children, title }) {
    return (
        <div className="card">
            <h2>{title}</h2>
            <div className="card-body">
                {children}
            </div>
        </div>
    );
}

<Card title="Welcome">
    <p>This paragraph is passed as children!</p>
    <button>Click me</button>
</Card>