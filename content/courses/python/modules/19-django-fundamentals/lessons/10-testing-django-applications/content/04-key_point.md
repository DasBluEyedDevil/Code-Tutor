---
type: "KEY_POINT"
title: "Fixtures and Test Data"
---

**Creating Test Data:**

**1. setUp / setUpTestData:**
```python
class TransactionTests(TestCase):
    @classmethod
    def setUpTestData(cls):
        # Runs once for entire test class (faster)
        cls.user = User.objects.create_user('testuser', 'test@example.com', 'pass')
    
    def setUp(self):
        # Runs before each test method
        self.client.force_login(self.user)
```

**2. Factory Functions:**
```python
def create_transaction(user, amount=50, description='Test'):
    return Transaction.objects.create(
        user=user, amount=amount, description=description
    )

def test_something(self):
    tx = create_transaction(self.user, amount=100)
    ...
```

**3. Factory Boy (third-party):**
```python
import factory

class UserFactory(factory.django.DjangoModelFactory):
    class Meta:
        model = User
    username = factory.Sequence(lambda n: f'user{n}')
    email = factory.LazyAttribute(lambda o: f'{o.username}@example.com')

class TransactionFactory(factory.django.DjangoModelFactory):
    class Meta:
        model = Transaction
    user = factory.SubFactory(UserFactory)
    amount = factory.Faker('pydecimal', min_value=1, max_value=1000)
    description = factory.Faker('sentence')
```

**4. JSON Fixtures:**
```python
class TransactionTests(TestCase):
    fixtures = ['test_users.json', 'test_transactions.json']
    # Loads data from fixtures/ directory
```

**Cleanup:**
```python
def tearDown(self):
    # Runs after each test
    Transaction.objects.all().delete()

@classmethod
def tearDownClass(cls):
    # Runs after all tests in class
    super().tearDownClass()
```