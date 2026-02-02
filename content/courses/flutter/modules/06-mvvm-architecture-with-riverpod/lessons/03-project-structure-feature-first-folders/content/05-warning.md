---
type: "WARNING"
title: "Common Structure Mistakes"
---

Avoid these common organizational mistakes:

### Mistake 1: Too Many Nested Folders

**Bad:**
```
features/auth/presentation/screens/login/widgets/forms/inputs/
```
This is 8 levels deep! Finding files becomes a nightmare.

**Good:** Keep it to 3-4 levels maximum:
```
features/auth/views/login_screen.dart
features/auth/widgets/login_form.dart
```

### Mistake 2: Putting Everything in core/

If your `core/widgets/` folder has 50 widgets, something is wrong. Most widgets are feature-specific and should live in their feature folder.

**Ask yourself:** Is this widget used by 2+ features? If not, it belongs in the feature.

### Mistake 3: Cross-Feature Imports

**Bad:**
```dart
// In features/checkout/viewmodels/checkout_viewmodel.dart
import '../../cart/models/cart_item.dart';
import '../../auth/viewmodels/auth_viewmodel.dart';
```

This creates tight coupling. If you change the cart feature, checkout breaks.

**Good:** Use services as intermediaries:
```dart
// In features/checkout/viewmodels/checkout_viewmodel.dart
import '../../services/cart_service.dart';
import '../../services/auth_service.dart';
```

### Mistake 4: No Structure At All

Dumping everything in `lib/` works for tiny apps but fails spectacularly as your app grows. Start with good structure from day one. Reorganizing 100 files later is painful.

### Mistake 5: Inconsistent Naming

Pick a naming convention and stick to it:
- Files: `snake_case.dart` (login_screen.dart, not LoginScreen.dart)
- Classes: `PascalCase` (LoginScreen, not loginScreen)
- Folders: `snake_case` (auth_feature/, not AuthFeature/)