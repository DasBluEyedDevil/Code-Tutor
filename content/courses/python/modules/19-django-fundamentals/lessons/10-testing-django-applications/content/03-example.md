---
type: "EXAMPLE"
title: "Testing Views with the Test Client"
---

Django's test client simulates HTTP requests without a real server:

**Expected Output:**
```
test_list_view_returns_200 ... ok
test_list_view_requires_login ... ok
test_create_view_saves_transaction ... ok
test_permission_denied_for_other_user ... ok
```

```python
import unittest
from dataclasses import dataclass, field
from typing import Dict, List, Any, Optional
from decimal import Decimal
import json

# Simulated models and views
@dataclass
class User:
    id: int
    username: str
    is_authenticated: bool = True

@dataclass
class AnonymousUser:
    is_authenticated: bool = False

@dataclass
class Transaction:
    id: int
    amount: Decimal
    description: str
    user_id: int


class TestClient:
    """
    Simulated Django Test Client.
    Makes fake HTTP requests without a real server.
    """
    
    def __init__(self):
        self.user = AnonymousUser()
        self.session = {}
    
    def login(self, username: str, password: str) -> bool:
        """Simulate logging in a user."""
        # In real Django, this validates credentials
        if username and password:
            self.user = User(id=1, username=username)
            return True
        return False
    
    def force_login(self, user: User):
        """Login without password (for tests)."""
        self.user = user
    
    def logout(self):
        """Clear the session."""
        self.user = AnonymousUser()
    
    def get(self, path: str, data: Dict = None) -> 'TestResponse':
        """Make a GET request."""
        return self._request('GET', path, data)
    
    def post(self, path: str, data: Dict = None, 
             content_type: str = 'application/json') -> 'TestResponse':
        """Make a POST request."""
        return self._request('POST', path, data, content_type)
    
    def _request(self, method: str, path: str, data: Dict = None,
                 content_type: str = None) -> 'TestResponse':
        """Internal request handler."""
        # Simulate view routing
        if path == '/transactions/':
            if method == 'GET':
                return self._transaction_list_view()
            elif method == 'POST':
                return self._transaction_create_view(data)
        elif path.startswith('/transactions/') and path.endswith('/'):
            pk = int(path.split('/')[2])
            return self._transaction_detail_view(pk)
        elif path == '/login/':
            return TestResponse(status_code=200, content=b'Login page')
        
        return TestResponse(status_code=404, content=b'Not found')
    
    def _transaction_list_view(self) -> 'TestResponse':
        if not self.user.is_authenticated:
            return TestResponse(status_code=302, headers={'Location': '/login/'})
        
        # Return user's transactions
        transactions = [
            {'id': 1, 'amount': '50.00', 'description': 'Groceries'},
            {'id': 2, 'amount': '30.00', 'description': 'Coffee'},
        ]
        return TestResponse(
            status_code=200,
            content=json.dumps({'results': transactions}).encode(),
            json_data={'results': transactions}
        )
    
    def _transaction_create_view(self, data: Dict) -> 'TestResponse':
        if not self.user.is_authenticated:
            return TestResponse(status_code=302, headers={'Location': '/login/'})
        
        # Validate and create
        if not data.get('amount') or not data.get('description'):
            return TestResponse(
                status_code=400,
                content=b'{"error": "Missing fields"}'
            )
        
        new_tx = {
            'id': 3,
            'amount': data['amount'],
            'description': data['description']
        }
        return TestResponse(
            status_code=201,
            content=json.dumps(new_tx).encode(),
            json_data=new_tx
        )
    
    def _transaction_detail_view(self, pk: int) -> 'TestResponse':
        if not self.user.is_authenticated:
            return TestResponse(status_code=302, headers={'Location': '/login/'})
        
        # Simulate ownership check
        owner_map = {1: 1, 2: 1, 3: 2}  # tx_id -> user_id
        if pk in owner_map:
            if owner_map[pk] != self.user.id:
                return TestResponse(status_code=403, content=b'Permission denied')
            return TestResponse(
                status_code=200,
                json_data={'id': pk, 'amount': '50.00', 'description': 'Test'}
            )
        return TestResponse(status_code=404, content=b'Not found')


@dataclass
class TestResponse:
    """Simulated Django test response."""
    status_code: int
    content: bytes = b''
    headers: Dict[str, str] = field(default_factory=dict)
    json_data: Dict = None
    
    def json(self) -> Dict:
        if self.json_data:
            return self.json_data
        return json.loads(self.content.decode())


class TransactionViewTests(unittest.TestCase):
    """Test transaction views using the test client."""
    
    def setUp(self):
        self.client = TestClient()
        self.user = User(id=1, username='alice')
    
    def test_list_view_returns_200(self):
        """Logged-in user can access transaction list."""
        self.client.force_login(self.user)
        response = self.client.get('/transactions/')
        
        self.assertEqual(response.status_code, 200)
        self.assertIn('results', response.json())
    
    def test_list_view_requires_login(self):
        """Anonymous users are redirected to login."""
        response = self.client.get('/transactions/')
        
        self.assertEqual(response.status_code, 302)
        self.assertEqual(response.headers.get('Location'), '/login/')
    
    def test_create_view_saves_transaction(self):
        """POST creates a new transaction."""
        self.client.force_login(self.user)
        response = self.client.post('/transactions/', {
            'amount': '75.00',
            'description': 'New purchase'
        })
        
        self.assertEqual(response.status_code, 201)
        self.assertEqual(response.json()['description'], 'New purchase')
    
    def test_create_view_validates_data(self):
        """POST with missing data returns 400."""
        self.client.force_login(self.user)
        response = self.client.post('/transactions/', {})
        
        self.assertEqual(response.status_code, 400)
    
    def test_detail_view_returns_transaction(self):
        """User can view their own transaction."""
        self.client.force_login(self.user)
        response = self.client.get('/transactions/1/')
        
        self.assertEqual(response.status_code, 200)
        self.assertEqual(response.json()['id'], 1)
    
    def test_permission_denied_for_other_user(self):
        """User cannot view other user's transaction."""
        self.client.force_login(self.user)  # User 1
        response = self.client.get('/transactions/3/')  # Owned by User 2
        
        self.assertEqual(response.status_code, 403)


# Run tests
if __name__ == '__main__':
    loader = unittest.TestLoader()
    suite = loader.loadTestsFromTestCase(TransactionViewTests)
    runner = unittest.TextTestRunner(verbosity=2)
    result = runner.run(suite)
    
    print(f"\nTests run: {result.testsRun}, Failures: {len(result.failures)}")
```
