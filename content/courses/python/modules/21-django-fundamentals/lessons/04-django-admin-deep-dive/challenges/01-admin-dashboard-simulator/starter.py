from dataclasses import dataclass, field
from typing import List, Dict, Any
from datetime import datetime, timedelta
from decimal import Decimal
import random

@dataclass
class AdminListView:
    """Represents a Django admin list view."""
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
        # Print header
        print(" | ".join(f"{col:15}" for col in self.columns))
        print("-" * 60)
        # Print rows
        for row in self.rows:
            values = [str(row.get(col, ''))[:15] for col in self.columns]
            print(" | ".join(f"{v:15}" for v in values))

@dataclass
class DashboardWidget:
    """A dashboard summary widget."""
    title: str
    value: Any
    change: float  # Percentage change from previous period
    
    def display(self):
        arrow = '+' if self.change >= 0 else ''
        print(f"{self.title}: {self.value} ({arrow}{self.change:.1f}%)")

class FinanceAdminDashboard:
    """Generates admin dashboard data for finance tracker."""
    
    def __init__(self):
        self.categories = ['Groceries', 'Salary', 'Rent', 'Utilities', 'Entertainment']
        self.accounts = ['Checking', 'Savings', 'Credit Card']
    
    def generate_transactions(self, count: int = 20) -> List[Dict]:
        """Generate realistic transaction data."""
        transactions = []
        # TODO: Generate `count` transactions with:
        # - id, date, description, category, amount, account
        # - dates within last 30 days
        # - realistic amounts (expenses: 10-500, income: 1000-5000)
        return transactions
    
    def get_transaction_list_view(
        self, 
        page_size: int = 10,
        category_filter: str = None
    ) -> AdminListView:
        """Generate admin list view for transactions."""
        all_transactions = self.generate_transactions(50)
        
        # TODO: Apply filter if category_filter is set
        # TODO: Return AdminListView with:
        #   columns: ['date', 'description', 'category', 'amount']
        #   rows: first page_size transactions
        #   total_count: total matching transactions
        pass
    
    def get_dashboard_widgets(self) -> List[DashboardWidget]:
        """Generate summary widgets for admin dashboard."""
        # TODO: Return list of DashboardWidget for:
        # - Total Income (this month)
        # - Total Expenses (this month)
        # - Net Balance
        # - Transaction Count
        pass

# Test the dashboard
dashboard = FinanceAdminDashboard()

# Show list view
list_view = dashboard.get_transaction_list_view(page_size=5)
if list_view:
    list_view.display()

# Show widgets
print("\n=== Dashboard Summary ===")
widgets = dashboard.get_dashboard_widgets()
if widgets:
    for widget in widgets:
        widget.display()