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
    method: HttpMethod
    path: str
    query_params: Dict[str, str]
    body: Dict[str, Any]
    user: Optional[str] = None

@dataclass
class Response:
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
    def __init__(self):
        self.routes: List[tuple] = []
    
    def _pattern_to_regex(self, pattern: str) -> str:
        """Convert Django-style pattern to regex."""
        regex = pattern
        regex = re.sub(r'<int:(\w+)>', r'(?P<\1>\d+)', regex)
        regex = re.sub(r'<str:(\w+)>', r'(?P<\1>[^/]+)', regex)
        regex = re.sub(r'<slug:(\w+)>', r'(?P<\1>[\w-]+)', regex)
        return f'^{regex}$'
    
    def route(self, pattern: str, methods: List[HttpMethod] = None, name: str = None):
        methods = methods or [HttpMethod.GET]
        regex = self._pattern_to_regex(pattern)
        
        def decorator(func: Callable):
            self.routes.append((regex, pattern, methods, func, name))
            return func
        return decorator
    
    def dispatch(self, request: Request) -> Response:
        path_matches = []
        
        for regex, pattern, methods, handler, name in self.routes:
            match = re.match(regex, request.path)
            if match:
                path_matches.append((methods, handler, match))
        
        if not path_matches:
            return Response.not_found()
        
        for methods, handler, match in path_matches:
            if request.method in methods:
                kwargs = {}
                for key, val in match.groupdict().items():
                    kwargs[key] = int(val) if val.isdigit() else val
                return handler(request, **kwargs)
        
        return Response.method_not_allowed()

# Create router and define views
router = Router()

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
    data = request.body.copy()
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

print("=== Router Tests ===")

req = Request(HttpMethod.GET, '/transactions/', {}, {})
resp = router.dispatch(req)
print(f"GET /transactions/ -> {resp.status_code}: {resp.body}")

req = Request(HttpMethod.GET, '/transactions/1/', {}, {})
resp = router.dispatch(req)
print(f"GET /transactions/1/ -> {resp.status_code}: {resp.body}")

req = Request(HttpMethod.POST, '/transactions/', {}, {'amount': 75.00, 'description': 'Coffee'})
resp = router.dispatch(req)
print(f"POST /transactions/ -> {resp.status_code}: {resp.body}")

req = Request(HttpMethod.GET, '/transactions/999/', {}, {})
resp = router.dispatch(req)
print(f"GET /transactions/999/ -> {resp.status_code}")

req = Request(HttpMethod.PUT, '/transactions/', {}, {})
resp = router.dispatch(req)
print(f"PUT /transactions/ -> {resp.status_code}")