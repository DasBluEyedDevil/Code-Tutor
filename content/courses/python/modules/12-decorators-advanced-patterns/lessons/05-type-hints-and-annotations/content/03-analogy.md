---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Basic types:**
```python
def func(x: int, y: str, z: bool) -> float:
    pass
```

**Collections:**
```python
# Old style (typing module)
from typing import List, Dict, Set, Tuple
list[str]     # Python 3.9+
List[str]     # Older, still works

dict[str, int]
Dict[str, int]

set[int]
Set[int]

tuple[str, int, bool]  # Fixed size
Tuple[str, int, bool]
```

**Optional and Union:**
```python
from typing import Optional, Union

# Can be None
Optional[str]  # Same as Union[str, None]
str | None     # Python 3.10+

# Multiple types
Union[int, str]
int | str      # Python 3.10+
```

**Any and None:**
```python
from typing import Any

Any          # Any type allowed
None         # Returns nothing
void         # NOT valid in Python, use None
```

**Callable (function types):**
```python
from typing import Callable

# Function that takes (int, str) and returns bool
Callable[[int, str], bool]
```