---
type: "KEY_POINT"
title: "CSRF Protection"
---

**Cross-Site Request Forgery (CSRF)** is an attack where a malicious site tricks a user's browser into submitting forms to your site.

**How Django Protects You:**
1. Django generates a unique token per session
2. The token must be included in all POST forms
3. Django validates the token on each POST request

**Using CSRF Tokens:**
```django
<form method="post">
    {% csrf_token %}
    {{ form.as_p }}
    <button type="submit">Save</button>
</form>
```

**For AJAX Requests:**
```javascript
// Get token from cookie
const csrftoken = document.cookie
    .split('; ')
    .find(row => row.startsWith('csrftoken='))
    ?.split('=')[1];

fetch('/api/transactions/', {
    method: 'POST',
    headers: {
        'X-CSRFToken': csrftoken,
        'Content-Type': 'application/json',
    },
    body: JSON.stringify(data)
});
```

**When to Exempt CSRF (rare):**
```python
from django.views.decorators.csrf import csrf_exempt

@csrf_exempt  # Only for API endpoints with token auth!
def webhook_handler(request):
    ...
```