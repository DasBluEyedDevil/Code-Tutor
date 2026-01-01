---
type: "THEORY"
title: "App Store Connect API"
---

### Creating API Key

1. Go to App Store Connect → Users and Access → Keys
2. Click "+" to generate new key
3. Name it and select role (Admin or App Manager)
4. Download the .p8 file (only available once!)
5. Note the Key ID and Issuer ID

### Using in CI/CD

```yaml
# GitHub Actions
- name: Upload to TestFlight
  env:
    APP_STORE_CONNECT_API_KEY_ID: ${{ secrets.APP_STORE_API_KEY_ID }}
    APP_STORE_CONNECT_ISSUER_ID: ${{ secrets.APP_STORE_ISSUER_ID }}
    APP_STORE_CONNECT_API_KEY: ${{ secrets.APP_STORE_API_KEY_BASE64 }}
  run: |
    mkdir -p ~/.appstoreconnect/private_keys
    echo "$APP_STORE_CONNECT_API_KEY" | base64 --decode > ~/.appstoreconnect/private_keys/AuthKey_${APP_STORE_CONNECT_API_KEY_ID}.p8
    
    xcrun altool --upload-app \
      -f iosApp.ipa \
      --type ios \
      --apiKey "$APP_STORE_CONNECT_API_KEY_ID" \
      --apiIssuer "$APP_STORE_CONNECT_ISSUER_ID"
```