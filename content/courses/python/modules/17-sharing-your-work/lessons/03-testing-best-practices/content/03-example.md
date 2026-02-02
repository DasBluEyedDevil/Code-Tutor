---
type: "EXAMPLE"
title: "Code Example: Mocking External Dependencies"
---

**Mocking = Fake external dependencies for testing**

**Why mock?**
- APIs cost money or have rate limits
- Databases are slow to set up
- External services may be unreliable
- Need to test error scenarios

**Python mocking tools:**
```python
from unittest.mock import Mock, patch, MagicMock

# Create a mock object
mock_api = Mock()
mock_api.get_user.return_value = {'id': 1, 'name': 'Test'}

# Patch a module function
@patch('mymodule.requests.get')
def test_api(mock_get):
    mock_get.return_value.json.return_value = {'data': 'test'}
    # Now requests.get is mocked!
```

**Common patterns:**
- `Mock()` - Create fake object
- `patch()` - Replace real with fake
- `return_value` - Set what mock returns
- `side_effect` - Raise exception or cycle values

```python
from unittest.mock import Mock, patch, MagicMock
import pytest

print("=== Mocking External APIs ===")

# Real function that calls external API
def get_weather(city):
    """Get weather from external API (simulated)"""
    import requests
    response = requests.get(f'https://api.weather.com/{city}')
    return response.json()

# Test WITHOUT mocking (bad - makes real API call)
# def test_weather_bad():
#     result = get_weather('London')  # Slow, unreliable, costs money!
#     assert 'temperature' in result

# Test WITH mocking (good - no real API call)
@patch('__main__.get_weather')
def test_weather_mocked(mock_get_weather):
    # Configure mock return value
    mock_get_weather.return_value = {
        'city': 'London',
        'temperature': 20,
        'conditions': 'sunny'
    }
    
    result = get_weather('London')
    
    assert result['temperature'] == 20
    assert result['city'] == 'London'
    mock_get_weather.assert_called_once_with('London')
    print("✓ API test passed without real API call!")

# Run the test
test_weather_mocked()

print("\n=== Mocking Database Calls ===")

class UserRepository:
    """Repository that talks to database"""
    
    def __init__(self, db_connection):
        self.db = db_connection
    
    def get_user(self, user_id):
        return self.db.query(f'SELECT * FROM users WHERE id = {user_id}')
    
    def save_user(self, user):
        return self.db.execute('INSERT INTO users ...')

def test_user_repository():
    # Create mock database
    mock_db = Mock()
    mock_db.query.return_value = {'id': 1, 'name': 'Alice', 'email': 'alice@test.com'}
    
    # Inject mock into repository
    repo = UserRepository(mock_db)
    
    # Test get_user
    user = repo.get_user(1)
    
    assert user['name'] == 'Alice'
    mock_db.query.assert_called_once()
    print("✓ Database test passed without real database!")

test_user_repository()

print("\n=== Testing Error Scenarios ===")

def test_api_error_handling():
    mock_api = Mock()
    
    # Simulate API error
    mock_api.get_data.side_effect = ConnectionError("API unavailable")
    
    # Test that our code handles errors properly
    try:
        mock_api.get_data()
        assert False, "Should have raised error"
    except ConnectionError as e:
        assert "unavailable" in str(e)
        print("✓ Error handling test passed!")

test_api_error_handling()

print("\n=== Using patch() Decorator ===")

# Module we want to test
class EmailService:
    def send_email(self, to, subject, body):
        # This would normally send a real email
        pass

class NotificationService:
    def __init__(self, email_service):
        self.email = email_service
    
    def notify_user(self, user_email, message):
        self.email.send_email(user_email, 'Notification', message)
        return True

def test_notification_sends_email():
    # Create mock email service
    mock_email = Mock(spec=EmailService)
    
    # Create notification service with mock
    notifier = NotificationService(mock_email)
    
    # Test notification
    result = notifier.notify_user('user@test.com', 'Hello!')
    
    # Verify email was "sent"
    assert result == True
    mock_email.send_email.assert_called_once_with(
        'user@test.com',
        'Notification',
        'Hello!'
    )
    print("✓ Email notification test passed without sending real email!")

test_notification_sends_email()

print("\n=== Mock return_value vs side_effect ===")

mock = Mock()

# return_value - always returns same thing
mock.get_data.return_value = {'status': 'ok'}
print(f"return_value: {mock.get_data()}")
print(f"return_value: {mock.get_data()}")

# side_effect with list - returns different values each call
mock.get_next.side_effect = [1, 2, 3]
print(f"side_effect list: {mock.get_next()}")
print(f"side_effect list: {mock.get_next()}")
print(f"side_effect list: {mock.get_next()}")

# side_effect with exception
mock.fail.side_effect = ValueError("Something went wrong")
try:
    mock.fail()
except ValueError as e:
    print(f"side_effect exception: {e}")

print("\n=== pytest-mock (Simpler Syntax) ===")

print("""
# Install: pip install pytest-mock

def test_with_mocker(mocker):
    # mocker fixture provided by pytest-mock
    mock_api = mocker.patch('mymodule.external_api')
    mock_api.return_value = {'data': 'test'}
    
    result = my_function()
    
    assert result == expected
    mock_api.assert_called_once()
""")

print("\n=== Key Mocking Patterns ===")
print("""
1. MOCK RETURN VALUES:
   mock.method.return_value = 'result'

2. MOCK EXCEPTIONS:
   mock.method.side_effect = ValueError('error')

3. VERIFY CALLS:
   mock.method.assert_called_once()
   mock.method.assert_called_with(arg1, arg2)
   mock.method.assert_not_called()

4. PATCH MODULES:
   @patch('module.function')
   def test(mock_func):
       mock_func.return_value = 'fake'

5. SPEC FOR TYPE SAFETY:
   mock = Mock(spec=RealClass)  # Only allows real methods
""")
```
