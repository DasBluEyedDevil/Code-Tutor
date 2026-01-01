---
type: "EXAMPLE"
title: "Testing Models and Business Logic"
---

Testing Django models and their methods:

**Expected Output:**
```
test_transaction_creation ... ok
test_transaction_str_representation ... ok
test_balance_calculation ... ok
test_category_transaction_count ... ok
```

```python
import unittest
from dataclasses import dataclass, field
from typing import List, Optional
from datetime import date, datetime
from decimal import Decimal


# Models (simulating Django models)
@dataclass
class Category:
    id: int
    name: str
    description: str = ""
    
    def __str__(self):
        return self.name

@dataclass
class Transaction:
    id: int
    amount: Decimal
    description: str
    category: Category
    transaction_type: str  # 'income' or 'expense'
    date: date = field(default_factory=date.today)
    
    def __str__(self):
        sign = '+' if self.transaction_type == 'income' else '-'
        return f"{sign}${self.amount} - {self.description}"
    
    def __post_init__(self):
        if self.transaction_type not in ('income', 'expense'):
            raise ValueError("transaction_type must be 'income' or 'expense'")
        if self.amount <= 0:
            raise ValueError("amount must be positive")


# Repository (simulating Django ORM)
class TransactionRepository:
    def __init__(self):
        self.transactions: List[Transaction] = []
    
    def add(self, tx: Transaction):
        self.transactions.append(tx)
    
    def filter_by_category(self, category: Category) -> List[Transaction]:
        return [t for t in self.transactions if t.category.id == category.id]
    
    def filter_by_type(self, tx_type: str) -> List[Transaction]:
        return [t for t in self.transactions if t.transaction_type == tx_type]
    
    def calculate_balance(self) -> Decimal:
        income = sum(t.amount for t in self.filter_by_type('income'))
        expenses = sum(t.amount for t in self.filter_by_type('expense'))
        return income - expenses


# Tests
class TransactionModelTests(unittest.TestCase):
    """Test Transaction model behavior."""
    
    def setUp(self):
        """Set up test fixtures - runs before each test."""
        self.category = Category(id=1, name='Food')
    
    def test_transaction_creation(self):
        """Test that transactions are created with correct values."""
        tx = Transaction(
            id=1,
            amount=Decimal('50.00'),
            description='Groceries',
            category=self.category,
            transaction_type='expense'
        )
        
        self.assertEqual(tx.amount, Decimal('50.00'))
        self.assertEqual(tx.description, 'Groceries')
        self.assertEqual(tx.category.name, 'Food')
        self.assertEqual(tx.transaction_type, 'expense')
    
    def test_transaction_str_representation(self):
        """Test __str__ shows correct format."""
        expense = Transaction(
            id=1, amount=Decimal('50.00'), description='Lunch',
            category=self.category, transaction_type='expense'
        )
        income = Transaction(
            id=2, amount=Decimal('100.00'), description='Salary',
            category=self.category, transaction_type='income'
        )
        
        self.assertEqual(str(expense), '-$50.00 - Lunch')
        self.assertEqual(str(income), '+$100.00 - Salary')
    
    def test_invalid_transaction_type_raises_error(self):
        """Test that invalid transaction types raise ValueError."""
        with self.assertRaises(ValueError) as context:
            Transaction(
                id=1, amount=Decimal('50.00'), description='Test',
                category=self.category, transaction_type='invalid'
            )
        self.assertIn('income', str(context.exception))
    
    def test_negative_amount_raises_error(self):
        """Test that negative amounts raise ValueError."""
        with self.assertRaises(ValueError):
            Transaction(
                id=1, amount=Decimal('-50.00'), description='Test',
                category=self.category, transaction_type='expense'
            )


class TransactionRepositoryTests(unittest.TestCase):
    """Test TransactionRepository methods."""
    
    def setUp(self):
        self.repo = TransactionRepository()
        self.food = Category(id=1, name='Food')
        self.transport = Category(id=2, name='Transport')
        
        # Add test transactions
        self.repo.add(Transaction(
            id=1, amount=Decimal('50'), description='Groceries',
            category=self.food, transaction_type='expense'
        ))
        self.repo.add(Transaction(
            id=2, amount=Decimal('30'), description='Bus',
            category=self.transport, transaction_type='expense'
        ))
        self.repo.add(Transaction(
            id=3, amount=Decimal('1000'), description='Salary',
            category=self.food, transaction_type='income'
        ))
    
    def test_balance_calculation(self):
        """Test that balance is income - expenses."""
        balance = self.repo.calculate_balance()
        # 1000 (income) - 50 - 30 (expenses) = 920
        self.assertEqual(balance, Decimal('920'))
    
    def test_filter_by_category(self):
        """Test filtering transactions by category."""
        food_transactions = self.repo.filter_by_category(self.food)
        self.assertEqual(len(food_transactions), 2)
        
        transport_transactions = self.repo.filter_by_category(self.transport)
        self.assertEqual(len(transport_transactions), 1)
    
    def test_filter_by_type(self):
        """Test filtering by income/expense."""
        expenses = self.repo.filter_by_type('expense')
        self.assertEqual(len(expenses), 2)
        
        income = self.repo.filter_by_type('income')
        self.assertEqual(len(income), 1)


# Run tests
if __name__ == '__main__':
    # Create test suite
    loader = unittest.TestLoader()
    suite = unittest.TestSuite()
    
    suite.addTests(loader.loadTestsFromTestCase(TransactionModelTests))
    suite.addTests(loader.loadTestsFromTestCase(TransactionRepositoryTests))
    
    # Run with verbose output
    runner = unittest.TextTestRunner(verbosity=2)
    result = runner.run(suite)
    
    print(f"\n{'='*50}")
    print(f"Tests run: {result.testsRun}")
    print(f"Failures: {len(result.failures)}")
    print(f"Errors: {len(result.errors)}")
```
