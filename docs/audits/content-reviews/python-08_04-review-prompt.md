# Lesson Content Quality Review

## Context

You are reviewing a programming lesson for a tutorial platform. Your task is to evaluate the lesson content for quality and identify improvements.

## Course Information

- **Course:** Python Backend Development (python)
- **Module:** Exception Handling
- **Lesson:** Raising Exceptions and Creating Custom Exceptions (ID: 08_04)
- **Difficulty:** intermediate
- **Estimated Time:** 35 minutes

## Current Lesson Content

{
    "id":  "08_04",
    "contentSections":  [
                            {
                                "type":  "ANALOGY",
                                "title":  "The Concept: Activating the Fire Alarm",
                                "content":  "So far, we\u0027ve been CATCHING exceptions (responding to the alarm when it goes off). But what about RAISING exceptions (pressing the fire alarm button yourself)?\n\n**Why would you raise an exception?**\n\nImagine you\u0027re a security guard checking IDs at a club:\n- Someone shows a fake ID → You raise an alarm (raise ValueError)\n- Someone is under 21 → You raise an alarm (raise ValueError)\n- Someone is on the banned list → You raise a CUSTOM alarm (raise BannedPersonError)\n\nYou\u0027re not waiting for something to break - you\u0027re actively detecting a problem and **signaling it** by raising an exception.\n\n**Real-world code scenarios:**\n\n1. **Validation:** Age is negative → raise ValueError(\"Age cannot be negative\")\n2. **Business rules:** Withdrawal exceeds balance → raise InsufficientFundsError(\"Not enough money\")\n3. **Preconditions:** Function requires positive number but got zero → raise ValueError(\"Expected positive number\")\n\n**Custom Exceptions** are like creating your own fire alarm sounds:\n- Standard alarm: ValueError, TypeError (built-in)\n- Custom alarm: InsufficientFundsError, InvalidPasswordError (your own)\n\nCustom exceptions make your code self-documenting: except InsufficientFundsError: is clearer than except ValueError: in a banking app.\n\n**Exception vs. Return Value:**\n- **Return error value:** User enters wrong password → return False (expected, common)\n- **Raise exception:** Credit card number has letters → raise ValueError (unexpected, exceptional)\n\nRule of thumb: If the error is EXCEPTIONAL (shouldn\u0027t normally happen), raise an exception. If it\u0027s EXPECTED (users often get it wrong), return a value."
                            },
                            {
                                "type":  "EXAMPLE",
                                "title":  "Code Example: Raising Exceptions",
                                "content":  "The code demonstrates:\n1. **Raising built-in exceptions** with raise ValueError() when validation fails\n2. **Custom exception classes** that inherit from Exception for domain-specific errors\n3. **Re-raising exceptions** with bare raise to pass the exception up after logging\n\nCustom exceptions make error handling more specific and self-documenting.",
                                "code":  "# Example 1: Raising built-in exceptions\nprint(\"=== Raising Built-in Exceptions ===\")\n\ndef calculate_discount(price, discount_percent):\n    \"\"\"Calculate discounted price with validation.\"\"\"\n    \n    # Validate price\n    if price \u003c 0:\n        raise ValueError(\"Price cannot be negative\")\n    \n    # Validate discount\n    if discount_percent \u003c 0 or discount_percent \u003e 100:\n        raise ValueError(\"Discount must be between 0 and 100\")\n    \n    # If validations pass, calculate discount\n    discount_amount = price * (discount_percent / 100)\n    final_price = price - discount_amount\n    return final_price\n\n# Valid usage\nprint(\"Test 1: Valid values\")\ntry:\n    result = calculate_discount(100, 20)\n    print(f\"$100 with 20% discount = ${result}\\n\")\nexcept ValueError as e:\n    print(f\"Error: {e}\\n\")\n\n# Invalid price\nprint(\"Test 2: Negative price\")\ntry:\n    result = calculate_discount(-50, 20)\n    print(f\"Result: ${result}\\n\")\nexcept ValueError as e:\n    print(f\"Error: {e}\\n\")\n\n# Invalid discount\nprint(\"Test 3: Invalid discount\")\ntry:\n    result = calculate_discount(100, 150)\n    print(f\"Result: ${result}\\n\")\nexcept ValueError as e:\n    print(f\"Error: {e}\\n\")\n\n# Example 2: Creating custom exceptions\nprint(\"=== Custom Exception Classes ===\")\n\n# Define custom exception\nclass InsufficientFundsError(Exception):\n    \"\"\"Raised when account has insufficient funds for withdrawal.\"\"\"\n    pass\n\nclass AccountLockedError(Exception):\n    \"\"\"Raised when attempting to access a locked account.\"\"\"\n    pass\n\nclass BankAccount:\n    \"\"\"Simple bank account with custom exceptions.\"\"\"\n    \n    def __init__(self, balance=0):\n        self.balance = balance\n        self.is_locked = False\n    \n    def withdraw(self, amount):\n        \"\"\"Withdraw money with validation.\"\"\"\n        \n        # Check if account is locked\n        if self.is_locked:\n            raise AccountLockedError(\"Account is locked. Contact bank.\")\n        \n        # Validate amount\n        if amount \u003c= 0:\n            raise ValueError(\"Withdrawal amount must be positive\")\n        \n        # Check sufficient funds\n        if amount \u003e self.balance:\n            raise InsufficientFundsError(\n                f\"Insufficient funds. Balance: ${self.balance}, \"\n                f\"Requested: ${amount}\"\n            )\n        \n        # Process withdrawal\n        self.balance -= amount\n        return self.balance\n    \n    def lock_account(self):\n        \"\"\"Lock the account.\"\"\"\n        self.is_locked = True\n\n# Test custom exceptions\naccount = BankAccount(balance=1000)\n\nprint(\"Test 1: Valid withdrawal\")\ntry:\n    new_balance = account.withdraw(200)\n    print(f\"Withdrew $200. New balance: ${new_balance}\\n\")\nexcept (InsufficientFundsError, AccountLockedError, ValueError) as e:\n    print(f\"Error: {e}\\n\")\n\nprint(\"Test 2: Insufficient funds\")\ntry:\n    new_balance = account.withdraw(5000)\n    print(f\"New balance: ${new_balance}\\n\")\nexcept InsufficientFundsError as e:\n    print(f\"Error: {e}\\n\")\n\nprint(\"Test 3: Locked account\")\naccount.lock_account()\ntry:\n    new_balance = account.withdraw(100)\n    print(f\"New balance: ${new_balance}\\n\")\nexcept AccountLockedError as e:\n    print(f\"Error: {e}\\n\")\n\n# Example 3: Re-raising exceptions\nprint(\"=== Re-raising Exceptions ===\")\n\ndef process_transaction(account, amount):\n    \"\"\"Process transaction with logging.\"\"\"\n    try:\n        account.withdraw(amount)\n        print(f\"Transaction successful: ${amount}\")\n    except InsufficientFundsError as e:\n        print(f\"[LOG] Failed transaction: {e}\")\n        raise  # Re-raise the same exception\n\naccount2 = BankAccount(balance=100)\ntry:\n    process_transaction(account2, 200)\nexcept InsufficientFundsError:\n    print(\"Transaction declined at higher level\")",
                                "language":  "python"
                            },
                            {
                                "type":  "ANALOGY",
                                "title":  "Syntax Breakdown: Raising and Creating Exceptions",
                                "content":  "**Raising Built-in Exceptions:**\n\n```python\n# Basic syntax\nraise ExceptionType(\"Error message\")\n\n# Examples\nraise ValueError(\"Age cannot be negative\")\nraise TypeError(\"Expected string, got int\")\nraise FileNotFoundError(\"Config file not found\")\n\n# Without message (less helpful)\nraise ValueError()\n```\n\n**Creating Custom Exception Classes:**\n\n**Basic custom exception:**\n```python\nclass MyCustomError(Exception):\n    \"\"\"Description of when this error occurs.\"\"\"\n    pass  # No additional code needed\n\n# Usage\nraise MyCustomError(\"Something specific went wrong\")\n```\n\n**Custom exception with default message:**\n```python\nclass InsufficientFundsError(Exception):\n    \"\"\"Raised when account balance is too low.\"\"\"\n    def __init__(self, balance, amount):\n        self.balance = balance\n        self.amount = amount\n        message = f\"Insufficient funds: ${balance} \u003c ${amount}\"\n        super().__init__(message)\n\n# Usage\nraise InsufficientFundsError(balance=100, amount=200)\n```\n\n**Exception hierarchy for related errors:**\n```python\nclass BankError(Exception):\n    \"\"\"Base exception for all bank errors.\"\"\"\n    pass\n\nclass InsufficientFundsError(BankError):\n    \"\"\"Not enough money.\"\"\"\n    pass\n\nclass AccountLockedError(BankError):\n    \"\"\"Account is locked.\"\"\"\n    pass\n\n# Can catch all bank errors:\ntry:\n    do_banking()\nexcept BankError:  # Catches both children\n    handle_bank_error()\n\n# Or catch specific ones:\nexcept InsufficientFundsError:\n    handle_insufficient_funds()\n```\n\n**Re-raising Exceptions:**\n\n**Bare raise (re-raises the same exception):**\n```python\ntry:\n    risky_operation()\nexcept ValueError as e:\n    log_error(e)  # Log it\n    raise  # Re-raise the original exception\n```\n\n**Raising a different exception:**\n```python\ntry:\n    risky_operation()\nexcept ValueError as e:\n    # Convert to custom exception\n    raise CustomError(f\"Failed: {e}\")\n```\n\n**When to Raise Exceptions:**\n\n✅ **DO raise exceptions when:**\n- Input validation fails (negative age, invalid email)\n- Preconditions not met (function expects positive number, got zero)\n- Business rules violated (withdrawal exceeds limit)\n- Unrecoverable errors (file corrupted, database connection lost)\n\n❌ **DON\u0027T raise exceptions when:**\n- Result could be legitimately empty (search returns 0 results)\n- User input is commonly wrong (wrong password)\n- You can return a sentinel value (None, False, -1)\n- It\u0027s an expected alternate flow (user cancels operation)\n\n**Guideline:** Exceptions are for EXCEPTIONAL cases, not control flow."
                            },
                            {
                                "type":  "KEY_POINT",
                                "title":  "Key Takeaways",
                                "content":  "- **Raise exceptions** with raise ExceptionType(\u0027message\u0027) when validation fails or preconditions aren\u0027t met. Always include a descriptive message.\n- **Custom exceptions** are classes that inherit from Exception. Use them for domain-specific errors: class MyError(Exception): pass\n- **Exception hierarchy** lets you group related exceptions. Create a base exception and inherit from it: class BankError(Exception), then class InsufficientFundsError(BankError).\n- **Re-raise exceptions** with bare raise (not raise e) to preserve the original stack trace. Useful for logging then re-raising.\n- **Use exceptions for exceptional cases** (validation failures, violated preconditions). Use return values for expected alternate flows (search returns no results).\n- **Exception messages should be actionable:** Include what went wrong, expected vs. actual values, and how to fix it.\n- **Custom exceptions make code self-documenting:** except InsufficientFundsError is clearer than except ValueError in a banking app.\n- **Don\u0027t use exceptions for control flow:** They\u0027re slower than if/else and make code harder to understand. Save them for truly exceptional situations."
                            }
                        ],
    "challenges":  [
                       {
                           "type":  "FREE_CODING",
                           "id":  "08_04-challenge-3",
                           "title":  "Interactive Exercise: Build a Password Validator",
                           "description":  "Create a password validator that raises custom exceptions for different validation failures:\n\n**Requirements:**\n- Password must be at least 8 characters → raise TooShortError\n- Password must contain at least one digit → raise NoDigitError  \n- Password must contain at least one uppercase letter → raise NoUppercaseError\n- If all validations pass, return True\n\n**Your task:**\n1. Create three custom exception classes\n2. Create a validate_password() function\n3. Raise appropriate exceptions when validation fails\n4. Test with various passwords\n\n**Starter code:**",
                           "instructions":  "Create a password validator that raises custom exceptions for different validation failures:\n\n**Requirements:**\n- Password must be at least 8 characters → raise TooShortError\n- Password must contain at least one digit → raise NoDigitError  \n- Password must contain at least one uppercase letter → raise NoUppercaseError\n- If all validations pass, return True\n\n**Your task:**\n1. Create three custom exception classes\n2. Create a validate_password() function\n3. Raise appropriate exceptions when validation fails\n4. Test with various passwords\n\n**Starter code:**",
                           "starterCode":  "# TODO: Create custom exception classes\nclass TooShortError(Exception):\n    \"\"\"Password is too short.\"\"\"\n    pass\n\n# TODO: Create NoDigitError class\n\n# TODO: Create NoUppercaseError class\n\ndef validate_password(password):\n    \"\"\"Validate password and raise exceptions on failure.\"\"\"\n    \n    # TODO: Check length (\u003c 8 chars)\n    # Raise TooShortError with helpful message\n    \n    # TODO: Check for at least one digit\n    # Use: any(char.isdigit() for char in password)\n    # Raise NoDigitError if no digits found\n    \n    # TODO: Check for at least one uppercase letter\n    # Use: any(char.isupper() for char in password)\n    # Raise NoUppercaseError if no uppercase found\n    \n    # If all checks pass\n    return True\n\n# Test cases\ntest_passwords = [\n    \"SecurePass123\",  # Valid\n    \"short\",          # Too short\n    \"nouppercase123\", # No uppercase\n    \"NODIGITS\",       # No digit\n]\n\nfor pwd in test_passwords:\n    try:\n        if validate_password(pwd):\n            print(f\"✓ \u0027{pwd}\u0027 is valid\")\n    except (TooShortError, NoDigitError, NoUppercaseError) as e:\n        print(f\"✗ \u0027{pwd}\u0027: {e}\")",
                           "solution":  "# Password Validator with Custom Exceptions\n# This solution demonstrates creating and raising custom exceptions\n\n# Step 1: Define custom exception classes\nclass TooShortError(Exception):\n    \"\"\"Password is too short.\"\"\"\n    pass\n\nclass NoDigitError(Exception):\n    \"\"\"Password has no digits.\"\"\"\n    pass\n\nclass NoUppercaseError(Exception):\n    \"\"\"Password has no uppercase letters.\"\"\"\n    pass\n\ndef validate_password(password):\n    \"\"\"Validate password and raise exceptions on failure.\"\"\"\n    \n    # Step 2: Check length (must be at least 8 characters)\n    if len(password) \u003c 8:\n        raise TooShortError(f\"Password must be at least 8 characters (got {len(password)})\")\n    \n    # Step 3: Check for at least one digit\n    if not any(char.isdigit() for char in password):\n        raise NoDigitError(\"Password must contain at least one digit\")\n    \n    # Step 4: Check for at least one uppercase letter\n    if not any(char.isupper() for char in password):\n        raise NoUppercaseError(\"Password must contain at least one uppercase letter\")\n    \n    # All checks passed!\n    return True\n\n# Test cases\ntest_passwords = [\n    \"SecurePass123\",  # Valid\n    \"short\",          # Too short\n    \"nouppercase123\", # No uppercase\n    \"NODIGITS\",       # No digit\n]\n\nfor pwd in test_passwords:\n    try:\n        if validate_password(pwd):\n            print(f\"Valid: \u0027{pwd}\u0027\")\n    except (TooShortError, NoDigitError, NoUppercaseError) as e:\n        print(f\"Invalid \u0027{pwd}\u0027: {e}\")",
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
                                             "text":  "Create each exception class with pass. In validate_password, use if len(password) \u003c 8: raise TooShortError(...). Use any() with generator expressions to check for digits and uppercase."
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
    "difficulty":  "intermediate",
    "title":  "Raising Exceptions and Creating Custom Exceptions",
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
- Search for "python Raising Exceptions and Creating Custom Exceptions 2024 2025" to find latest practices
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
  "lessonId": "08_04",
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

