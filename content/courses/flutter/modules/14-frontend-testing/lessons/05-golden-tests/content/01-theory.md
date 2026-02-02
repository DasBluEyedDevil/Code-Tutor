---
type: "THEORY"
title: "What Are Golden Tests?"
---


**Golden tests** (also called snapshot tests) compare your widget's rendered output against a saved reference image.

**Use Cases:**
- Visual regression testing
- Ensuring UI consistency across changes
- Catching unintended visual changes
- Design system component verification

**How It Works:**
1. First run: Flutter renders widget and saves as "golden" image
2. Subsequent runs: Compare current render to golden
3. If different: Test fails, shows visual diff

