---
type: "EXAMPLE"
title: "Section 4: Setting Up Codemagic"
---


Codemagic is Flutter-first and easier to set up than GitHub Actions.

### Step 1: Sign Up for Codemagic

1. Go to [codemagic.io](https://codemagic.io)
2. Sign up with GitHub, GitLab, or Bitbucket
3. Grant access to your repositories

### Step 2: Add Your Flutter App

1. Click "Add application"
2. Select your repository
3. Codemagic auto-detects it's a Flutter project ✅

### Step 3: Configure Workflow (UI Method)

**Easiest way: Use the workflow editor**

1. Click "Start your first build"
2. Codemagic automatically:
   - ✅ Installs Flutter
   - ✅ Runs `flutter pub get`
   - ✅ Runs `flutter test`
   - ✅ Builds Android APK
3. Click "Start new build"

**That's it!** Codemagic handles everything.

### Step 4: Configure Workflow (YAML Method)

For more control, create `codemagic.yaml` in your repository root:


### Step 5: Automatic Deployment with Codemagic


### Codemagic Features

✅ **Pre-installed Flutter** - No setup needed
✅ **Apple M1 machines** - Super fast iOS builds
✅ **Automatic code signing** - Handles certificates for you
✅ **Store publishing built-in** - One-click deployment
✅ **Visual workflow editor** - No YAML knowledge needed
✅ **Free tier** - 500 minutes/month



```yaml
workflows:
  deploy-workflow:
    name: Deploy to Stores
    max_build_duration: 60
    instance_type: mac_mini_m1

    environment:
      groups:
        - google_play  # Credentials stored in Codemagic
        - app_store

    scripts:
      - name: Get dependencies
        script: flutter pub get

      - name: Run tests
        script: flutter test

      - name: Build Android App Bundle
        script: flutter build appbundle --release

      - name: Build iOS
        script: |
          flutter build ipa --release \
            --export-options-plist=ios/ExportOptions.plist

    artifacts:
      - build/app/outputs/bundle/release/*.aab
      - build/ios/ipa/*.ipa

    publishing:
      google_play:
        credentials: $GOOGLE_PLAY_CREDENTIALS
        track: internal  # or: alpha, beta, production
        in_app_update_priority: 3

      app_store_connect:
        api_key: $APP_STORE_CONNECT_API_KEY
        key_id: $APP_STORE_CONNECT_KEY_ID
        issuer_id: $APP_STORE_CONNECT_ISSUER_ID
        submit_to_testflight: true

      email:
        recipients:
          - team@example.com
        notify:
          success: true
```
