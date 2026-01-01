---
type: "THEORY"
title: "Quiz Answers"
---


**Question 1: C) Heroku**

Heroku is designed for simplicity:
- Deploy with `git push heroku main`
- Automatic HTTPS
- Built-in database
- No server management

AWS/GCP are more powerful but complex.

---

**Question 2: B) HTTPS**

HTTPS is mandatory for production:
- Encrypts data in transit
- Prevents man-in-the-middle attacks
- Required by browsers
- Improves SEO

HTTP is only for local development.

---

**Question 3: B) Adding more servers**

Scaling types:
- **Horizontal**: Add more servers (better)
- **Vertical**: Bigger server (limited)

Horizontal scaling = unlimited capacity

---

**Question 4: B) Keep secrets out of code**

Environment variables:
- Don't commit secrets to Git
- Different values per environment
- Easy to rotate
- More secure

Never hardcode secrets!

---

**Question 5: B) Distributes traffic across multiple servers**

Load balancers:
- Distribute requests evenly
- Health check servers
- Remove failed servers
- Enable horizontal scaling

Essential for high availability.

---

