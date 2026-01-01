---
type: "EXAMPLE"
title: "Finance Tracker: Complete View/Template Example"
---

**Putting It All Together**

This example shows a complete dashboard view with:
- Class-based view with custom context
- Template inheritance
- Dynamic data display
- Integration with the Finance Tracker theme

```python
# views.py - Dashboard with CBV
from django.views.generic import TemplateView
from django.contrib.auth.mixins import LoginRequiredMixin
from django.db.models import Sum, Count
from django.db.models.functions import TruncMonth
from .models import Transaction, Category


class DashboardView(LoginRequiredMixin, TemplateView):
    """Finance Tracker Dashboard with summary statistics."""
    template_name = 'dashboard.html'
    
    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        user = self.request.user
        
        # Get all user transactions
        transactions = Transaction.objects.filter(user=user)
        
        # Calculate totals
        totals = transactions.aggregate(
            income=Sum('amount', filter=models.Q(transaction_type='income')),
            expenses=Sum('amount', filter=models.Q(transaction_type='expense'))
        )
        
        context['income'] = totals['income'] or 0
        context['expenses'] = abs(totals['expenses'] or 0)
        context['balance'] = context['income'] - context['expenses']
        
        # Recent transactions
        context['recent_transactions'] = transactions.order_by(
            '-created_at'
        )[:5]
        
        # Spending by category
        context['by_category'] = transactions.filter(
            transaction_type='expense'
        ).values('category__name').annotate(
            total=Sum('amount')
        ).order_by('-total')[:5]
        
        # Monthly trend (last 6 months)
        context['monthly_data'] = transactions.annotate(
            month=TruncMonth('created_at')
        ).values('month').annotate(
            income=Sum('amount', filter=models.Q(transaction_type='income')),
            expenses=Sum('amount', filter=models.Q(transaction_type='expense'))
        ).order_by('-month')[:6]
        
        return context


# urls.py
from django.urls import path
from .views import DashboardView

urlpatterns = [
    path('dashboard/', DashboardView.as_view(), name='dashboard'),
]


# templates/dashboard.html
"""
{% extends 'base.html' %}

{% block title %}Dashboard - Finance Tracker{% endblock %}

{% block content %}
<div class="max-w-6xl mx-auto">
    <h1 class="text-3xl font-bold mb-8">Dashboard</h1>
    
    <!-- Summary Cards -->
    <div class="grid grid-cols-1 md:grid-cols-3 gap-6 mb-8">
        <div class="bg-white rounded-lg shadow p-6">
            <h3 class="text-gray-500 text-sm">Total Income</h3>
            <p class="text-2xl font-bold text-green-500">
                ${{ income|floatformat:2 }}
            </p>
        </div>
        <div class="bg-white rounded-lg shadow p-6">
            <h3 class="text-gray-500 text-sm">Total Expenses</h3>
            <p class="text-2xl font-bold text-red-500">
                ${{ expenses|floatformat:2 }}
            </p>
        </div>
        <div class="bg-white rounded-lg shadow p-6">
            <h3 class="text-gray-500 text-sm">Balance</h3>
            <p class="text-2xl font-bold 
                {% if balance >= 0 %}text-blue-500{% else %}text-red-500{% endif %}">
                ${{ balance|floatformat:2 }}
            </p>
        </div>
    </div>
    
    <div class="grid grid-cols-1 lg:grid-cols-2 gap-8">
        <!-- Recent Transactions -->
        <div class="bg-white rounded-lg shadow">
            <div class="p-4 border-b flex justify-between items-center">
                <h2 class="font-bold">Recent Transactions</h2>
                <a href="{% url 'transaction-list' %}" class="text-blue-500 text-sm">
                    View All
                </a>
            </div>
            <div class="divide-y">
                {% for tx in recent_transactions %}
                    <div class="p-4 flex justify-between">
                        <div>
                            <p class="font-medium">{{ tx.description }}</p>
                            <p class="text-sm text-gray-500">
                                {{ tx.created_at|date:"M d" }}
                            </p>
                        </div>
                        <span class="font-bold
                            {% if tx.transaction_type == 'income' %}
                                text-green-500
                            {% else %}
                                text-red-500
                            {% endif %}">
                            {% if tx.transaction_type == 'income' %}+{% else %}-{% endif %}
                            ${{ tx.amount|floatformat:2 }}
                        </span>
                    </div>
                {% empty %}
                    <p class="p-4 text-gray-500">No transactions yet</p>
                {% endfor %}
            </div>
        </div>
        
        <!-- Spending by Category -->
        <div class="bg-white rounded-lg shadow">
            <div class="p-4 border-b">
                <h2 class="font-bold">Top Spending Categories</h2>
            </div>
            <div class="p-4">
                {% for cat in by_category %}
                    <div class="mb-3">
                        <div class="flex justify-between mb-1">
                            <span>{{ cat.category__name }}</span>
                            <span class="text-red-500">
                                ${{ cat.total|floatformat:2 }}
                            </span>
                        </div>
                        <div class="bg-gray-200 rounded-full h-2">
                            <div class="bg-red-500 rounded-full h-2" 
                                 style="width: {{ cat.total|divisibleby:expenses|default:0|multiply:100 }}%">
                            </div>
                        </div>
                    </div>
                {% empty %}
                    <p class="text-gray-500">No expense data</p>
                {% endfor %}
            </div>
        </div>
    </div>
</div>
{% endblock %}
"""

print("=== Finance Tracker Dashboard ===")

print("\nDashboardView provides:")
print("  - income: Total income")
print("  - expenses: Total expenses")
print("  - balance: Net balance")
print("  - recent_transactions: Last 5 transactions")
print("  - by_category: Top spending categories")
print("  - monthly_data: Monthly trends")

print("\nTemplate Features:")
print("  - Grid layout with summary cards")
print("  - Conditional styling (green/red)")
print("  - Date formatting")
print("  - Empty state handling")
```
