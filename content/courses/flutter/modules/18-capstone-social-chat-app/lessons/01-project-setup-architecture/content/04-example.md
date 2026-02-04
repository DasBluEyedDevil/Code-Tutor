---
type: "EXAMPLE"
title: "Server Configuration"
---


**Server pubspec.yaml**

The server needs Serverpod and our shared models:



```yaml
# server/pubspec.yaml
name: social_chat_server
description: Social Chat Serverpod Backend

environment:
  sdk: '>=3.10.0 <4.0.0'

dependencies:
  serverpod: ^2.0.0
  serverpod_auth_server: ^2.0.0
  
  # Our shared models
  social_chat_shared:
    path: ../shared

dev_dependencies:
  lints: ^4.0.0
  test: ^1.24.0
  serverpod_test: ^2.0.0
```
