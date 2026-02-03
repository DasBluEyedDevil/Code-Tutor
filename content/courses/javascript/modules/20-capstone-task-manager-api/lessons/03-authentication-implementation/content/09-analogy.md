---
type: "ANALOGY"
title: "The Concert Wristband"
---

JWT authentication works like a concert wristband system. At the entrance (login), you show your ID (email + password). If valid, you get a wristband (JWT token) that encodes your name and access level. Inside the venue, security (middleware) just checks your wristband -- they do not call the box office every time. The wristband has an expiration (the `exp` claim), after which you need to get a new one. And just like a concert wristband, if someone copies yours, they get your access. That is why the signing secret must stay private.
