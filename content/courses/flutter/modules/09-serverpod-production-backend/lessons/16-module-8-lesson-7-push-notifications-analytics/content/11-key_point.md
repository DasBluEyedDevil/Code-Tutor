---
type: "KEY_POINT"
title: "Answer Key"
---


### Answer 1: B
**Correct**: So you can send targeted notifications to specific users

FCM tokens are unique per device. By saving them to Firestore with the user's ID, you can send notifications to specific users (e.g., "John sent you a message"). Without storing tokens, you can only broadcast to all users or topics.

### Answer 2: B
**Correct**: After users see the value of notifications

If you ask for permission immediately, users don't understand why they need it and often decline. Show value first (e.g., let them start a chat), then request permission with context ("Get notified when you receive messages").

### Answer 3: C
**Correct**: Passwords and credit card numbers

NEVER track personally identifiable information (PII) or sensitive data like passwords, credit cards, SSN, health information. This violates privacy laws (GDPR, CCPA) and puts users at risk. Track user behavior, not sensitive data.

