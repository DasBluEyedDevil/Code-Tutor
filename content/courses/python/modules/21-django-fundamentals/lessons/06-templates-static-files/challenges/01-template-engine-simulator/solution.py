from dataclasses import dataclass, field
from typing import Dict, Any, List, Optional
import re

@dataclass
class TemplateEngine:
    def __init__(self):
        self.filters = {
            'upper': lambda x: str(x).upper(),
            'lower': lambda x: str(x).lower(),
            'title': lambda x: str(x).title(),
            'length': lambda x: len(x) if hasattr(x, '__len__') else 0,
            'default': lambda x, d='': x if x else d,
        }
    
    def render(self, template: str, context: Dict[str, Any]) -> str:
        result = template
        
        # Process for loops first
        for_pattern = r'\{%\s*for\s+(\w+)\s+in\s+(\w+)\s*%\}(.+?)\{%\s*endfor\s*%\}'
        
        def replace_for(match):
            item_name = match.group(1)
            list_name = match.group(2)
            loop_content = match.group(3)
            items = self._get_value(list_name, context)
            
            if not items:
                return ''
            
            output = []
            for item in items:
                loop_context = {**context, item_name: item}
                rendered = self._render_variables(loop_content, loop_context)
                output.append(rendered)
            return ''.join(output)
        
        result = re.sub(for_pattern, replace_for, result, flags=re.DOTALL)
        
        # Process if statements
        if_pattern = r'\{%\s*if\s+([\w.]+)\s*%\}(.+?)\{%\s*endif\s*%\}'
        
        def replace_if(match):
            condition = match.group(1)
            content = match.group(2)
            value = self._get_value(condition, context)
            return content if value else ''
        
        result = re.sub(if_pattern, replace_if, result, flags=re.DOTALL)
        
        # Process variable interpolation
        result = self._render_variables(result, context)
        
        return result
    
    def _render_variables(self, template: str, context: Dict[str, Any]) -> str:
        var_pattern = r'\{\{\s*([\w.]+)(?:\|(\w+))?\s*\}\}'
        
        def replace_var(match):
            key = match.group(1)
            filter_name = match.group(2)
            value = self._get_value(key, context)
            
            if filter_name and filter_name in self.filters:
                value = self.filters[filter_name](value)
            
            return str(value)
        
        return re.sub(var_pattern, replace_var, template)
    
    def _get_value(self, key: str, context: Dict[str, Any]) -> Any:
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