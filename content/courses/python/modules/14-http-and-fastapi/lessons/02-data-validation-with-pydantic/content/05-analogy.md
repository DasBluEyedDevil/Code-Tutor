---
type: "ANALOGY"
title: "Pydantic as a Customs Agent"
---

**Understanding Pydantic Through Border Control**

Imagine your API is a country, and incoming data is travelers at the border.

**Without Pydantic (No Customs):**
Anyone can enter with anything. Smugglers bring malicious data, tourists bring wrong documents, and chaos ensues in your application.

**With Pydantic (Strict Customs Agent):**

```python
class Traveler(BaseModel):
    passport: str       # Must have ID
    age: int           # Must be a number
    purpose: Literal["tourism", "business", "transit"]
```

The customs agent:
1. **Checks documents** - Are all required fields present?
2. **Validates format** - Is the passport a string? Is age a number?
3. **Allows only valid purposes** - Tourism, business, or transit?
4. **Rejects invalid entries** - Clear reason for rejection

**What Pydantic Does:**

| Border Control | Pydantic |
|----------------|----------|
| Passport check | Required field validation |
| Age verification | Type validation |
| Visa type | Enum/Literal validation |
| Rejection notice | ValidationError with details |
| Stamped passport | Validated model instance |

**The Key Insight:**

Pydantic stands at your API's border, ensuring only valid, properly-formatted data enters your application. Bad data gets turned away with a clear explanation.

**Your Job:**
Define the entry requirements (Pydantic models). Pydantic enforces them automatically.
