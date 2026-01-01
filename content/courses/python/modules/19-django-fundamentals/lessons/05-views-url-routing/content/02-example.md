---
type: "EXAMPLE"
title: "URL Patterns and Views"
---

Complete URL and view setup for transactions:

**Expected Output:**
```
URL Patterns defined: 6 routes
Views: list, detail, create, update, delete, dashboard
```

```python
from dataclasses import dataclass
from typing import List, Dict, Any, Optional, Callable
from enum import Enum
import re

class HttpMethod(Enum):
    GET = 'GET'
    POST = 'POST'
    PUT = 'PUT'
    DELETE = 'DELETE'

@dataclass
class URLPattern:
    """Simulates Django URL pattern."""
    pattern: str  # e.g., 'transactions/<int:pk>/'
    view_name: str
    name: str  # URL name for reverse()
    methods: List[HttpMethod]
    
    def matches(self, path: str) -> Optional[Dict[str, Any]]:
        """Check if path matches pattern and extract parameters."""
        # Convert Django pattern to regex
        regex_pattern = self.pattern
        regex_pattern = regex_pattern.replace('<int:', '(?P<')
        regex_pattern = regex_pattern.replace('<str:', '(?P<')
        regex_pattern = regex_pattern.replace('<slug:', '(?P<')
        regex_pattern = regex_pattern.replace('>', '>[^/]+)')
        regex_pattern = f'^{regex_pattern}$'
        
        match = re.match(regex_pattern, path)
        if match:
            params = match.groupdict()
            # Convert int params
            for key, val in params.items():
                if val.isdigit():
                    params[key] = int(val)
            return params
        return None

class URLRouter:
    """Simulates Django URL routing."""
    
    def __init__(self, app_name: str):
        self.app_name = app_name
        self.patterns: List[URLPattern] = []
    
    def path(self, pattern: str, view_name: str, name: str, 
             methods: List[HttpMethod] = None):
        """Add a URL pattern."""
        self.patterns.append(URLPattern(
            pattern=pattern,
            view_name=view_name,
            name=name,
            methods=methods or [HttpMethod.GET]
        ))
    
    def resolve(self, path: str, method: HttpMethod = HttpMethod.GET) -> Optional[Dict]:
        """Find matching view for a path."""
        for pattern in self.patterns:
            params = pattern.matches(path)
            if params is not None:
                if method in pattern.methods:
                    return {
                        'view': pattern.view_name,
                        'name': pattern.name,
                        'kwargs': params
                    }
                else:
                    return {'error': f'Method {method.value} not allowed'}
        return None
    
    def reverse(self, name: str, **kwargs) -> Optional[str]:
        """Generate URL from name and parameters."""
        for pattern in self.patterns:
            if pattern.name == name:
                url = pattern.pattern
                for key, val in kwargs.items():
                    url = re.sub(f'<[^:]+:{key}>', str(val), url)
                return f'/{self.app_name}/{url}'
        return None

# Define URL patterns for transactions app
router = URLRouter('transactions')

# List view - GET /transactions/
router.path('', 'TransactionListView', 'transaction_list')

# Detail view - GET /transactions/5/
router.path('<int:pk>/', 'TransactionDetailView', 'transaction_detail')

# Create view - GET/POST /transactions/create/
router.path('create/', 'TransactionCreateView', 'transaction_create',
            methods=[HttpMethod.GET, HttpMethod.POST])

# Update view - GET/POST /transactions/5/edit/
router.path('<int:pk>/edit/', 'TransactionUpdateView', 'transaction_update',
            methods=[HttpMethod.GET, HttpMethod.POST])

# Delete view - GET/POST /transactions/5/delete/
router.path('<int:pk>/delete/', 'TransactionDeleteView', 'transaction_delete',
            methods=[HttpMethod.GET, HttpMethod.POST])

# Dashboard - GET /transactions/dashboard/
router.path('dashboard/', 'DashboardView', 'dashboard')

print(f"URL Patterns defined: {len(router.patterns)} routes")
print("Views: list, detail, create, update, delete, dashboard")

# Test routing
print("\n--- Routing Tests ---")
test_paths = [
    ('', HttpMethod.GET),
    ('5/', HttpMethod.GET),
    ('create/', HttpMethod.POST),
    ('42/edit/', HttpMethod.GET),
    ('dashboard/', HttpMethod.GET),
]

for path, method in test_paths:
    result = router.resolve(path, method)
    if result and 'view' in result:
        print(f"{method.value} /{path} -> {result['view']} {result['kwargs']}")

# Test reverse URL generation
print("\n--- Reverse URL Tests ---")
print(f"transaction_detail(pk=5): {router.reverse('transaction_detail', pk=5)}")
print(f"transaction_update(pk=42): {router.reverse('transaction_update', pk=42)}")
```
