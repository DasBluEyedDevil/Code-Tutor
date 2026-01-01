---
type: "THEORY"
title: "Setting Up Play Store API Access"
---

### Create Service Account

1. Go to Play Console → Setup → API access
2. Click "Create new service account"
3. Follow link to Google Cloud Console
4. Create service account with role "Service Account User"
5. Create JSON key and download

### Grant Permissions

1. Back in Play Console → Users and permissions
2. Invite the service account email
3. Grant permissions:
   - Release to production
   - Release apps to testing tracks
   - Manage store presence

### Use in CI/CD

```yaml
# GitHub Actions
- name: Upload to Play Store
  uses: r0adkll/upload-google-play@v1
  with:
    serviceAccountJsonPlainText: ${{ secrets.PLAY_STORE_SERVICE_ACCOUNT }}
    packageName: com.yourcompany.yourapp
    releaseFiles: composeApp/build/outputs/bundle/release/*.aab
    track: internal
    # track options: internal, alpha, beta, production
```