---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: B) If database is compromised, all passwords are exposed**

Storing plaintext passwords = catastrophic breach:
- Attackers get all passwords
- Users reuse passwords across sites
- One breach = compromise everywhere

Always hash passwords with bcrypt!

---

**Question 2: B) Creates performance issues that can be exploited for DoS**

N+1 queries = performance vulnerability:
- Attacker requests large dataset
- Triggers thousands of queries
- Server becomes unresponsive (DoS)

Solution: Use JOINs and optimize queries

---

**Question 3: B) Prevents man-in-the-middle attacks**

Certificate pinning ensures:
- App only trusts specific certificates
- Can't be fooled by fake certificates
- Prevents attackers intercepting traffic

---

**Question 4: B) Fail securely (deny access)**

When in doubt, deny:
- Error in authentication? Deny
- Exception in authorization? Deny
- Can't verify request? Deny

Never fail open!

---

**Question 5: B) bcrypt includes salt and is designed to be slow**

bcrypt advantages:
- Automatically salts (unique hash per password)
- Configurable cost (slower = harder to crack)
- Designed for passwords (SHA-256 is not)

---

