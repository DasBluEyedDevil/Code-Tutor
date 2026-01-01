---
type: "EXAMPLE"
title: "Automated Backup Script"
---

**Python script for automated backups:**

```python
import subprocess
import os
from datetime import datetime, timedelta
from pathlib import Path
import logging

logging.basicConfig(level=logging.INFO)
logger = logging.getLogger(__name__)

class DatabaseBackup:
    """Automated PostgreSQL backup manager."""
    
    def __init__(
        self,
        host: str = 'localhost',
        port: int = 5432,
        user: str = 'finance_user',
        database: str = 'finance_tracker',
        backup_dir: str = './backups',
        retention_days: int = 30
    ):
        self.host = host
        self.port = port
        self.user = user
        self.database = database
        self.backup_dir = Path(backup_dir)
        self.retention_days = retention_days
        
        self.backup_dir.mkdir(parents=True, exist_ok=True)
    
    def create_backup(self, backup_type: str = 'full') -> Path:
        """Create a database backup."""
        timestamp = datetime.now().strftime('%Y%m%d_%H%M%S')
        filename = f"{self.database}_{backup_type}_{timestamp}.dump"
        filepath = self.backup_dir / filename
        
        cmd = [
            'pg_dump',
            '-h', self.host,
            '-p', str(self.port),
            '-U', self.user,
            '-d', self.database,
            '-Fc',  # Custom format (compressed)
            '-f', str(filepath)
        ]
        
        logger.info(f"Starting backup: {filename}")
        
        try:
            result = subprocess.run(
                cmd,
                capture_output=True,
                text=True,
                check=True,
                env={**os.environ, 'PGPASSWORD': os.getenv('PGPASSWORD', '')}
            )
            
            size_mb = filepath.stat().st_size / (1024 * 1024)
            logger.info(f"Backup complete: {filename} ({size_mb:.2f} MB)")
            return filepath
            
        except subprocess.CalledProcessError as e:
            logger.error(f"Backup failed: {e.stderr}")
            raise
    
    def restore_backup(self, filepath: Path, target_db: str = None):
        """Restore a database from backup."""
        target = target_db or f"{self.database}_restored"
        
        # Create target database
        create_cmd = [
            'createdb',
            '-h', self.host,
            '-p', str(self.port),
            '-U', self.user,
            target
        ]
        
        try:
            subprocess.run(create_cmd, capture_output=True, check=True)
            logger.info(f"Created database: {target}")
        except subprocess.CalledProcessError:
            logger.warning(f"Database {target} may already exist")
        
        # Restore
        restore_cmd = [
            'pg_restore',
            '-h', self.host,
            '-p', str(self.port),
            '-U', self.user,
            '-d', target,
            '-j', '4',  # Parallel restore
            str(filepath)
        ]
        
        logger.info(f"Restoring to {target}...")
        subprocess.run(restore_cmd, capture_output=True, check=True)
        logger.info(f"Restore complete!")
    
    def cleanup_old_backups(self):
        """Remove backups older than retention period."""
        cutoff = datetime.now() - timedelta(days=self.retention_days)
        removed = 0
        
        for backup in self.backup_dir.glob('*.dump'):
            if datetime.fromtimestamp(backup.stat().st_mtime) < cutoff:
                backup.unlink()
                removed += 1
                logger.info(f"Removed old backup: {backup.name}")
        
        logger.info(f"Cleanup complete: removed {removed} old backups")
    
    def list_backups(self) -> list:
        """List available backups."""
        backups = []
        for backup in sorted(self.backup_dir.glob('*.dump'), reverse=True):
            stat = backup.stat()
            backups.append({
                'name': backup.name,
                'size_mb': stat.st_size / (1024 * 1024),
                'created': datetime.fromtimestamp(stat.st_mtime)
            })
        return backups

# Usage
if __name__ == '__main__':
    backup_mgr = DatabaseBackup()
    
    # Create backup
    backup_path = backup_mgr.create_backup()
    
    # List backups
    for b in backup_mgr.list_backups()[:5]:
        print(f"{b['name']}: {b['size_mb']:.2f} MB - {b['created']}")
    
    # Cleanup old backups
    backup_mgr.cleanup_old_backups()
```
