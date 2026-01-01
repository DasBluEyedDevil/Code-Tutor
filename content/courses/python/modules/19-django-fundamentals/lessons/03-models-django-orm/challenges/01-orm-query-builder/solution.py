from dataclasses import dataclass, field
from typing import List, Optional

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
        self._select = list(fields)
        return self
    
    def filter(self, **conditions) -> 'QueryBuilder':
        for key, value in conditions.items():
            if '__' in key:
                field_name, lookup = key.rsplit('__', 1)
                if lookup == 'gt':
                    self._where.append(f"{field_name} > {value}")
                elif lookup == 'gte':
                    self._where.append(f"{field_name} >= {value}")
                elif lookup == 'lt':
                    self._where.append(f"{field_name} < {value}")
                elif lookup == 'lte':
                    self._where.append(f"{field_name} <= {value}")
                elif lookup == 'contains':
                    self._where.append(f"{field_name} LIKE '%{value}%'")
                elif lookup == 'startswith':
                    self._where.append(f"{field_name} LIKE '{value}%'")
                elif lookup == 'isnull':
                    op = 'IS NULL' if value else 'IS NOT NULL'
                    self._where.append(f"{field_name} {op}")
                else:
                    # Treat unknown lookups as exact match on compound field
                    self._where.append(f"{key} = {repr(value)}")
            else:
                # Exact match
                if isinstance(value, str):
                    self._where.append(f"{key} = '{value}'")
                else:
                    self._where.append(f"{key} = {value}")
        return self
    
    def order_by(self, *fields: str) -> 'QueryBuilder':
        for f in fields:
            if f.startswith('-'):
                self._order_by.append(f"{f[1:]} DESC")
            else:
                self._order_by.append(f"{f} ASC")
        return self
    
    def limit(self, n: int) -> 'QueryBuilder':
        self._limit = n
        return self
    
    def select_related(self, *relations: str) -> 'QueryBuilder':
        for rel in relations:
            # Assume convention: relation name matches table name
            # and foreign key is {relation}_id
            table_name = f"{rel}s"  # Simple pluralization
            self._joins.append(
                f"JOIN {table_name} ON {self.table}.{rel}_id = {table_name}.id"
            )
        return self
    
    def to_sql(self) -> str:
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