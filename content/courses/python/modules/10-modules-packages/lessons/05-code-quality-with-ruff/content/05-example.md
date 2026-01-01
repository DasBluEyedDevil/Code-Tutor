---
type: "EXAMPLE"
title: "Code Example: IDE Integration (VS Code)"
---

**Ruff integrates with VS Code for real-time feedback.** This means you see errors as you type, and can fix them with a single click.

**The Ruff extension provides:**
- Red squiggles for errors
- Yellow squiggles for warnings
- Quick fix menu (lightbulb icon)
- Format on save
- Organize imports on save

```python
print("="*60)
print("VS CODE INTEGRATION")
print("="*60)

print("""
# ============================================
# STEP 1: Install the Ruff Extension
# ============================================

1. Open VS Code
2. Go to Extensions (Ctrl+Shift+X / Cmd+Shift+X)
3. Search for "Ruff"
4. Install "Ruff" by Astral

# ============================================
# STEP 2: Configure VS Code Settings
# ============================================

Add to your settings.json (Ctrl+, then click "Open Settings (JSON)"):
""")

vscode_settings = '''
{
    // Use Ruff as the default formatter for Python
    "[python]": {
        "editor.formatOnSave": true,
        "editor.defaultFormatter": "charliermarsh.ruff",
        "editor.codeActionsOnSave": {
            "source.fixAll": "explicit",
            "source.organizeImports": "explicit"
        }
    },
    
    // Ruff extension settings
    "ruff.lint.run": "onType",  // Check as you type
    "ruff.organizeImports": true,
    "ruff.fixAll": true
}
'''
print(vscode_settings)

print("""
# ============================================
# STEP 3: Enjoy Real-Time Feedback!
# ============================================

Now when you code:

1. Errors show immediately as you type
2. Saving auto-formats and fixes issues
3. Imports are automatically organized
4. Hover over issues to see explanations
5. Click the lightbulb for quick fixes

# ============================================
# OTHER IDE SUPPORT
# ============================================

- PyCharm: Use External Tools or Ruff plugin
- Neovim: Use null-ls or conform.nvim with ruff
- Sublime Text: Use SublimeLinter-ruff
- Emacs: Use ruff-lsp

Ruff also provides an LSP server:
  ruff server
  
This enables IDE integration for any LSP-compatible editor.
""")
```
