---
type: "THEORY"
title: "Syntax Breakdown"
---

### Understanding the Tools:
#### 1. Addition (+) and Subtraction (-)
The simplest operators - they work exactly like you learned in elementary school:

```
total = 10 + 5    # Result: 15
difference = 10 - 5  # Result: 5

```
#### 2. Multiplication (*)
**Important:** In Python, we use the asterisk (*), not the × symbol:

```
result = 6 * 7  # Correct
result = 6 × 7  # ERROR! This won't work

```
#### 3. Division (/) vs Floor Division (//)
This is where Python has TWO division tools:

```
# Regular Division (/): Always gives a decimal (float)
10 / 3   # Result: 3.3333333333333335
10 / 2   # Result: 5.0 (still a float, even though it's a whole number!)

# Floor Division (//): Gives only the whole number part
10 // 3  # Result: 3 (drops the .333 part)
10 // 2  # Result: 5 (an integer)

```
**Memory trick:** Regular division (/) gives you the <em>complete answer</em> with decimals. Floor division (//) gives you just the <em>whole pieces</em>.

#### 4. Modulo (%): The Leftover Finder
The modulo operator answers the question: "What's left over after dividing?"

```
10 % 3  # Result: 1 (because 10 ÷ 3 = 3 with 1 left over)
15 % 4  # Result: 3 (because 15 ÷ 4 = 3 with 3 left over)
10 % 5  # Result: 0 (because 10 ÷ 5 = 2 with nothing left over)

```
**Common uses:**

- Check if a number is even: `number % 2 == 0`
- Find remainders in division
- Convert minutes to hours/minutes: `100 % 60 = 40 minutes`

#### 5. Exponentiation (**): Power Up!
This multiplies a number by itself multiple times:

```
2 ** 3  # 2 × 2 × 2 = 8
5 ** 2  # 5 × 5 = 25
10 ** 0 # Always equals 1 (any number to the power of 0 is 1)

```
#### 6. Operator Precedence (Order of Operations)
Python follows **PEMDAS** just like math class:

- **P**arentheses: `()`
- **E**xponents: `**`
- **M**ultiplication/Division: `* / // %` (left to right)
- **A**ddition/Subtraction: `+ -` (left to right)

```
result = 2 + 3 * 4      # Result: 14 (not 20! Multiplication first)
result = (2 + 3) * 4    # Result: 20 (parentheses force addition first)
result = 10 - 2 ** 3    # Result: 2 (exponent first: 10 - 8)

```
**Best practice:** When in doubt, use parentheses to make your intention clear!

#### 7. Mixing Data Types
When you mix integers and floats, Python gives you a float:

```
5 + 2.0    # Result: 7.0 (float)
10 / 2     # Result: 5.0 (division always returns float)
10 // 2    # Result: 5 (floor division returns int when both inputs are ints)

```