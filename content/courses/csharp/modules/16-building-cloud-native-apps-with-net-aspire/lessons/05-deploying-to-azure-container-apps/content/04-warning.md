---
type: "WARNING"
title: "Common Pitfalls"
---

## Deployment Disasters to Avoid

**azd down Destroys EVERYTHING**: Running `azd down` deletes ALL Azure resources including databases with your data! Only use for dev/test environments. Production cleanup should be manual and deliberate.

**External Endpoints = Public Internet**: `.WithExternalHttpEndpoints()` exposes services to the ENTIRE internet. Only add this to services that genuinely need public access (web frontends, public APIs). Keep internal services internal!

**Cost Awareness**: Azure Container Apps charges for compute, storage, and managed services. Running `azd up` creates billable resources immediately. Use `azd down` for dev environments when not in use.

**Secrets in Source Control**: Never commit azure.yaml with secrets or .azure/ folder with credentials to git. Add `.azure/` to .gitignore. Use Azure Key Vault for sensitive configuration.

**Environment Confusion**: `azd env` manages multiple environments. Double-check which environment is selected before running `azd up` or `azd down`. Wrong environment = wrong deployment or data loss!

**Connection String Differences**: Local Aspire uses container connection strings. Production uses Azure managed service connection strings. Test thoroughly in staging before production - connection behavior may differ.