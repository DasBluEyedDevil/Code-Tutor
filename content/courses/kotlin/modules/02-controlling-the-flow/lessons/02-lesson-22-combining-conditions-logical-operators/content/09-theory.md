---
type: "THEORY"
title: "Hands-On Practice"
---


### Exercise 1: Age and Height Restriction

**Challenge:** An amusement park ride requires:
- Age >= 12 AND height >= 48 inches

Write a program that checks if someone can ride.

<details>
<summary>Click to see solution</summary>


**Output:**

**Both conditions must be true:**
- `14 >= 12` → true
- `50 >= 48` → true
- `true && true` → true
</details>

---

### Exercise 2: Weekend or Holiday

**Challenge:** Write a program that prints "Day off!" if it's either:
- Saturday OR Sunday OR a holiday

<details>
<summary>Click to see solution</summary>


**Output:**

**At least one condition is true:**
- `day == "Saturday"` → false
- `day == "Sunday"` → false
- `isHoliday` → true
- `false || false || true` → true
</details>

---

### Exercise 3: Password Validation

**Challenge:** Create a password validator that checks if a password is valid. A valid password must:
- Be at least 8 characters long AND
- NOT be "password123" (too common)

<details>
<summary>Click to see solution</summary>


**Output:**

**Evaluation:**
- `password.length >= 8` → `12 >= 8` → true
- `password != "password123"` → true
- `true && true` → true
</details>

---

### Exercise 4: Temperature Alert System

**Challenge:** Write a program that alerts if temperature is dangerous:
- Below 32°F (freezing) OR above 100°F (heat danger)

<details>
<summary>Click to see solution</summary>


**Output:**

**Evaluation:**
- `28 < 32` → true
- `28 > 100` → false
- `true || false` → true
</details>

---



```kotlin
⚠️ Temperature alert! Take precautions.
```
