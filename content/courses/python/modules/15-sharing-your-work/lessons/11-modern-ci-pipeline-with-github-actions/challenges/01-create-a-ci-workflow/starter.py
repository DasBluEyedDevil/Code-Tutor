# .github/workflows/ci.yml
name: CI

on:
  ____:
    branches: [main]
  ____:
    branches: [main]

jobs:
  test:
    runs-on: ubuntu-latest

    services:
      postgres:
        image: postgres:16
        env:
          POSTGRES_USER: test
          POSTGRES_PASSWORD: test
          POSTGRES_DB: test
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5
        ports:
          - 5432:5432

    steps:
      - uses: actions/checkout@v4

      - name: Install uv
        uses: ____
        with:
          version: "latest"

      - name: Set up Python
        run: uv python install ____

      - name: Install dependencies
        run: ____

      - name: Lint with ruff
        run: ____

      - name: Format check
        run: ____

      - name: Run tests
        run: ____
        env:
          DATABASE_URL: postgresql+asyncpg://test:test@localhost:5432/test