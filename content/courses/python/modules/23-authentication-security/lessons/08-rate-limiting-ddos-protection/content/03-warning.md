---
type: "WARNING"
title: "DDoS Attack Vectors"
---

**Distributed Denial of Service (DDoS)** attacks aim to overwhelm your systems. Understanding attack vectors helps you defend against them:

**Layer 3/4 Attacks (Network/Transport)**

1. **SYN Flood**: Sends TCP SYN packets without completing handshake
   - Mitigation: SYN cookies, connection timeouts, cloud DDoS protection

2. **UDP Flood**: Overwhelms with UDP packets
   - Mitigation: Rate limiting, blocking unused UDP ports

3. **ICMP Flood (Ping of Death)**: Floods with ICMP packets
   - Mitigation: Block/rate limit ICMP at firewall

**Layer 7 Attacks (Application)**

4. **HTTP Flood**: Legitimate-looking HTTP requests at massive scale
   - Mitigation: Rate limiting, CAPTCHA, behavior analysis

5. **Slowloris**: Opens many connections, sends data very slowly
   - Mitigation: Connection timeouts, max connections per IP

6. **API Abuse**: Targets expensive endpoints (search, reports)
   - Mitigation: Endpoint-specific rate limits, caching

**Application-Specific Attacks**

7. **Login Brute Force**: Attempts many password combinations
   - Mitigation: Account lockout, progressive delays, CAPTCHA

8. **Resource Exhaustion**: Requests that consume excessive resources
   - Mitigation: Request size limits, timeout limits, pagination

**Defense Strategy for Finance Tracker:**

```
[CDN/WAF Layer]
|-- Geographic blocking (block countries you don't serve)
|-- Known bad IPs (threat intelligence feeds)
|-- Layer 3/4 attack mitigation
|-- SSL/TLS termination

[Load Balancer Layer]
|-- Connection limits per IP
|-- Request rate limiting
|-- Health checks

[Application Layer]
|-- Authentication rate limiting
|-- Endpoint-specific limits
|-- Request validation
|-- Resource quotas per user

[Database Layer]
|-- Query timeouts
|-- Connection pooling
|-- Read replicas for queries
```

**Red Flags in Production:**
- Sudden spike in 4xx/5xx errors
- Increased response times
- Connection pool exhaustion
- Memory/CPU spikes
- Traffic from unusual geographic regions