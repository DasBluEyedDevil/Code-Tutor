---
type: "THEORY"
title: "Exercise 1: Secure User Registration"
---


Build a secure user registration system.

### Requirements

1. **Password Requirements**:
   - Minimum 12 characters
   - Uppercase, lowercase, number, special char
   - Not in common password list

2. **Email Validation**:
   - Valid format
   - Domain verification (MX record check)
   - Unique in database

3. **Security Features**:
   - Hash passwords with bcrypt (cost 12)
   - Email verification required
   - Rate limiting (5 attempts per hour per IP)
   - CAPTCHA on repeated failures

---

