---
type: "THEORY"
title: "useState and useEffect for State Management"
---

React hooks are the foundation of functional component state management. Let us understand how to use them effectively.

useState - Managing Component State:
```jsx
// Basic state
const [count, setCount] = useState(0);

// Object state
const [formData, setFormData] = useState({
  title: '',
  description: '',
});

// Updating object state (always create new object)
setFormData(prev => ({ ...prev, title: 'New Title' }));

// Array state
const [tasks, setTasks] = useState([]);

// Adding to array
setTasks(prev => [...prev, newTask]);

// Removing from array
setTasks(prev => prev.filter(t => t.id !== taskId));

// Updating item in array
setTasks(prev => prev.map(t => 
  t.id === taskId ? { ...t, status: 'COMPLETED' } : t
));
```

useEffect - Side Effects and Lifecycle:
```jsx
// Run once on mount (empty dependency array)
useEffect(() => {
  loadInitialData();
}, []);

// Run when dependency changes
useEffect(() => {
  if (searchTerm) {
    searchTasks(searchTerm);
  }
}, [searchTerm]); // Re-runs when searchTerm changes

// Cleanup function (runs on unmount or before re-run)
useEffect(() => {
  const subscription = subscribeToUpdates();
  
  return () => {
    subscription.unsubscribe(); // Cleanup
  };
}, []);

// Multiple effects for different concerns
useEffect(() => {
  loadTasks();
}, []);

useEffect(() => {
  loadCategories();
}, []);
```

Common Patterns:
```jsx
// Loading state pattern
const [data, setData] = useState(null);
const [loading, setLoading] = useState(true);
const [error, setError] = useState(null);

useEffect(() => {
  async function fetchData() {
    try {
      setLoading(true);
      const result = await api.getData();
      setData(result);
      setError(null);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }
  fetchData();
}, []);

// Conditional rendering based on state
if (loading) return <Spinner />;
if (error) return <Error message={error} />;
if (!data) return <Empty />;
return <DataDisplay data={data} />;
```