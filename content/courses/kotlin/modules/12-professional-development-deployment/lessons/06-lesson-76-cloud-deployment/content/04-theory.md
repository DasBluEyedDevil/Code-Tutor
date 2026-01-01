---
type: "THEORY"
title: "Heroku Deployment (Easiest)"
---


### Why Heroku?

- ✅ Deploy in 5 minutes
- ✅ Free tier available
- ✅ Automatic HTTPS
- ✅ Built-in database hosting
- ✅ Zero DevOps knowledge needed

### Deploy Ktor to Heroku

**1. Create Procfile**:

**2. Update build.gradle.kts**:

**3. Create app.json** (optional):

**4. Deploy**:

**5. Configure port** (Heroku provides PORT env var):

**Your app is live at**: `https://my-ktor-app.herokuapp.com`

---



```kotlin
// Application.kt
fun main() {
    val port = System.getenv("PORT")?.toInt() ?: 8080
    embeddedServer(Netty, port = port, host = "0.0.0.0") {
        module()
    }.start(wait = true)
}
```
