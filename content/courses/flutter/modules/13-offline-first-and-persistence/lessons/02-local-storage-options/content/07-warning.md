---
type: WARNING
---

**SharedPreferences and Hive are not encrypted.** Both storage solutions write data as plain files on the device. On rooted Android or jailbroken iOS devices, any app can read this data. Never store sensitive information (tokens, passwords, personal data) in either one.

| Storage | Use For | Never Store |
|---------|---------|-------------|
| SharedPreferences | Theme, language, flags | Tokens, passwords |
| Hive | Cached content, app data | API keys, credentials |
| flutter_secure_storage | Tokens, secrets | Large data sets |

Also be aware of storage limits on mobile devices. Hive databases or large SharedPreferences files can grow silently. Implement a cache eviction strategy (e.g., delete entries older than 30 days) to prevent your app from consuming excessive storage, which may cause the OS to terminate it or prompt the user to clear data.
