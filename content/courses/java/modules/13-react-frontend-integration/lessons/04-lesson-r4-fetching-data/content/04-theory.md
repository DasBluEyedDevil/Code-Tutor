---
type: "THEORY"
title: "Loading and Error States"
---

Good UX requires handling all states:

THREE STATES OF DATA FETCHING:

1. LOADING: Request in progress
   - Show spinner or skeleton
   - Disable form submissions
   - Indicate progress

2. SUCCESS: Data received
   - Render the data
   - Hide loading indicators

3. ERROR: Request failed
   - Show error message
   - Offer retry option
   - Log for debugging

REUSABLE FETCH HOOK:

function useFetch(url) {
    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    
    useEffect(() => {
        let isMounted = true;  // Prevent state update on unmounted component
        
        async function fetchData() {
            try {
                setLoading(true);
                const response = await fetch(url);
                if (!response.ok) throw new Error(`HTTP ${response.status}`);
                const json = await response.json();
                if (isMounted) {
                    setData(json);
                    setError(null);
                }
            } catch (err) {
                if (isMounted) {
                    setError(err.message);
                }
            } finally {
                if (isMounted) {
                    setLoading(false);
                }
            }
        }
        
        fetchData();
        
        return () => { isMounted = false; };  // Cleanup
    }, [url]);
    
    return { data, loading, error };
}

// Usage:
const { data: users, loading, error } = useFetch('/api/users');