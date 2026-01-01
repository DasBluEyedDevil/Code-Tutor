# This code has multiple Ruff violations
# All issues have been fixed according to the rules

bad_code = '''
import os
import json
from typing import List, Optional
import sys

def get_items(data:dict)->List[str]:
    result=[]           
    unused_temp = "temp" 
    for key,value in data.items():
        if value is not None:
            result.append(key)
    return result

def check_empty(items: list) -> bool:
    if len(items) == 0:
        return True
    else:
        return False

def process(x,y):
    total=x+y
    return total
'''

fixed_code = '''
import json  # I001: Sorted alphabetically, removed unused os/sys


def get_items(data: dict) -> list[str]:  # UP006: list instead of List, E225: spacing
    result = []  # E225: spacing around =
    # F841: Removed unused_temp variable
    for key, value in data.items():  # E231: spacing after comma
        if value is not None:
            result.append(key)
    return result


def check_empty(items: list) -> bool:
    return len(items) == 0  # SIM103: Return condition directly


def process(x, y):  # E231: spacing after comma
    total = x + y  # E225: spacing around operators
    return total
'''

print("Original code with issues:")
print(bad_code)

print("\n" + "=" * 60)
print("Fixed code with explanations:")
print("=" * 60)
print(fixed_code)

print("\n" + "=" * 60)
print("Summary of fixes applied:")
print("=" * 60)

fixes = [
    ("F401", "Removed unused imports: os, sys, Optional"),
    ("I001", "Sorted imports alphabetically"),
    ("UP006", "Changed List[str] to list[str] (Python 3.9+ syntax)"),
    ("E225", "Added whitespace around = and other operators"),
    ("E231", "Added whitespace after commas in function parameters"),
    ("F841", "Removed unused variable 'unused_temp'"),
    ("SIM103", "Simplified check_empty to return condition directly"),
]

for code, description in fixes:
    print(f"  {code}: {description}")

print("\n" + "=" * 60)
print("Ruff commands that would catch these:")
print("=" * 60)
print("""
# Check for issues:
ruff check original.py

# Auto-fix most issues:
ruff check --fix original.py

# Format the code:
ruff format original.py
""")