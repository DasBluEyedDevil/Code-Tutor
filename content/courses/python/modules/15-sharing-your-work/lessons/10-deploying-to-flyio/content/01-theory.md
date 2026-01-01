---
type: "THEORY"
title: "What is Fly.io?"
---

**Fly.io = Global edge deployment with low latency**

Fly.io runs your apps on lightweight VMs (Firecracker) in data centers worldwide:

**Why choose Fly.io?**

1. **Global edge network**
   - 30+ regions worldwide
   - Deploy close to your users
   - Low latency everywhere
   - Automatic request routing

2. **VM-based (not containers)**
   - Full Linux VMs (Firecracker)
   - More control than containers
   - Persistent storage options
   - SSH into running machines

3. **Excellent free tier**
   - 3 shared VMs free
   - 1GB persistent storage
   - 160GB outbound bandwidth
   - Generous for hobby projects

4. **Fly Postgres**
   - Managed PostgreSQL
   - Automatic failover
   - Read replicas
   - Point-in-time recovery

**Fly.io architecture:**
```
User Request → Anycast IP → Nearest Region → Your App
                              ↓
                         Auto-scaled VMs
```

**When to choose Fly.io:**
- Users in multiple geographic regions
- Need low latency (gaming, real-time apps)
- Want full VM control
- Need persistent storage

**For our Personal Finance Tracker:**
- FastAPI running on Fly Machines
- Fly Postgres for database
- Multi-region for global users
- Automatic HTTPS with certificates