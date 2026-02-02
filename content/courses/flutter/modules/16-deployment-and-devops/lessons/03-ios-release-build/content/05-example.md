---
type: "EXAMPLE"
title: "Registering Devices"
---


For development and ad hoc builds, devices must be registered with Apple:

**Finding Device UDID:**



```bash
# Method 1: Using Xcode
# Connect device -> Window -> Devices and Simulators
# Select device -> Copy "Identifier"

# Method 2: Using command line
# Connect device and run:
xcrun xctrace list devices

# Method 3: On the device
# Install Apple Configurator 2 or a UDID app
# Or visit udid.io in Safari on the device

# Example UDID format:
# 00008030-001234567890ABCD
```
