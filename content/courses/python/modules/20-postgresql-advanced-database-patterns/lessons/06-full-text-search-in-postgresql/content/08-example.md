---
type: "EXAMPLE"
title: "Complete Search Implementation"
---

**A full-featured search function for the Finance Tracker:**

```python
import asyncpg
import asyncio
from dataclasses import dataclass
from typing import List, Optional
from datetime import date

@dataclass
class SearchResult:
    id: int
    description: str
    amount: float
    date: date
    category: str
    rank: float
    headline: str

@dataclass
class SearchFilters:
    min_amount: Optional[float] = None
    max_amount: Optional[float] = None
    start_date: Optional[date] = None
    end_date: Optional[date] = None
    category_id: Optional[int] = None

async def advanced_search(
    pool,
    query: str,
    user_id: int,
    filters: Optional[SearchFilters] = None,
    page: int = 1,
    per_page: int = 20
) -> tuple[List[SearchResult], int]:
    """Advanced search with filters and pagination."""
    
    filters = filters or SearchFilters()
    offset = (page - 1) * per_page
    
    async with pool.acquire() as conn:
        # Build dynamic query with filters
        sql = '''
            WITH search_results AS (
                SELECT 
                    t.id,
                    t.description,
                    t.amount,
                    t.transaction_date,
                    c.name AS category,
                    ts_rank_cd(t.search_vector, query) AS rank,
                    ts_headline(
                        'english', 
                        t.description, 
                        query,
                        'MaxWords=25, MinWords=10, StartSel=**, StopSel=**'
                    ) AS headline
                FROM transactions t
                LEFT JOIN categories c ON t.category_id = c.id,
                     websearch_to_tsquery('english', $1) AS query
                WHERE t.user_id = $2
                  AND t.search_vector @@ query
        '''
        
        params = [query, user_id]
        param_num = 3
        
        if filters.min_amount:
            sql += f' AND ABS(t.amount) >= ${param_num}'
            params.append(filters.min_amount)
            param_num += 1
        
        if filters.max_amount:
            sql += f' AND ABS(t.amount) <= ${param_num}'
            params.append(filters.max_amount)
            param_num += 1
        
        if filters.start_date:
            sql += f' AND t.transaction_date >= ${param_num}'
            params.append(filters.start_date)
            param_num += 1
        
        if filters.end_date:
            sql += f' AND t.transaction_date <= ${param_num}'
            params.append(filters.end_date)
            param_num += 1
        
        if filters.category_id:
            sql += f' AND t.category_id = ${param_num}'
            params.append(filters.category_id)
            param_num += 1
        
        sql += '''
            )
            SELECT 
                *,
                COUNT(*) OVER() AS total_count
            FROM search_results
            ORDER BY rank DESC, transaction_date DESC
            LIMIT $''' + str(param_num) + ' OFFSET $' + str(param_num + 1)
        
        params.extend([per_page, offset])
        
        rows = await conn.fetch(sql, *params)
        
        if not rows:
            return [], 0
        
        total = rows[0]['total_count']
        results = [
            SearchResult(
                id=r['id'],
                description=r['description'],
                amount=float(r['amount']),
                date=r['transaction_date'],
                category=r['category'] or 'Uncategorized',
                rank=float(r['rank']),
                headline=r['headline']
            )
            for r in rows
        ]
        
        return results, total

async def main():
    pool = await asyncpg.create_pool(
        host='localhost', port=5432,
        user='finance_user', password='secure_password',
        database='finance_tracker'
    )
    
    # Search with filters
    filters = SearchFilters(
        min_amount=10.0,
        max_amount=200.0
    )
    
    results, total = await advanced_search(
        pool,
        query='grocery OR food',
        user_id=1,
        filters=filters,
        page=1
    )
    
    print(f"Found {total} results")
    for r in results:
        print(f"  [{r.rank:.3f}] ${r.amount:.2f} - {r.category}")
        print(f"          {r.headline}")
    
    await pool.close()

asyncio.run(main())
```
