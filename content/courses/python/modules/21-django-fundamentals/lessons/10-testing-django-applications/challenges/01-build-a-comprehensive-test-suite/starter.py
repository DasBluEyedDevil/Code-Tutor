import unittest
from dataclasses import dataclass, field
from typing import Dict, List, Any, Optional
from decimal import Decimal
from datetime import date

# Models
@dataclass
class Transaction:
    id: int
    amount: Decimal
    description: str
    transaction_type: str  # 'income' or 'expense'
    user_id: int
    
    def __post_init__(self):
        # TODO: Validate amount is positive
        # TODO: Validate transaction_type is 'income' or 'expense'
        pass

# Repository
class TransactionRepository:
    def __init__(self):
        self.transactions: List[Transaction] = []
        self._next_id = 1
    
    def create(self, amount: Decimal, description: str, 
               transaction_type: str, user_id: int) -> Transaction:
        tx = Transaction(
            id=self._next_id,
            amount=amount,
            description=description,
            transaction_type=transaction_type,
            user_id=user_id
        )
        self._next_id += 1
        self.transactions.append(tx)
        return tx
    
    def get_user_transactions(self, user_id: int) -> List[Transaction]:
        return [t for t in self.transactions if t.user_id == user_id]
    
    def calculate_balance(self, user_id: int) -> Decimal:
        user_txs = self.get_user_transactions(user_id)
        income = sum(t.amount for t in user_txs if t.transaction_type == 'income')
        expenses = sum(t.amount for t in user_txs if t.transaction_type == 'expense')
        return income - expenses
    
    def delete(self, tx_id: int, user_id: int) -> bool:
        for i, tx in enumerate(self.transactions):
            if tx.id == tx_id and tx.user_id == user_id:
                del self.transactions[i]
                return True
        return False


# Tests
class TransactionModelTests(unittest.TestCase):
    """Test Transaction model validation."""
    
    def test_valid_transaction_creation(self):
        """Test creating a valid transaction."""
        # TODO: Create a valid transaction and assert fields are correct
        pass
    
    def test_negative_amount_raises_error(self):
        """Test that negative amounts raise ValueError."""
        # TODO: Use assertRaises to check ValueError is raised
        pass
    
    def test_invalid_type_raises_error(self):
        """Test that invalid transaction_type raises ValueError."""
        # TODO: Test with invalid type like 'transfer'
        pass


class TransactionRepositoryTests(unittest.TestCase):
    """Test TransactionRepository methods."""
    
    def setUp(self):
        """Create a fresh repository for each test."""
        self.repo = TransactionRepository()
        self.user_id = 1
    
    def test_create_adds_transaction(self):
        """Test that create() adds a transaction."""
        # TODO: Create transaction and verify it's in the list
        pass
    
    def test_balance_with_no_transactions(self):
        """Test balance is 0 when no transactions exist."""
        # TODO: Assert balance is 0 for empty account
        pass
    
    def test_balance_calculation(self):
        """Test balance is income - expenses."""
        # TODO: Add income and expenses, verify balance
        pass
    
    def test_user_isolation(self):
        """Test that users only see their own transactions."""
        # TODO: Create transactions for two users, verify isolation
        pass
    
    def test_delete_own_transaction(self):
        """Test user can delete their own transaction."""
        # TODO: Create and delete, verify it's gone
        pass
    
    def test_cannot_delete_other_users_transaction(self):
        """Test user cannot delete another user's transaction."""
        # TODO: Try to delete other user's transaction, verify failure
        pass


class BalanceEdgeCaseTests(unittest.TestCase):
    """Test edge cases for balance calculation."""
    
    def setUp(self):
        self.repo = TransactionRepository()
        self.user_id = 1
    
    def test_only_income(self):
        """Balance with only income transactions."""
        pass
    
    def test_only_expenses(self):
        """Balance with only expense transactions (negative balance)."""
        pass
    
    def test_large_amounts(self):
        """Test with very large transaction amounts."""
        pass


# Run tests
if __name__ == '__main__':
    unittest.main(verbosity=2)