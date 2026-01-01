---
type: "WARNING"
title: "Common MVVM Mistakes"
---

Even experienced developers make these mistakes. Learn to recognize and avoid them.

### Mistake 1: Putting Business Logic in the View

**Symptom:** Your widget has `if` statements that decide what to do with data.

```dart
// BAD: Logic in View
onPressed: () {
  if (cart.items.length < 10 && product.inStock && user.isLoggedIn) {
    cart.add(product);
  } else {
    showError('Cannot add');
  }
}

// GOOD: Logic in ViewModel
onPressed: () {
  ref.read(cartViewModelProvider.notifier).addToCart(product);
  // ViewModel handles all the validation
}
```

### Mistake 2: Making ViewModel Aware of UI

**Symptom:** Your ViewModel has methods like `showDialog()` or references `BuildContext`.

The ViewModel should only update state. The View observes state and decides how to present it (dialog, snackbar, new screen, etc.).

### Mistake 3: Skipping the Model Layer

**Symptom:** Your ViewModel uses `Map<String, dynamic>` instead of proper classes.

```dart
// BAD: No Model
class UserViewModel extends Notifier<Map<String, dynamic>> {
  void updateName(String name) {
    state = {...state, 'name': name};  // No type safety!
  }
}

// GOOD: With Model
class UserViewModel extends Notifier<User> {
  void updateName(String name) {
    state = state.copyWith(name: name);  // Type safe!
  }
}
```

### Mistake 4: Giant ViewModel

**Symptom:** One ViewModel handles authentication, cart, user profile, and settings.

Each ViewModel should handle ONE feature. If it grows too large, split it into multiple ViewModels.