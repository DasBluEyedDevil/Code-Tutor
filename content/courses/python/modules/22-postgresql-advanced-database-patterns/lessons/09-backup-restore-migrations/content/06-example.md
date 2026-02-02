---
type: "EXAMPLE"
title: "Sample Migration: Add Budgets Table"
---

**migrations/versions/001_add_budgets.py:**

```python
"""Add budgets table

Revision ID: 001_add_budgets
Create Date: 2024-01-15
"""
from alembic import op
import sqlalchemy as sa

# Revision identifiers
revision = '001_add_budgets'
down_revision = None
branch_labels = None
depends_on = None

def upgrade():
    """Add budgets table for tracking spending limits."""
    op.create_table(
        'budgets',
        sa.Column('id', sa.Integer(), primary_key=True),
        sa.Column('user_id', sa.Integer(), sa.ForeignKey('users.id', ondelete='CASCADE'), nullable=False),
        sa.Column('category_id', sa.Integer(), sa.ForeignKey('categories.id', ondelete='CASCADE')),
        sa.Column('name', sa.String(100), nullable=False),
        sa.Column('amount', sa.Numeric(12, 2), nullable=False),
        sa.Column('period', sa.String(20), server_default='monthly'),
        sa.Column('start_date', sa.Date(), nullable=False),
        sa.Column('end_date', sa.Date()),
        sa.Column('created_at', sa.DateTime(), server_default=sa.func.now()),
        sa.Column('updated_at', sa.DateTime(), server_default=sa.func.now()),
        sa.Column('deleted_at', sa.DateTime()),
    )
    
    # Create indexes
    op.create_index(
        'idx_budgets_user_id',
        'budgets',
        ['user_id']
    )
    
    op.create_index(
        'idx_budgets_active',
        'budgets',
        ['user_id', 'start_date'],
        postgresql_where=sa.text('deleted_at IS NULL')
    )
    
    # Add check constraint
    op.create_check_constraint(
        'chk_budget_amount_positive',
        'budgets',
        'amount > 0'
    )
    
    op.create_check_constraint(
        'chk_budget_period',
        'budgets',
        "period IN ('daily', 'weekly', 'monthly', 'yearly')"
    )

def downgrade():
    """Remove budgets table."""
    op.drop_constraint('chk_budget_period', 'budgets')
    op.drop_constraint('chk_budget_amount_positive', 'budgets')
    op.drop_index('idx_budgets_active')
    op.drop_index('idx_budgets_user_id')
    op.drop_table('budgets')

# Run with: alembic upgrade head
# Rollback with: alembic downgrade -1
```
