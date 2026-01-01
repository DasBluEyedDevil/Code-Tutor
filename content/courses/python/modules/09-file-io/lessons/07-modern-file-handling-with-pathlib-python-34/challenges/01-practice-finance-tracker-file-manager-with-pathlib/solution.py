from pathlib import Path
from datetime import datetime
import json
import shutil

def setup_finance_directories():
    """Create directory structure for Personal Finance Tracker."""
    base = Path('finance_tracker')
    
    directories = [
        base / 'data' / 'transactions',
        base / 'data' / 'budgets',
        base / 'backups',
        base / 'reports'
    ]
    
    for directory in directories:
        directory.mkdir(parents=True, exist_ok=True)
    
    print(f"Created finance tracker structure at: {base.resolve()}")
    return base

def find_transaction_files(data_dir):
    """Find all transaction files (JSON and CSV)."""
    path = Path(data_dir)
    
    json_files = list(path.rglob('*.json'))
    csv_files = list(path.rglob('*.csv'))
    
    return json_files + csv_files

def get_file_info(file_path):
    """Get metadata about a file."""
    path = Path(file_path)
    
    if not path.exists():
        return None
    
    stats = path.stat()
    
    return {
        'name': path.name,
        'stem': path.stem,
        'suffix': path.suffix,
        'size_bytes': stats.st_size,
        'modified': datetime.fromtimestamp(stats.st_mtime).isoformat()
    }

def backup_files(source_dir, backup_dir):
    """Backup all transaction files to backup directory."""
    source = Path(source_dir)
    backup = Path(backup_dir)
    backup.mkdir(parents=True, exist_ok=True)
    
    timestamp = datetime.now().strftime('%Y%m%d_%H%M%S')
    backup_folder = backup / timestamp
    backup_folder.mkdir(exist_ok=True)
    
    files = find_transaction_files(source)
    for f in files:
        dest = backup_folder / f.name
        shutil.copy2(f, dest)
        print(f"  Backed up: {f.name}")
    
    return len(files)

def format_size(bytes_size):
    """Format bytes as human-readable size."""
    for unit in ['B', 'KB', 'MB', 'GB']:
        if bytes_size < 1024:
            return f"{bytes_size:.1f} {unit}"
        bytes_size /= 1024
    return f"{bytes_size:.1f} TB"

# Test: Set up directories
print("=== Finance Tracker File Manager ===")
base_dir = setup_finance_directories()

# Create sample transaction files
sample_data = {'transactions': [{'amount': 50, 'category': 'food'}]}
(base_dir / 'data' / 'transactions' / '2024_01.json').write_text(
    json.dumps(sample_data, indent=2)
)
(base_dir / 'data' / 'transactions' / '2024_02.json').write_text(
    json.dumps(sample_data, indent=2)
)
print("Created sample transaction files")

# Find all transaction files
print("\n=== Finding Transaction Files ===")
files = find_transaction_files(base_dir / 'data')
for f in files:
    info = get_file_info(f)
    if info:
        print(f"  {info['name']}: {format_size(info['size_bytes'])} (modified: {info['modified']})")

# Backup files
print("\n=== Backing Up Files ===")
count = backup_files(base_dir / 'data', base_dir / 'backups')
print(f"Backed up {count} files")