---
type: "KEY_POINT"
title: "OpenTelemetry Observability"
---

## Key Takeaways

- **Three signals: Logs, Metrics, Traces** -- structured logs record events, metrics track numerical trends (request count, latency), traces follow requests across service boundaries. Together they give full visibility.

- **Use structured logging: `_logger.LogInformation("Order {OrderId} placed", orderId)`** -- `{OrderId}` is a named placeholder, not string interpolation. The value is indexed and searchable in the Aspire dashboard.

- **`ActivitySource.StartActivity("name")` creates trace spans** -- wrap units of work in spans to see timing breakdowns. Add `.SetTag("key", value)` for searchable metadata like user IDs or order amounts.
