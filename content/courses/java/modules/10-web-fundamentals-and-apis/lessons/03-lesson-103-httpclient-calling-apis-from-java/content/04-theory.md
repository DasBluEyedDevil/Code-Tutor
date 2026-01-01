---
type: "THEORY"
title: "Handling HTTP Headers"
---

COMMON HEADERS:

Content-Type: What format is the body?
- application/json (JSON data)
- application/x-www-form-urlencoded (form data)
- text/html (HTML)

Authorization: Authentication token
- Bearer <token>
- Basic <base64-credentials>

Accept: What format do you want back?
- application/json

SETTING HEADERS:
HttpRequest request = HttpRequest.newBuilder()
    .uri(URI.create(url))
    .header("Content-Type", "application/json")
    .header("Authorization", "Bearer abc123xyz")
    .header("Accept", "application/json")
    .GET()
    .build();

READING RESPONSE HEADERS:
String contentType = response.headers()
    .firstValue("content-type")
    .orElse("unknown");