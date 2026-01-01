from dataclasses import dataclass, field
from typing import List, Dict, Any
from datetime import datetime, timedelta
from decimal import Decimal
import random

@dataclass
class AdminListView:
    model_name: str
    columns: List[str]
    rows: List[Dict[str, Any]]
    total_count: int
    filters_applied: Dict[str, Any] = field(default_factory=dict)
    
    def display(self):
        print(f"\n=== {self.model_name} Admin ===")
        print(f"Showing {len(self.rows)} of {self.total_count} items")
        if self.filters_applied:
            print(f"Filters: {self.filters_applied}")
        print("-" * 60)
        print(" | ".join(f"{col:15}" for col in self.columns))
        print("-" * 60)
        for row in self.rows:
            values = [str(row.get(col, ''))[:15] for col in self.columns]
            print(" | ".join(f"{v:15}" for v in values))

@dataclass
class DashboardWidget:
    title: str
    value: Any
    change: float
    
    def display(self):
        arrow = '+' if self.change >= 0 else ''
        print(f"{self.title}: {self.value} ({arrow}{self.change:.1f}%)")

class FinanceAdminDashboard:
    def __init__(self):
        self.categories = [
            ('Groceries', 'expense'),
            ('Salary', 'income'),
            ('Rent', 'expense'),
            ('Utilities', 'expense'),
            ('Entertainment', 'expense'),
            ('Freelance', 'income'),
        ]
        self.accounts = ['Checking', 'Savings', 'Credit Card']
        self._cached_transactions = None
    
    def generate_transactions(self, count: int = 20) -> List[Dict]:
        if self._cached_transactions and len(self._cached_transactions) >= count:
            return self._cached_transactions[:count]
        
        transactions = []
        today = datetime.now().date()
        
        for i in range(count):
            cat_name, cat_type = random.choice(self.categories)
            days_ago = random.randint(0, 30)
            date = today - timedelta(days=days_ago)
            
            if cat_type == 'income':
                amount = Decimal(random.randint(100000, 500000)) / 100
            else:
                amount = Decimal(random.randint(1000, 50000)) / 100
            
            descriptions = {
                'Groceries': ['Whole Foods', 'Trader Joes', 'Costco'],
                'Salary': ['Monthly Salary', 'Bonus', 'Commission'],
                'Rent': ['Monthly Rent', 'Rent Payment'],
                'Utilities': ['Electric Bill', 'Water Bill', 'Internet'],
                'Entertainment': ['Netflix', 'Movies', 'Concert'],
                'Freelance': ['Client Project', 'Consulting', 'Contract Work'],
            }
            
            transactions.append({
                'id': i + 1,
                'date': date.isoformat(),
                'description': random.choice(descriptions.get(cat_name, ['Payment'])),
                'category': cat_name,
                'category_type': cat_type,
                'amount': float(amount),
                'account': random.choice(self.accounts),
            })
        
        transactions.sort(key=lambda x: x['date'], reverse=True)
        self._cached_transactions = transactions
        return transactions
    
    def get_transaction_list_view(
        self, 
        page_size: int = 10,
        category_filter: str = None
    ) -> AdminListView:
        all_transactions = self.generate_transactions(50)
        
        filtered = all_transactions
        filters = {}
        
        if category_filter:
            filtered = [t for t in all_transactions if t['category'] == category_filter]
            filters['category'] = category_filter
        
        return AdminListView(
            model_name='Transaction',
            columns=['date', 'description', 'category', 'amount'],
            rows=filtered[:page_size],
            total_count=len(filtered),
            filters_applied=filters
        )
    
    def get_dashboard_widgets(self) -> List[DashboardWidget]:
        transactions = self.generate_transactions(50)
        
        income = sum(t['amount'] for t in transactions if t['category_type'] == 'income')
        expenses = sum(t['amount'] for t in transactions if t['category_type'] == 'expense')
        net = income - expenses
        count = len(transactions)
        
        return [
            DashboardWidget('Total Income', f'${income:,.2f}', random.uniform(5, 15)),
            DashboardWidget('Total Expenses', f'${expenses:,.2f}', random.uniform(-10, 10)),
            DashboardWidget('Net Balance', f'${net:,.2f}', random.uniform(-5, 20)),
            DashboardWidget('Transactions', str(count), random.uniform(0, 25)),
        ]

# Test the dashboard
dashboard = FinanceAdminDashboard()

list_view = dashboard.get_transaction_list_view(page_size=5)
if list_view:
    list_view.display()

print("\n=== Dashboard Summary ===")
widgets = dashboard.get_dashboard_widgets()
if widgets:
    for widget in widgets:
        widget.display()