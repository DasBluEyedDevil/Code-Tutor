---
type: "THEORY"
title: "Android Security"
---


### KeyStore for Secrets

❌ **Bad** (Hardcoded secrets):

✅ **Good** (KeyStore):

### ProGuard/R8 Configuration


**proguard-rules.pro**:

### Certificate Pinning


---



```kotlin
// build.gradle.kts
dependencies {
    implementation("com.squareup.okhttp3:okhttp:4.12.0")
}

// Certificate pinning
val certificatePinner = CertificatePinner.Builder()
    .add(
        "api.example.com",
        "sha256/AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA="
    )
    .build()

val client = OkHttpClient.Builder()
    .certificatePinner(certificatePinner)
    .build()

// Get SHA256 hash:
// openssl s_client -connect api.example.com:443 | openssl x509 -pubkey -noout | openssl pkey -pubin -outform der | openssl dgst -sha256 -binary | openssl enc -base64
```
