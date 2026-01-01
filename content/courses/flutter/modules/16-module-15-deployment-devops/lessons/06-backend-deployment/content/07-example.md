---
type: "EXAMPLE"
title: "Database Configuration Examples"
---


Production database configuration for Serverpod:



```yaml
## Serverpod Production Database Config

# config/production.yaml
database:
  host: ${DATABASE_HOST}
  port: ${DATABASE_PORT:5432}
  name: ${DATABASE_NAME}
  user: ${DATABASE_USER}
  password: ${DATABASE_PASSWORD}
  requireSsl: true
  
  # Connection pool settings
  pool:
    minConnections: 5
    maxConnections: 20
    idleTimeout: 300000  # 5 minutes
    connectionTimeout: 30000  # 30 seconds

---

## Running Migrations

# Development
cd my_app_server
dart run bin/main.dart --apply-migrations

# Production (Railway/Fly.io)
# Add as a release command or init container

# Railway - set in railway.json
{
  "build": {
    "builder": "dockerfile"
  },
  "deploy": {
    "startCommand": "./server --apply-migrations --mode production"
  }
}

# Fly.io - add to fly.toml
[deploy]
  release_command = "./server --apply-migrations --mode production --migrate-only"

---

## Database Backup Script

#!/bin/bash
# backup-db.sh

set -e

DB_HOST="${DATABASE_HOST}"
DB_NAME="${DATABASE_NAME}"
DB_USER="${DATABASE_USER}"
BACKUP_DIR="/backups"
DATE=$(date +%Y%m%d_%H%M%S)

# Create backup
PGPASSWORD="${DATABASE_PASSWORD}" pg_dump \
  -h "$DB_HOST" \
  -U "$DB_USER" \
  -d "$DB_NAME" \
  -F c \
  --no-owner \
  -f "${BACKUP_DIR}/${DB_NAME}_${DATE}.dump"

# Compress
gzip "${BACKUP_DIR}/${DB_NAME}_${DATE}.dump"

# Upload to cloud storage (example: S3)
aws s3 cp "${BACKUP_DIR}/${DB_NAME}_${DATE}.dump.gz" \
  "s3://my-backups/database/${DB_NAME}_${DATE}.dump.gz"

# Clean up local backups older than 7 days
find "$BACKUP_DIR" -name "*.dump.gz" -mtime +7 -delete

echo "Backup completed: ${DB_NAME}_${DATE}.dump.gz"

---

## Managed Database Options

### Railway PostgreSQL
# Automatically provisioned, connection string in $DATABASE_URL
# Parse in Dart:
final uri = Uri.parse(Platform.environment['DATABASE_URL']!);
final host = uri.host;
final port = uri.port;
final database = uri.pathSegments.first;
final user = uri.userInfo.split(':').first;
final password = uri.userInfo.split(':').last;

### Fly.io Postgres
# Provisioned via fly postgres create
# Attached with fly postgres attach
# Connection details in $DATABASE_URL

### Supabase (External)
# 1. Create project at supabase.com
# 2. Get connection string from Settings > Database
# 3. Use direct connection for Serverpod (not pooler)
# 4. Enable SSL in connection settings

### Neon (Serverless Postgres)
# 1. Create project at neon.tech
# 2. Get connection string
# 3. Note: Connection pooling built-in
# 4. Great for cost-sensitive deployments
```
