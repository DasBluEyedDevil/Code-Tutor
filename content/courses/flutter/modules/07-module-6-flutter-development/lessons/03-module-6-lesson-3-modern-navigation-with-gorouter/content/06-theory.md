---
type: "THEORY"
title: "Query Parameters"
---


Handle URLs like `/search?q=flutter&sort=newest`:




```dart
GoRoute(
  path: '/search',
  builder: (context, state) {
    final query = state.uri.queryParameters['q'] ?? '';
    final sort = state.uri.queryParameters['sort'] ?? 'relevance';
    return SearchScreen(query: query, sort: sort);
  },
),

// Navigate
context.go('/search?q=flutter&sort=newest');
```
