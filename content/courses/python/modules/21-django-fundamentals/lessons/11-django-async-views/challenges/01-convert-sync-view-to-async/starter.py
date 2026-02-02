from django.http import JsonResponse
from django.db.models import Sum
from .models import Transaction  # Assume this model exists

# SYNC VERSION - Convert this to async
def transaction_summary(request):
    """Get user's transaction summary - SYNC version."""
    user = request.user
    
    # Get recent transactions
    recent = []
    for tx in Transaction.objects.filter(
        user=user
    ).order_by('-created_at')[:10]:
        recent.append({
            'id': tx.id,
            'amount': str(tx.amount),
            'description': tx.description,
        })
    
    # Get count
    total_count = Transaction.objects.filter(user=user).count()
    
    # Get total amount (aggregate)
    total = Transaction.objects.filter(
        user=user
    ).aggregate(total=Sum('amount'))['total'] or 0
    
    return JsonResponse({
        'recent': recent,
        'total_count': total_count,
        'total_amount': str(total),
    })


# TODO: Create async version below
# async def transaction_summary_async(request):
#     ...