---
type: "ANALOGY"
title: "The Concept: Code is Read More Than Written"
---

**Documentation = Instructions for humans**

**Think of it like:**
- Recipe for cooking
- User manual for appliances
- Assembly instructions for furniture

**Why documentation matters:**

1. **Future you** 🔮
   - You'll forget why you wrote code
   - 6 months = forever
   - Save yourself debugging time

2. **Other developers** 👥
   - Team members need to understand
   - Open source contributors
   - Code reviews

3. **Users** 👤
   - How to install
   - How to use
   - Troubleshooting

**Types of documentation:**

**1. Code comments** 💬
```python
# Explain WHY, not WHAT
# Good: Cache result to avoid expensive API call
# Bad: This stores the result
```

**2. Docstrings** 📝
```python
def calculate_total(items, tax_rate):
    """Calculate total price including tax.
    
    Args:
        items: List of item prices
        tax_rate: Tax rate as decimal (0.1 = 10%)
    
    Returns:
        Total price with tax applied
    """
```

**3. README.md** 📄
- What the project does
- How to install
- How to use
- Examples
- Contributing guide

**4. API documentation** 🔗
- Endpoint descriptions
- Request/response examples
- Authentication details

**Code quality = Readable, maintainable code**

**PEP 8 (Python Style Guide):**
- 4 spaces for indentation
- Max 79 characters per line
- 2 blank lines between functions
- snake_case for variables
- PascalCase for classes

**Tools:**
- **Black** - Auto-formatter
- **flake8** - Style checker
- **pylint** - Code analyzer
- **mypy** - Type checker