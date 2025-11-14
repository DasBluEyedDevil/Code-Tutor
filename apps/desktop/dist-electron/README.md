# Code Tutor Desktop - Installers

This directory contains the distributable installers for Code Tutor Desktop.

## Available Installers

### Linux

- **Code Tutor-1.0.0.AppImage** (102 MB)
  - Universal Linux package
  - Works on all major distributions
  - No installation required
  - Just make it executable and run: `chmod +x Code-Tutor-1.0.0.AppImage && ./Code-Tutor-1.0.0.AppImage`

- **code-tutor-desktop_1.0.0_amd64.deb** (71 MB)
  - Debian/Ubuntu package
  - Install with: `sudo dpkg -i code-tutor-desktop_1.0.0_amd64.deb`
  - Then run: `code-tutor-desktop`

### Installation Instructions

See [DISTRIBUTION.md](../../../DISTRIBUTION.md) for complete installation instructions for all platforms.

## Quick Start

### Linux AppImage
```bash
chmod +x Code-Tutor-1.0.0.AppImage
./Code-Tutor-1.0.0.AppImage
```

### Linux Debian Package
```bash
sudo dpkg -i code-tutor-desktop_1.0.0_amd64.deb
code-tutor-desktop
```

## System Requirements

- **Linux:** Ubuntu 20.04+, Debian 10+, or equivalent
- **RAM:** 4 GB minimum, 8 GB recommended
- **Storage:** 500 MB free space
- **Display:** 1280x720 minimum resolution

## Troubleshooting

If AppImage doesn't run, install libfuse2:
```bash
# Ubuntu/Debian
sudo apt install libfuse2

# Fedora
sudo dnf install fuse-libs
```

## Support

For issues, questions, or contributions:
- GitHub: https://github.com/DasBluEyedDevil/Code-Tutor
- Email: support@codetutor.dev

## Building from Source

To build the installers yourself:
```bash
cd /path/to/Code-Tutor
./build-installers.sh
```

See [DISTRIBUTION.md](../../../DISTRIBUTION.md) for detailed build instructions.
