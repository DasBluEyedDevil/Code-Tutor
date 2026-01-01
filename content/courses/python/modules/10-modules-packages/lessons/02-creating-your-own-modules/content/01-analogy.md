---
type: "ANALOGY"
title: "The Concept: Your Personal Library"
---

**A module is just a .py file with functions/classes you can import.**

Instead of copying the same functions between projects, create a module once and import it everywhere!

**Example:** You write utility functions for string formatting:
```python
# utils.py
def capitalize_words(text):
    return text.title()

def remove_spaces(text):
    return text.replace(' ', '')
```

Now use it in any project:
```python
# my_app.py
import utils
result = utils.capitalize_words('hello world')
```

**Benefits:**
1. **Reusability** - Write once, use everywhere
2. **Organization** - Keep related code together
3. **Testing** - Test modules independently
4. **Collaboration** - Share code with team