from dataclasses import dataclass, field
from typing import Dict, Any, List, Optional
import re

@dataclass
class TemplateEngine:
    """
    Mini template engine supporting:
    - {{ variable }} - Variable interpolation
    - {{ variable|upper }} - Filters
    - {% if condition %}...{% endif %}
    - {% for item in items %}...{% endfor %}
    """
    
    def __init__(self):
        self.filters = {
            'upper': lambda x: str(x).upper(),
            'lower': lambda x: str(x).lower(),
            'title': lambda x: str(x).title(),
            'length': lambda x: len(x),
            'default': lambda x, d='': x if x else d,
        }
    
    def render(self, template: str, context: Dict[str, Any]) -> str:
        """
        Render a template string with the given context.
        
        1. Process {% for %}...{% endfor %} loops
        2. Process {% if %}...{% endif %} conditionals
        3. Replace {{ variable }} with values
        4. Apply filters like {{ name|upper }}
        """
        result = template
        
        # TODO: Process for loops
        # Pattern: {% for item in items %}...{% endfor %}
        # Replace with repeated content for each item
        
        # TODO: Process if statements
        # Pattern: {% if condition %}...{% endif %}
        # Include content only if condition is truthy
        
        # TODO: Process variable interpolation
        # Pattern: {{ variable }} or {{ variable|filter }}
        # Replace with value from context, apply filter if present
        
        return result
    
    def _get_value(self, key: str, context: Dict[str, Any]) -> Any:
        """Get nested value from context (supports dot notation)."""
        parts = key.split('.')
        value = context
        for part in parts:
            if isinstance(value, dict):
                value = value.get(part, '')
            elif hasattr(value, part):
                value = getattr(value, part)
            else:
                return ''
        return value

# Test the template engine
engine = TemplateEngine()

template = """
<h1>{{ title|upper }}</h1>

{% if show_welcome %}
<p>Welcome, {{ user.name }}!</p>
{% endif %}

<ul>
{% for transaction in transactions %}
  <li>{{ transaction.description }}: ${{ transaction.amount }}</li>
{% endfor %}
</ul>

<p>Total transactions: {{ transactions|length }}</p>
"""

context = {
    'title': 'Finance Dashboard',
    'show_welcome': True,
    'user': {'name': 'Alice'},
    'transactions': [
        {'description': 'Groceries', 'amount': 85.50},
        {'description': 'Gas', 'amount': 45.00},
        {'description': 'Dinner', 'amount': 32.00},
    ]
}

result = engine.render(template, context)
print(result)