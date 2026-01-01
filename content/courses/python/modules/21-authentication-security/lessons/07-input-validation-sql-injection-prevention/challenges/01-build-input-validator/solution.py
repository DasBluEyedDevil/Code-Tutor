from dataclasses import dataclass
from typing import Optional, List, Dict, Any
from decimal import Decimal, InvalidOperation
from datetime import date, datetime
import re

# Simulated database for testing
class MockDatabase:
    def __init__(self):
        self.queries: List[tuple] = []  # Track (query, params)
    
    def execute(self, query: str, params: tuple) -> List[Dict]:
        self.queries.append((query, params))
        return [{"id": 1, "status": "success"}]

@dataclass
class ValidationResult:
    is_valid: bool
    errors: List[str]
    sanitized_data: Optional[Dict] = None

class TransactionValidator:
    """Validates transaction input data."""
    
    VALID_CATEGORIES = ["food", "transport", "utilities", "entertainment", "income", "other"]
    SQL_INJECTION_PATTERNS = [
        r"(--|#)",              # SQL comments
        r"(;\s*DROP)",          # DROP statements
        r"(;\s*DELETE)",        # DELETE statements
        r"('\s*OR\s+'1)",       # OR injection
        r"(UNION\s+SELECT)",    # UNION injection
    ]
    
    def validate_amount(self, amount: Any) -> tuple[bool, str, Optional[Decimal]]:
        """Validate transaction amount."""
        try:
            decimal_amount = Decimal(str(amount))
        except (InvalidOperation, ValueError, TypeError):
            return (False, "Amount must be a valid number", None)
        
        if decimal_amount <= 0:
            return (False, "Amount must be positive", None)
        
        if decimal_amount > Decimal("1000000"):
            return (False, "Amount cannot exceed 1,000,000", None)
        
        # Check decimal places
        if decimal_amount.as_tuple().exponent < -2:
            return (False, "Amount cannot have more than 2 decimal places", None)
        
        return (True, "", decimal_amount)
    
    def validate_description(self, description: Any) -> tuple[bool, str, Optional[str]]:
        """Validate and sanitize description."""
        if not isinstance(description, str):
            return (False, "Description must be a string", None)
        
        # Strip whitespace first
        description = description.strip()
        
        if len(description) < 1:
            return (False, "Description cannot be empty", None)
        
        if len(description) > 500:
            return (False, "Description cannot exceed 500 characters", None)
        
        # Check for SQL injection
        if self.detect_sql_injection(description):
            return (False, "Description contains invalid characters", None)
        
        # Strip HTML tags
        description = re.sub(r'<[^>]+>', '', description)
        
        return (True, "", description.strip())
    
    def validate_category(self, category: Any) -> tuple[bool, str, Optional[str]]:
        """Validate category against allowed values."""
        if not isinstance(category, str):
            return (False, "Category must be a string", None)
        
        category_lower = category.lower().strip()
        
        if category_lower not in self.VALID_CATEGORIES:
            return (False, f"Category must be one of: {', '.join(self.VALID_CATEGORIES)}", None)
        
        return (True, "", category_lower)
    
    def validate_date(self, tx_date: Any) -> tuple[bool, str, Optional[date]]:
        """Validate transaction date."""
        if tx_date is None:
            return (True, "", date.today())
        
        if isinstance(tx_date, date):
            if tx_date > date.today():
                return (False, "Date cannot be in the future", None)
            return (True, "", tx_date)
        
        if isinstance(tx_date, str):
            try:
                parsed_date = datetime.strptime(tx_date, "%Y-%m-%d").date()
                if parsed_date > date.today():
                    return (False, "Date cannot be in the future", None)
                return (True, "", parsed_date)
            except ValueError:
                return (False, "Date must be in YYYY-MM-DD format", None)
        
        return (False, "Date must be a string or date object", None)
    
    def detect_sql_injection(self, value: str) -> bool:
        """Check if value contains SQL injection patterns."""
        for pattern in self.SQL_INJECTION_PATTERNS:
            if re.search(pattern, value, re.IGNORECASE):
                return True
        return False
    
    def validate_transaction(self, data: Dict[str, Any]) -> ValidationResult:
        """Validate complete transaction data."""
        errors = []
        sanitized = {}
        
        # Validate amount
        is_valid, error, value = self.validate_amount(data.get("amount"))
        if not is_valid:
            errors.append(f"Amount: {error}")
        else:
            sanitized["amount"] = value
        
        # Validate description
        is_valid, error, value = self.validate_description(data.get("description"))
        if not is_valid:
            errors.append(f"Description: {error}")
        else:
            sanitized["description"] = value
        
        # Validate category
        is_valid, error, value = self.validate_category(data.get("category"))
        if not is_valid:
            errors.append(f"Category: {error}")
        else:
            sanitized["category"] = value
        
        # Validate date
        is_valid, error, value = self.validate_date(data.get("date"))
        if not is_valid:
            errors.append(f"Date: {error}")
        else:
            sanitized["date"] = value
        
        if errors:
            return ValidationResult(is_valid=False, errors=errors, sanitized_data=None)
        
        return ValidationResult(is_valid=True, errors=[], sanitized_data=sanitized)


class SafeQueryBuilder:
    """Builds parameterized SQL queries."""
    
    def __init__(self, db: MockDatabase):
        self.db = db
    
    def insert_transaction(
        self,
        user_id: int,
        amount: Decimal,
        category: str,
        description: str,
        tx_date: date
    ) -> Dict:
        """Insert transaction using parameterized query."""
        query = """
            INSERT INTO transactions (user_id, amount, category, description, transaction_date)
            VALUES ($1, $2, $3, $4, $5)
            RETURNING id, user_id, amount, category, description, transaction_date
        """
        params = (user_id, amount, category, description, tx_date)
        result = self.db.execute(query, params)
        return result[0] if result else {}
    
    def search_transactions(
        self,
        user_id: int,
        category: Optional[str] = None,
        min_amount: Optional[Decimal] = None,
        max_amount: Optional[Decimal] = None,
        limit: int = 20,
        offset: int = 0
    ) -> List[Dict]:
        """Search transactions with optional filters using parameterized query."""
        query_parts = ["SELECT * FROM transactions WHERE user_id = $1"]
        params = [user_id]
        param_count = 1
        
        if category is not None:
            param_count += 1
            query_parts.append(f"AND category = ${param_count}")
            params.append(category)
        
        if min_amount is not None:
            param_count += 1
            query_parts.append(f"AND amount >= ${param_count}")
            params.append(min_amount)
        
        if max_amount is not None:
            param_count += 1
            query_parts.append(f"AND amount <= ${param_count}")
            params.append(max_amount)
        
        param_count += 1
        query_parts.append(f"ORDER BY transaction_date DESC LIMIT ${param_count}")
        params.append(limit)
        
        param_count += 1
        query_parts.append(f"OFFSET ${param_count}")
        params.append(offset)
        
        query = " ".join(query_parts)
        return self.db.execute(query, tuple(params))


# Tests
print("Input Validation Tests")
print("=" * 50)

validator = TransactionValidator()

# Test 1: Valid transaction
print("\nTest 1: Valid transaction")
result = validator.validate_transaction({
    "amount": "99.99",
    "category": "Food",
    "description": "Grocery shopping",
    "date": None
})
assert result.is_valid, f"Expected valid, got errors: {result.errors}"
assert result.sanitized_data["amount"] == Decimal("99.99")
assert result.sanitized_data["category"] == "food"
print("Valid transaction accepted")

# Test 2: Invalid amount
print("\nTest 2: Invalid amount")
result = validator.validate_transaction({
    "amount": "-50",
    "category": "food",
    "description": "Test"
})
assert not result.is_valid
assert any("amount" in e.lower() for e in result.errors)
print(f"Rejected with: {result.errors}")

# Test 3: SQL injection in description
print("\nTest 3: SQL injection detection")
result = validator.validate_transaction({
    "amount": "10.00",
    "category": "food",
    "description": "Test'; DROP TABLE users; --"
})
assert not result.is_valid
assert any("injection" in e.lower() or "invalid" in e.lower() for e in result.errors)
print(f"SQL injection blocked: {result.errors}")

# Test 4: Invalid category
print("\nTest 4: Invalid category")
result = validator.validate_transaction({
    "amount": "10.00",
    "category": "hacking",
    "description": "Test"
})
assert not result.is_valid
assert any("category" in e.lower() for e in result.errors)
print(f"Invalid category rejected: {result.errors}")

# Test 5: HTML stripping
print("\nTest 5: HTML tag stripping")
result = validator.validate_transaction({
    "amount": "25.00",
    "category": "other",
    "description": "<script>alert('xss')</script>Normal text"
})
assert result.is_valid
assert "<script>" not in result.sanitized_data["description"]
assert "Normal text" in result.sanitized_data["description"]
print(f"HTML stripped: {result.sanitized_data['description']}")

# Test 6: Safe query builder
print("\nTest 6: Parameterized queries")
db = MockDatabase()
builder = SafeQueryBuilder(db)

builder.insert_transaction(
    user_id=1,
    amount=Decimal("50.00"),
    category="food",
    description="Lunch",
    tx_date=date.today()
)

assert len(db.queries) == 1
query, params = db.queries[0]
assert "$1" in query and "$2" in query  # Parameterized
assert "50.00" not in query  # Amount not in query string
assert params == (1, Decimal("50.00"), "food", "Lunch", date.today())
print(f"Query: {query[:50]}...")
print(f"Params: {params}")

# Test 7: Search with filters
print("\nTest 7: Search with parameterized filters")
builder.search_transactions(
    user_id=1,
    category="food",
    min_amount=Decimal("10.00"),
    limit=10,
    offset=0
)

query, params = db.queries[-1]
assert "$1" in query
assert "food" not in query  # Category not in query string
assert 1 in params and "food" in params
print(f"Query: {query[:60]}...")
print(f"Params: {params}")

print("\n" + "=" * 50)
print("All input validation tests passed!")