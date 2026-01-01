---
type: "ANALOGY"
title: "Syntax Breakdown"
---

**Basic import:**
```python
import math
result = math.sqrt(16)  # Must use math.sqrt()
```

**Import specific items:**
```python
from math import sqrt, pi
result = sqrt(16)  # Use directly
```

**Import with alias:**
```python
import datetime as dt
now = dt.datetime.now()  # Use dt instead of datetime
```

**Common modules:**
- **math** - sqrt(), sin(), cos(), pi, e
- **random** - randint(), choice(), shuffle()
- **datetime** - datetime.now(), timedelta
- **time** - time(), sleep()
- **os** - getcwd(), listdir(), mkdir()
- **sys** - argv, exit(), version