---
type: "KEY_POINT"
title: "Navigation and Active Links"
---

LINK VS NAVLINK:

import { Link, NavLink } from 'react-router-dom';

// Link - basic navigation
<Link to="/about">About</Link>

// NavLink - adds active class automatically
<NavLink 
    to="/about" 
    className={({ isActive }) => isActive ? 'active' : ''}
>
    About
</NavLink>

NAVIGATE PROGRAMMATICALLY:

import { useNavigate } from 'react-router-dom';

function SomeComponent() {
    const navigate = useNavigate();
    
    function handleClick() {
        navigate('/path');         // Navigate to path
        navigate(-1);               // Go back
        navigate(1);                // Go forward
        navigate('/path', { replace: true }); // Replace current history
    }
}

PASS STATE VIA NAVIGATION:

// Sender
navigate('/checkout', { state: { cart: cartItems } });

// Receiver
import { useLocation } from 'react-router-dom';

function Checkout() {
    const location = useLocation();
    const { cart } = location.state || {};
}

QUERY PARAMETERS:

import { useSearchParams } from 'react-router-dom';

function SearchResults() {
    const [searchParams, setSearchParams] = useSearchParams();
    const query = searchParams.get('q');      // ?q=react
    const page = searchParams.get('page');    // ?page=2
    
    // Update search params
    setSearchParams({ q: 'new query', page: 1 });
}