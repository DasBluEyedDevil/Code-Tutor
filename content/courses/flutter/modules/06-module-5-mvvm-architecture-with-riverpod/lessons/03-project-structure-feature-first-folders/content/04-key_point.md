---
type: "KEY_POINT"
title: "Structure Rules"
---

Follow these rules to keep your project organized and maintainable:

### Rule 1: One Feature = One Folder
Each feature gets its own folder under `features/`. A feature is a distinct piece of functionality: authentication, shopping cart, user profile, settings, etc.

### Rule 2: Shared Code Goes in core/
If something is used by multiple features, it belongs in `core/`. This includes:
- Custom widgets used on multiple screens
- Theme and styling constants
- Utility functions and extensions
- Validation logic

### Rule 3: Services Handle External Communication
All API calls, database access, and external service communication goes in `services/`. Features should not import `http` or `dio` directly. They ask services to do the work.

### Rule 4: Keep Features Independent
Features should NOT import from other features directly. If `cart` needs user data from `auth`, both should use a shared service or a provider in `core/`.

**Bad:**
```dart
// In cart/viewmodels/cart_viewmodel.dart
import '../auth/models/user.dart';  // Direct import from another feature!
```

**Good:**
```dart
// In cart/viewmodels/cart_viewmodel.dart
import '../../services/auth_service.dart';  // Uses shared service
```

### Rule 5: Feature Widgets Stay in Feature
Widgets that are ONLY used within a feature stay in that feature's `widgets/` folder. Only truly reusable widgets go in `core/widgets/`.