---
type: "WARNING"
title: "Secrets Management"
---


**Storing Secrets Securely**

Never commit sensitive data to your repository. Use GitHub Secrets for:

- API keys and tokens
- Signing certificates and keystores
- Service account credentials
- Database passwords

**Adding Secrets:**

1. Go to Repository Settings > Secrets and variables > Actions
2. Click "New repository secret"
3. Enter name (e.g., `KEYSTORE_PASSWORD`) and value
4. Access in workflow: `${{ secrets.KEYSTORE_PASSWORD }}`

**Encoding Binary Files:**

For certificates and keystores, encode as base64:

```bash
# Encode keystore
base64 -i android/app/keystore.jks | pbcopy

# Decode in workflow
echo "${{ secrets.KEYSTORE_BASE64 }}" | base64 -d > keystore.jks
```

**Required Secrets for Flutter CI/CD:**

| Secret | Purpose |
|--------|----------|
| `KEYSTORE_BASE64` | Android signing keystore (base64) |
| `KEYSTORE_PASSWORD` | Keystore password |
| `KEY_ALIAS` | Key alias name |
| `KEY_PASSWORD` | Key password |
| `BUILD_CERTIFICATE_BASE64` | iOS distribution certificate (base64) |
| `P12_PASSWORD` | Certificate password |
| `PROVISIONING_PROFILE_BASE64` | iOS provisioning profile (base64) |
| `GOOGLE_PLAY_SERVICE_ACCOUNT` | Play Store API JSON key |
| `ASC_KEY_ID` | App Store Connect API key ID |
| `ASC_ISSUER_ID` | App Store Connect issuer ID |
| `ASC_KEY` | App Store Connect private key |
| `FLY_API_TOKEN` | Fly.io deployment token |

**Security Best Practices:**

- Rotate secrets regularly
- Use environment-specific secrets
- Limit secret access to required workflows
- Never echo or log secrets
- Use OIDC for cloud provider authentication when possible

