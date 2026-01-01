---
type: "KEY_POINT"
title: "Setting Up React Router"
---

Install React Router:

npm install react-router-dom

BASIC SETUP:

import { BrowserRouter, Routes, Route, Link } from 'react-router-dom';

function App() {
    return (
        <BrowserRouter>
            <nav>
                <Link to="/">Home</Link>
                <Link to="/about">About</Link>
                <Link to="/users">Users</Link>
            </nav>
            
            <Routes>
                <Route path="/" element={<HomePage />} />
                <Route path="/about" element={<AboutPage />} />
                <Route path="/users" element={<UsersPage />} />
                <Route path="*" element={<NotFoundPage />} />
            </Routes>
        </BrowserRouter>
    );
}

KEY COMPONENTS:
- BrowserRouter: Wraps your entire app, enables routing
- Routes: Container for Route components
- Route: Maps a path to a component
- Link: Navigation links (like <a> but no page reload)

Now:
- / renders HomePage
- /about renders AboutPage
- /users renders UsersPage
- Anything else renders NotFoundPage