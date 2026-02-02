---
type: "THEORY"
title: "Testing Streaming Endpoints"
---

Serverpod streaming endpoints use WebSockets for real-time communication. Testing these requires special handling:

1. **Simulated Connections**: Create test streams that mimic WebSocket behavior
2. **Message Ordering**: Verify messages arrive in the correct order
3. **Disconnection Handling**: Test what happens when connections drop
4. **Broadcast Verification**: Ensure messages reach all intended recipients