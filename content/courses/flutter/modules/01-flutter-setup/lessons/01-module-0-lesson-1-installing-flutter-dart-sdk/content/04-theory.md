---
type: "THEORY"
title: "Introduction"
---

### **FOR MAC USERS:**

**Step 1: Download Flutter**
1. Open Safari and go to: `https://docs.flutter.dev/get-started/install/macos`
2. Choose your Mac type:
   - **Intel Mac**: Download "Intel Chip" version
   - **Apple Silicon (M1/M2/M3)**: Download "Apple Silicon" version

**Step 2: Extract and Move the Files**

Open Terminal (press `Cmd + Space`, type "Terminal", press Enter) and run these commands to create a folder and move the zip file there:

```bash
mkdir ~/development
cd ~/development
unzip ~/Downloads/flutter_macos_*.zip
```

**Step 3: Add Flutter to Your PATH**

We need to tell your computer where Flutter is permanently. Run this command to add it to your Z-Shell config:

```bash
echo 'export PATH="$PATH:$HOME/development/flutter/bin"' >> ~/.zshrc
source ~/.zshrc
```

**Step 4: Verify Installation**

Run this command to check if everything is working:

```bash
flutter doctor
```
