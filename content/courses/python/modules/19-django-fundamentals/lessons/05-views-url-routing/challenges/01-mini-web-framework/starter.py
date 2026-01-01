from dataclasses import dataclass
from typing import Dict, Any, Callable, Optional, List
from enum import Enum
import re

class HttpMethod(Enum):
    GET = 'GET'
    POST = 'POST'
    PUT = 'PUT'
    DELETE = 'DELETE'

@dataclass
class Request:
    """Simulates HTTP request."""
    method: HttpMethod
    path: str
    query_params: Dict[str, str]
    body: Dict[str, Any]
    user: Optional[str] = None

@dataclass
class Response:
    """Simulates HTTP response."""
    status_code: int
    body: Any
    headers: Dict[str, str]
    
    @classmethod
    def ok(cls, data: Any) -> 'Response':
        return cls(200, data, {'Content-Type': 'application/json'})
    
    @classmethod
    def created(cls, data: Any) -> 'Response':
        return cls(201, data, {'Content-Type': 'application/json'})
    
    @classmethod
    def not_found(cls) -> 'Response':
        return cls(404, {'error': 'Not found'}, {})
    
    @classmethod
    def method_not_allowed(cls) -> 'Response':
        return cls(405, {'error': 'Method not allowed'}, {})

class Router:
    """URL router that dispatches requests to views."""
    
    def __init__(self):
        self.routes: List[tuple] = []  # (pattern, methods, handler, name)
    
    def route(self, pattern: str, methods: List[HttpMethod] = None, name: str = None):
        """
        Decorator to register a route.
        
        Usage:
            @router.route('/transactions/<int:pk>/', methods=[HttpMethod.GET])
            def get_transaction(request, pk):
                return Response.ok({'id': pk})
        """
        # TODO: Return a decorator that registers the function
        pass
    
    def dispatch(self, request: Request) -> Response:
        """
        Find matching route and call the handler.
        
        1. Find route matching request.path
        2. Check if request.method is allowed
        3. Extract URL parameters
        4. Call handler with request and params
        5. Return response or 404/405
        """
        # TODO: Implement request dispatch logic
        pass

# Create router and define views
router = Router()

# In-memory data store
transactions = {
    1: {'id': 1, 'amount': 50.00, 'description': 'Groceries'},
    2: {'id': 2, 'amount': 1500.00, 'description': 'Rent'},
}
next_id = 3

@router.route('/transactions/', methods=[HttpMethod.GET], name='list')
def list_transactions(request):
    return Response.ok(list(transactions.values()))

@router.route('/transactions/<int:pk>/', methods=[HttpMethod.GET], name='detail')
def get_transaction(request, pk):
    if pk in transactions:
        return Response.ok(transactions[pk])
    return Response.not_found()

@router.route('/transactions/', methods=[HttpMethod.POST], name='create')
def create_transaction(request):
    global next_id
    data = request.body
    data['id'] = next_id
    transactions[next_id] = data
    next_id += 1
    return Response.created(data)

@router.route('/transactions/<int:pk>/', methods=[HttpMethod.DELETE], name='delete')
def delete_transaction(request, pk):
    if pk in transactions:
        del transactions[pk]
        return Response.ok({'deleted': pk})
    return Response.not_found()

# Test the router
print("=== Router Tests ===")

# Test GET list
req = Request(HttpMethod.GET, '/transactions/', {}, {})
resp = router.dispatch(req)
print(f"GET /transactions/ -> {resp.status_code}: {resp.body}")

# Test GET detail
req = Request(HttpMethod.GET, '/transactions/1/', {}, {})
resp = router.dispatch(req)
print(f"GET /transactions/1/ -> {resp.status_code}: {resp.body}")

# Test POST create
req = Request(HttpMethod.POST, '/transactions/', {}, {'amount': 75.00, 'description': 'Coffee'})
resp = router.dispatch(req)
print(f"POST /transactions/ -> {resp.status_code}: {resp.body}")

# Test 404
req = Request(HttpMethod.GET, '/transactions/999/', {}, {})
resp = router.dispatch(req)
print(f"GET /transactions/999/ -> {resp.status_code}")

# Test 405
req = Request(HttpMethod.PUT, '/transactions/', {}, {})
resp = router.dispatch(req)
print(f"PUT /transactions/ -> {resp.status_code}")