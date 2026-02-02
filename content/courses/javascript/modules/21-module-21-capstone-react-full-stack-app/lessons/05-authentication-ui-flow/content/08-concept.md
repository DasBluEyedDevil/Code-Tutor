---
type: "THEORY"
title: "Challenge: Add Password Strength Indicator"
---

## Challenge: Visual Password Strength Feedback

Enhance the RegisterForm component to:
1. **Display a strength meter** showing password strength as user types
2. **Color-code the meter**: Red (weak) → Yellow (fair) → Green (strong)
3. **Show strength requirements**:
   - At least 8 characters
   - Contains both lowercase and uppercase letters
   - Contains at least one number
   - Contains at least one special character (!@#$%^&*)

**Requirements:**
- Create a separate PasswordStrengthMeter component
- Use React state to track which requirements are met
- Update the meter in real-time as user types
- Disable submit button until password meets all requirements
- Show a helpful message like "Strong password!"

**Bonus Challenge:**
- Add a "Show password" toggle button
- Generate a password suggestion using a library
- Store password preferences in context (optional requirements)