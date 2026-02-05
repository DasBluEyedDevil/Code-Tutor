---
type: "ANALOGY"
title: "OAuth2 as Valet Parking"
---

**Understanding OAuth2 Through Valet Service**

**Without OAuth2 (Giving Away Your Keys):**
You want a restaurant to park your car. You give them your house keys, car keys, and garage code. They could enter your house, take your other cars, or copy your keys.

**With OAuth2 (Valet Key):**
You give them a valet key that ONLY starts the car and ONLY for a limited time. They can't open the trunk, can't access the glovebox, and the key expires.

**The OAuth2 Analogy:**

| Valet Parking | OAuth2 |
|---------------|--------|
| You (car owner) | Resource Owner (user) |
| Restaurant (valet service) | Client Application |
| Car | Protected Resources (your data) |
| Valet key | Access Token |
| Key limitations | Scopes |
| Key expiration | Token expiration |

**The OAuth2 Flow:**

```
1. Restaurant asks: "May we park your car?"
   → App requests access to your Google data

2. You go to parking authority: "Give them a valet key"
   → Redirect to Google login

3. Parking authority confirms: "Just parking? No trunk access?"
   → Google shows: "This app wants to read your email"

4. You approve, they get valet key
   → You click "Allow", app gets access token

5. Restaurant parks car with valet key
   → App accesses your data with token

6. Key expires at midnight
   → Token expires after 1 hour
```

**In Code:**

```python
# Step 1: App asks for permission
redirect_url = f"https://google.com/oauth?client_id={CLIENT_ID}&scope=email"

# Step 2-4: User approves, app gets code
@app.get("/callback")
async def callback(code: str):
    # Exchange code for "valet key" (access token)
    token = await exchange_code_for_token(code)
    
# Step 5: Use the valet key
async def get_user_email(token: str):
    async with httpx.AsyncClient() as client:
        response = await client.get(
            "https://api.google.com/user/email",
            headers={"Authorization": f"Bearer {token}"}
        )
```

**The Key Insight:**
OAuth2 lets users grant limited, revocable access to their data without sharing their password. Like a valet key, the access is scoped and time-limited.
