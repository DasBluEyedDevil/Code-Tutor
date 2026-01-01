# Add code coverage to this test job

jobs:
  test:
    runs-on: ubuntu-latest
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Setup .NET
        uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '9.0.x'
      
      - name: Restore dependencies
        run: dotnet restore
      
      - name: Build
        run: dotnet build --no-restore --configuration Release
      
      # TODO: Update this step to collect code coverage
      - name: Run tests
        run: dotnet test --no-build --configuration Release
      
      # TODO: Add step to upload coverage to Codecov
      # Use: codecov/codecov-action@v4
      # Hint: Use the 'if: always()' condition to upload even on test failure
      
      # TODO: Add step to fail if coverage is below threshold
      # Check that line coverage is at least 70%