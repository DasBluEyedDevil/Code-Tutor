---
type: "EXAMPLE"
title: "Context and Template Tags"
---

**Passing Data to Templates**

Context is a dictionary of data passed to the template for rendering.

**In Views:**
```python
return render(request, 'template.html', {
    'transactions': transactions,
    'user': request.user,
    'total': total_amount,
})
```

**In Templates:**
```html
<p>Hello, {{ user.username }}</p>
<p>Total: ${{ total }}</p>
```

**Built-in Context Processors:**
Django automatically adds:
- `request` - The current request
- `user` - The logged-in user
- `messages` - Flash messages
- `csrf_token` - CSRF token

**Custom Template Tags:**
For complex logic, create custom template tags rather than putting logic in templates.

```html
<!-- templates/transactions/form.html -->
{% extends 'base.html' %}
{% load crispy_forms_tags %}  {# Load custom tags #}

{% block title %}{{ title }} - Finance Tracker{% endblock %}

{% block content %}
<div class="max-w-lg mx-auto bg-white rounded-lg shadow p-6">
    <h1 class="text-2xl font-bold mb-6">{{ title }}</h1>
    
    <form method="post" novalidate>
        {% csrf_token %}  {# Required for POST forms #}
        
        {# Loop through form fields #}
        {% for field in form %}
            <div class="mb-4">
                <label class="block text-gray-700 font-medium mb-2">
                    {{ field.label }}
                    {% if field.field.required %}
                        <span class="text-red-500">*</span>
                    {% endif %}
                </label>
                
                {{ field }}
                
                {% if field.errors %}
                    {% for error in field.errors %}
                        <p class="text-red-500 text-sm mt-1">{{ error }}</p>
                    {% endfor %}
                {% endif %}
                
                {% if field.help_text %}
                    <p class="text-gray-500 text-sm mt-1">{{ field.help_text }}</p>
                {% endif %}
            </div>
        {% endfor %}
        
        {# Non-field errors #}
        {% if form.non_field_errors %}
            <div class="bg-red-100 text-red-700 p-3 rounded mb-4">
                {% for error in form.non_field_errors %}
                    <p>{{ error }}</p>
                {% endfor %}
            </div>
        {% endif %}
        
        <div class="flex gap-4">
            <button type="submit" 
                    class="bg-blue-500 text-white px-6 py-2 rounded">
                {% if object %}Update{% else %}Create{% endif %}
            </button>
            <a href="{% url 'transaction-list' %}" 
               class="text-gray-500 py-2">Cancel</a>
        </div>
    </form>
</div>
{% endblock %}


<!-- templates/partials/transaction_card.html - Reusable partial -->
<div class="bg-white rounded-lg shadow p-4 mb-4">
    <div class="flex justify-between items-start">
        <div>
            <h3 class="font-medium">{{ transaction.description }}</h3>
            <p class="text-sm text-gray-500">
                {{ transaction.category.name }} | 
                {{ transaction.created_at|date:"M d, Y" }}
            </p>
        </div>
        <span class="text-lg font-bold
            {% if transaction.transaction_type == 'income' %}
                text-green-500
            {% else %}
                text-red-500
            {% endif %}">
            {% if transaction.transaction_type == 'income' %}+{% else %}-{% endif %}
            ${{ transaction.amount|floatformat:2 }}
        </span>
    </div>
</div>


<!-- Usage: Include the partial -->
{% for transaction in transactions %}
    {% include 'partials/transaction_card.html' with transaction=transaction %}
{% endfor %}


{# Custom filters and tags example #}

{# In templatetags/finance_extras.py #}
{#
from django import template

register = template.Library()

@register.filter
def currency(value):
    return f"${value:,.2f}"

@register.simple_tag
def transaction_badge(transaction_type):
    colors = {'income': 'green', 'expense': 'red'}
    return f'bg-{colors.get(transaction_type, "gray")}-500'
#}

{# Usage in template: #}
{# {% load finance_extras %} #}
{# {{ amount|currency }} #}
{# <span class="{% transaction_badge type %}">...</span> #}


print("=== Template Tags and Context ===")

print("\nCommon Tags:")
print("  {% for x in list %}...{% endfor %}")
print("  {% if condition %}...{% endif %}")
print("  {% include 'partial.html' %}")
print("  {% url 'view-name' arg %}")
print("  {% csrf_token %}")

print("\nCommon Filters:")
print("  {{ text|title }}")
print("  {{ date|date:'M d, Y' }}")
print("  {{ number|floatformat:2 }}")
print("  {{ text|truncatewords:20 }}")
print("  {{ list|length }}")
```
