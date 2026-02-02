---
type: "THEORY"
title: "When to Use Use Cases"
---

### Use Cases are Optional

For simple CRUD operations, use cases add boilerplate without value:

```kotlin
// ❌ Unnecessary use case - just wraps repository
class GetNoteByIdUseCase(private val repo: NoteRepository) {
    suspend operator fun invoke(id: String) = repo.getNoteById(id)
}

// ✅ ViewModel can call repository directly for simple operations
class NoteViewModel(private val noteRepository: NoteRepository) {
    suspend fun loadNote(id: String) = noteRepository.getNoteById(id)
}
```

### Use Cases ARE Valuable When

**1. Combining multiple repositories:**
```kotlin
class CreateOrderUseCase(
    private val orderRepo: OrderRepository,
    private val inventoryRepo: InventoryRepository,
    private val notificationRepo: NotificationRepository
) {
    suspend operator fun invoke(order: Order): Result<Order> {
        // Check inventory
        // Create order
        // Send notification
        // Complex orchestration logic
    }
}
```

**2. Complex business rules:**
```kotlin
class CalculateShippingUseCase(
    private val addressRepo: AddressRepository,
    private val ratesApi: ShippingRatesApi
) {
    suspend operator fun invoke(orderId: String): ShippingOptions {
        // Apply discount rules
        // Calculate tax
        // Determine available carriers
        // Business logic that shouldn't be in ViewModel
    }
}
```

**3. Reusable across ViewModels:**
```kotlin
// Used in CartViewModel, CheckoutViewModel, OrderHistoryViewModel
class ValidateCouponUseCase(...)
```