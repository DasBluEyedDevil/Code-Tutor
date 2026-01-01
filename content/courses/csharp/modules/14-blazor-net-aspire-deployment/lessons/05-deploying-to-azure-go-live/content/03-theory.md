---
type: "THEORY"
title: "Syntax Breakdown"
---

## Breaking Down the Syntax

**`az login`**: Azure CLI authentication. Opens browser to login. Required before running az commands. Use 'az account show' to verify.

**`Resource Group`**: Container for resources. Group related resources: app, database, storage. Easy to manage/delete together. Like folder for cloud resources.

**`App Service Plan`**: Defines compute resources. B1 = Basic tier. F1 = Free (limited). P1V2 = Premium. Plan determines price and performance.

**`Connection strings`**: Store in Azure Configuration, NOT code! Access via Environment.GetEnvironmentVariable(). Azure encrypts at rest.