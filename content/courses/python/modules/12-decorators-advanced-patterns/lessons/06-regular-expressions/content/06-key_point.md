---
type: "KEY_POINT"
title: "Key Takeaways"
---

- **re.search() finds anywhere, re.match() only at start** - Most common confusion
- **Always use raw strings r''** - Prevents backslash escaping issues
- **\d digit, \w word, \s space** - Most common character classes
- **+ means 1+, * means 0+, ? means 0 or 1** - Quantifiers
- **^ start, $ end** - Anchors for full string matching
- **() creates groups** - Access with match.group(1), match.groups()
- **Named groups: (?P<name>...)** - Better than numbers
- **Compile patterns if reusing:** pattern = re.compile(r'...')