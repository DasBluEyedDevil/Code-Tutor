---
type: "EXAMPLE"
title: "Customizing ModelAdmin"
---

Advanced admin configuration for our finance tracker:

**Expected Output:**
```
Admin classes defined: CategoryAdmin, AccountAdmin, TransactionAdmin
Features: list display, filters, search, inline editing
```

```python
from django.contrib import admin
from django.db.models import Sum
from django.utils.html import format_html

# Simulating Django models for demonstration
class Category:
    pass

class Account:
    pass

class Transaction:
    pass

# Admin configuration classes
class CategoryAdmin:
    """django.contrib.admin.ModelAdmin subclass"""
    
    # Columns shown in list view
    list_display = ['name', 'type', 'icon', 'colored_badge', 'transaction_count']
    
    # Filters in sidebar
    list_filter = ['type']
    
    # Search fields
    search_fields = ['name']
    
    # Fields in edit form
    fields = ['name', 'type', 'icon', 'color']
    
    def colored_badge(self, obj):
        """Show color as a colored badge."""
        return format_html(
            '<span style="background:{}; padding:2px 8px; '
            'border-radius:4px; color:white">{}</span>',
            obj.color, obj.color
        )
    colored_badge.short_description = 'Color'
    
    def transaction_count(self, obj):
        """Show number of transactions in this category."""
        return obj.transactions.count()
    transaction_count.short_description = '# Transactions'


class TransactionInline:
    """Inline editor for transactions within Account view."""
    # model = Transaction
    extra = 1  # Show 1 empty form for new transactions
    fields = ['date', 'category', 'amount', 'description']
    readonly_fields = ['created_at']


class AccountAdmin:
    """Admin for bank accounts."""
    
    list_display = ['name', 'user', 'formatted_balance', 'is_active', 'transaction_summary']
    list_filter = ['is_active', 'created_at']
    search_fields = ['name', 'user__username', 'user__email']
    
    # Group fields in sections
    fieldsets = [
        (None, {
            'fields': ['user', 'name']
        }),
        ('Financial', {
            'fields': ['balance', 'is_active'],
            'classes': ['collapse']  # Collapsible section
        }),
    ]
    
    # Add inline transaction editor
    # inlines = [TransactionInline]
    
    def formatted_balance(self, obj):
        """Color-coded balance display."""
        color = 'green' if obj.balance >= 0 else 'red'
        return format_html(
            '<span style="color:{}">${:,.2f}</span>',
            color, obj.balance
        )
    formatted_balance.short_description = 'Balance'
    formatted_balance.admin_order_field = 'balance'
    
    def transaction_summary(self, obj):
        """Show income/expense summary."""
        # In real code: use aggregation
        return format_html(
            '<span style="color:green">+$1,000</span> / '
            '<span style="color:red">-$500</span>'
        )
    transaction_summary.short_description = 'This Month'


class TransactionAdmin:
    """Admin for transactions."""
    
    list_display = ['date', 'description', 'category', 'formatted_amount', 'account']
    list_filter = ['category', 'date', 'account']
    search_fields = ['description']
    date_hierarchy = 'date'  # Date drill-down navigation
    
    # Optimize queries (prevent N+1)
    list_select_related = ['category', 'account', 'account__user']
    
    # Bulk actions
    actions = ['mark_as_reviewed', 'export_to_csv']
    
    def formatted_amount(self, obj):
        """Show amount with + or - prefix."""
        if obj.category.type == 'INC':
            return format_html('<span style="color:green">+${:,.2f}</span>', obj.amount)
        return format_html('<span style="color:red">-${:,.2f}</span>', obj.amount)
    formatted_amount.short_description = 'Amount'
    formatted_amount.admin_order_field = 'amount'
    
    def mark_as_reviewed(self, request, queryset):
        """Custom admin action."""
        count = queryset.update(reviewed=True)
        self.message_user(request, f'{count} transactions marked as reviewed.')
    mark_as_reviewed.short_description = 'Mark selected as reviewed'

print("Admin classes defined: CategoryAdmin, AccountAdmin, TransactionAdmin")
print("Features: list display, filters, search, inline editing")
```
