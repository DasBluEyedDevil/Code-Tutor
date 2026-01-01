---
type: "EXAMPLE"
title: "Step 2: Package Templates Module"
---

**Template modules** define project blueprints:
- Each template is a separate module
- Exports: requirements, folder structure, starter code
- `get_template()` function returns complete configuration
- Demonstrates module organization and reusability

```python
# project_initializer/templates/web_template.py
"""Templates for web development projects."""

REQUIREMENTS = """
# Web Development Dependencies
flask>=3.0.0
requests>=2.31.0
python-dotenv>=1.0.0
pytest>=7.4.0
""".strip()

STRUCTURE = {
    'app': {
        'routes': {},
        'models': {},
        'static': {
            'css': {},
            'js': {}
        },
        'templates': {}
    },
    'tests': {}
}

MAIN_CODE = '''
from flask import Flask, jsonify
from dotenv import load_dotenv
import os

load_dotenv()

app = Flask(__name__)
app.config['SECRET_KEY'] = os.getenv('SECRET_KEY', 'dev-key-change-this')

@app.route('/')
def home():
    return jsonify({
        'message': 'Welcome to your Flask API!',
        'status': 'running'
    })

@app.route('/api/health')
def health():
    return jsonify({'status': 'healthy'})

if __name__ == '__main__':
    app.run(debug=True, port=5000)
'''

ENV_TEMPLATE = """
# Environment Variables
SECRET_KEY=your-secret-key-here
DATABASE_URL=sqlite:///app.db
DEBUG=True
""".strip()

def get_template():
    """Return complete web project template."""
    return {
        'requirements': REQUIREMENTS,
        'structure': STRUCTURE,
        'main_code': MAIN_CODE,
        'env': ENV_TEMPLATE,
        'description': 'Flask web application with API endpoints'
    }

# project_initializer/templates/data_template.py
"""Templates for data science projects."""

REQUIREMENTS = """
# Data Science Dependencies
pandas>=2.0.0
numpy>=1.24.0
matplotlib>=3.7.0
seaborn>=0.12.0
jupyter>=1.0.0
pytest>=7.4.0
""".strip()

STRUCTURE = {
    'data': {
        'raw': {},
        'processed': {}
    },
    'notebooks': {},
    'src': {
        'analysis': {},
        'visualization': {}
    },
    'tests': {}
}

MAIN_CODE = '''
import pandas as pd
import matplotlib.pyplot as plt
from pathlib import Path

def load_data(filepath):
    """Load data from CSV file."""
    return pd.read_csv(filepath)

def analyze_data(df):
    """Perform basic data analysis."""
    print("Dataset Info:")
    print(f"Rows: {len(df)}")
    print(f"Columns: {len(df.columns)}")
    print("\nSummary Statistics:")
    print(df.describe())
    return df.describe()

def visualize_data(df, column):
    """Create visualization for a column."""
    plt.figure(figsize=(10, 6))
    df[column].hist(bins=30)
    plt.title(f'Distribution of {column}')
    plt.xlabel(column)
    plt.ylabel('Frequency')
    plt.savefig(f'output_{column}.png')
    print(f"✓ Saved visualization: output_{column}.png")

if __name__ == '__main__':
    print("Data Analysis Pipeline Ready!")
    print("Place your CSV files in the data/raw/ directory")
'''

def get_template():
    """Return complete data science project template."""
    return {
        'requirements': REQUIREMENTS,
        'structure': STRUCTURE,
        'main_code': MAIN_CODE,
        'description': 'Data science project with pandas and visualization'
    }
```
