---
type: "THEORY"
title: "🧪 Testing Your API"
---


### Method 1: Web Browser (Simplest)

1. Open your web browser
2. Visit: `http://localhost:8080/`
3. You should see: **"Hello, Ktor! Your server is running! 🚀"**

Try these URLs:
- `http://localhost:8080/health` → "OK"
- `http://localhost:8080/api/hello` → "Hello from the API!"

### Method 2: curl (Command Line)


The `-i` flag shows headers:


### Method 3: Postman (GUI Tool)

1. Download Postman (free): https://www.postman.com/downloads/
2. Create a new request
3. Set method to GET
4. Enter URL: `http://localhost:8080/`
5. Click "Send"
6. See the response in the bottom panel

---



```kotlin
HTTP/1.1 200 OK
Content-Length: 42
Content-Type: text/plain; charset=UTF-8

Hello, Ktor! Your server is running! 🚀
```
