---
type: "THEORY"
title: "Introduction"
---

### **FOR LINUX USERS:**

**Step 1: Download Flutter**

1. Open your browser and go to: `https://docs.flutter.dev/get-started/install/linux`

2. Download the `flutter_linux_3.x.x-stable.tar.xz` file.



**Step 2: Extract the Files**

Open your terminal and run these commands to create a directory and extract the file (assuming it's in Downloads):



```bash

mkdir ~/development

cd ~/development

tar xf ~/Downloads/flutter_linux_*-stable.tar.xz

```



**Step 3: Add Flutter to Your PATH**

Add Flutter to your shell configuration (usually `.bashrc`):



```bash

echo 'export PATH="$PATH:$HOME/development/flutter/bin"' >> ~/.bashrc

source ~/.bashrc

```



**Step 4: Verify Installation**



```bash

flutter doctor

```
