---
type: "THEORY"
title: "Nested Routes and Layouts"
---

Create layouts with nested routes:

function App() {
    return (
        <BrowserRouter>
            <Routes>
                {/* Public routes */}
                <Route path="/" element={<PublicLayout />}>
                    <Route index element={<Home />} />
                    <Route path="about" element={<About />} />
                    <Route path="login" element={<Login />} />
                </Route>
                
                {/* Protected routes with different layout */}
                <Route path="/dashboard" element={<DashboardLayout />}>
                    <Route index element={<DashboardHome />} />
                    <Route path="settings" element={<Settings />} />
                    <Route path="users" element={<Users />} />
                </Route>
            </Routes>
        </BrowserRouter>
    );
}

// Layout component with Outlet for child routes
import { Outlet, Link } from 'react-router-dom';

function DashboardLayout() {
    return (
        <div className="dashboard">
            <nav className="sidebar">
                <Link to="/dashboard">Home</Link>
                <Link to="/dashboard/settings">Settings</Link>
                <Link to="/dashboard/users">Users</Link>
            </nav>
            <main className="content">
                <Outlet />  {/* Child routes render here */}
            </main>
        </div>
    );
}

URL STRUCTURE:
/dashboard          -> DashboardLayout + DashboardHome
/dashboard/settings -> DashboardLayout + Settings
/dashboard/users    -> DashboardLayout + Users