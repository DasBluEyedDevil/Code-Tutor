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
      
      - name: Run tests with coverage
        run: |
          dotnet test --no-build --configuration Release \
            --collect:"XPlat Code Coverage" \
            --results-directory ./coverage \
            -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=cobertura
      
      - name: Upload coverage to Codecov
        uses: codecov/codecov-action@v4
        if: always()
        with:
          token: ${{ secrets.CODECOV_TOKEN }}
          files: ./coverage/**/coverage.cobertura.xml
          fail_ci_if_error: false
          verbose: true
      
      - name: Check coverage threshold
        run: |
          # Install coverage tool
          dotnet tool install -g dotnet-reportgenerator-globaltool
          
          # Generate summary
          reportgenerator \
            -reports:./coverage/**/coverage.cobertura.xml \
            -targetdir:./coverage/report \
            -reporttypes:TextSummary
          
          # Check threshold
          COVERAGE=$(grep -oP 'Line coverage: \K[0-9.]+' ./coverage/report/Summary.txt)
          echo "Line coverage: $COVERAGE%"
          
          if (( $(echo "$COVERAGE < 70" | bc -l) )); then
            echo "::error::Coverage $COVERAGE% is below threshold of 70%"
            exit 1
          fi