---
type: "WARNING"
title: "Common Pitfalls"
---

## Environment-Based Deployment Pitfalls

**Using Production Database in Staging**: Connecting your staging environment to the production database for "realistic testing" risks accidental data corruption, deletion, or exposure of real customer data. Always use separate databases per environment with synthetic or anonymized test data.

**Missing Environment-Specific Configuration**: Hardcoding values that differ between environments (API URLs, connection strings, feature flags) leads to "works on my machine" failures. Use environment variables, configuration files per environment (`appsettings.Staging.json`), or a configuration service for all environment-specific values.

**No Approval Gates for Production**: Automated deployment to production without human review means a merged PR immediately reaches customers. Add manual approval steps in your workflow for production deployments: `environment: production` with required reviewers gives a final checkpoint before release.

**Inconsistent Environments**: When staging and production use different OS versions, .NET versions, or infrastructure configurations, bugs appear in production that never occurred in staging. Use identical Docker images and infrastructure-as-code to ensure environment parity. The only differences should be configuration values and scale.
