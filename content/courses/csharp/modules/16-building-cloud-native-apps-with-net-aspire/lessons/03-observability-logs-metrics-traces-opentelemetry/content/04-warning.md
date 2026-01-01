---
type: "WARNING"
title: "Common Pitfalls"
---

## Observability Anti-Patterns

**String Interpolation in Logs**: NEVER use `_logger.LogInformation($"Order {orderId}")`. This evaluates the string BEFORE logging, losing structured properties. Use `_logger.LogInformation("Order {OrderId}", orderId)` with placeholders.

**High-Cardinality Tags**: Don't use user IDs, order IDs, or email addresses as metric tags! Each unique value creates a new time series. Millions of users = millions of metrics = exploded storage costs.

**Forgetting Activity Disposal**: `StartActivity()` without `using` leaves spans open forever. Always use `using var activity = ...` or call `Dispose()` manually.

**Activity Returns Null**: `StartActivity()` returns null if no listener is registered! Always use null-conditional: `activity?.SetTag(...)` to avoid NullReferenceException.

**Dashboard is Dev-Only**: The Aspire Dashboard stores telemetry in memory and is NOT for production monitoring. Use Azure Monitor, Prometheus/Grafana, or Jaeger for production.

**OTLP Endpoint Configuration**: If telemetry isn't appearing in the dashboard, verify `OTEL_EXPORTER_OTLP_ENDPOINT` is set correctly. The default is `http://localhost:4317`.