---
type: "EXAMPLE"
title: "Dynamic Routes and URL Parameters"
---

Routes with parameters for dynamic content:

```jsx
import { 
    BrowserRouter, Routes, Route, Link, 
    useParams, useNavigate 
} from 'react-router-dom';

// Route with parameter
function App() {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/users" element={<UserList />} />
                <Route path="/users/:userId" element={<UserProfile />} />
                <Route path="/users/:userId/posts" element={<UserPosts />} />
            </Routes>
        </BrowserRouter>
    );
}

// Access URL parameters with useParams
function UserProfile() {
    const { userId } = useParams();  // Gets :userId from URL
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);
    
    useEffect(() => {
        fetch(`/api/users/${userId}`)
            .then(res => res.json())
            .then(data => {
                setUser(data);
                setLoading(false);
            });
    }, [userId]);  // Refetch when userId changes
    
    if (loading) return <div>Loading...</div>;
    if (!user) return <div>User not found</div>;
    
    return (
        <div>
            <h1>{user.name}</h1>
            <p>{user.email}</p>
            <Link to={`/users/${userId}/posts`}>View Posts</Link>
        </div>
    );
}

// Programmatic navigation with useNavigate
function LoginForm() {
    const navigate = useNavigate();
    
    async function handleSubmit(e) {
        e.preventDefault();
        const success = await login(credentials);
        if (success) {
            navigate('/dashboard');  // Redirect after login
            // or navigate(-1) to go back
        }
    }
    
    return <form onSubmit={handleSubmit}>...</form>;
}
```
