try:
    import compression.zstd as zstd
except ImportError:
    import zstandard
    class zstd:
        @staticmethod
        def compress(data, level=3):
            return zstandard.ZstdCompressor(level=level).compress(data)
        @staticmethod
        def decompress(data):
            return zstandard.ZstdDecompressor().decompress(data)

import json
from pathlib import Path
from datetime import datetime

class FinanceArchive:
    def __init__(self, archive_dir: str):
        self.archive_dir = Path(archive_dir)
        self.archive_dir.mkdir(exist_ok=True)
    
    def compress_month(self, transactions: list, month: str, level: int = 3) -> dict:
        """Compress a month's transactions and return stats."""
        json_bytes = json.dumps(transactions, indent=2).encode('utf-8')
        compressed = zstd.compress(json_bytes, level=level)
        
        path = self.archive_dir / f'{month}.zst'
        path.write_bytes(compressed)
        
        return {
            'month': month,
            'original_size': len(json_bytes),
            'compressed_size': len(compressed),
            'ratio': len(json_bytes) / len(compressed)
        }
    
    def decompress_month(self, month: str) -> list:
        """Decompress and return a month's transactions."""
        path = self.archive_dir / f'{month}.zst'
        if not path.exists():
            return []
        
        compressed = path.read_bytes()
        json_bytes = zstd.decompress(compressed)
        return json.loads(json_bytes.decode('utf-8'))
    
    def compare_levels(self, data: bytes) -> list:
        """Compare compression levels."""
        results = []
        for level in [1, 3, 9, 19]:
            start = datetime.now()
            compressed = zstd.compress(data, level=level)
            elapsed = (datetime.now() - start).total_seconds()
            
            results.append({
                'level': level,
                'size': len(compressed),
                'ratio': len(data) / len(compressed),
                'time': elapsed
            })
        
        return results
    
    def archive_summary(self) -> dict:
        """Get summary of all archived months."""
        files = list(self.archive_dir.glob('*.zst'))
        total_size = sum(f.stat().st_size for f in files)
        
        return {
            'months': len(files),
            'total_compressed_size': total_size,
            'files': [f.stem for f in files]
        }

# Test the archive system
archive = FinanceArchive('finance_archive')

# Generate sample data
transactions = [
    {'date': f'2024-01-{i:02d}', 'amount': 50.00 + i, 'category': 'food'}
    for i in range(1, 101)
]

print("=== Compressed Finance Archive ===")

# Compress multiple months
for month in ['january', 'february', 'march']:
    stats = archive.compress_month(transactions, month)
    print(f"\n{month.title()}:")
    print(f"  Original: {stats['original_size']:,} bytes")
    print(f"  Compressed: {stats['compressed_size']:,} bytes")
    print(f"  Ratio: {stats['ratio']:.1f}x")

# Compare compression levels
print("\n=== Compression Level Comparison ===")
json_bytes = json.dumps(transactions * 10).encode('utf-8')
levels = archive.compare_levels(json_bytes)

for result in levels:
    print(f"Level {result['level']:2d}: {result['size']:,} bytes, "
          f"{result['ratio']:.1f}x ratio, {result['time']:.3f}s")

# Archive summary
print("\n=== Archive Summary ===")
summary = archive.archive_summary()
print(f"Months archived: {summary['months']}")
print(f"Total size: {summary['total_compressed_size']:,} bytes")
print(f"Files: {', '.join(summary['files'])}")