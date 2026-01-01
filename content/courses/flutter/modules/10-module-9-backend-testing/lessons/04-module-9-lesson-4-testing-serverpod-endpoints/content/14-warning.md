---
type: "WARNING"
title: "Streaming Test Considerations"
---

Keep these points in mind when testing streaming endpoints:

1. **Timing Issues**: Add small delays when waiting for messages to propagate

2. **Resource Cleanup**: Always call streamClosed in tearDown to prevent resource leaks

3. **Order Dependencies**: Messages may arrive in different orders - test for content, not always order

4. **Connection State**: Test behavior when connections are in different states (connecting, connected, disconnected)

5. **Error Recovery**: Test that streams recover gracefully from errors