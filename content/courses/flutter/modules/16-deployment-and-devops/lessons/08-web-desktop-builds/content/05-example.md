---
type: "EXAMPLE"
title: "Linux Builds"
---


Build and package Linux applications using Snap, Flatpak, and AppImage:



```yaml
# Prerequisites:
# - clang, cmake, ninja-build, pkg-config
# - libgtk-3-dev, liblzma-dev, libstdc++-12-dev

# Install dependencies (Ubuntu/Debian)
sudo apt install clang cmake ninja-build pkg-config \
  libgtk-3-dev liblzma-dev libstdc++-12-dev

# Build Linux executable
flutter build linux --release

# Output: build/linux/x64/release/bundle/
# Contains: your_app executable and required libraries

---

# Snap Package (Ubuntu Store)
# Create snap/snapcraft.yaml

name: my-flutter-app
version: '1.0.0'
summary: My Flutter Application
description: |
  A cross-platform application built with Flutter.

base: core22
confinement: strict
grade: stable

apps:
  my-flutter-app:
    command: my_app
    extensions: [gnome]
    plugs:
      - network
      - network-bind
      - home
      - removable-media

parts:
  my-flutter-app:
    plugin: nil
    source: .
    build-packages:
      - clang
      - cmake
      - ninja-build
      - libgtk-3-dev
      - liblzma-dev
    override-build: |
      flutter build linux --release
      cp -r build/linux/x64/release/bundle/* $SNAPCRAFT_PART_INSTALL/

# Build snap
snapcraft

# Test locally
sudo snap install my-flutter-app_1.0.0_amd64.snap --dangerous

# Publish to Snap Store
snapcraft login
snapcraft upload my-flutter-app_1.0.0_amd64.snap --release=stable

---

# Flatpak Package
# Create com.yourcompany.MyApp.yml

app-id: com.yourcompany.MyApp
runtime: org.gnome.Platform
runtime-version: '45'
sdk: org.gnome.Sdk
command: my_app

finish-args:
  - --share=ipc
  - --socket=fallback-x11
  - --socket=wayland
  - --share=network
  - --device=dri
  - --filesystem=home

modules:
  - name: my-flutter-app
    buildsystem: simple
    build-commands:
      - cp -r bundle/* /app/
      - install -Dm755 bundle/my_app /app/bin/my_app
    sources:
      - type: dir
        path: build/linux/x64/release/bundle

# Build flatpak
flatpak-builder --force-clean build-dir com.yourcompany.MyApp.yml

# Create bundle for distribution
flatpak build-bundle repo my-app.flatpak com.yourcompany.MyApp

# Install and test
flatpak install my-app.flatpak
flatpak run com.yourcompany.MyApp

---

# AppImage (Portable Linux)
# Create AppDir structure

mkdir -p AppDir/usr/bin
mkdir -p AppDir/usr/lib
mkdir -p AppDir/usr/share/applications
mkdir -p AppDir/usr/share/icons/hicolor/256x256/apps

# Copy application
cp -r build/linux/x64/release/bundle/* AppDir/usr/bin/
cp assets/icons/app_icon.png AppDir/usr/share/icons/hicolor/256x256/apps/my_app.png

# Create .desktop file
cat > AppDir/usr/share/applications/my_app.desktop << EOF
[Desktop Entry]
Type=Application
Name=My Flutter App
Exec=my_app
Icon=my_app
Categories=Utility;
EOF

# Create AppRun script
cat > AppDir/AppRun << 'EOF'
#!/bin/bash
HERE=$(dirname "$(readlink -f "${0}")")
export LD_LIBRARY_PATH="$HERE/usr/lib:$LD_LIBRARY_PATH"
exec "$HERE/usr/bin/my_app" "$@"
EOF
chmod +x AppDir/AppRun

# Download appimagetool
wget https://github.com/AppImage/AppImageKit/releases/download/continuous/appimagetool-x86_64.AppImage
chmod +x appimagetool-x86_64.AppImage

# Create AppImage
./appimagetool-x86_64.AppImage AppDir MyApp-1.0.0-x86_64.AppImage

---

# GitHub Actions for Linux Build
jobs:
  build-linux:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      
      - name: Install dependencies
        run: |
          sudo apt-get update
          sudo apt-get install -y clang cmake ninja-build \
            libgtk-3-dev liblzma-dev libstdc++-12-dev
      
      - uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.38.0'
          cache: true

      - run: flutter config --enable-linux-desktop
      - run: flutter pub get
      - run: flutter build linux --release

      - uses: actions/upload-artifact@v4
        with:
          name: linux-bundle
          path: build/linux/x64/release/bundle/
```
