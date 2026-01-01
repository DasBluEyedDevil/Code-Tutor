# Data Analysis Project Requirements
# This solution shows popular packages for each category

requirements = """# Data Analysis Project Dependencies
# Install with: pip install -r requirements.txt

# ============ Data Reading/Writing ============
# Pandas - The go-to library for data analysis
pandas>=2.0.0
# OpenPyXL - Read/write Excel files
openpyxl>=3.1.0

# ============ HTTP Requests ============
# Requests - Simple HTTP library
requests>=2.28.0

# ============ Data Visualization ============
# Matplotlib - Comprehensive plotting library
matplotlib>=3.7.0
# Seaborn - Statistical data visualization
seaborn>=0.12.0

# ============ Testing ============
# Pytest - Modern testing framework
pytest>=7.3.0
# Pytest-cov - Coverage reporting
pytest-cov>=4.0.0

# ============ Optional but Recommended ============
# NumPy - Numerical computing
numpy>=1.24.0
# Jupyter - Interactive notebooks
jupyterlab>=4.0.0
"""

from pathlib import Path
Path('requirements.txt').write_text(requirements.strip())

print("Created requirements.txt for Data Analysis project")
print("\n" + "=" * 50)
print(requirements)

print("\nPackage Summary:")
print("  - pandas, openpyxl: Data reading/writing")
print("  - requests: HTTP requests")
print("  - matplotlib, seaborn: Visualization")
print("  - pytest, pytest-cov: Testing")