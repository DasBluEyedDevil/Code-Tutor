---
type: "WARNING"
title: "Pre-Submission Checklist"
---


**Before uploading to Play Store:**

1. **Verify Application ID:**
   - Must match your Play Console app
   - Cannot be changed after first upload
   - Check for flavor suffixes accidentally included

2. **Check Version Code:**
   - Must be higher than any previous upload
   - Play Store rejects duplicate or lower version codes

3. **Upload Mapping File:**
   - Go to Play Console -> Release -> App bundle explorer
   - Upload `mapping.txt` for readable crash reports
   - Do this for every release

4. **Review App Signing:**
   - New apps should use Play App Signing
   - Existing apps: ensure you're using correct keystore

5. **Test on Multiple Devices:**
   - Different screen sizes
   - Different Android versions (minSdk to latest)
   - Different manufacturers (Samsung, Pixel, etc.)

