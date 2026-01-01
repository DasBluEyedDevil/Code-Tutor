from dataclasses import dataclass, field
from typing import Dict, List, Any, Optional
from datetime import date
from decimal import Decimal
import json

# Models
@dataclass
class Category:
    id: int
    name: str
    user_id: int

@dataclass
class Transaction:
    id: int
    amount: Decimal
    description: str
    category_id: int
    user_id: int
    date: date = None

@dataclass
class User:
    id: int
    username: str

# Request/Response
@dataclass
class Request:
    method: str
    user: User
    data: Dict[str, Any] = None
    query_params: Dict[str, str] = field(default_factory=dict)

@dataclass
class Response:
    data: Any
    status: int = 200


# Serializers
class BaseSerializer:
    def __init__(self, instance=None, data=None, many=False, context=None):
        self.instance = instance
        self.initial_data = data
        self.many = many
        self.context = context or {}
        self._validated_data = None
        self._errors = {}
    
    def is_valid(self) -> bool:
        # TODO: Implement validation
        pass
    
    @property
    def data(self) -> Dict:
        # TODO: Serialize instance(s) to dict
        pass

class TransactionSerializer(BaseSerializer):
    # TODO: Implement with fields: id, amount, description, category_id, date
    pass


# ViewSet
class TransactionViewSet:
    # In-memory database
    transactions = [
        Transaction(1, Decimal('50'), 'Coffee', 1, 1, date(2025, 1, 15)),
        Transaction(2, Decimal('150'), 'Groceries', 1, 1, date(2025, 1, 16)),
        Transaction(3, Decimal('30'), 'Bus', 2, 2, date(2025, 1, 15)),
    ]
    
    def get_queryset(self, request: Request) -> List[Transaction]:
        # TODO: Filter to user's transactions only
        pass
    
    def list(self, request: Request) -> Response:
        # TODO: Return filtered list with optional query param filtering
        pass
    
    def create(self, request: Request) -> Response:
        # TODO: Create new transaction for current user
        pass
    
    def retrieve(self, request: Request, pk: int) -> Response:
        # TODO: Get single transaction (check ownership!)
        pass
    
    def update(self, request: Request, pk: int) -> Response:
        # TODO: Update transaction (check ownership!)
        pass
    
    def destroy(self, request: Request, pk: int) -> Response:
        # TODO: Delete transaction (check ownership!)
        pass


# Router
class Router:
    def __init__(self):
        self.routes = {}
    
    def register(self, prefix: str, viewset_class: type):
        self.routes[prefix] = viewset_class
    
    def dispatch(self, method: str, path: str, request: Request) -> Response:
        # TODO: Route request to appropriate viewset method
        pass


# Test the API
print("=== REST API Tests ===")

user1 = User(1, "alice")
user2 = User(2, "bob")

router = Router()
router.register('transactions', TransactionViewSet)

# Test 1: List user's transactions
print("\n1. List Alice's Transactions:")
request = Request(method='GET', user=user1)
response = router.dispatch('GET', '/transactions/', request)
print(f"   Found {len(response.data.get('results', []))} transactions")

# Test 2: Create transaction
print("\n2. Create Transaction:")
request = Request(
    method='POST', 
    user=user1,
    data={'amount': '25.00', 'description': 'Lunch', 'category_id': 1}
)
response = router.dispatch('POST', '/transactions/', request)
print(f"   Created: {response.data}")

# Test 3: Try to access other user's transaction
print("\n3. Permission Check (Bob accessing Alice's data):")
request = Request(method='GET', user=user2)
response = router.dispatch('GET', '/transactions/1/', request)
print(f"   Status: {response.status} (should be 403 or 404)")