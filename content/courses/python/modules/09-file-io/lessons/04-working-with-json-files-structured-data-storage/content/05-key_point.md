---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **JSON = JavaScript Object Notation** - Universal format for data exchange. Every programming language and web API uses it.
- **import json** - Python's built-in module for working with JSON. No installation needed.
- **dump/dumps (Serialize):** Python → JSON. dumps = string, dump = file. Remember 's' for string!
- **load/loads (Deserialize):** JSON → Python. loads = string, load = file. Remember 's' for string!
- **Always use indent=2** when writing JSON files: json.dump(data, file, indent=2). Makes JSON human-readable.
- **Type conversions:** dict↔object, list↔array, str↔string, int/float↔number, True/False↔true/false, None↔null.
- **Handle errors:** FileNotFoundError (file missing) and json.JSONDecodeError (invalid JSON). Always use try/except.
- **JSON limitations:** Can only encode dict, list, str, int, float, bool, None. Cannot encode sets, functions, or custom objects.
- **JSON keys must be strings!** {'name': 'Alice'} works, {123: 'Alice'} will have key converted to string '123'.
- **Common use:** Save app state, config files, API communication, data exchange between programs.