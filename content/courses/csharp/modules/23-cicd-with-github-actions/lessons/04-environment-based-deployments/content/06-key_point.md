---
type: "KEY_POINT"
title: "Environment-Based Deployment"
---

## Key Takeaways

- **Use GitHub Environments for deployment gates** -- configure `environment: production` on jobs to require manual approval, enforce branch protection, and scope secrets to specific environments.

- **Promote the same artifact through environments** -- build once, deploy to dev, then staging, then production. Never rebuild between environments; the same Docker image or publish output moves forward.

- **Environment-specific configuration uses GitHub Secrets** -- `secrets.PROD_CONNECTION_STRING` and `secrets.STAGING_CONNECTION_STRING` are scoped to their environments. The workflow references the same secret name; the environment provides the correct value.
