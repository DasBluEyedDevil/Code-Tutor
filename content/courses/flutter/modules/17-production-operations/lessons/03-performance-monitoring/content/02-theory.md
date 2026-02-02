---
type: "THEORY"
title: "Firebase Performance Monitoring Overview"
---


**What Firebase Performance Monitors Automatically**

Once integrated, Firebase Performance captures:

1. **App Start Traces**
   - Cold start (app not in memory)
   - Warm start (app in background)
   
2. **Screen Rendering**
   - Slow rendering frames (>16ms)
   - Frozen frames (>700ms)
   - Per-screen breakdown

3. **HTTP/S Network Requests**
   - Response time
   - Payload size
   - Success/failure rates
   - By URL pattern

**Custom Traces**

For operations Firebase doesn't track automatically, create custom traces:

- Login/authentication flow
- Data synchronization
- Image processing
- Database queries
- Complex calculations

**Metrics vs Attributes**

- **Metrics**: Numeric values that can be aggregated (duration, count, size)
- **Attributes**: String labels for filtering (user_type, region, feature_flag)

**Dashboard Insights**

Firebase Performance console shows:

- Performance trends over time
- Breakdown by device, OS, country
- Slowest network requests
- Screens with rendering issues
- Comparison between app versions

