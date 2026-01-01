---
type: "WARNING"
title: "Testing Pitfalls to Avoid"
---

**1. Testing Implementation, Not Behavior:**
```python
# BAD: Tests internal implementation
def test_uses_orm_filter(self):
    with self.assertNumQueries(1):
        list(Transaction.objects.filter(user=self.user))

# GOOD: Tests observable behavior
def test_returns_only_user_transactions(self):
    response = self.client.get('/transactions/')
    for tx in response.json()['results']:
        self.assertEqual(tx['user_id'], self.user.id)
```

**2. Tests That Depend on Each Other:**
```python
# BAD: test_b depends on test_a running first
def test_a_create(self):
    Transaction.objects.create(...)  # Creates ID 1

def test_b_get(self):
    response = self.client.get('/transactions/1/')  # Assumes ID 1 exists

# GOOD: Each test is independent
def test_get(self):
    tx = Transaction.objects.create(...)  # Create in this test
    response = self.client.get(f'/transactions/{tx.id}/')
```

**3. Not Testing Edge Cases:**
```python
# Test the happy path AND edge cases
def test_balance_with_no_transactions(self):
    self.assertEqual(self.account.balance, Decimal('0'))

def test_balance_with_only_expenses(self):
    create_expense(self.account, Decimal('100'))
    self.assertEqual(self.account.balance, Decimal('-100'))

def test_very_large_amount(self):
    create_transaction(amount=Decimal('999999999.99'))
    ...
```

**4. Slow Tests:**
```python
# BAD: Creates database records in loop
for i in range(1000):
    Transaction.objects.create(...)

# GOOD: Use bulk_create
Transaction.objects.bulk_create([
    Transaction(...) for i in range(1000)
])

# Or use setUpTestData for class-level fixtures
```

**5. Not Mocking External Services:**
```python
# BAD: Actually calls external API in tests
def test_payment(self):
    result = stripe.Charge.create(...)  # Real API call!

# GOOD: Mock external calls
@patch('payments.stripe.Charge.create')
def test_payment(self, mock_charge):
    mock_charge.return_value = {'id': 'ch_123', 'status': 'succeeded'}
    result = process_payment(...)
    mock_charge.assert_called_once()
```