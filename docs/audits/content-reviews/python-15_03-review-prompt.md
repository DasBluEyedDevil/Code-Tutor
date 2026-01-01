# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Sharing Your Work
- **Lesson:** Testing Best Practices (ID: 15_03)
- **Difficulty:** advanced
- **Estimated Time:** 35 minutes

## Current Lesson Content

{
    "id":  "15_03",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: Testing is Insurance",
                                "content":  "**Tests = Proof your code works**\n\n**Think of testing like:**\n- Car safety tests before sale\n- Taste-testing food before serving\n- Checking parachute before jumping\n\n**Why test:**\n\n1. **Catch bugs early** 🐛\n   - Before users find them\n   - Cheaper to fix\n   - Build confidence\n\n2. **Prevent regressions** ↩️\n   - New code doesn\u0027t break old features\n   - Safe to refactor\n   - Automated checks\n\n3. **Document behavior** 📖\n   - Tests show how code should work\n   - Examples of usage\n   - Living documentation\n\n4. **Design improvement** 🎨\n   - Hard to test = bad design\n   - Forces modular code\n   - Clear interfaces\n\n**Types of tests:**\n\n**1. Unit Tests** 🔬\n- Test single function/class\n- Fast (milliseconds)\n- Isolated (no database, network)\n- Most common\n\n**2. Integration Tests** 🔗\n- Test components together\n- Database, API calls\n- Slower\n- Realistic scenarios\n\n**3. End-to-End (E2E) Tests** 🎬\n- Test entire application\n- User perspective\n- Slowest\n- Most realistic\n\n**Test pyramid:**\n```\n      /\\  \n     /E2E\\      ← Few (expensive, slow)\n    /─────\\\n   /Integr\\     ← Some (moderate speed)\n  /────────\\\n /Unit Tests\\   ← Many (cheap, fast)\n/────────────\\\n```\n\n**Write tests that:**\n✓ Are fast\n✓ Are independent\n✓ Are repeatable\n✓ Validate one thing\n✓ Have clear names"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Unit Testing with pytest",
                                "content":  "**pytest features:**\n\n**1. Simple assertions:**\n```python\nassert result == expected\nassert value \u003e 0\nassert item in list\n```\n\n**2. Testing exceptions:**\n```python\nwith pytest.raises(ValueError):\n    function_that_fails()\n```\n\n**3. Fixtures (setup/teardown):**\n```python\n@pytest.fixture\ndef database():\n    db = create_db()\n    yield db  # Test runs here\n    db.close()\n```\n\n**4. Parametrized tests:**\n```python\n@pytest.mark.parametrize(\"input,expected\", [\n    (1, 2),\n    (2, 4),\n])\ndef test_double(input, expected):\n    assert double(input) == expected\n```\n\n**Test naming:**\n- Prefix with `test_`\n- Descriptive names\n- One test = one concept",
                                "code":  "# Example functions to test\n\ndef add(a, b):\n    \"\"\"Add two numbers\"\"\"\n    return a + b\n\ndef divide(a, b):\n    \"\"\"Divide two numbers\"\"\"\n    if b == 0:\n        raise ValueError(\"Cannot divide by zero\")\n    return a / b\n\ndef is_palindrome(text):\n    \"\"\"Check if text is a palindrome\"\"\"\n    cleaned = \u0027\u0027.join(c.lower() for c in text if c.isalnum())\n    return cleaned == cleaned[::-1]\n\nclass Calculator:\n    \"\"\"Simple calculator class\"\"\"\n    \n    def __init__(self):\n        self.history = []\n    \n    def add(self, a, b):\n        result = a + b\n        self.history.append(f\"{a} + {b} = {result}\")\n        return result\n    \n    def get_history(self):\n        return self.history.copy()\n\nprint(\"=== Writing Tests with pytest ===\")\n\n# Test file: test_calculator.py\ntest_code = \u0027\u0027\u0027\nimport pytest\nfrom calculator import add, divide, is_palindrome, Calculator\n\n# Basic unit tests\ndef test_add_positive_numbers():\n    \"\"\"Test adding positive numbers\"\"\"\n    assert add(2, 3) == 5\n    assert add(10, 20) == 30\n\ndef test_add_negative_numbers():\n    \"\"\"Test adding negative numbers\"\"\"\n    assert add(-1, -1) == -2\n    assert add(-5, 5) == 0\n\ndef test_divide_normal():\n    \"\"\"Test normal division\"\"\"\n    assert divide(10, 2) == 5\n    assert divide(9, 3) == 3\n\ndef test_divide_by_zero():\n    \"\"\"Test division by zero raises error\"\"\"\n    with pytest.raises(ValueError, match=\"Cannot divide by zero\"):\n        divide(10, 0)\n\ndef test_is_palindrome_simple():\n    \"\"\"Test simple palindromes\"\"\"\n    assert is_palindrome(\"racecar\") == True\n    assert is_palindrome(\"hello\") == False\n\ndef test_is_palindrome_case_insensitive():\n    \"\"\"Test case insensitivity\"\"\"\n    assert is_palindrome(\"RaceCar\") == True\n    assert is_palindrome(\"A man a plan a canal Panama\") == True\n\n# Test with fixtures\n@pytest.fixture\ndef calculator():\n    \"\"\"Fixture providing a fresh Calculator instance\"\"\"\n    return Calculator()\n\ndef test_calculator_add(calculator):\n    \"\"\"Test calculator add method\"\"\"\n    result = calculator.add(5, 3)\n    assert result == 8\n\ndef test_calculator_history(calculator):\n    \"\"\"Test calculator maintains history\"\"\"\n    calculator.add(5, 3)\n    calculator.add(10, 2)\n    history = calculator.get_history()\n    assert len(history) == 2\n    assert \"5 + 3 = 8\" in history\n\n# Parametrized tests (test multiple inputs)\n@pytest.mark.parametrize(\"a,b,expected\", [\n    (2, 3, 5),\n    (10, 20, 30),\n    (-1, 1, 0),\n    (0, 0, 0),\n    (100, 200, 300)\n])\ndef test_add_parametrized(a, b, expected):\n    \"\"\"Test add with multiple parameter sets\"\"\"\n    assert add(a, b) == expected\n\n# Test error messages\ndef test_divide_error_message():\n    \"\"\"Test error message is helpful\"\"\"\n    try:\n        divide(10, 0)\n        assert False, \"Should have raised ValueError\"\n    except ValueError as e:\n        assert \"Cannot divide by zero\" in str(e)\n\u0027\u0027\u0027\n\nprint(\"Test file example:\")\nprint(test_code)\n\nprint(\"\\n=== Running Tests ===\")\nprint(\"\"\"\nCommand: pytest test_calculator.py\n\nOutput:\n========================= test session starts =========================\ncollected 10 items\n\ntest_calculator.py ..........                                   [100%]\n\n========================= 10 passed in 0.03s =========================\n\nOptions:\n  pytest -v                    # Verbose output\n  pytest -k \"palindrome\"       # Run tests matching name\n  pytest --cov                 # Show code coverage\n  pytest -x                    # Stop on first failure\n  pytest --pdb                 # Drop into debugger on failure\n\"\"\")\n\nprint(\"\\n=== Test Organization ===\")\n\nproject_structure = \"\"\"\nmy_project/\n├── src/\n│   ├── __init__.py\n│   ├── calculator.py\n│   ├── models.py\n│   └── services.py\n│\n├── tests/\n│   ├── __init__.py\n│   ├── conftest.py          # Shared fixtures\n│   ├── test_calculator.py   # Test calculator module\n│   ├── test_models.py       # Test models\n│   └── test_services.py     # Test services\n│\n├── pytest.ini               # pytest configuration\n└── requirements-test.txt    # Test dependencies\n\"\"\"\n\nprint(project_structure)",
                                "language":  "python"
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Mocking External Dependencies",
                                "content":  "**Mocking = Fake external dependencies for testing**\n\n**Why mock?**\n- APIs cost money or have rate limits\n- Databases are slow to set up\n- External services may be unreliable\n- Need to test error scenarios\n\n**Python mocking tools:**\n```python\nfrom unittest.mock import Mock, patch, MagicMock\n\n# Create a mock object\nmock_api = Mock()\nmock_api.get_user.return_value = {\u0027id\u0027: 1, \u0027name\u0027: \u0027Test\u0027}\n\n# Patch a module function\n@patch(\u0027mymodule.requests.get\u0027)\ndef test_api(mock_get):\n    mock_get.return_value.json.return_value = {\u0027data\u0027: \u0027test\u0027}\n    # Now requests.get is mocked!\n```\n\n**Common patterns:**\n- `Mock()` - Create fake object\n- `patch()` - Replace real with fake\n- `return_value` - Set what mock returns\n- `side_effect` - Raise exception or cycle values",
                                "code":  "from unittest.mock import Mock, patch, MagicMock\nimport pytest\n\nprint(\"=== Mocking External APIs ===\")\n\n# Real function that calls external API\ndef get_weather(city):\n    \"\"\"Get weather from external API (simulated)\"\"\"\n    import requests\n    response = requests.get(f\u0027https://api.weather.com/{city}\u0027)\n    return response.json()\n\n# Test WITHOUT mocking (bad - makes real API call)\n# def test_weather_bad():\n#     result = get_weather(\u0027London\u0027)  # Slow, unreliable, costs money!\n#     assert \u0027temperature\u0027 in result\n\n# Test WITH mocking (good - no real API call)\n@patch(\u0027__main__.get_weather\u0027)\ndef test_weather_mocked(mock_get_weather):\n    # Configure mock return value\n    mock_get_weather.return_value = {\n        \u0027city\u0027: \u0027London\u0027,\n        \u0027temperature\u0027: 20,\n        \u0027conditions\u0027: \u0027sunny\u0027\n    }\n    \n    result = get_weather(\u0027London\u0027)\n    \n    assert result[\u0027temperature\u0027] == 20\n    assert result[\u0027city\u0027] == \u0027London\u0027\n    mock_get_weather.assert_called_once_with(\u0027London\u0027)\n    print(\"✓ API test passed without real API call!\")\n\n# Run the test\ntest_weather_mocked()\n\nprint(\"\\n=== Mocking Database Calls ===\")\n\nclass UserRepository:\n    \"\"\"Repository that talks to database\"\"\"\n    \n    def __init__(self, db_connection):\n        self.db = db_connection\n    \n    def get_user(self, user_id):\n        return self.db.query(f\u0027SELECT * FROM users WHERE id = {user_id}\u0027)\n    \n    def save_user(self, user):\n        return self.db.execute(\u0027INSERT INTO users ...\u0027)\n\ndef test_user_repository():\n    # Create mock database\n    mock_db = Mock()\n    mock_db.query.return_value = {\u0027id\u0027: 1, \u0027name\u0027: \u0027Alice\u0027, \u0027email\u0027: \u0027alice@test.com\u0027}\n    \n    # Inject mock into repository\n    repo = UserRepository(mock_db)\n    \n    # Test get_user\n    user = repo.get_user(1)\n    \n    assert user[\u0027name\u0027] == \u0027Alice\u0027\n    mock_db.query.assert_called_once()\n    print(\"✓ Database test passed without real database!\")\n\ntest_user_repository()\n\nprint(\"\\n=== Testing Error Scenarios ===\")\n\ndef test_api_error_handling():\n    mock_api = Mock()\n    \n    # Simulate API error\n    mock_api.get_data.side_effect = ConnectionError(\"API unavailable\")\n    \n    # Test that our code handles errors properly\n    try:\n        mock_api.get_data()\n        assert False, \"Should have raised error\"\n    except ConnectionError as e:\n        assert \"unavailable\" in str(e)\n        print(\"✓ Error handling test passed!\")\n\ntest_api_error_handling()\n\nprint(\"\\n=== Using patch() Decorator ===\")\n\n# Module we want to test\nclass EmailService:\n    def send_email(self, to, subject, body):\n        # This would normally send a real email\n        pass\n\nclass NotificationService:\n    def __init__(self, email_service):\n        self.email = email_service\n    \n    def notify_user(self, user_email, message):\n        self.email.send_email(user_email, \u0027Notification\u0027, message)\n        return True\n\ndef test_notification_sends_email():\n    # Create mock email service\n    mock_email = Mock(spec=EmailService)\n    \n    # Create notification service with mock\n    notifier = NotificationService(mock_email)\n    \n    # Test notification\n    result = notifier.notify_user(\u0027user@test.com\u0027, \u0027Hello!\u0027)\n    \n    # Verify email was \"sent\"\n    assert result == True\n    mock_email.send_email.assert_called_once_with(\n        \u0027user@test.com\u0027,\n        \u0027Notification\u0027,\n        \u0027Hello!\u0027\n    )\n    print(\"✓ Email notification test passed without sending real email!\")\n\ntest_notification_sends_email()\n\nprint(\"\\n=== Mock return_value vs side_effect ===\")\n\nmock = Mock()\n\n# return_value - always returns same thing\nmock.get_data.return_value = {\u0027status\u0027: \u0027ok\u0027}\nprint(f\"return_value: {mock.get_data()}\")\nprint(f\"return_value: {mock.get_data()}\")\n\n# side_effect with list - returns different values each call\nmock.get_next.side_effect = [1, 2, 3]\nprint(f\"side_effect list: {mock.get_next()}\")\nprint(f\"side_effect list: {mock.get_next()}\")\nprint(f\"side_effect list: {mock.get_next()}\")\n\n# side_effect with exception\nmock.fail.side_effect = ValueError(\"Something went wrong\")\ntry:\n    mock.fail()\nexcept ValueError as e:\n    print(f\"side_effect exception: {e}\")\n\nprint(\"\\n=== pytest-mock (Simpler Syntax) ===\")\n\nprint(\"\"\"\n# Install: pip install pytest-mock\n\ndef test_with_mocker(mocker):\n    # mocker fixture provided by pytest-mock\n    mock_api = mocker.patch(\u0027mymodule.external_api\u0027)\n    mock_api.return_value = {\u0027data\u0027: \u0027test\u0027}\n    \n    result = my_function()\n    \n    assert result == expected\n    mock_api.assert_called_once()\n\"\"\")\n\nprint(\"\\n=== Key Mocking Patterns ===\")\nprint(\"\"\"\n1. MOCK RETURN VALUES:\n   mock.method.return_value = \u0027result\u0027\n\n2. MOCK EXCEPTIONS:\n   mock.method.side_effect = ValueError(\u0027error\u0027)\n\n3. VERIFY CALLS:\n   mock.method.assert_called_once()\n   mock.method.assert_called_with(arg1, arg2)\n   mock.method.assert_not_called()\n\n4. PATCH MODULES:\n   @patch(\u0027module.function\u0027)\n   def test(mock_func):\n       mock_func.return_value = \u0027fake\u0027\n\n5. SPEC FOR TYPE SAFETY:\n   mock = Mock(spec=RealClass)  # Only allows real methods\n\"\"\")",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Test-Driven Development (TDD)",
                                "content":  "**TDD = Write tests before code**\n\n**The cycle:**\n\n```\n1. 🔴 Red: Write failing test\n   ↓\n2. 🟢 Green: Write minimal code to pass\n   ↓\n3. 🔵 Refactor: Improve code quality\n   ↓\n   Repeat\n```\n\n**Example workflow:**\n\n**Step 1: Red (failing test)**\n```python\ndef test_validate_email():\n    assert validate_email(\"user@example.com\") == True\n    assert validate_email(\"invalid\") == False\n# Test fails - function doesn\u0027t exist\n```\n\n**Step 2: Green (make it pass)**\n```python\ndef validate_email(email):\n    return \u0027@\u0027 in email\n# Test passes (barely)\n```\n\n**Step 3: Refactor (improve)**\n```python\nimport re\n\ndef validate_email(email):\n    pattern = r\u0027^[\\w\\.-]+@[\\w\\.-]+\\.\\w+$\u0027\n    return bool(re.match(pattern, email))\n# Better implementation, tests still pass\n```\n\n**TDD benefits:**\n- Forces you to think about requirements\n- Every line of code is tested\n- No over-engineering\n- Confidence to refactor\n- Design emerges naturally"
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **Tests are insurance** - Catch bugs before users do\n- **Unit tests are fastest** - Test small pieces in isolation\n- **pytest makes testing easy** - Simple syntax, powerful features\n- **Mock external dependencies** - Use `unittest.mock` to fake APIs, databases, emails\n- **TDD = Red, Green, Refactor** - Write test, make it pass, improve\n- **One test, one concept** - Keep tests focused and clear\n- **Fixtures for setup** - Reusable test dependencies\n- **Parametrize for multiple inputs** - Test many cases efficiently\n- **Good tests = good design** - Hard to test = bad design"
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "14_03-challenge-4",
                           "title":  "Interactive Exercise",
                           "description":  "Practice TDD by creating a Password Validator:\n1. Write tests for password requirements:\n   - Min 8 characters\n   - At least one uppercase letter\n   - At least one number\n   - At least one special character\n2. Implement the validator to pass all tests\n3. Use pytest parametrize for multiple test cases",
                           "instructions":  "Practice TDD by creating a Password Validator:\n1. Write tests for password requirements:\n   - Min 8 characters\n   - At least one uppercase letter\n   - At least one number\n   - At least one special character\n2. Implement the validator to pass all tests\n3. Use pytest parametrize for multiple test cases",
                           "starterCode":  "import pytest\nimport re\n\n# TODO: Write tests first!\ndef test_password_length():\n    # TODO: Test minimum length\n    pass\n\ndef test_password_uppercase():\n    # TODO: Test uppercase requirement\n    pass\n\n# TODO: Implement after tests\nclass PasswordValidator:\n    def __init__(self, min_length=8):\n        pass\n    \n    def validate(self, password):\n        # TODO: Return True if valid, False otherwise\n        pass\n    \n    def get_errors(self, password):\n        # TODO: Return list of error messages\n        pass",
                           "solution":  "import pytest\nimport re\nfrom typing import List\n\n\n# ============================================\n# STEP 1: Write tests first (TDD Red Phase)\n# ============================================\n\nclass PasswordValidator:\n    \"\"\"Validates passwords against security requirements.\n    \n    Requirements:\n    - Minimum length (default 8 characters)\n    - At least one uppercase letter\n    - At least one number\n    - At least one special character\n    \"\"\"\n    \n    # Define special characters for validation\n    SPECIAL_CHARS = \u0027!@#$%^\u0026*()_+-=[]{}|;:,.\u003c\u003e?\u0027\n    \n    def __init__(self, min_length: int = 8):\n        \"\"\"Initialize validator with minimum length requirement.\n        \n        Args:\n            min_length: Minimum password length (default 8)\n        \"\"\"\n        self.min_length = min_length\n    \n    def validate(self, password: str) -\u003e bool:\n        \"\"\"Check if password meets all requirements.\n        \n        Args:\n            password: The password to validate\n        \n        Returns:\n            True if password meets all requirements, False otherwise\n        \"\"\"\n        # Get list of errors - if empty, password is valid\n        errors = self.get_errors(password)\n        return len(errors) == 0\n    \n    def get_errors(self, password: str) -\u003e List[str]:\n        \"\"\"Get list of validation errors for a password.\n        \n        Args:\n            password: The password to validate\n        \n        Returns:\n            List of error messages (empty if valid)\n        \"\"\"\n        errors = []\n        \n        # Check minimum length\n        if len(password) \u003c self.min_length:\n            errors.append(\n                f\u0027Password must be at least {self.min_length} characters\u0027\n            )\n        \n        # Check for uppercase letter\n        if not any(c.isupper() for c in password):\n            errors.append(\u0027Password must contain at least one uppercase letter\u0027)\n        \n        # Check for number\n        if not any(c.isdigit() for c in password):\n            errors.append(\u0027Password must contain at least one number\u0027)\n        \n        # Check for special character\n        if not any(c in self.SPECIAL_CHARS for c in password):\n            errors.append(\u0027Password must contain at least one special character\u0027)\n        \n        return errors\n    \n    def get_strength(self, password: str) -\u003e str:\n        \"\"\"Evaluate password strength.\n        \n        Args:\n            password: The password to evaluate\n        \n        Returns:\n            Strength rating: \u0027weak\u0027, \u0027medium\u0027, \u0027strong\u0027, or \u0027invalid\u0027\n        \"\"\"\n        if not self.validate(password):\n            return \u0027invalid\u0027\n        \n        score = 0\n        \n        # Length bonuses\n        if len(password) \u003e= 12:\n            score += 2\n        elif len(password) \u003e= 10:\n            score += 1\n        \n        # Character variety bonuses\n        if sum(1 for c in password if c.isupper()) \u003e= 2:\n            score += 1\n        if sum(1 for c in password if c.isdigit()) \u003e= 2:\n            score += 1\n        if sum(1 for c in password if c in self.SPECIAL_CHARS) \u003e= 2:\n            score += 1\n        \n        # Determine strength\n        if score \u003e= 4:\n            return \u0027strong\u0027\n        elif score \u003e= 2:\n            return \u0027medium\u0027\n        else:\n            return \u0027weak\u0027\n\n\n# ============================================\n# STEP 2: Comprehensive Tests\n# ============================================\n\n# Test fixture - provides a validator instance\n@pytest.fixture\ndef validator():\n    \"\"\"Create a PasswordValidator instance for testing.\"\"\"\n    return PasswordValidator(min_length=8)\n\n\n# Test: Password minimum length\ndef test_password_length(validator):\n    \"\"\"Test that passwords must meet minimum length.\"\"\"\n    # Too short - should fail\n    assert validator.validate(\u0027Aa1!\u0027) == False\n    assert validator.validate(\u0027Aa1!abc\u0027) == False  # 7 chars\n    \n    # Exactly 8 chars - should pass (if other requirements met)\n    assert validator.validate(\u0027Aa1!abcd\u0027) == True\n    \n    # Longer - should pass\n    assert validator.validate(\u0027Aa1!abcdef\u0027) == True\n\n\n# Test: Uppercase letter requirement\ndef test_password_uppercase(validator):\n    \"\"\"Test that passwords must contain uppercase letter.\"\"\"\n    # No uppercase - should fail\n    assert validator.validate(\u0027aa1!abcd\u0027) == False\n    \n    # With uppercase - should pass\n    assert validator.validate(\u0027Aa1!abcd\u0027) == True\n    \n    # Multiple uppercase - should pass\n    assert validator.validate(\u0027AA1!abcd\u0027) == True\n\n\n# Test: Number requirement\ndef test_password_number(validator):\n    \"\"\"Test that passwords must contain a number.\"\"\"\n    # No number - should fail\n    assert validator.validate(\u0027Aaa!abcd\u0027) == False\n    \n    # With number - should pass\n    assert validator.validate(\u0027Aa1!abcd\u0027) == True\n    \n    # Multiple numbers - should pass\n    assert validator.validate(\u0027Aa12!bcd\u0027) == True\n\n\n# Test: Special character requirement\ndef test_password_special_char(validator):\n    \"\"\"Test that passwords must contain special character.\"\"\"\n    # No special char - should fail\n    assert validator.validate(\u0027Aa1aabcd\u0027) == False\n    \n    # With special char - should pass\n    assert validator.validate(\u0027Aa1!abcd\u0027) == True\n    \n    # Different special chars - all should pass\n    for char in \u0027@#$%^\u0026*\u0027:\n        assert validator.validate(f\u0027Aa1{char}abcd\u0027) == True\n\n\n# Test: Error messages\ndef test_get_errors_returns_all_issues(validator):\n    \"\"\"Test that get_errors returns all validation issues.\"\"\"\n    # Empty password - should have all errors\n    errors = validator.get_errors(\u0027\u0027)\n    assert len(errors) == 4\n    assert any(\u0027length\u0027 in e.lower() for e in errors)\n    assert any(\u0027uppercase\u0027 in e.lower() for e in errors)\n    assert any(\u0027number\u0027 in e.lower() for e in errors)\n    assert any(\u0027special\u0027 in e.lower() for e in errors)\n    \n    # Valid password - should have no errors\n    errors = validator.get_errors(\u0027Aa1!abcd\u0027)\n    assert errors == []\n\n\n# Test: Custom minimum length\ndef test_custom_min_length():\n    \"\"\"Test validator with custom minimum length.\"\"\"\n    validator = PasswordValidator(min_length=12)\n    \n    # 8 chars now too short\n    assert validator.validate(\u0027Aa1!abcd\u0027) == False\n    \n    # 12 chars - should pass\n    assert validator.validate(\u0027Aa1!abcdefgh\u0027) == True\n\n\n# Test: Parametrized tests for multiple valid passwords\n@pytest.mark.parametrize(\u0027password\u0027, [\n    \u0027Aa1!abcd\u0027,\n    \u0027Password123!\u0027,\n    \u0027MyP@ssw0rd\u0027,\n    \u0027Secure#Pass1\u0027,\n    \u0027Test1234$$\u0027,\n])\ndef test_valid_passwords(validator, password):\n    \"\"\"Test that various valid passwords pass validation.\"\"\"\n    assert validator.validate(password) == True\n\n\n# Test: Parametrized tests for invalid passwords\n@pytest.mark.parametrize(\u0027password,reason\u0027, [\n    (\u0027short1!\u0027, \u0027too short\u0027),\n    (\u0027alllowercase1!\u0027, \u0027no uppercase\u0027),\n    (\u0027ALLUPPERCASE1!\u0027, \u0027has uppercase but valid actually\u0027),\n    (\u0027NoNumbers!!\u0027, \u0027no numbers\u0027),\n    (\u0027NoSpecial123\u0027, \u0027no special chars\u0027),\n])\ndef test_invalid_passwords(validator, password, reason):\n    \"\"\"Test that invalid passwords fail validation.\"\"\"\n    # Note: \u0027ALLUPPERCASE1!\u0027 is actually valid, so we skip that case\n    if password != \u0027ALLUPPERCASE1!\u0027:\n        assert validator.validate(password) == False\n\n\n# Test: Password strength\ndef test_password_strength(validator):\n    \"\"\"Test password strength evaluation.\"\"\"\n    # Invalid password\n    assert validator.get_strength(\u0027weak\u0027) == \u0027invalid\u0027\n    \n    # Weak but valid\n    assert validator.get_strength(\u0027Aa1!abcd\u0027) in [\u0027weak\u0027, \u0027medium\u0027]\n    \n    # Strong password (long, multiple special chars, etc.)\n    assert validator.get_strength(\u0027MyStr0ng!P@ssword123\u0027) == \u0027strong\u0027\n\n\n# ============================================\n# Demo the validator\n# ============================================\n\nif __name__ == \u0027__main__\u0027:\n    print(\u0027Password Validator Demo\u0027)\n    print(\u0027=\u0027 * 40)\n    \n    validator = PasswordValidator()\n    \n    test_passwords = [\n        \u0027weak\u0027,\n        \u0027Password\u0027,\n        \u0027password123\u0027,\n        \u0027Password123\u0027,\n        \u0027Password123!\u0027,\n        \u0027MyStr0ng!P@ssword\u0027,\n    ]\n    \n    for pwd in test_passwords:\n        is_valid = validator.validate(pwd)\n        errors = validator.get_errors(pwd)\n        strength = validator.get_strength(pwd)\n        \n        print(f\"\\nPassword: \u0027{pwd}\u0027\")\n        print(f\"  Valid: {is_valid}\")\n        print(f\"  Strength: {strength}\")\n        if errors:\n            print(f\"  Errors:\")\n            for error in errors:\n                print(f\"    - {error}\")\n    \n    print(\u0027\\n\u0027 + \u0027=\u0027 * 40)\n    print(\u0027Run tests with: pytest -v\u0027)",
                           "language":  "python",
                           "testCases":  [
                                             {
                                                 "id":  "test-1",
                                                 "description":  "Code runs without errors",
                                                 "expectedOutput":  "",
                                                 "isVisible":  true
                                             }
                                         ],
                           "hints":  [
                                         {
                                             "level":  1,
                                             "text":  "Start with simple tests. Use regex for character checks. Return helpful error messages."
                                         }
                                     ],
                           "commonMistakes":  [
                                                  {
                                                      "mistake":  "Forgetting the colon after if/for/while",
                                                      "consequence":  "SyntaxError",
                                                      "correction":  "Add : at the end of the line"
                                                  },
                                                  {
                                                      "mistake":  "Using = instead of == for comparison",
                                                      "consequence":  "Assignment instead of comparison",
                                                      "correction":  "Use == for equality checks"
                                                  },
                                                  {
                                                      "mistake":  "Incorrect indentation",
                                                      "consequence":  "IndentationError",
                                                      "correction":  "Use consistent 4-space indentation"
                                                  }
                                              ],
                           "difficulty":  "beginner"
                       }
                   ],
    "difficulty":  "advanced",
    "title":  "Testing Best Practices",
    "estimatedMinutes":  35
}

## Review Instructions

Perform the following analysis:

### 1. Accuracy Check
- Verify all code examples are syntactically correct
- Confirm technical explanations match current python documentation
- Search the web for the latest python version and verify examples work with it
- Flag any deprecated APIs, syntax, or patterns

### 2. Completeness Check
- Does the lesson cover all concepts needed for a beginner to understand this topic?
- Are there missing explanations between concepts that would confuse a learner?
- Does the lesson have:
  - [ ] A clear analogy or real-world example (ANALOGY section)
  - [ ] Theoretical explanation (THEORY section)
  - [ ] Working code example (EXAMPLE section)
  - [ ] Common mistakes to avoid (WARNING section)
  - [ ] At least one practice challenge

### 3. Freshness Check
- Search for "python Testing Best Practices 2024 2025" to find latest practices
- Compare lesson content against current best practices
- Identify any outdated patterns or recommendations

### 4. Pedagogical Gap Analysis
- What prerequisite knowledge is assumed but not explained?
- What follow-up questions would a learner likely have?
- What practical applications or use cases are missing?
- Are the challenges appropriately scaffolded for the difficulty level?

## Output Format

Provide your review as structured JSON:

```json
{
  "lessonId": "15_03",
  "reviewDate": "YYYY-MM-DD",
  "overallScore": 1-10,
  "accuracy": {
    "score": 1-10,
    "issues": ["issue 1", "issue 2"],
    "recommendations": ["fix 1", "fix 2"]
  },
  "completeness": {
    "score": 1-10,
    "missingSections": ["section type needed"],
    "gaps": ["gap 1", "gap 2"],
    "recommendations": ["add X", "expand Y"]
  },
  "freshness": {
    "score": 1-10,
    "outdatedItems": ["item 1"],
    "currentVersion": "language version checked",
    "recommendations": ["update X to Y"]
  },
  "pedagogicalGaps": {
    "score": 1-10,
    "missingPrerequisites": ["concept 1"],
    "unansweredQuestions": ["question learner would have"],
    "missingUseCases": ["practical application"],
    "recommendations": ["add section on X"]
  },
  "contentLengthIssues": {
    "shortSections": [
      {"sectionTitle": "title", "currentLength": 42, "recommendation": "expand to explain X"}
    ]
  },
  "suggestedNewContent": [
    {
      "sectionType": "THEORY|EXAMPLE|WARNING|etc",
      "title": "suggested title",
      "contentOutline": "what this section should cover"
    }
  ],
  "priority": "HIGH|MEDIUM|LOW"
}
```

