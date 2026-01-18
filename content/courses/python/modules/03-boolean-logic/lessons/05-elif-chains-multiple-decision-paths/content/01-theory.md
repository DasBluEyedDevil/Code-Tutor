---
type: "THEORY"
title: "Understanding the Concept"
---

Imagine you're at a train station with a ticket kiosk. You enter your age, and the machine determines your fare:

- **IF** age < 5 → "Free (infant)"
- **ELSE IF** age < 13 → "$5 (child)"
- **ELSE IF** age < 65 → "$10 (adult)"
- **ELSE** → "$7 (senior)"

The machine checks conditions **in order** from top to bottom. When it finds the first match, it stops checking and executes that path. This is an **elif chain**!

### You've Learned:

- **Lesson 3 (if)**: One condition, one action
  ```python
  if temperature > 100:
      print("Boiling")
  ```

- **Lesson 4 (if-else)**: Two paths, exactly one runs
  ```python
  if age >= 18:
      print("Adult")
  else:
      print("Minor")
  ```

### Now: elif - Multiple Mutually Exclusive Paths

What if you need to handle **3, 4, 5, or more** different scenarios?

```python
if score >= 90:
    grade = "A"
elif score >= 80:  # "else if" - only checked if first was False
    grade = "B"
elif score >= 70:  # Only checked if above were False
    grade = "C"
elif score >= 60:  # Only checked if above were False
    grade = "D"
else:              # Catches everything below 60
    grade = "F"
```

Python checks from top to bottom. **First match wins**, and all remaining conditions are skipped!

### Real-World elif Chain Examples:

- **Shipping calculator**:
  - IF weight < 1lb → $5
  - ELIF weight < 5lb → $10
  - ELIF weight < 20lb → $20
  - ELSE → $40

- **Weather response**:
  - IF temp > 90 → "Very hot"
  - ELIF temp > 75 → "Warm"
  - ELIF temp > 60 → "Mild"
  - ELIF temp > 32 → "Cold"
  - ELSE → "Freezing"

- **BMI category**:
  - IF bmi < 18.5 → "Underweight"
  - ELIF bmi < 25 → "Normal"
  - ELIF bmi < 30 → "Overweight"
  - ELSE → "Obese"
