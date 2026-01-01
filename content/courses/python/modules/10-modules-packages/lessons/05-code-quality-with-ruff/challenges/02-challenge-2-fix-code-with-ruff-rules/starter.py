# This code has multiple Ruff violations
# Fix all issues following the rules

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

# TODO: Write the fixed version of this code
fixed_code = '''
# Write your fixed code here
'''

print("Original code with issues:")
print(bad_code)
print("\nYour fixed code:")
print(fixed_code)