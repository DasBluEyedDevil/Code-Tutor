---
type: "THEORY"
title: "How Data Flows Through MVVM"
---

Understanding data flow is crucial. In MVVM, data flows in a **unidirectional** (one-way) cycle. This predictability makes debugging easy because you always know where to look.

### The Flow Cycle

**Step 1: User Action**
User taps a button in the View (e.g., "Add to Cart")

**Step 2: View Notifies ViewModel**
The View calls a method on the ViewModel: `viewModel.addToCart(product)`

**Step 3: ViewModel Processes**
The ViewModel:
- Validates the action (Is the product in stock?)
- Updates its internal state
- May call Repository to persist data

**Step 4: State Changes**
The ViewModel's state updates (cart now has 1 item)

**Step 5: View Rebuilds**
The View is watching the ViewModel's state. When state changes, the View automatically rebuilds with new data.

### Why Unidirectional?

In older patterns, data could flow anywhere. The View could modify the Model directly. This created chaos:
- Hard to track where changes came from
- Bugs were difficult to reproduce
- Testing was nearly impossible

Unidirectional flow means: **User Action -> ViewModel -> State Change -> View Update**. Always. No exceptions.

```dart
// DATA FLOW EXAMPLE: Adding item to cart
//
// 1. USER TAPS "Add to Cart" button
//    |
//    v
// 2. VIEW calls ViewModel method
//    ref.read(cartViewModelProvider.notifier).addItem(product);
//    |
//    v
// 3. VIEWMODEL processes the action
//    void addItem(Product product) {
//      // Validate: Is item in stock?
//      if (product.stock > 0) {
//        // Update state
//        state = state.copyWith(
//          items: [...state.items, CartItem(product: product, quantity: 1)],
//        );
//        // Persist to database (via Repository)
//        _repository.saveCart(state);
//      }
//    }
//    |
//    v
// 4. STATE CHANGES
//    CartState { items: [CartItem(product, qty: 1)], total: 29.99 }
//    |
//    v
// 5. VIEW REBUILDS automatically
//    CartIcon now shows badge with "1"
//    Cart screen now lists the item
```
