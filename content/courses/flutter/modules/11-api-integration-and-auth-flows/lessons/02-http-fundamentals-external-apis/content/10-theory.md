---
type: "THEORY"
title: "Best Practices for External API Integration"
---


**1. API Key Management**

Never hardcode API keys in your source code. They will end up in version control and potentially in your compiled app.

**Safe approaches:**

```dart
// Option 1: Compile-time environment variables (recommended)
// Run with: flutter run --dart-define=API_KEY=your_key_here
const apiKey = String.fromEnvironment('API_KEY');

// Option 2: Load from .env file (using flutter_dotenv package)
await dotenv.load(fileName: '.env');
final apiKey = dotenv.env['API_KEY'];

// Option 3: Fetch from your backend (most secure for client secrets)
final apiKey = await client.settings.getApiKey('weather');
```

**Never do this:**
```dart
// WRONG - Key will be in your git history forever!
const apiKey = 'sk_live_abc123xyz789';
```

**2. Rate Limiting**

Most APIs limit how many requests you can make. Strategies to stay within limits:

- **Cache responses** - Do not fetch the same data repeatedly
- **Debounce user input** - Wait until user stops typing before searching
- **Batch requests** - Combine multiple requests when possible
- **Respect Retry-After headers** - When rate limited, wait as instructed

**3. Caching Considerations**

```dart
// Simple in-memory cache
class CachedWeatherService {
  final WeatherApiService _api;
  final Map<String, _CacheEntry<Weather>> _cache = {};
  final Duration _cacheDuration = const Duration(minutes: 10);

  Future<Weather> getWeather(String city) async {
    final cached = _cache[city.toLowerCase()];
    if (cached != null && !cached.isExpired) {
      return cached.data;
    }

    final weather = await _api.getWeatherByCity(city);
    _cache[city.toLowerCase()] = _CacheEntry(weather, _cacheDuration);
    return weather;
  }
}

class _CacheEntry<T> {
  final T data;
  final DateTime expiresAt;

  _CacheEntry(this.data, Duration duration)
      : expiresAt = DateTime.now().add(duration);

  bool get isExpired => DateTime.now().isAfter(expiresAt);
}
```

**4. When to Use Serverpod Client vs Dio**

| Use Serverpod Client | Use Dio |
|---------------------|--------|
| Your own Serverpod backend | Third-party REST APIs |
| Type-safe method calls | External services (Stripe, etc.) |
| Shared model classes | APIs without Dart SDKs |
| Automatic serialization | When you need interceptors |
| WebSocket support | File downloads with progress |

**5. Combining Both in Your App**

Many production apps use both:

```dart
class PaymentService {
  final Client _serverpodClient; // Your backend
  final Dio _stripeClient;        // Stripe API

  // Create payment intent via YOUR backend (which calls Stripe securely)
  Future<PaymentIntent> createPaymentIntent(double amount) async {
    return await _serverpodClient.payments.createIntent(amount);
  }

  // But confirm payment client-side with Stripe SDK
  Future<void> confirmPayment(String clientSecret) async {
    // Use Stripe's Flutter SDK, which uses HTTP internally
  }
}
```

The pattern is: **sensitive operations through your backend**, non-sensitive reads directly from external APIs.

