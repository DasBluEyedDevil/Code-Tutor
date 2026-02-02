---
type: "ANALOGY"
title: "The Concept: Shipping Your Code"
---

**Deployment = Making your app available to users**

**Think of it like:**
- Opening a restaurant (not just cooking at home)
- Publishing a book (not just writing it)
- Launching a rocket (not just building it)

**Development vs Production:**

**Development** 💻
- On your computer
- Debug mode enabled
- Small test database
- You're the only user
- Frequent changes

**Production** 🚀
- On a server
- Debug mode OFF
- Real database
- Many users
- Stable, tested code

**Deployment platforms:**

**1. Platform-as-a-Service (PaaS)** ☁️
- Railway (recommended - easy Heroku alternative)
- Render (great free tier)
- Fly.io (edge deployment)
- PythonAnywhere (Python-specific)

Note: Heroku discontinued free tier in 2022. Railway and Render offer similar experiences with free tiers.

**Pros:**
- Easy to use
- Auto-scaling
- Managed services

**Cons:**
- More expensive
- Less control

**2. Infrastructure-as-a-Service (IaaS)** 🏢
- AWS EC2
- DigitalOcean
- Linode

**Pros:**
- Full control
- Cheaper at scale

**Cons:**
- More setup
- You manage servers

**3. Serverless** ⚡
- AWS Lambda
- Vercel
- Netlify Functions

**Pros:**
- No servers to manage
- Pay per use

**Cons:**
- Cold starts
- Vendor lock-in

**Deployment checklist:**

```
1. ✅ Environment variables (.env)
2. ✅ Production database
3. ✅ Debug mode = False
4. ✅ Secret key changed
5. ✅ Dependencies listed (requirements.txt)
6. ✅ HTTPS enabled
7. ✅ Error logging
8. ✅ Backups configured
9. ✅ Tests passing
10. ✅ Performance tested
```

**CI/CD (Continuous Integration/Deployment):**
- Automated testing
- Automated deployment
- Every push triggers tests
- Passing tests auto-deploy

**Example workflow:**
```
Developer → Git Push → Tests Run → Deploy to Server
```