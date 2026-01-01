from django.http import JsonResponse
from django.db.models import Sum
from asgiref.sync import sync_to_async
from .models import Transaction


# ASYNC VERSION
async def transaction_summary_async(request):
    """Get user's transaction summary - ASYNC version."""
    user = request.user
    
    # Get recent transactions using async iteration
    recent = []
    async for tx in Transaction.objects.filter(
        user=user
    ).order_by('-created_at')[:10]:
        recent.append({
            'id': tx.id,
            'amount': str(tx.amount),
            'description': tx.description,
        })
    
    # Get count using async method
    total_count = await Transaction.objects.filter(
        user=user
    ).acount()
    
    # Wrap aggregate in sync_to_async
    @sync_to_async
    def get_total_amount():
        result = Transaction.objects.filter(
            user=user
        ).aggregate(total=Sum('amount'))
        return result['total'] or 0
    
    total = await get_total_amount()
    
    return JsonResponse({
        'recent': recent,
        'total_count': total_count,
        'total_amount': str(total),
    })


# Test output
print("=== Async Transaction Summary ===")

print("\nChanges Made:")
print("  1. def -> async def")
print("  2. for loop -> async for loop")
print("  3. .count() -> await .acount()")
print("  4. .aggregate() -> @sync_to_async wrapper")

print("\nAsync ORM Methods Used:")
print("  - async for tx in queryset (async iteration)")
print("  - await queryset.acount()")
print("  - await sync_to_async(aggregate_func)()")

print("\nKey Points:")
print("  - Filter chains remain sync (just build query)")
print("  - Terminal methods (iteration, count) are async")
print("  - Complex aggregates need sync_to_async")