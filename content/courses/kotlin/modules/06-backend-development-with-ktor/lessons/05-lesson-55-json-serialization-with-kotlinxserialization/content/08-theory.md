---
type: "THEORY"
title: "🎭 Enums and Sealed Classes"
---


### Enum Serialization


**JSON:**

### Custom Enum Serialization

Sometimes you want custom enum values:


### Polymorphic Serialization (Inheritance)


**JSON with type discrimination:**

---



```json
{
  "notifications": [
    {
      "type": "email",
      "id": 1,
      "timestamp": "2024-11-13T10:00:00",
      "recipient": "alice@example.com",
      "subject": "Welcome"
    },
    {
      "type": "sms",
      "id": 2,
      "timestamp": "2024-11-13T10:05:00",
      "phoneNumber": "+1234567890",
      "message": "Your code is 123456"
    }
  ]
}
```
