---
type: "THEORY"
title: "Introduction: When You Need External APIs"
---


In the previous lesson, you learned how to connect Flutter to your Serverpod backend using the type-safe generated client. That approach is perfect when you control both ends of the connection. But what happens when you need to communicate with services you do not control?

**Real-World Scenarios Requiring External APIs:**

1. **Payment Processing**: Stripe, PayPal, Square - these payment providers have their own REST APIs. You cannot use Serverpod's client to call Stripe.

2. **Weather Data**: OpenWeatherMap, WeatherAPI - getting current conditions and forecasts requires calling their HTTP endpoints.

3. **Social Media Integration**: Twitter/X API, Instagram Graph API, Facebook SDK - posting, reading feeds, or authenticating users.

4. **Maps and Location**: Google Maps Platform, Mapbox, HERE - geocoding, directions, and place search.

5. **Communication Services**: Twilio for SMS, SendGrid for email, Firebase Cloud Messaging for push notifications.

6. **AI and Machine Learning**: OpenAI API, Google Cloud Vision, AWS Rekognition - these require direct HTTP calls with specific authentication.

7. **Analytics and Monitoring**: Mixpanel, Amplitude, Sentry - tracking events and errors.

**The Key Difference:**

With Serverpod, you had a generated client that handled all the HTTP complexity. With external APIs, you must:

- Construct URLs manually
- Set headers (Authorization, Content-Type, API keys)
- Serialize request bodies to JSON
- Parse JSON responses
- Handle various HTTP status codes
- Manage timeouts and retries
- Deal with rate limiting

This lesson teaches you how to do all of this professionally using Dio, the most powerful HTTP client for Dart.

**Why Not Just Use the http Package?**

Flutter includes a basic `http` package, but Dio offers significant advantages:

| Feature | http package | Dio |
|---------|-------------|-----|
| Interceptors | No | Yes |
| Request cancellation | Limited | Full support |
| File upload progress | No | Yes |
| Timeout configuration | Basic | Granular |
| Automatic JSON parsing | No | Yes |
| Request/response logging | Manual | Built-in |
| Retry logic | Manual | Interceptor-based |
| FormData for multipart | Manual | Built-in |

For production apps calling external APIs, Dio is the industry standard choice.

