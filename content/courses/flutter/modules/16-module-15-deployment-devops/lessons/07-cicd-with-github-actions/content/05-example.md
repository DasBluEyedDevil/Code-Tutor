---
type: "EXAMPLE"
title: "Store and Backend Deployment"
---


Automate deployment to app stores and backend platforms:



```yaml
# .github/workflows/deploy.yml
name: Deploy to Production

on:
  push:
    tags:
      - 'v*'

jobs:
  # Deploy Android to Play Store
  deploy-android:
    name: Deploy to Play Store
    runs-on: ubuntu-latest
    needs: [build-android]  # From previous workflow
    environment:
      name: production
      url: https://play.google.com/store/apps/details?id=com.example.app
    
    steps:
      - uses: actions/checkout@v4

      - name: Download AAB artifact
        uses: actions/download-artifact@v4
        with:
          name: android-aab
          path: build/

      - name: Deploy to Play Store
        uses: r0adkll/upload-google-play@v1
        with:
          serviceAccountJsonPlainText: ${{ secrets.GOOGLE_PLAY_SERVICE_ACCOUNT }}
          packageName: com.example.app
          releaseFiles: build/app-release.aab
          track: internal  # internal, alpha, beta, production
          status: completed
          whatsNewDirectory: distribution/whatsnew/

  # Deploy iOS to TestFlight
  deploy-ios:
    name: Deploy to TestFlight
    runs-on: macos-latest
    needs: [build-ios]
    environment: production
    
    steps:
      - uses: actions/checkout@v4

      - name: Download IPA artifact
        uses: actions/download-artifact@v4
        with:
          name: ios-ipa
          path: build/

      - name: Install Fastlane
        run: gem install fastlane

      - name: Upload to TestFlight
        env:
          APP_STORE_CONNECT_API_KEY_ID: ${{ secrets.ASC_KEY_ID }}
          APP_STORE_CONNECT_API_ISSUER_ID: ${{ secrets.ASC_ISSUER_ID }}
          APP_STORE_CONNECT_API_KEY: ${{ secrets.ASC_KEY }}
        run: |
          fastlane pilot upload \
            --ipa build/*.ipa \
            --skip_waiting_for_build_processing

  # Deploy Serverpod backend
  deploy-backend:
    name: Deploy Backend to Fly.io
    runs-on: ubuntu-latest
    environment:
      name: production
      url: https://api.myapp.com
    defaults:
      run:
        working-directory: ./my_app_server
    
    steps:
      - uses: actions/checkout@v4

      - name: Setup Fly.io CLI
        uses: superfly/flyctl-actions/setup-flyctl@master

      - name: Deploy to Fly.io
        env:
          FLY_API_TOKEN: ${{ secrets.FLY_API_TOKEN }}
        run: flyctl deploy --remote-only

      - name: Run database migrations
        env:
          FLY_API_TOKEN: ${{ secrets.FLY_API_TOKEN }}
        run: |
          flyctl ssh console -C "./server --apply-migrations --mode production --migrate-only"

      - name: Verify deployment
        run: |
          sleep 30
          curl -f https://api.myapp.com/health || exit 1

  # Alternative: Deploy to Railway
  deploy-railway:
    name: Deploy Backend to Railway
    runs-on: ubuntu-latest
    environment: production
    if: false  # Enable if using Railway instead
    
    steps:
      - uses: actions/checkout@v4

      - name: Install Railway CLI
        run: npm install -g @railway/cli

      - name: Deploy to Railway
        env:
          RAILWAY_TOKEN: ${{ secrets.RAILWAY_TOKEN }}
        run: |
          cd my_app_server
          railway up --detach
```
