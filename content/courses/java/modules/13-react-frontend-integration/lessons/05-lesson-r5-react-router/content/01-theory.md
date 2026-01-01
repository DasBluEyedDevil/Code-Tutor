---
type: "THEORY"
title: "The Problem: Single Page Apps Need Routes"
---

React creates Single Page Applications (SPAs). The browser loads ONE HTML page, and React handles all the UI changes.

But users expect:
- Different URLs for different views (/home, /about, /users/123)
- Browser back/forward buttons to work
- Bookmarkable pages
- Shareable links

WITHOUT ROUTING:
function App() {
    const [page, setPage] = useState('home');
    
    return (
        <div>
            <nav>
                <button onClick={() => setPage('home')}>Home</button>
                <button onClick={() => setPage('about')}>About</button>
            </nav>
            {page === 'home' && <HomePage />}
            {page === 'about' && <AboutPage />}
        </div>
    );
}
// Problems:
// - URL doesn't change (always http://localhost:5173/)
// - Can't bookmark pages
// - Back button doesn't work
// - Can't share links to specific pages

REACT ROUTER solves this by syncing the URL with your components.