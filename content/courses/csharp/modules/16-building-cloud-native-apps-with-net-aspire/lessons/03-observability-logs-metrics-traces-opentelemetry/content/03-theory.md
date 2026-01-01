---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`AddOpenTelemetry().WithMetrics().WithTracing()`**: Fluent API to configure OpenTelemetry. Chain WithMetrics() and WithTracing() to enable each signal.

**`AddAspNetCoreInstrumentation()`**: Auto-instruments ASP.NET Core requests. Every HTTP request becomes a trace span with timing, status codes, etc.

**`_logger.LogInformation("Message {Property}", value)`**: Structured logging! {Property} is a placeholder, value fills it. Properties are indexed for searching in the dashboard.

**`meter.CreateCounter<int>("metric.name")`**: Creates a metric that only goes up (counts events). Other types: Histogram (distributions), UpDownCounter (can decrease), Gauge (point-in-time value).

**`ActivitySource.StartActivity("name")`**: Starts a trace span. The span represents a unit of work. Use 'using' to auto-close when done.

**`activity?.SetTag("key", value)`**: Adds metadata to a span. Tags are indexed and searchable. Use for request IDs, user IDs, amounts, etc.