---
type: "EXAMPLE"
title: "Code Example"
---

**Expected Output:**
```
String '25' → Integer 25
Type: <class 'str'> → <class 'int'>

String '19.99' → Float 19.99

Integer 100 → String '100'
Now we can combine: 'Your score is ' + score_text
Your score is 100

Float 98.6 → Integer 98
⚠️ Notice: The .6 is gone! int() truncates (cuts off) decimals.

IMPLICIT CONVERSION:
5 (int) + 2.5 (float) = 7.5 (type: float)
Python automatically converted 5 to 5.0 to match the float!

PRACTICAL EXAMPLE - Birth Year Calculator:
If you're 28, you were born around 1997

COMMON MISTAKES:
❌ Error: can only concatenate str (not "int") to str
Fix: Use str(25) or f-string
✅ Correct: Age: 25
```

```python
# Type Conversion Examples

# ===== EXPLICIT CONVERSION (You control it) =====

# String to Integer
age_text = "25"                    # This is a string
age_number = int(age_text)         # Convert to integer
print(f"String '{age_text}' → Integer {age_number}")
print(f"Type: {type(age_text)} → {type(age_number)}")
print()

# String to Float
price_text = "19.99"
price_number = float(price_text)   # Convert to float
print(f"String '{price_text}' → Float {price_number}")
print()

# Number to String
score = 100
score_text = str(score)            # Convert to string
print(f"Integer {score} → String '{score_text}'")
print(f"Now we can combine: 'Your score is ' + score_text")
print("Your score is " + score_text)  # This works!
print()

# Float to Integer (CAREFUL: Decimals are lost!)
temperature = 98.6
temp_whole = int(temperature)      # Cuts off .6
print(f"Float {temperature} → Integer {temp_whole}")
print("⚠️ Notice: The .6 is gone! int() truncates (cuts off) decimals.")
print()

# ===== IMPLICIT CONVERSION (Python does it automatically) =====

print("IMPLICIT CONVERSION:")
result1 = 5 + 2.5                  # int + float
print(f"5 (int) + 2.5 (float) = {result1} (type: {type(result1).__name__})")
print("Python automatically converted 5 to 5.0 to match the float!")
print()

# ===== PRACTICAL EXAMPLE =====

print("PRACTICAL EXAMPLE - Birth Year Calculator:")
age_input = "28"  # User typed this (always comes as string)
current_year = 2025

# Must convert age_input to int before math
birth_year = current_year - int(age_input)
print(f"If you're {age_input}, you were born around {birth_year}")
print()

# ===== WHAT DOESN'T WORK =====

print("COMMON MISTAKES:")
try:
    # This will cause an ERROR:
    result = "Age: " + 25  # Can't add string + int
except TypeError as e:
    print(f"❌ Error: {e}")
    print("Fix: Use str(25) or f-string")
    print(f"✅ Correct: Age: {25}")
```
