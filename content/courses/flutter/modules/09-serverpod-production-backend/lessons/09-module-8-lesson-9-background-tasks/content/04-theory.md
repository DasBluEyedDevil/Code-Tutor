---
type: "THEORY"
title: "Serverpod Background Task Architecture"
---

Serverpod provides three primary mechanisms for background processing:

**1. Future Calls (Delayed Execution)**

Schedule a task to run once at a specific time in the future.

Use cases:
- Send reminder email 24 hours after signup
- Expire a coupon after 7 days
- Auto-cancel unpaid order after 30 minutes
- Follow-up notification 3 days after purchase

**2. Scheduled Jobs (Cron-Style)**

Run tasks on a recurring schedule.

Use cases:
- Daily report generation at midnight
- Hourly cache cleanup
- Weekly database maintenance
- Monthly billing processing

**3. Message Passing (Event-Driven)**

Decouple work by posting messages that workers consume.

Use cases:
- Process uploaded images asynchronously
- Send notifications without blocking
- Distribute work across multiple servers
- Handle spikes in traffic gracefully

**Architecture Overview:**

```
                    +------------------+
                    |   HTTP Request   |
                    +--------+---------+
                             |
                             v
                    +------------------+
                    |    Endpoint      |
                    | (Quick Response) |
                    +--------+---------+
                             |
            +----------------+----------------+
            |                |                |
            v                v                v
    +-------+------+  +------+-------+  +-----+------+
    | Future Call  |  | Post Message |  | Direct     |
    | (Delayed)    |  | (Queue)      |  | Scheduled  |
    +-------+------+  +------+-------+  +-----+------+
            |                |                |
            v                v                v
    +-------+------+  +------+-------+  +-----+------+
    | Runs at      |  | Worker       |  | Cron Job   |
    | scheduled    |  | processes    |  | runs on    |
    | time         |  | queue        |  | schedule   |
    +--------------+  +--------------+  +------------+
```

