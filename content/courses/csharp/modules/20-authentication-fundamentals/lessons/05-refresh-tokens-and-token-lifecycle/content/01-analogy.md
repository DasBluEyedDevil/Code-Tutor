---
type: "ANALOGY"
title: "Access Token + Refresh Token Pattern"
---

Imagine checking into a hotel for a week-long stay. At the front desk, the receptionist verifies your ID and credit card (authentication), then gives you two things: a room key card and a registration receipt.

The room key card (Access Token) grants you immediate access to your room and hotel facilities. However, the hotel programs it to expire at midnight each day. This short expiration means if someone steals your key card, they can only use it until midnight - limiting the damage.

The registration receipt (Refresh Token) proves you are a registered guest. Each morning, you take the receipt to the front desk and they issue you a new key card for that day. The receipt itself has a longer validity - the duration of your stay. If you lose your receipt, you must re-verify your identity with the original ID and credit card.

This two-token system provides the best of both worlds:

**Short-lived access token benefits:**
- If stolen, limited time for misuse
- Does not require database lookup on every request (stateless validation)
- Contains claims needed for authorization decisions

**Long-lived refresh token benefits:**
- User does not need to re-enter password constantly
- Can be revoked immediately (requires database lookup only when refreshing)
- Can detect suspicious activity (same refresh token used from different locations)

The hotel analogy extends further. When you get a new key card each morning, the hotel also gives you a new registration receipt. This is called token rotation - even the refresh token changes on each use. Why? If someone photographs your receipt, they can only use it once. The moment you use the real receipt, their copy becomes invalid.

If the hotel detects the same receipt being used twice (you refreshing your key, then the attacker trying), they know something is wrong and can revoke all your tokens, forcing you to re-authenticate. This is refresh token reuse detection - a critical security feature.