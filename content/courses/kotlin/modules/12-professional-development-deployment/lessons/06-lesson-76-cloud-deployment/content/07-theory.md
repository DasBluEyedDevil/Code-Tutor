---
type: "THEORY"
title: "Database Hosting"
---

### PostgreSQL on Heroku

```bash
# Add PostgreSQL add-on
heroku addons:create heroku-postgresql:essential-0

# Get connection URL
heroku config:get DATABASE_URL
```

**Connect from Ktor**:

```kotlin
// The DATABASE_URL is automatically set
val dbUrl = System.getenv("DATABASE_URL")
    ?: "jdbc:postgresql://localhost:5432/myapp"

Database.connect(dbUrl)
```

### Amazon RDS

**1. Create PostgreSQL instance**:

```bash
aws rds create-db-instance \
  --db-instance-identifier my-app-db \
  --db-instance-class db.t3.micro \
  --engine postgres \
  --master-username admin \
  --master-user-password YOUR_PASSWORD \
  --allocated-storage 20
```

**2. Connect from application**:

```kotlin
val host = System.getenv("DB_HOST")
val port = System.getenv("DB_PORT") ?: "5432"
val name = System.getenv("DB_NAME")
val user = System.getenv("DB_USER")
val password = System.getenv("DB_PASSWORD")

Database.connect(
    url = "jdbc:postgresql://$host:$port/$name",
    user = user,
    password = password
)
```

### Google Cloud SQL

**1. Create instance**:

```bash
gcloud sql instances create my-app-db \
  --database-version=POSTGRES_15 \
  --tier=db-f1-micro \
  --region=us-central1
```

**2. Create database and user**:

```bash
gcloud sql databases create myapp --instance=my-app-db
gcloud sql users create appuser --instance=my-app-db --password=YOUR_PASSWORD
```

**3. Connect from Cloud Run**:

```bash
# Deploy with Cloud SQL connection
gcloud run deploy my-app \
  --add-cloudsql-instances=PROJECT_ID:REGION:my-app-db \
  --set-env-vars "INSTANCE_CONNECTION_NAME=PROJECT_ID:REGION:my-app-db" \
  --set-env-vars "DB_NAME=myapp" \
  --set-env-vars "DB_USER=appuser" \
  --set-secrets "DB_PASSWORD=db-password:latest"
```
