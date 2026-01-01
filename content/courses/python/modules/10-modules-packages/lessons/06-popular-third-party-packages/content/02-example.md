---
type: "EXAMPLE"
title: "Code Example: Popular Packages Demo"
---

**Key packages and what they're best for:**
- **requests**: HTTP requests (APIs, web scraping)
- **pandas**: Data analysis, CSV/Excel manipulation
- **Flask/FastAPI**: Building web apps and APIs
- **beautifulsoup4**: Parsing HTML/XML
- **python-dotenv**: Managing config/secrets

**Installation:** `uv add package-name` (or `pip install package-name`)

```python
# Note: These examples show usage. Install with: uv add <package>

print("=== requests - HTTP Made Easy ===")
print("""
# Without requests (painful!):
import urllib.request
response = urllib.request.urlopen('https://api.github.com')
data = response.read().decode('utf-8')

# With requests (easy!):
import requests
response = requests.get('https://api.github.com')
data = response.json()  # Auto-parses JSON!
""")

print("\n=== pandas - Data Analysis ===")
print("""
import pandas as pd

# Read CSV
df = pd.read_csv('data.csv')

# Quick stats
print(df.describe())

# Filter data
filtered = df[df['age'] > 25]

# Group and aggregate
by_city = df.groupby('city')['salary'].mean()

# Export to Excel
df.to_excel('output.xlsx')
""")

print("\n=== Flask - Web Framework ===")
print("""
from flask import Flask
app = Flask(__name__)

@app.route('/')
def home():
    return 'Hello, World!'

@app.route('/api/users/<int:user_id>')
def get_user(user_id):
    return {'id': user_id, 'name': 'Alice'}

if __name__ == '__main__':
    app.run(debug=True)
""")

print("\n=== beautifulsoup4 - Web Scraping ===")
print("""
from bs4 import BeautifulSoup
import requests

# Fetch webpage
response = requests.get('https://example.com')
soup = BeautifulSoup(response.content, 'html.parser')

# Extract data
title = soup.find('title').text
links = [a['href'] for a in soup.find_all('a')]
headings = soup.find_all('h2')
""")

print("\n=== python-dotenv - Environment Variables ===")
print("""
# .env file:
# DATABASE_URL=postgresql://localhost/mydb
# SECRET_KEY=my-secret-key

from dotenv import load_dotenv
import os

load_dotenv()  # Load from .env file

db_url = os.getenv('DATABASE_URL')
secret = os.getenv('SECRET_KEY')
""")

print("\n=== Package Combinations ===")
print("""
Common project stacks:

Web API:
  - FastAPI (framework)
  - pydantic (data validation)
  - sqlalchemy (database)
  - requests (external APIs)

Data Pipeline:
  - pandas (data processing)
  - sqlalchemy (database)
  - schedule (task scheduling)
  - python-dotenv (config)

Web Scraping:
  - requests (fetch pages)
  - beautifulsoup4 (parse HTML)
  - pandas (organize data)
  - openpyxl (export to Excel)
""")
```
