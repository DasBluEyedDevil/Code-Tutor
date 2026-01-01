---
type: "THEORY"
title: "OWASP Top 10"
---


### 1. Broken Access Control

❌ **Bad**:

✅ **Good**:

### 2. Cryptographic Failures

✅ **Use HTTPS everywhere**:

### 3. Injection

✅ **Always use parameterized queries** (shown earlier)

### 4. Insecure Design

✅ **Security by design**:

### 5. Security Misconfiguration

✅ **Secure defaults**:

---



```kotlin
// application.conf
ktor {
    deployment {
        port = 8080
        watch = []  # Disable auto-reload in production
    }
    application {
        modules = [ com.example.ApplicationKt.module ]
    }
}

security {
    ssl {
        enabled = true
        keyStore = ${?SSL_KEY_STORE}
        keyStorePassword = ${?SSL_KEY_STORE_PASSWORD}
    }
}
```
