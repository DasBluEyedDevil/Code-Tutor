---
type: "EXAMPLE"
title: "Code Example: Compressing Finance Data"
---

**Key operations:**
1. `zstd.compress(data)` - Compress bytes
2. `zstd.decompress(data)` - Decompress bytes
3. Compression levels (1-22): Higher = smaller but slower
4. Works with any binary data

```python
# Python 3.14+ native zstd
# For Python < 3.14, use: pip install zstandard

try:
    # Python 3.14+
    import compression.zstd as zstd
except ImportError:
    # Fallback for older Python
    import zstandard
    class zstd:
        @staticmethod
        def compress(data, level=3):
            cctx = zstandard.ZstdCompressor(level=level)
            return cctx.compress(data)
        
        @staticmethod
        def decompress(data):
            dctx = zstandard.ZstdDecompressor()
            return dctx.decompress(data)

import json
from pathlib import Path
from datetime import datetime

def compress_transactions(transactions: list) -> bytes:
    """Compress transaction data."""
    # Convert to JSON bytes
    json_data = json.dumps(transactions, indent=2).encode('utf-8')
    
    # Compress with zstd
    compressed = zstd.compress(json_data)
    
    return compressed

def decompress_transactions(compressed_data: bytes) -> list:
    """Decompress transaction data."""
    # Decompress
    json_data = zstd.decompress(compressed_data)
    
    # Parse JSON
    return json.loads(json_data.decode('utf-8'))

def save_compressed(path: str, data: list) -> dict:
    """Save data with compression, return stats."""
    json_bytes = json.dumps(data, indent=2).encode('utf-8')
    compressed = zstd.compress(json_bytes)
    
    Path(path).write_bytes(compressed)
    
    return {
        'original_size': len(json_bytes),
        'compressed_size': len(compressed),
        'ratio': len(json_bytes) / len(compressed)
    }

def load_compressed(path: str) -> list:
    """Load compressed data."""
    compressed = Path(path).read_bytes()
    json_bytes = zstd.decompress(compressed)
    return json.loads(json_bytes.decode('utf-8'))

# Demo: Finance Tracker Compression
print("=== Finance Tracker Data Compression ===")

# Generate sample transaction data
transactions = []
for i in range(1000):
    transactions.append({
        'id': i,
        'date': f'2024-{(i % 12) + 1:02d}-{(i % 28) + 1:02d}',
        'amount': round(50 + (i * 7.3) % 500, 2),
        'category': ['food', 'transport', 'utilities', 'entertainment'][i % 4],
        'description': f'Transaction #{i} - Regular purchase'
    })

print(f"\nGenerated {len(transactions)} transactions")

# Compress and save
base = Path('compression_demo')
base.mkdir(exist_ok=True)

stats = save_compressed(str(base / 'transactions.zst'), transactions)

print(f"\n=== Compression Results ===")
print(f"  Original size: {stats['original_size']:,} bytes")
print(f"  Compressed size: {stats['compressed_size']:,} bytes")
print(f"  Compression ratio: {stats['ratio']:.1f}x")
print(f"  Space saved: {(1 - 1/stats['ratio']) * 100:.1f}%")

# Load and verify
loaded = load_compressed(str(base / 'transactions.zst'))
print(f"\n=== Verification ===")
print(f"  Loaded {len(loaded)} transactions")
print(f"  First transaction: {loaded[0]}")
print(f"  Data integrity: {'OK' if loaded == transactions else 'FAILED'}")
```
