---
type: "THEORY"
title: "Fixture Patterns"
---

**1. Factory Fixtures** - When you need to create multiple instances:
```python
@pytest.fixture
def make_order():
    def _make_order(product, quantity=1):
        return Order(product=product, quantity=quantity)
    return _make_order
```

**2. Parameterized Fixtures** - Run tests with different setups:
```python
@pytest.fixture(params=["sqlite", "postgres", "mysql"])
def database(request):
    return connect_to(request.param)
```

**3. Fixture Dependencies** - Fixtures can use other fixtures:
```python
@pytest.fixture
def authenticated_client(client, test_user):
    client.login(test_user)
    return client
```