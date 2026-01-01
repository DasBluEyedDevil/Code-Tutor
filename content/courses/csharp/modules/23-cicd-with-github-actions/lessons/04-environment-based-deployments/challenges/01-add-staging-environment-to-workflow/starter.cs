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

  # TODO: Add staging deployment job
  # - Use 'environment:' with name 'staging' and url
  # - Add 'needs: build' to wait for build
  # - Add 'if:' condition for main branch only
  # - Include smoke tests after deployment

  deploy-production:
    runs-on: ubuntu-latest
    # TODO: Update needs to include staging
    environment:
      name: production
      url: https://shopflow.example.com
    steps:
      - name: Deploy to production
        run: echo "Deploying to production"