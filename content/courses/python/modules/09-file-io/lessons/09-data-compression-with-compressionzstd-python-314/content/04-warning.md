---
type: "WARNING"
title: "Common Pitfalls"
---

### Watch Out For These Common Mistakes:

**1. Compressing Already-Compressed Data**
```python
# WRONG - JPEG/PNG/MP3 are already compressed
image_data = Path('photo.jpg').read_bytes()
compressed = zstd.compress(image_data)  # Wastes CPU, no benefit!

# CORRECT - Only compress uncompressed data
text_data = Path('transactions.json').read_bytes()
compressed = zstd.compress(text_data)  # Good compression!
```

**2. Forgetting to Encode Strings**
```python
# WRONG - zstd only works with bytes
data = "Hello, World!"
compressed = zstd.compress(data)  # TypeError!

# CORRECT - Encode to bytes first
data = "Hello, World!"
compressed = zstd.compress(data.encode('utf-8'))
```

**3. Not Handling Decompression Errors**
```python
# WRONG - Crashes on corrupted data
data = zstd.decompress(corrupted_bytes)  # Error!

# CORRECT - Handle decompression errors
try:
    data = zstd.decompress(compressed_bytes)
except Exception as e:
    print(f"Decompression failed: {e}")
    data = None
```

**4. Using Wrong Compression Level**
```python
# WRONG - Level 22 for frequent operations
# Very slow, diminishing returns
for file in many_files:
    zstd.compress(file.read_bytes(), level=22)  # Too slow!

# CORRECT - Use appropriate level
# Level 3 (default) for most cases
# Level 1 for speed-critical operations
# Level 19+ only for archival storage
```

**5. Python Version Mismatch**
```python
# WRONG - Using 3.14 syntax in older Python
import compression.zstd  # ImportError in Python < 3.14!

# CORRECT - Version-aware import
try:
    import compression.zstd as zstd
except ImportError:
    import zstandard
    # Create compatible wrapper
```