---
type: "KEY_POINT"
title: "When to Use Hooks"
---

### Use Hooks For:

**1. Local UI State**
- Form controllers and focus nodes
- Animation controllers
- Scroll/page/tab controllers
- Local boolean flags (isExpanded, isEditing, etc.)

**2. Auto-Disposing Resources**
- Anything that needs dispose() called
- Controllers, subscriptions, listeners
- Hooks handle cleanup automatically

**3. Side Effects Tied to Widget Lifecycle**
- Fetching data when widget mounts
- Setting up listeners
- Logging or analytics

### Do NOT Use Hooks For:

**1. Shared State**
If multiple widgets need the same data, use Riverpod:
```dart
// BAD: Each widget gets its own copy
final cartItems = useState<List<Item>>([]);

// GOOD: All widgets share the same state
final cartItems = ref.watch(cartProvider);
```

**2. Data That Survives Navigation**
Hooks die when the widget is disposed:
```dart
// BAD: Lost when navigating away
final searchQuery = useState('');

// GOOD: Persists during navigation
final searchQuery = ref.watch(searchQueryProvider);
```

**3. Complex Business Logic**
Keep business logic in Riverpod providers where it can be tested:
```dart
// BAD: Hard to test, mixed concerns
useEffect(() {
  calculateTax();
  applyDiscount();
  updateInventory();
}, [cart]);

// GOOD: Testable, separated concerns
ref.watch(orderTotalProvider);  // Handles all calculation logic
```

### The Golden Rule

**Hooks = Local to one widget, needs cleanup**

**Riverpod = Shared between widgets, or complex logic**