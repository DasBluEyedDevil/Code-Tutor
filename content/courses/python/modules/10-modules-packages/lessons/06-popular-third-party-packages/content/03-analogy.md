---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Finding packages:**
```bash
# Search PyPI
pip search keyword  # (disabled, use pypi.org instead)

# Browse at https://pypi.org
# Check GitHub for popular projects
```

**Installing:**
```bash
# Basic install
pip install requests

# Specific version
pip install requests==2.31.0

# Upgrade
pip install --upgrade requests

# With extras
pip install fastapi[all]  # Installs optional dependencies
```

**Common package patterns:**
```python
# requests - HTTP
import requests
response = requests.get(url)
data = response.json()

# pandas - Data
import pandas as pd
df = pd.read_csv('file.csv')

# flask - Web
from flask import Flask
app = Flask(__name__)

# beautifulsoup - Parsing
from bs4 import BeautifulSoup
soup = BeautifulSoup(html, 'html.parser')
```