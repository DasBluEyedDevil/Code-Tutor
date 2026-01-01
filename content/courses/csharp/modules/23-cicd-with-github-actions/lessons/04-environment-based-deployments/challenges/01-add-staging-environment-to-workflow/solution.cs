# Add staging environment to this deployment workflow

name: Deploy ShopFlow

on:
  push:
    branches: [main]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - name: Build and push image
        run: echo "Image built and pushed"

  deploy-staging:
    name: Deploy to Staging
    runs-on: ubuntu-latest
    needs: build
    if: github.ref == 'refs/heads/main'
    environment:
      name: staging
      url: https://staging.shopflow.example.com
    
    steps:
      - uses: actions/checkout@v4
      
      - name: Deploy to staging
        run: |
          echo "Deploying to staging environment"
          # In real workflow: az containerapp update ...
      
      - name: Wait for deployment
        run: sleep 30
      
      - name: Run smoke tests
        run: |
          # Check health endpoint
          curl -f https://staging.shopflow.example.com/healthz/ready || exit 1
          echo "Health check passed"
          
          # Verify API returns data
          PRODUCT_COUNT=$(curl -s https://staging.shopflow.example.com/api/products | jq '.items | length')
          if [ "$PRODUCT_COUNT" -gt 0 ]; then
            echo "API returning $PRODUCT_COUNT products"
          else
            echo "API returned no products!"
            exit 1
          fi

  deploy-production:
    name: Deploy to Production
    runs-on: ubuntu-latest
    needs: [build, deploy-staging]
    if: github.ref == 'refs/heads/main'
    environment:
      name: production
      url: https://shopflow.example.com
    
    steps:
      - name: Deploy to production
        run: echo "Deploying to production"