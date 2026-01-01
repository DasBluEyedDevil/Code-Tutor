---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **Python 3.14+ includes `compression.zstd`** as a built-in module
- **For older Python:** `pip install zstandard`
- **zstd.compress(bytes)** - Compress binary data
- **zstd.decompress(bytes)** - Decompress back to original
- **Compression levels 1-22:** Higher = smaller but slower
- **Default level (3)** provides good balance of speed and size
- **Encode strings first:** `text.encode('utf-8')` before compressing
- **Great for:** JSON, CSV, text logs, transaction data
- **Not useful for:** Already-compressed files (images, videos, archives)
- **Typical ratios:** 3-10x smaller for text/JSON data

### Quick Reference:
```python
import compression.zstd as zstd

# Compress
compressed = zstd.compress(b"data")

# Decompress
original = zstd.decompress(compressed)

# With compression level
compressed = zstd.compress(data, level=6)

# With JSON
import json
json_bytes = json.dumps(data).encode('utf-8')
compressed = zstd.compress(json_bytes)
```