---
type: "WARNING"
title: "Critical: Keystore Security"
---


**NEVER lose your keystore or forget your passwords!**

If you lose access to your release keystore:
- You can NEVER update your existing app on the Play Store
- Users must uninstall and reinstall (losing their data)
- You must publish as a new app with a new package name

**Best Practices:**

1. **Store multiple backups:**
   - Cloud storage (Google Drive, Dropbox)
   - USB drive in a safe location
   - Company secure storage

2. **Document your passwords:**
   - Use a password manager (1Password, Bitwarden)
   - Store separately from the keystore file
   - Share with trusted team members

3. **NEVER commit to git:**
   - Add `*.jks` and `*.keystore` to `.gitignore`
   - Add `key.properties` to `.gitignore`
   - Use CI/CD secrets for automated builds

4. **Consider Play App Signing:**
   - Google manages your app signing key
   - You use an upload key (replaceable if lost)
   - Recommended for new apps

