---
type: "KEY_POINT"
title: "Fly.io Deployment"
---


**Why Fly.io for Dart Backends?**

Fly.io excels at edge deployment, running your backend close to users worldwide. It supports persistent volumes, WebSockets, and provides excellent tooling.

**Key Fly.io Concepts:**

- **Apps**: Your deployed service
- **Machines**: Individual VMs running your app
- **Regions**: Global datacenters (iad, lhr, sin, syd, etc.)
- **Volumes**: Persistent storage attached to machines
- **Secrets**: Encrypted environment variables

**Fly.io vs Railway:**

| Feature | Fly.io | Railway |
|---------|--------|--------|
| Global regions | 35+ | Limited |
| Pricing model | Usage-based | Predictable monthly |
| WebSocket support | Native | Supported |
| Learning curve | Moderate | Easy |
| Database | Separate service | Built-in |

