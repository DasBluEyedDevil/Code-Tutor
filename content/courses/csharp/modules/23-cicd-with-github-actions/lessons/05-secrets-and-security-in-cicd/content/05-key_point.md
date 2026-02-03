---
type: "KEY_POINT"
title: "Secrets Management in CI/CD"
---

## Key Takeaways

- **Never hardcode secrets in workflow files** -- use `${{ secrets.SECRET_NAME }}` to reference encrypted secrets stored in GitHub Settings. Secrets are masked in logs automatically.

- **Use OIDC for cloud authentication** -- `azure/login` with OIDC eliminates long-lived credentials. GitHub and Azure exchange short-lived tokens per workflow run. No secrets to rotate or leak.

- **Audit third-party actions** -- pin actions to specific commit SHAs (`uses: actions/checkout@abc123`) instead of tags. Tags can be moved to point at malicious code; commit SHAs are immutable.
