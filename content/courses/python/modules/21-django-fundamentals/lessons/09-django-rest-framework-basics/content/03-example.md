---
type: "EXAMPLE"
title: "ViewSets and Routers"
---

ViewSets combine list, create, retrieve, update, delete into one class. Routers generate URLs automatically:

**Expected Output:**
```
GET /transactions/ -> list all
POST /transactions/ -> create new
GET /transactions/1/ -> retrieve one
PUT /transactions/1/ -> update
DELETE /transactions/1/ -> delete
```

```python
from dataclasses import dataclass, field
from typing import Dict, List, Optional, Any
from decimal import Decimal
import json

@dataclass
class Request:
    """Simulated DRF Request object."""
    method: str
    data: Dict[str, Any] = None
    user: Any = None
    query_params: Dict[str, str] = None

@dataclass
class Response:
    """Simulated DRF Response object."""
    data: Any
    status: int = 200
    
    def __repr__(self):
        return f"Response({self.status}): {json.dumps(self.data, default=str)[:50]}..."


class ViewSet:
    """Base ViewSet - provides CRUD operations."""
    
    def __init__(self):
        self.queryset = []  # Override in subclass
    
    def list(self, request: Request) -> Response:
        """GET /resource/ - List all objects."""
        return Response({'results': self.queryset})
    
    def create(self, request: Request) -> Response:
        """POST /resource/ - Create new object."""
        obj = request.data
        obj['id'] = len(self.queryset) + 1
        self.queryset.append(obj)
        return Response(obj, status=201)
    
    def retrieve(self, request: Request, pk: int) -> Response:
        """GET /resource/{pk}/ - Get single object."""
        for obj in self.queryset:
            if obj.get('id') == pk:
                return Response(obj)
        return Response({'error': 'Not found'}, status=404)
    
    def update(self, request: Request, pk: int) -> Response:
        """PUT /resource/{pk}/ - Update object."""
        for i, obj in enumerate(self.queryset):
            if obj.get('id') == pk:
                self.queryset[i] = {**obj, **request.data, 'id': pk}
                return Response(self.queryset[i])
        return Response({'error': 'Not found'}, status=404)
    
    def destroy(self, request: Request, pk: int) -> Response:
        """DELETE /resource/{pk}/ - Delete object."""
        for i, obj in enumerate(self.queryset):
            if obj.get('id') == pk:
                del self.queryset[i]
                return Response({}, status=204)
        return Response({'error': 'Not found'}, status=404)


class TransactionViewSet(ViewSet):
    """ViewSet for Transaction CRUD operations."""
    
    def __init__(self):
        super().__init__()
        # Initial data
        self.queryset = [
            {'id': 1, 'amount': '50.00', 'description': 'Coffee', 'category': 'food'},
            {'id': 2, 'amount': '150.00', 'description': 'Groceries', 'category': 'food'},
            {'id': 3, 'amount': '30.00', 'description': 'Bus pass', 'category': 'transport'},
        ]
    
    def list(self, request: Request) -> Response:
        """Override to add filtering."""
        results = self.queryset
        
        # Filter by category if provided
        if request.query_params and 'category' in request.query_params:
            cat = request.query_params['category']
            results = [t for t in results if t['category'] == cat]
        
        return Response({
            'count': len(results),
            'results': results
        })


class Router:
    """Simulated DRF Router - generates URL patterns."""
    
    def __init__(self):
        self.registry = []
        self.urls = []
    
    def register(self, prefix: str, viewset_class: type, basename: str = None):
        """Register a viewset with a URL prefix."""
        basename = basename or prefix
        self.registry.append((prefix, viewset_class, basename))
        
        # Generate URL patterns
        self.urls.extend([
            f"GET /{prefix}/ -> {basename}-list",
            f"POST /{prefix}/ -> {basename}-create",
            f"GET /{prefix}/{{pk}}/ -> {basename}-detail",
            f"PUT /{prefix}/{{pk}}/ -> {basename}-update",
            f"DELETE /{prefix}/{{pk}}/ -> {basename}-delete",
        ])
    
    def get_urls(self) -> List[str]:
        return self.urls
    
    def route(self, method: str, path: str, data: Dict = None) -> Response:
        """Route a request to the appropriate viewset method."""
        for prefix, viewset_class, _ in self.registry:
            if path.startswith(f"/{prefix}"):
                viewset = viewset_class()
                request = Request(method=method, data=data, query_params={})
                
                # Parse path
                parts = path.strip('/').split('/')
                if len(parts) == 1:
                    # Collection actions
                    if method == 'GET':
                        return viewset.list(request)
                    elif method == 'POST':
                        return viewset.create(request)
                elif len(parts) == 2:
                    # Instance actions
                    pk = int(parts[1])
                    if method == 'GET':
                        return viewset.retrieve(request, pk)
                    elif method == 'PUT':
                        return viewset.update(request, pk)
                    elif method == 'DELETE':
                        return viewset.destroy(request, pk)
        
        return Response({'error': 'Not found'}, status=404)


# Demo
print("=== ViewSet & Router Demo ===")

# Create router and register viewset
router = Router()
router.register('transactions', TransactionViewSet)

print("\n1. Generated URL Patterns:")
for url in router.get_urls():
    print(f"   {url}")

print("\n2. API Operations:")

# List all
print("\nGET /transactions/ -> list all")
response = router.route('GET', '/transactions/')
print(f"   {response}")

# Create new
print("\nPOST /transactions/ -> create new")
response = router.route('POST', '/transactions/', 
                        data={'amount': '25.00', 'description': 'Snacks', 'category': 'food'})
print(f"   {response}")

# Retrieve one
print("\nGET /transactions/1/ -> retrieve one")
response = router.route('GET', '/transactions/1/')
print(f"   {response}")

# Update
print("\nPUT /transactions/1/ -> update")
response = router.route('PUT', '/transactions/1/', 
                        data={'amount': '55.00', 'description': 'Updated coffee'})
print(f"   {response}")

# Delete
print("\nDELETE /transactions/1/ -> delete")
response = router.route('DELETE', '/transactions/1/')
print(f"   Status: {response.status}")
```
