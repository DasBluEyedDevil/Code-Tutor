---
type: "THEORY"
title: "Common Environment Types"
---


**Development (dev)**
- Local or development API server
- Verbose logging enabled
- Debug mode features (inspector, dev tools)
- Fast iteration, hot reload friendly
- Uses test credentials

**Staging**
- Production-like environment
- Real backend with test data
- Used for QA testing
- May use test payment gateways
- Mirrors production setup

**Production (prod)**
- Real API server
- Logging minimized or sent to crash reporting
- Debug tools disabled
- Real payment processing
- This is what users download

**Why Three Environments?**

1. **Dev** - Developers break things here safely
2. **Staging** - QA tests here before release
3. **Production** - Only stable, tested code goes here

Some teams add more: UAT (User Acceptance Testing), Beta, Canary.

