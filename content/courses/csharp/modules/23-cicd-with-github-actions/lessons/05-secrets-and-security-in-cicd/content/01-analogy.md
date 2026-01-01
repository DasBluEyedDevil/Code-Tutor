---
type: "ANALOGY"
title: "Secrets as Safety Deposit Box Keys"
---

Consider how banks protect safety deposit boxes. You never receive a copy of the master key - instead, you present identification and the bank employee uses their key combined with yours to open the box. The vault has security cameras, access logs, and multiple layers of protection. If your key is lost, you go through an identity verification process rather than simply getting a duplicate.

CI/CD secrets work the same way. Your database password, API keys, and service credentials are stored in a secure vault (GitHub Secrets, Azure Key Vault, HashiCorp Vault). When your workflow needs to access a secret, it does not receive a permanent copy. Instead, it presents its identity (the workflow is running in repository X, triggered by event Y, for environment Z) and receives temporary access to use that secret for the current operation.

Just like the bank, your CI/CD system maintains audit logs of every secret access. You can see which workflows used which secrets and when. If a secret is compromised, you can revoke it and issue a new one without changing your workflows - they will automatically receive the new secret on their next run.

The key insight is that secrets should never be copied or stored outside the vault. Your workflow receives a secret, uses it immediately, and forgets it. The secret never appears in logs, artifacts, or outputs. Just as you would never photocopy your safety deposit box key and leave copies around your house, you never echo secrets to console output or write them to files that persist beyond the workflow run.

This principle extends to secret rotation. Banks periodically require you to update your signature card and verify your identity. Similarly, production secrets should be rotated regularly - every 90 days is a common policy. Automated rotation means even if a secret leaks, it becomes invalid before an attacker can exploit it. The vault handles rotation automatically, and your workflows continue working with no changes required.