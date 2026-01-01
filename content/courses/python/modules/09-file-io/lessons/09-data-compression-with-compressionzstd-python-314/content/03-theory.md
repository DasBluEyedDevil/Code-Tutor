---
type: "THEORY"
title: "Syntax Breakdown: compression.zstd API"
---

### Basic Compression:
```python
import compression.zstd as zstd

# Compress bytes
data = b"Hello, World!"
compressed = zstd.compress(data)

# Decompress bytes
original = zstd.decompress(compressed)
```

### Compression Levels:
```python
# Level 1: Fastest, least compression
fast = zstd.compress(data, level=1)

# Level 3: Default, good balance
default = zstd.compress(data)  # level=3

# Level 19: Slow, best compression
best = zstd.compress(data, level=19)
```

### Working with Files:
```python
from pathlib import Path

def save_compressed(path, data):
    """Save data with zstd compression."""
    compressed = zstd.compress(data)
    Path(path).write_bytes(compressed)

def load_compressed(path):
    """Load zstd-compressed data."""
    compressed = Path(path).read_bytes()
    return zstd.decompress(compressed)
```

### Compressing JSON:
```python
import json

def compress_json(data):
    """Compress Python object as JSON."""
    json_bytes = json.dumps(data).encode('utf-8')
    return zstd.compress(json_bytes)

def decompress_json(compressed):
    """Decompress to Python object."""
    json_bytes = zstd.decompress(compressed)
    return json.loads(json_bytes.decode('utf-8'))
```

### Streaming Large Files:
```python
# For very large files, use streaming
with zstd.open('large_file.zst', 'wb') as f:
    for chunk in data_chunks:
        f.write(chunk)

with zstd.open('large_file.zst', 'rb') as f:
    for chunk in f:
        process(chunk)
```

### Fallback for Python < 3.14:
```python
try:
    import compression.zstd as zstd
except ImportError:
    # Use pip package instead
    import zstandard as zstd_lib
    
    class zstd:
        @staticmethod
        def compress(data, level=3):
            return zstd_lib.ZstdCompressor(level=level).compress(data)
        
        @staticmethod
        def decompress(data):
            return zstd_lib.ZstdDecompressor().decompress(data)
```