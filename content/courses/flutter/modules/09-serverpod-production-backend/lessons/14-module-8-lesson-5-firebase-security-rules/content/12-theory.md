---
type: "THEORY"
title: "Monitoring Security"
---


### View Rule Violations

1. Go to Firebase Console → Firestore → Usage
2. Check "Denied requests" graph
3. High denial rate might indicate:
   - **Attack attempt** (good - rules working!)
   - **Bug in your app** (bad - fix your code)
   - **Overly restrictive rules** (bad - adjust rules)

### Set Up Alerts

1. Firebase Console → Project Settings → Integrations
2. Enable Cloud Functions alerts
3. Monitor for:
   - Unusual traffic spikes
   - High error rates
   - Storage quota nearing limit

