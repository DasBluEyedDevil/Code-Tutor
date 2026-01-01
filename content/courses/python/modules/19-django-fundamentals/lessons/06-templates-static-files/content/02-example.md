---
type: "EXAMPLE"
title: "Template Inheritance"
---

Django's inheritance system eliminates HTML duplication across pages:

**Expected Output:**
```
base.html -> defines structure and blocks
list.html -> extends base, fills transaction content
detail.html -> extends base, fills single transaction
```

```python
# Simulating Django template inheritance
from dataclasses import dataclass, field
from typing import Dict, Any, Optional, List

@dataclass
class Block:
    """Represents a {% block %} in a template."""
    name: str
    content: str
    
@dataclass
class Template:
    """Simulates a Django template with inheritance."""
    name: str
    extends: Optional[str] = None
    blocks: Dict[str, str] = field(default_factory=dict)
    raw_content: str = ""
    
    def render(self, context: Dict[str, Any], registry: Dict[str, 'Template']) -> str:
        if self.extends:
            parent = registry.get(self.extends)
            if parent:
                # Merge blocks: child overrides parent
                merged_blocks = {**parent.blocks, **self.blocks}
                result = parent.raw_content
                for name, content in merged_blocks.items():
                    result = result.replace(f"{{{{ block.{name} }}}}", content)
                return result
        return self.raw_content

# Define template registry
templates: Dict[str, Template] = {}

# Base template - the skeleton
templates['base.html'] = Template(
    name='base.html',
    raw_content="""
<!DOCTYPE html>
<html>
<head>
    <title>{{ block.title }} - Finance Tracker</title>
    <link rel="stylesheet" href="/static/css/style.css">
</head>
<body>
    <nav>
        <a href="/">Dashboard</a>
        <a href="/transactions/">Transactions</a>
        <a href="/budgets/">Budgets</a>
    </nav>
    
    <main>
        {{ block.content }}
    </main>
    
    <footer>Finance Tracker 2025</footer>
    <script src="/static/js/app.js"></script>
</body>
</html>
""",
    blocks={
        'title': 'Home',
        'content': '<p>Welcome!</p>'
    }
)

# Child template - transaction list
templates['transactions/list.html'] = Template(
    name='transactions/list.html',
    extends='base.html',
    blocks={
        'title': 'Transactions',
        'content': '''
        <h1>Your Transactions</h1>
        <a href="/transactions/create/" class="btn">Add New</a>
        
        <table>
            <tr><th>Date</th><th>Description</th><th>Amount</th></tr>
            <!-- {% for t in transactions %} -->
            <tr>
                <td>2025-01-15</td>
                <td>Grocery Shopping</td>
                <td>-$85.50</td>
            </tr>
            <!-- {% endfor %} -->
        </table>
        '''
    }
)

# Render the child template
result = templates['transactions/list.html'].render({}, templates)
print("Rendered template (truncated):")
print(result[:500] + "...")

print("\nTemplate inheritance demo:")
print("base.html -> defines structure and blocks")
print("list.html -> extends base, fills transaction content")
print("detail.html -> extends base, fills single transaction")
```
