---
type: "EXAMPLE"
title: "Template Inheritance and Blocks"
---

**Template Inheritance - DRY for HTML**

Template inheritance lets you create a base template with common structure and override specific blocks in child templates.

**The Pattern:**
1. Create a `base.html` with the common structure
2. Define `{% block %}` placeholders for variable content
3. Child templates `{% extends %}` the base and fill in blocks

**Block Properties:**
- `{% block name %}` defines a replaceable section
- `{{ block.super }}` includes parent's content
- Blocks can be nested
- Undefined blocks keep parent's content

**Best Practices:**
- Keep base templates minimal
- Use semantic block names (title, content, scripts)
- Create intermediate templates for sections (e.g., dashboard_base.html)

```html
<!-- templates/base.html - The foundation -->
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>{% block title %}Finance Tracker{% endblock %}</title>
    <link href="https://cdn.jsdelivr.net/npm/tailwindcss@2/dist/tailwind.min.css" rel="stylesheet">
    {% block extra_css %}{% endblock %}
</head>
<body class="bg-gray-100">
    <!-- Navigation -->
    <nav class="bg-blue-600 text-white p-4">
        <div class="container mx-auto flex justify-between">
            <a href="{% url 'home' %}" class="font-bold">Finance Tracker</a>
            {% block nav %}
            <div>
                {% if user.is_authenticated %}
                    <a href="{% url 'transaction-list' %}">Transactions</a>
                    <a href="{% url 'logout' %}" class="ml-4">Logout</a>
                {% else %}
                    <a href="{% url 'login' %}">Login</a>
                {% endif %}
            </div>
            {% endblock %}
        </div>
    </nav>
    
    <!-- Main Content -->
    <main class="container mx-auto py-8 px-4">
        {% block content %}
        <!-- Child templates fill this in -->
        {% endblock %}
    </main>
    
    <!-- Footer -->
    <footer class="bg-gray-800 text-white p-4 text-center">
        {% block footer %}
        <p>&copy; 2024 Finance Tracker</p>
        {% endblock %}
    </footer>
    
    <!-- Scripts -->
    {% block scripts %}{% endblock %}
</body>
</html>


<!-- templates/transactions/list.html -->
{% extends 'base.html' %}

{% block title %}My Transactions - {{ block.super }}{% endblock %}

{% block content %}
<div class="max-w-4xl mx-auto">
    <div class="flex justify-between items-center mb-6">
        <h1 class="text-2xl font-bold">My Transactions</h1>
        <a href="{% url 'transaction-create' %}" 
           class="bg-blue-500 text-white px-4 py-2 rounded">
            + New Transaction
        </a>
    </div>
    
    {% if transactions %}
        <div class="bg-white rounded-lg shadow">
            {% for transaction in transactions %}
                <div class="p-4 border-b flex justify-between items-center">
                    <div>
                        <span class="font-medium">{{ transaction.description }}</span>
                        <span class="text-gray-500 text-sm ml-2">
                            {{ transaction.category.name }}
                        </span>
                    </div>
                    <div class="flex items-center gap-4">
                        <span class="{% if transaction.amount < 0 %}text-red-500{% else %}text-green-500{% endif %} font-bold">
                            ${{ transaction.amount|floatformat:2 }}
                        </span>
                        <span class="text-gray-400 text-sm">
                            {{ transaction.created_at|date:"M d" }}
                        </span>
                        <a href="{% url 'transaction-detail' transaction.pk %}" 
                           class="text-blue-500">View</a>
                    </div>
                </div>
            {% endfor %}
        </div>
        
        <!-- Pagination -->
        {% if is_paginated %}
            <div class="mt-4 flex justify-center gap-2">
                {% if page_obj.has_previous %}
                    <a href="?page={{ page_obj.previous_page_number }}">&laquo; Prev</a>
                {% endif %}
                <span>Page {{ page_obj.number }} of {{ page_obj.paginator.num_pages }}</span>
                {% if page_obj.has_next %}
                    <a href="?page={{ page_obj.next_page_number }}">Next &raquo;</a>
                {% endif %}
            </div>
        {% endif %}
    {% else %}
        <div class="bg-white rounded-lg shadow p-8 text-center text-gray-500">
            <p>No transactions yet.</p>
            <a href="{% url 'transaction-create' %}" class="text-blue-500">
                Create your first transaction
            </a>
        </div>
    {% endif %}
</div>
{% endblock %}


<!-- templates/transactions/detail.html -->
{% extends 'base.html' %}

{% block title %}{{ transaction.description }} - Finance Tracker{% endblock %}

{% block content %}
<div class="max-w-2xl mx-auto bg-white rounded-lg shadow p-6">
    <h1 class="text-2xl font-bold mb-4">{{ transaction.description }}</h1>
    
    <div class="space-y-3">
        <p><strong>Amount:</strong> 
            <span class="{% if transaction.amount < 0 %}text-red-500{% else %}text-green-500{% endif %}">
                ${{ transaction.amount|floatformat:2 }}
            </span>
        </p>
        <p><strong>Category:</strong> {{ transaction.category.name }}</p>
        <p><strong>Type:</strong> {{ transaction.get_transaction_type_display }}</p>
        <p><strong>Date:</strong> {{ transaction.created_at|date:"F d, Y" }}</p>
    </div>
    
    <div class="mt-6 flex gap-4">
        <a href="{% url 'transaction-update' transaction.pk %}" 
           class="bg-blue-500 text-white px-4 py-2 rounded">Edit</a>
        <a href="{% url 'transaction-delete' transaction.pk %}" 
           class="bg-red-500 text-white px-4 py-2 rounded">Delete</a>
        <a href="{% url 'transaction-list' %}" 
           class="text-gray-500">Back to List</a>
    </div>
</div>
{% endblock %}


print("=== Template Inheritance ===")

print("\nBase Template (base.html):")
print("  - Common HTML structure")
print("  - {% block title %}{% endblock %}")
print("  - {% block content %}{% endblock %}")
print("  - {% block scripts %}{% endblock %}")

print("\nChild Template:")
print("  {% extends 'base.html' %}")
print("  {% block content %}...{% endblock %}")

print("\nInclude Parent Content:")
print("  {{ block.super }}")
```
