---
type: "EXAMPLE"
title: "Complete Project Structure"
---

Here is a complete, production-ready Flutter project structure using feature-first organization. Study it carefully and understand what goes where.

```text
lib/
├── main.dart                    # App entry point
│
├── core/                        # SHARED across ALL features
│   ├── theme/
│   │   ├── app_theme.dart       # Colors, text styles, themes
│   │   └── app_colors.dart      # Color constants
│   │
│   ├── widgets/                 # Reusable widgets
│   │   ├── app_button.dart      # Custom button used everywhere
│   │   ├── loading_indicator.dart
│   │   └── error_widget.dart
│   │
│   ├── utils/                   # Helper functions
│   │   ├── validators.dart      # Email, password validation
│   │   ├── formatters.dart      # Date, currency formatting
│   │   └── extensions.dart      # String, DateTime extensions
│   │
│   └── constants/               # App-wide constants
│       ├── api_constants.dart   # Base URLs, endpoints
│       └── app_constants.dart   # Timeouts, limits
│
├── features/                    # FEATURE MODULES
│   ├── auth/                    # Authentication feature
│   │   ├── models/
│   │   │   └── user.dart        # User data class
│   │   ├── viewmodels/
│   │   │   ├── login_viewmodel.dart
│   │   │   └── register_viewmodel.dart
│   │   ├── views/
│   │   │   ├── login_screen.dart
│   │   │   └── register_screen.dart
│   │   └── widgets/             # Auth-specific widgets
│   │       └── social_login_buttons.dart
│   │
│   ├── home/                    # Home/Dashboard feature
│   │   ├── models/
│   │   │   └── dashboard_data.dart
│   │   ├── viewmodels/
│   │   │   └── home_viewmodel.dart
│   │   ├── views/
│   │   │   └── home_screen.dart
│   │   └── widgets/
│   │       ├── stats_card.dart
│   │       └── recent_activity.dart
│   │
│   ├── products/                # Products feature
│   │   ├── models/
│   │   │   ├── product.dart
│   │   │   └── category.dart
│   │   ├── viewmodels/
│   │   │   ├── products_viewmodel.dart
│   │   │   └── product_detail_viewmodel.dart
│   │   ├── views/
│   │   │   ├── products_screen.dart
│   │   │   └── product_detail_screen.dart
│   │   └── widgets/
│   │       ├── product_card.dart
│   │       └── product_grid.dart
│   │
│   ├── cart/                    # Shopping cart feature
│   │   ├── models/
│   │   │   └── cart_item.dart
│   │   ├── viewmodels/
│   │   │   └── cart_viewmodel.dart
│   │   ├── views/
│   │   │   └── cart_screen.dart
│   │   └── widgets/
│   │       └── cart_item_tile.dart
│   │
│   └── settings/                # Settings feature
│       ├── models/
│       │   └── app_settings.dart
│       ├── viewmodels/
│       │   └── settings_viewmodel.dart
│       └── views/
│           └── settings_screen.dart
│
└── services/                    # BACKEND COMMUNICATION
    ├── api/
    │   ├── api_client.dart      # HTTP client setup
    │   └── api_endpoints.dart   # API endpoint definitions
    ├── auth_service.dart        # Login, logout, token refresh
    ├── product_service.dart     # Product API calls
    └── storage_service.dart     # Local storage (SharedPrefs)
```
