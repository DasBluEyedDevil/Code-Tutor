---
type: "THEORY"
title: "Testing Deep Links"
---


### On Android (using ADB)


### On iOS (using xcrun)


### Manual Testing

1. **Email yourself** the link: `https://mycompany.com/product/laptop`
2. Open email on your phone
3. Tap the link
4. App should open to product page!



```bash
# Test deep link
xcrun simctl openurl booted "https://mycompany.com/product/laptop"

# Test another route
xcrun simctl openurl booted "https://mycompany.com/cart"
```
