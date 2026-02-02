---
type: "THEORY"
title: "The Platform Thread Problem"
---

Before Java 21, every Java thread mapped to an OS thread (platform thread):

PLATFORM THREAD COSTS:
- ~1MB stack memory each
- OS scheduling overhead
- Context switch: ~1-10 microseconds
- Limited by OS (typically < 10,000)

The problem for I/O-bound apps:

// Web server handling 10,000 concurrent connections
for (request : requests) {
    executor.submit(() -> {
        data = database.query();  // Thread BLOCKED 50ms
        result = process(data);   // Thread WORKING 1ms
    });
}

Each blocked thread:
- Uses 1MB memory doing NOTHING
- Wastes OS scheduling resources
- Could be handling other requests

10,000 connections = 10GB just for thread stacks!
And 98% of the time, threads are waiting on I/O, not computing.