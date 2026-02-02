import unittest
from dataclasses import dataclass, field
from typing import Dict, List, Any, Optional
from decimal import Decimal
from datetime import date

@dataclass
class Transaction:
    id: int
    amount: Decimal
    description: str
    transaction_type: str
    user_id: int
    
    def __post_init__(self):
        if self.amount <= 0:
            raise ValueError("Amount must be positive")
        if self.transaction_type not in ('income', 'expense'):
            raise ValueError("transaction_type must be 'income' or 'expense'")


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


class TransactionModelTests(unittest.TestCase):
    def test_valid_transaction_creation(self):
        tx = Transaction(
            id=1,
            amount=Decimal('50.00'),
            description='Test',
            transaction_type='expense',
            user_id=1
        )
        self.assertEqual(tx.amount, Decimal('50.00'))
        self.assertEqual(tx.transaction_type, 'expense')
    
    def test_negative_amount_raises_error(self):
        with self.assertRaises(ValueError) as context:
            Transaction(
                id=1, amount=Decimal('-50.00'), description='Test',
                transaction_type='expense', user_id=1
            )
        self.assertIn('positive', str(context.exception))
    
    def test_zero_amount_raises_error(self):
        with self.assertRaises(ValueError):
            Transaction(
                id=1, amount=Decimal('0'), description='Test',
                transaction_type='expense', user_id=1
            )
    
    def test_invalid_type_raises_error(self):
        with self.assertRaises(ValueError) as context:
            Transaction(
                id=1, amount=Decimal('50'), description='Test',
                transaction_type='transfer', user_id=1
            )
        self.assertIn('income', str(context.exception))


class TransactionRepositoryTests(unittest.TestCase):
    def setUp(self):
        self.repo = TransactionRepository()
        self.user_id = 1
    
    def test_create_adds_transaction(self):
        tx = self.repo.create(
            amount=Decimal('50'),
            description='Test',
            transaction_type='expense',
            user_id=self.user_id
        )
        self.assertEqual(len(self.repo.transactions), 1)
        self.assertEqual(tx.id, 1)
    
    def test_balance_with_no_transactions(self):
        balance = self.repo.calculate_balance(self.user_id)
        self.assertEqual(balance, Decimal('0'))
    
    def test_balance_calculation(self):
        self.repo.create(Decimal('100'), 'Salary', 'income', self.user_id)
        self.repo.create(Decimal('30'), 'Lunch', 'expense', self.user_id)
        self.repo.create(Decimal('20'), 'Coffee', 'expense', self.user_id)
        
        balance = self.repo.calculate_balance(self.user_id)
        self.assertEqual(balance, Decimal('50'))  # 100 - 30 - 20
    
    def test_user_isolation(self):
        self.repo.create(Decimal('100'), 'User1 income', 'income', 1)
        self.repo.create(Decimal('50'), 'User2 income', 'income', 2)
        
        user1_txs = self.repo.get_user_transactions(1)
        user2_txs = self.repo.get_user_transactions(2)
        
        self.assertEqual(len(user1_txs), 1)
        self.assertEqual(len(user2_txs), 1)
        self.assertEqual(user1_txs[0].amount, Decimal('100'))
    
    def test_delete_own_transaction(self):
        tx = self.repo.create(Decimal('50'), 'Test', 'expense', self.user_id)
        result = self.repo.delete(tx.id, self.user_id)
        
        self.assertTrue(result)
        self.assertEqual(len(self.repo.transactions), 0)
    
    def test_cannot_delete_other_users_transaction(self):
        tx = self.repo.create(Decimal('50'), 'Test', 'expense', 1)
        result = self.repo.delete(tx.id, 2)  # Different user
        
        self.assertFalse(result)
        self.assertEqual(len(self.repo.transactions), 1)


class BalanceEdgeCaseTests(unittest.TestCase):
    def setUp(self):
        self.repo = TransactionRepository()
        self.user_id = 1
    
    def test_only_income(self):
        self.repo.create(Decimal('100'), 'Salary', 'income', self.user_id)
        self.repo.create(Decimal('50'), 'Bonus', 'income', self.user_id)
        
        balance = self.repo.calculate_balance(self.user_id)
        self.assertEqual(balance, Decimal('150'))
    
    def test_only_expenses(self):
        self.repo.create(Decimal('100'), 'Rent', 'expense', self.user_id)
        self.repo.create(Decimal('50'), 'Food', 'expense', self.user_id)
        
        balance = self.repo.calculate_balance(self.user_id)
        self.assertEqual(balance, Decimal('-150'))
    
    def test_large_amounts(self):
        self.repo.create(Decimal('999999999.99'), 'Big income', 'income', self.user_id)
        self.repo.create(Decimal('999999999.98'), 'Big expense', 'expense', self.user_id)
        
        balance = self.repo.calculate_balance(self.user_id)
        self.assertEqual(balance, Decimal('0.01'))
    
    def test_many_small_transactions(self):
        for i in range(100):
            self.repo.create(Decimal('1'), f'Small {i}', 'income', self.user_id)
        
        balance = self.repo.calculate_balance(self.user_id)
        self.assertEqual(balance, Decimal('100'))


if __name__ == '__main__':
    unittest.main(verbosity=2)