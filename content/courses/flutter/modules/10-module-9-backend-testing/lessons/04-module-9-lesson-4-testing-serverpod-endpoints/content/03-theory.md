---
type: "THEORY"
title: "Setting Up Your Test Environment"
---

Before writing Serverpod tests, configure your project properly.

First, add test dependencies to your server's pubspec.yaml:

```yaml
dev_dependencies:
  test: ^1.24.0
  serverpod_test: ^1.2.0
```

Create a test directory structure:

```
my_project_server/
  lib/
    src/
      endpoints/
        user_endpoint.dart
  test/
    integration/
      user_endpoint_test.dart
    unit/
      user_service_test.dart
    test_utils/
      test_session.dart
```

For integration tests, configure a test database in config/test.yaml:

```yaml
database:
  host: localhost
  port: 5432
  name: my_project_test
  user: postgres
  password: test_password
```