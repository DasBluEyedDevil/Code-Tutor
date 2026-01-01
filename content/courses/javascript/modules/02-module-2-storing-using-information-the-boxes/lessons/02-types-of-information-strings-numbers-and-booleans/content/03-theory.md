---
type: "THEORY"
title: "Breaking Down the Syntax"
---

The three basic data types:

1. STRING - Text
   - Always enclosed in quotes (single ' or double ")
   - Can be empty: let empty = '';
   - Can contain numbers as text: let code = '12345';
   - Use + to join strings: 'Hello' + ' ' + 'World' = 'Hello World'

2. NUMBER - Numeric values
   - NO quotes
   - Can be positive, negative, or decimal
   - Can do math: +, -, *, / (division), % (remainder)
   - Special values: Infinity, -Infinity, NaN (Not a Number)

3. BOOLEAN - True or False
   - Only two possible values: true or false
   - NO quotes (quotes would make it a string)
   - Used for yes/no, on/off, exists/doesn't exist
   - We'll use these a lot when making decisions (if statements)

How to remember:
- If it's text, it needs quotes → String
- If it's a number for math, no quotes → Number
- If it's true or false, no quotes → Boolean

The + operator:
- With numbers: 5 + 3 = 8 (addition)
- With strings: 'Hello' + 'World' = 'HelloWorld' (joining)
- Mixed: 'Age: ' + 25 = 'Age: 25' (converts number to string and joins)