---
type: "EXAMPLE"
title: "CSRF Protection"
---

Cross-Site Request Forgery protection is CRITICAL for sessions:

```java
// CSRF Attack scenario:
// 1. User logs into yourbank.com
// 2. User visits evil-site.com
// 3. Evil site has:
//    <img src="https://yourbank.com/transfer?to=attacker&amount=10000">
// 4. Browser sends request WITH user's session cookie!
// 5. Bank processes transfer - user never clicked anything

// CSRF TOKEN PROTECTION:
// Spring Security generates unique token per session
// Token must be included in state-changing requests
// Attacker can't know the token!

// Thymeleaf form (token added automatically):
<form th:action="@{/transfer}" method="post">
    <!-- Spring adds: <input type="hidden" name="_csrf" value="abc123"> -->
    <input type="text" name="to" placeholder="Recipient">
    <input type="number" name="amount" placeholder="Amount">
    <button type="submit">Transfer</button>
</form>

// For AJAX requests, include token in header:
const csrfToken = document.querySelector('meta[name="_csrf"]').content;
const csrfHeader = document.querySelector('meta[name="_csrf_header"]').content;

fetch('/api/transfer', {
    method: 'POST',
    headers: {
        [csrfHeader]: csrfToken,  // X-CSRF-TOKEN: abc123
        'Content-Type': 'application/json'
    },
    body: JSON.stringify({ to: 'recipient', amount: 100 })
});

// In HTML head:
<meta name="_csrf" th:content="${_csrf.token}">
<meta name="_csrf_header" th:content="${_csrf.headerName}">
```
