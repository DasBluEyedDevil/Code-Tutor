---
type: "THEORY"
title: "Encrypting SQLite Data"
---

### SQLCipher for Full Database Encryption

For highly sensitive apps, encrypt the entire database:

```kotlin
// build.gradle.kts
implementation("net.zetetic:sqlcipher-android:4.13.0")

// Android encrypted driver
fun createEncryptedDriver(
    context: Context,
    password: String
): SqlDriver {
    // Load SQLCipher library
    System.loadLibrary("sqlcipher")
    
    val database = SQLiteDatabase.openOrCreateDatabase(
        context.getDatabasePath("encrypted.db"),
        password,
        null
    )
    
    return AndroidSqliteDriver(database)
}
```

### Column-Level Encryption

For specific sensitive columns:

```kotlin
// Encrypt before storing
fun encryptSensitiveData(data: String, key: SecretKey): String {
    val cipher = Cipher.getInstance("AES/GCM/NoPadding")
    cipher.init(Cipher.ENCRYPT_MODE, key)
    val encrypted = cipher.doFinal(data.toByteArray())
    return Base64.encodeToString(encrypted, Base64.DEFAULT)
}

// Type adapter for encrypted column
val encryptedAdapter = object : ColumnAdapter<String, String> {
    override fun decode(databaseValue: String): String {
        return decrypt(databaseValue)
    }
    override fun encode(value: String): String {
        return encrypt(value)
    }
}
```