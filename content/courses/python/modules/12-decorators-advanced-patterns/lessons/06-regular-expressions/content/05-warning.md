---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Forgetting Raw Strings for Patterns**
```python
# WRONG - Backslash gets interpreted as escape
pattern = '\d+'  # Actually matches 'd+'
re.search(pattern, '123')  # None!

# CORRECT - Use raw string
pattern = r'\d+'  # Matches digits
re.search(pattern, '123')  # Match!
```

**2. Confusing match() vs search()**
```python
# WRONG - match() only checks start of string
text = 'hello 123 world'
re.match(r'\d+', text)  # None! Doesn't start with digit

# CORRECT - search() finds anywhere
re.search(r'\d+', text)  # Match! Finds '123'

# CORRECT - match() with ^ anchor equivalent
re.search(r'^\d+', text)  # None (same as match)
```

**3. Greedy vs Non-Greedy Matching**
```python
# WRONG - Greedy .* matches too much
html = '<b>bold</b> and <b>more</b>'
re.search(r'<b>.*</b>', html).group()  # '<b>bold</b> and <b>more</b>'

# CORRECT - Non-greedy .*? matches minimum
re.search(r'<b>.*?</b>', html).group()  # '<b>bold</b>'
```

**4. Not Checking for None Before Using Match**
```python
# WRONG - AttributeError if no match
result = re.search(r'\d+', 'no numbers').group()  # Error!

# CORRECT - Check for match first
match = re.search(r'\d+', 'no numbers')
if match:
    result = match.group()
else:
    result = None
```

**5. Forgetting to Escape Special Characters**
```python
# WRONG - . and $ are special regex chars
re.search(r'file.txt', 'file_txt')  # Match! . matches any char
re.search(r'$100', 'costs $100')  # None! $ means end of string

# CORRECT - Escape special characters
re.search(r'file\.txt', 'file_txt')  # None (correct)
re.search(r'\$100', 'costs $100')  # Match!
```