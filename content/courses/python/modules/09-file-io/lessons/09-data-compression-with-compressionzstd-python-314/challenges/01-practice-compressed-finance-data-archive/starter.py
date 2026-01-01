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
        # TODO: Convert transactions to JSON bytes
        json_bytes = json.dumps(transactions, indent=2).encode('utf-8')
        
        # TODO: Compress with zstd
        compressed = zstd.compress(json_bytes, level=level)
        
        # TODO: Save to archive directory
        path = self.archive_dir / f'{month}.zst'
        path.write_bytes(compressed)
        
        # TODO: Return stats
        return {
            'month': month,
            'original_size': len(json_bytes),
            'compressed_size': len(compressed),
            'ratio': len(json_bytes) / len(compressed)
        }
    
    def decompress_month(self, month: str) -> list:
        """Decompress and return a month's transactions."""
        # TODO: Read compressed file
        path = self.archive_dir / f'{month}.zst'
        
        # TODO: Decompress and parse JSON
        pass
    
    def compare_levels(self, data: bytes) -> list:
        """Compare compression levels 1, 3, 9, 19."""
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

# Test the archive system
archive = FinanceArchive('finance_archive')

# Generate sample data
transactions = [
    {'date': '2024-01-15', 'amount': 50.00, 'category': 'food'}
    for _ in range(100)
]

print("=== Compressed Finance Archive ===")

# Compress January data
stats = archive.compress_month(transactions, 'january')
print(f"\nJanuary Archive:")
print(f"  Original: {stats['original_size']:,} bytes")
print(f"  Compressed: {stats['compressed_size']:,} bytes")
print(f"  Ratio: {stats['ratio']:.1f}x")