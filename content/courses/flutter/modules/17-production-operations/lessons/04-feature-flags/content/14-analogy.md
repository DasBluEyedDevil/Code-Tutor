---
type: "ANALOGY"
title: "The Light Switch Panel"
---

Feature flags are like a panel of light switches in a theater. Each switch controls a different set of lights -- stage left, stage right, spotlights, house lights. The electrician (your team) can flip any switch at any moment during the show without rebuilding the theater or rewiring anything. The audience sees changes instantly.

In your app, each feature flag is a switch. "New chat UI" -- flip it on for 10% of users. "Holiday theme" -- flip it on December 1st, off January 2nd. "Experimental search algorithm" -- on for beta testers, off for everyone else. The code for all these features is already deployed; the flags control which users see what.

**The real power is the kill switch.** If the new chat UI causes a spike in crashes, you flip the switch off and every user instantly goes back to the old version -- no emergency app update, no waiting for store review, no downtime. Feature flags turn risky deployments into reversible experiments.
