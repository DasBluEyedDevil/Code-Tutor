---
type: "THEORY"
title: "Introduction to Serverpod Insights"
---


**What is Serverpod Insights?**

Serverpod Insights is the built-in monitoring and debugging dashboard that comes with every Serverpod deployment. It provides real-time visibility into your backend's health, performance, and behavior without requiring third-party tools.

**Key Features**

| Feature | Description |
|---------|-------------|
| Log Viewer | Centralized logging with filtering and search |
| Health Metrics | Server health, memory, CPU monitoring |
| Database Stats | Query performance and connection pool status |
| Session Tracking | Active user sessions and request tracing |
| Error Dashboard | Aggregated errors with stack traces |

**Accessing Serverpod Insights**

Serverpod Insights runs on a separate port from your main API:

```
Main API:     https://api.yourapp.com:8080
Insights:     https://api.yourapp.com:8081
```

In development:
```
Main API:     http://localhost:8080
Insights:     http://localhost:8081
```

**Architecture Overview**

```
+------------------+     +-------------------+
|  Flutter App     | --> |  Serverpod API    |
+------------------+     |  (Port 8080)      |
                         +-------------------+
                                  |
                                  v
                         +-------------------+
                         |  Insights Web UI  |
                         |  (Port 8081)      |
                         +-------------------+
                                  |
                                  v
                         +-------------------+
                         |  PostgreSQL DB    |
                         |  (Logs & Metrics) |
                         +-------------------+
```

**Why Use Serverpod Insights?**

1. **Zero Configuration** - Works out of the box with every Serverpod project
2. **Integrated Experience** - Logs, metrics, and traces in one place
3. **Production Ready** - Scales with your application
4. **Secure** - Built-in authentication for dashboard access
5. **Cost Effective** - No external monitoring service fees

**When to Use External Tools**

While Serverpod Insights covers most needs, consider external tools for:
- Long-term metric storage and trending
- Complex alerting rules
- Multi-service correlation
- Compliance requirements

