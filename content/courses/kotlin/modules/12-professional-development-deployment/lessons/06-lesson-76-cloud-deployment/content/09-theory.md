---
type: "THEORY"
title: "Environment Configuration"
---

### application.conf

Use environment variables with fallback defaults:

```hocon
# resources/application.conf
ktor {
    deployment {
        port = ${?PORT}
        port = 8080
    }
}

database {
    url = ${?DATABASE_URL}
    url = "jdbc:postgresql://localhost:5432/myapp"
    driver = "org.postgresql.Driver"
    user = ${?DB_USER}
    password = ${?DB_PASSWORD}
}

jwt {
    secret = ${JWT_SECRET}
    issuer = ${?JWT_ISSUER}
    issuer = "myapp"
    audience = ${?JWT_AUDIENCE}
    audience = "myapp-users"
}
```

### Secrets Management

**AWS Secrets Manager**:

```kotlin
import aws.sdk.kotlin.services.secretsmanager.SecretsManagerClient
import aws.sdk.kotlin.services.secretsmanager.model.GetSecretValueRequest

suspend fun getSecret(secretName: String): String {
    val client = SecretsManagerClient { region = "us-east-1" }
    val request = GetSecretValueRequest { secretId = secretName }
    val response = client.getSecretValue(request)
    return response.secretString ?: throw Exception("Secret not found")
}
```

**Google Secret Manager**:

```kotlin
import com.google.cloud.secretmanager.v1.SecretManagerServiceClient
import com.google.cloud.secretmanager.v1.SecretVersionName

fun getSecret(projectId: String, secretId: String): String {
    SecretManagerServiceClient.create().use { client ->
        val name = SecretVersionName.of(projectId, secretId, "latest")
        val response = client.accessSecretVersion(name)
        return response.payload.data.toStringUtf8()
    }
}

// Usage in Ktor
fun Application.module() {
    val dbPassword = if (System.getenv("GOOGLE_CLOUD_PROJECT") != null) {
        getSecret(System.getenv("GOOGLE_CLOUD_PROJECT"), "db-password")
    } else {
        System.getenv("DB_PASSWORD") ?: "local-dev-password"
    }
}
```
