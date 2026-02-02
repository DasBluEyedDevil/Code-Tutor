---
type: "WARNING"
title: "Template Security Considerations"
---

**Auto-Escaping Protects Against XSS:**
```django
{# User input is automatically escaped #}
{{ user_input }}
{# <script>alert('xss')</script> becomes &lt;script&gt;... #}

{# DANGER: |safe disables escaping #}
{{ html_content|safe }}  {# Only use for trusted content! #}
```

**Common Template Mistakes:**

1. **Using |safe on user input:**
   ```django
   {# NEVER do this with user data #}
   {{ comment.body|safe }}  {# XSS vulnerability! #}
   ```

2. **Hardcoding URLs instead of {% url %}:**
   ```django
   {# BAD: Breaks if URL pattern changes #}
   <a href="/transactions/5/">View</a>
   
   {# GOOD: Dynamic URL resolution #}
   <a href="{% url 'transaction_detail' pk=5 %}">View</a>
   ```

3. **Logic in templates:**
   ```django
   {# BAD: Complex logic belongs in views #}
   {% if request.user.profile.subscription.plan == 'premium' and ... %}
   
   {# GOOD: Prepare in view, use simple check #}
   {% if is_premium %}
   ```

4. **Missing {% csrf_token %} in forms:**
   ```django
   <form method="post">
       {% csrf_token %}  {# Required for POST! #}
       ...
   </form>
   ```