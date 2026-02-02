---
type: "KEY_POINT"
title: "Automated Deployment"
---


**Deployment Strategies:**

| Strategy | Description | Use Case |
|----------|-------------|----------|
| Manual approval | Requires human approval before deploy | Production releases |
| Automatic | Deploys on every merge to main | Staging environments |
| Tag-based | Deploys when version tag is pushed | Versioned releases |
| Scheduled | Deploys at specific times | Nightly builds |

**Store Deployment Options:**

**Google Play Store:**
- Use `r0adkll/upload-google-play` action
- Requires service account JSON key
- Supports tracks: internal, alpha, beta, production

**Apple App Store:**
- Use Fastlane with `maierj/fastlane-action`
- Requires App Store Connect API key
- TestFlight for beta distribution

**Backend Deployment:**
- Railway: Auto-deploys on push via GitHub integration
- Fly.io: Use `superfly/flyctl-actions`
- Docker: Build and push to registry, then deploy

**Deployment Environments:**

GitHub Actions supports environment protection rules:

```yaml
jobs:
  deploy:
    environment:
      name: production
      url: https://myapp.com
    # Requires approval from designated reviewers
```

