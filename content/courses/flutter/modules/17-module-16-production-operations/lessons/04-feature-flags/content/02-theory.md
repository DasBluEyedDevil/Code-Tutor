---
type: "THEORY"
title: "Firebase Remote Config Overview"
---


**What is Firebase Remote Config?**

Firebase Remote Config is a cloud service that lets you change the behavior and appearance of your app without requiring users to download an update. It's perfect for feature flags.

**How It Works**

1. **Define Defaults** - Set in-app default values
2. **Create Parameters** - Add parameters in Firebase Console
3. **Set Conditions** - Target specific users or percentages
4. **Publish Changes** - Make live without app update
5. **Fetch in App** - App downloads new values
6. **Activate** - Apply the new values

**Key Concepts**

- **Parameters** - Key-value pairs you can change remotely
- **Conditions** - Rules that determine which users get which values
- **Default Values** - What to use if fetch fails or parameters aren't set
- **Fetch** - Download latest values from server
- **Activate** - Apply fetched values to the app

**Parameter Types**

```dart
// Remote Config supports these types:
remoteConfig.getBool('feature_enabled');      // true/false
remoteConfig.getString('welcome_message');    // Text
remoteConfig.getInt('max_items');             // Whole numbers
remoteConfig.getDouble('discount_rate');      // Decimal numbers
remoteConfig.getValue('complex_data');        // JSON as string
```

**Fetch Strategy**

Remote Config caches values to minimize network calls:

- Default cache expiry: 12 hours
- Minimum fetch interval: 1 hour (production)
- Development mode: Can fetch frequently for testing

**Conditions for Targeting**

Firebase Console lets you target users by:

- App version
- Platform (iOS, Android)
- Country/region
- User property (from Analytics)
- Random percentile (for gradual rollout)
- Date/time
- User in audience

