---
type: "KEY_POINT"
title: "Components: The Building Blocks"
---

React apps are built from COMPONENTS - reusable pieces of UI:

function Welcome() {
    return <h1>Hello, World!</h1>;
}

A component is:
- A JavaScript function
- Returns JSX (HTML-like syntax)
- Name starts with CAPITAL letter
- Can be reused anywhere

COMPONENT HIERARCHY:

App
├── Header
│   ├── Logo
│   └── Navigation
├── MainContent
│   ├── UserList
│   │   └── UserCard (repeated)
│   └── Sidebar
└── Footer

Each component manages its own piece of the UI. Changes in one component don't affect others (unless they share data).

USING COMPONENTS:

function App() {
    return (
        <div>
            <Header />
            <MainContent />
            <Footer />
        </div>
    );
}

Components are like LEGO blocks - snap them together to build complex UIs.