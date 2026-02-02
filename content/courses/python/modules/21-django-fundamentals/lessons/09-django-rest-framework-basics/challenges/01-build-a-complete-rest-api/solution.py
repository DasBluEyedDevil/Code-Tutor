from dataclasses import dataclass, field
from typing import Dict, List, Any, Optional
from datetime import date, datetime
from decimal import Decimal
import json

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


class BaseSerializer:
    fields = []
    
    def __init__(self, instance=None, data=None, many=False, context=None):
        self.instance = instance
        self.initial_data = data
        self.many = many
        self.context = context or {}
        self._validated_data = None
        self._errors = {}
    
    def is_valid(self) -> bool:
        self._errors = {}
        self._validated_data = {}
        
        for field_name in self.fields:
            if field_name == 'id':  # read-only
                continue
            value = self.initial_data.get(field_name)
            validator = getattr(self, f'validate_{field_name}', None)
            try:
                if validator:
                    value = validator(value)
                self._validated_data[field_name] = value
            except ValueError as e:
                self._errors[field_name] = [str(e)]
        
        return not self._errors
    
    @property
    def validated_data(self):
        return self._validated_data
    
    @property
    def errors(self):
        return self._errors
    
    @property
    def data(self) -> Any:
        if self.many:
            return [self._serialize_one(obj) for obj in self.instance]
        return self._serialize_one(self.instance)
    
    def _serialize_one(self, obj) -> Dict:
        result = {}
        for field_name in self.fields:
            value = getattr(obj, field_name, None)
            if isinstance(value, Decimal):
                value = str(value)
            elif isinstance(value, date):
                value = value.isoformat() if value else None
            result[field_name] = value
        return result


class TransactionSerializer(BaseSerializer):
    fields = ['id', 'amount', 'description', 'category_id', 'date']
    
    def validate_amount(self, value):
        if value is None:
            raise ValueError("Amount is required")
        amount = Decimal(str(value))
        if amount <= 0:
            raise ValueError("Amount must be positive")
        return amount
    
    def validate_description(self, value):
        if not value or len(str(value)) < 3:
            raise ValueError("Description must be at least 3 characters")
        return str(value)


class TransactionViewSet:
    transactions = [
        Transaction(1, Decimal('50'), 'Coffee', 1, 1, date(2025, 1, 15)),
        Transaction(2, Decimal('150'), 'Groceries', 1, 1, date(2025, 1, 16)),
        Transaction(3, Decimal('30'), 'Bus', 2, 2, date(2025, 1, 15)),
    ]
    next_id = 4
    
    def get_queryset(self, request: Request) -> List[Transaction]:
        return [t for t in self.transactions if t.user_id == request.user.id]
    
    def list(self, request: Request) -> Response:
        queryset = self.get_queryset(request)
        
        # Filter by category
        if 'category_id' in request.query_params:
            cat_id = int(request.query_params['category_id'])
            queryset = [t for t in queryset if t.category_id == cat_id]
        
        serializer = TransactionSerializer(instance=queryset, many=True)
        return Response({'count': len(queryset), 'results': serializer.data})
    
    def create(self, request: Request) -> Response:
        serializer = TransactionSerializer(data=request.data)
        if not serializer.is_valid():
            return Response({'errors': serializer.errors}, status=400)
        
        tx = Transaction(
            id=TransactionViewSet.next_id,
            amount=serializer.validated_data['amount'],
            description=serializer.validated_data['description'],
            category_id=serializer.validated_data.get('category_id', 1),
            user_id=request.user.id,
            date=date.today()
        )
        TransactionViewSet.next_id += 1
        self.transactions.append(tx)
        
        return Response(TransactionSerializer(instance=tx).data, status=201)
    
    def retrieve(self, request: Request, pk: int) -> Response:
        for tx in self.transactions:
            if tx.id == pk:
                if tx.user_id != request.user.id:
                    return Response({'error': 'Permission denied'}, status=403)
                return Response(TransactionSerializer(instance=tx).data)
        return Response({'error': 'Not found'}, status=404)
    
    def update(self, request: Request, pk: int) -> Response:
        for i, tx in enumerate(self.transactions):
            if tx.id == pk:
                if tx.user_id != request.user.id:
                    return Response({'error': 'Permission denied'}, status=403)
                
                serializer = TransactionSerializer(data=request.data)
                if not serializer.is_valid():
                    return Response({'errors': serializer.errors}, status=400)
                
                tx.amount = serializer.validated_data['amount']
                tx.description = serializer.validated_data['description']
                return Response(TransactionSerializer(instance=tx).data)
        return Response({'error': 'Not found'}, status=404)
    
    def destroy(self, request: Request, pk: int) -> Response:
        for i, tx in enumerate(self.transactions):
            if tx.id == pk:
                if tx.user_id != request.user.id:
                    return Response({'error': 'Permission denied'}, status=403)
                del self.transactions[i]
                return Response({}, status=204)
        return Response({'error': 'Not found'}, status=404)


class Router:
    def __init__(self):
        self.routes = {}
    
    def register(self, prefix: str, viewset_class: type):
        self.routes[prefix] = viewset_class
    
    def dispatch(self, method: str, path: str, request: Request) -> Response:
        parts = path.strip('/').split('/')
        prefix = parts[0]
        
        if prefix not in self.routes:
            return Response({'error': 'Not found'}, status=404)
        
        viewset = self.routes[prefix]()
        
        if len(parts) == 1:
            if method == 'GET':
                return viewset.list(request)
            elif method == 'POST':
                return viewset.create(request)
        elif len(parts) == 2:
            pk = int(parts[1])
            if method == 'GET':
                return viewset.retrieve(request, pk)
            elif method == 'PUT':
                return viewset.update(request, pk)
            elif method == 'DELETE':
                return viewset.destroy(request, pk)
        
        return Response({'error': 'Not found'}, status=404)


print("=== REST API Tests ===")

user1 = User(1, "alice")
user2 = User(2, "bob")

router = Router()
router.register('transactions', TransactionViewSet)

print("\n1. List Alice's Transactions:")
request = Request(method='GET', user=user1)
response = router.dispatch('GET', '/transactions/', request)
print(f"   Found {len(response.data.get('results', []))} transactions")

print("\n2. Create Transaction:")
request = Request(
    method='POST', 
    user=user1,
    data={'amount': '25.00', 'description': 'Lunch', 'category_id': 1}
)
response = router.dispatch('POST', '/transactions/', request)
print(f"   Created: {response.data}")

print("\n3. Permission Check (Bob accessing Alice's data):")
request = Request(method='GET', user=user2)
response = router.dispatch('GET', '/transactions/1/', request)
print(f"   Status: {response.status} (should be 403 or 404)")