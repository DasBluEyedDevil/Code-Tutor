from dataclasses import dataclass, field
from typing import List, Dict, Any, Optional

@dataclass
class QueryBuilder:
    """Simulates Django ORM query building."""
    
    table: str
    _select: List[str] = field(default_factory=lambda: ['*'])
    _where: List[str] = field(default_factory=list)
    _order_by: List[str] = field(default_factory=list)
    _limit: Optional[int] = None
    _joins: List[str] = field(default_factory=list)
    
    def select(self, *fields: str) -> 'QueryBuilder':
        """Select specific fields (like .values() in Django)"""
        self._select = list(fields)
        return self
    
    def filter(self, **conditions) -> 'QueryBuilder':
        """
        Add WHERE conditions.
        Supports: field=value, field__gt=val, field__contains=val
        """
        # TODO: Parse conditions and add to _where
        # Example: amount__gt=100 -> "amount > 100"
        # Example: description__contains='food' -> "description LIKE '%food%'"
        pass
    
    def order_by(self, *fields: str) -> 'QueryBuilder':
        """Add ORDER BY. Prefix with '-' for DESC."""
        # TODO: Parse fields and add to _order_by
        # Example: '-date' -> "date DESC"
        pass
    
    def limit(self, n: int) -> 'QueryBuilder':
        """Add LIMIT clause (like [:n] in Django)"""
        self._limit = n
        return self
    
    def select_related(self, *relations: str) -> 'QueryBuilder':
        """Add JOINs for related tables."""
        # TODO: Add JOIN clauses
        # Example: 'account' -> "JOIN accounts ON transactions.account_id = accounts.id"
        pass
    
    def to_sql(self) -> str:
        """Generate SQL query string."""
        parts = [f"SELECT {', '.join(self._select)}"]
        parts.append(f"FROM {self.table}")
        
        if self._joins:
            parts.extend(self._joins)
        if self._where:
            parts.append(f"WHERE {' AND '.join(self._where)}")
        if self._order_by:
            parts.append(f"ORDER BY {', '.join(self._order_by)}")
        if self._limit:
            parts.append(f"LIMIT {self._limit}")
        
        return '\n'.join(parts)

# Test: Build a complex query
query = (
    QueryBuilder('transactions')
    .select('id', 'amount', 'description', 'date')
    .filter(amount__gt=100, category_id=5)
    .filter(description__contains='grocery')
    .select_related('account', 'category')
    .order_by('-date', 'amount')
    .limit(10)
)

print("Generated SQL:")
print(query.to_sql())