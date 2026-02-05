---
type: "ANALOGY"
title: "JWT as Concert Wristband"
---

**Understanding JWT Through Wristbands**

**Session-Based Auth = Checking IDs at Every Bar**
At a festival, you show your ID at every drink stand. The bartender checks your age in a database (server-side). Works, but slow and requires database access.

**JWT Auth = Concert Wristband**
At entry, they verify your ID once and give you a colored wristband. Inside, staff just glance at your wrist. No need to re-verify your identity every time.

**The JWT Wristband:**

```
┌─────────────────────────────────────────┐
│  🎫 JWT WRISTBAND                       │
├─────────────────────────────────────────┤
│  Color (Header): VIP/General/Staff      │
│  ────────────────────────────────       │
│  Info (Payload): Name, Section, Expiry  │
│  ────────────────────────────────       │
│  Hologram (Signature): Can't be faked   │
└─────────────────────────────────────────┘
```

**How It Works:**

| Festival | JWT |
|----------|-----|
| Entry gate | Login endpoint |
| ID verification | Username/password check |
| Wristband given | JWT returned |
| Wristband color | Token claims |
| Hologram stamp | Cryptographic signature |
| Checking wristband | Verifying token |
| Wristband expires midnight | Token expiration (exp) |

**Why JWTs Work:**

1. **Self-Contained**: Wristband has all info needed (no database check)
2. **Tamper-Evident**: Hologram proves authenticity (signature)
3. **Stateless**: Staff don't remember you; they trust the wristband
4. **Expiring**: Wristband only valid for today (exp claim)

**In Code:**

```python
# Entry gate: Get your wristband
@app.post("/login")
async def login(credentials: Credentials):
    user = verify_user(credentials)  # Check ID
    token = create_access_token({"sub": user.id})  # Give wristband
    return {"access_token": token}

# Inside festival: Show wristband
@app.get("/vip-area")
async def vip_area(user = Depends(get_current_user)):  # Check wristband
    return {"message": f"Welcome to VIP, {user.name}!"}
```

**The Key Insight:**
JWT lets you verify identity without checking a database every time. The signature (hologram) proves the token is authentic and unmodified.
