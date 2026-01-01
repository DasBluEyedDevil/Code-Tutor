---
type: "THEORY"
title: "Django Template System Basics"
---

**Templates - The Presentation Layer**

Django templates are HTML files with special syntax for dynamic content.

**Template Location:**
By default, Django looks for templates in:
1. `<app>/templates/<app>/` - App-specific templates
2. `templates/` in project root (configure in settings)

**Basic Syntax:**

**1. Variables:** `{{ variable }}`
```html
<h1>Hello, {{ user.username }}!</h1>
<p>Balance: ${{ account.balance }}</p>
```

**2. Tags:** `{% tag %}`
```html
{% if user.is_authenticated %}
    Welcome back!
{% else %}
    Please log in.
{% endif %}
```

**3. Filters:** `{{ variable|filter }}`
```html
{{ name|title }}           {# Capitalize words #}
{{ date|date:"M d, Y" }}   {# Format date #}
{{ text|truncatewords:30 }} {# Limit words #}
{{ amount|floatformat:2 }}  {# Two decimals #}
```

**4. Comments:** `{# comment #}`
```html
{# This won't appear in the output #}
```

**Common Template Tags:**
- `{% for %}...{% endfor %}` - Loop through items
- `{% if %}...{% elif %}...{% else %}...{% endif %}` - Conditionals
- `{% include 'partial.html' %}` - Include other templates
- `{% url 'view-name' %}` - Generate URLs
- `{% csrf_token %}` - CSRF protection for forms
- `{% static 'path' %}` - Static file URLs