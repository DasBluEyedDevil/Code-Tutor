---
type: "ANALOGY"
title: "Understanding the Concept"
---

Imagine you're a detective investigating why a pizza delivery was late. You need THREE types of evidence:

1. LOGS (The Diary): What happened step by step
   'Order received at 6:00 PM'
   'Pizza made at 6:15 PM'
   'Driver left at 6:20 PM'
   'ERROR: GPS lost signal at 6:30 PM'

2. METRICS (The Dashboard): Numbers over time
   'Orders per hour: 50'
   'Average delivery time: 35 mins'
   'Error rate: 2%'

3. TRACES (The Journey): Follow one request across services
   Order Service -> Kitchen Service -> Driver Service -> Delivery
   'This specific order took 50 mins because Kitchen was slow'

OPENTELEMETRY: Industry standard for all three!
- One API for logs, metrics, traces
- Works with any backend (Jaeger, Zipkin, Prometheus)
- Auto-instrumentation for common libraries

ASPIRE DASHBOARD: Built-in visualization!
- See all services and their status
- View logs, metrics, traces in one place
- No external tools needed for development

Think: 'Logs tell you WHAT happened. Metrics tell you HOW MUCH. Traces tell you WHERE in the journey.'