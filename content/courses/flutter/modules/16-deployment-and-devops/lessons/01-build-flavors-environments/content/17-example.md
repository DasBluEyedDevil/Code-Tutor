---
type: "EXAMPLE"
title: "IDE Configuration - VS Code"
---


Create launch configurations for easy debugging:



```json
// .vscode/launch.json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": "Dev",
      "request": "launch",
      "type": "dart",
      "program": "lib/main.dart",
      "args": [
        "--flavor", "dev",
        "--dart-define-from-file=config/dev.json"
      ]
    },
    {
      "name": "Staging",
      "request": "launch",
      "type": "dart",
      "program": "lib/main.dart",
      "args": [
        "--flavor", "staging",
        "--dart-define-from-file=config/staging.json"
      ]
    },
    {
      "name": "Prod (Debug)",
      "request": "launch",
      "type": "dart",
      "program": "lib/main.dart",
      "args": [
        "--flavor", "prod",
        "--dart-define-from-file=config/prod.json"
      ]
    }
  ]
}
```
