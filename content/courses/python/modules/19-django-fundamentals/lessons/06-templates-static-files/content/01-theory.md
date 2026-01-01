---
type: "THEORY"
title: "Django Template Language (DTL)"
---

Django's template language bridges Python and HTML, enabling dynamic web pages while keeping logic separate from presentation.

**Template Syntax Basics:**
- `{{ variable }}` - Output a variable
- `{% tag %}` - Template logic (loops, conditions)
- `{# comment #}` - Comments (not rendered)
- `{{ variable|filter }}` - Apply a filter

**Common Tags:**
```django
{% if user.is_authenticated %}
  Welcome, {{ user.username }}!
{% else %}
  <a href="{% url 'login' %}">Login</a>
{% endif %}

{% for transaction in transactions %}
  <li>{{ transaction.description }}: ${{ transaction.amount }}</li>
{% empty %}
  <li>No transactions yet.</li>
{% endfor %}
```

**Built-in Filters:**
- `{{ name|title }}` - Capitalize words
- `{{ amount|floatformat:2 }}` - Format decimal
- `{{ date|date:"M d, Y" }}` - Format date
- `{{ text|truncatewords:20 }}` - Limit words
- `{{ html|safe }}` - Mark as safe HTML
- `{{ list|length }}` - Get list length
- `{{ value|default:"N/A" }}` - Default if empty

**Template Location:**
```python
# settings.py
TEMPLATES = [{
    'DIRS': [BASE_DIR / 'templates'],  # Project-level
    'APP_DIRS': True,  # Look in app/templates/
}]
```