---
type: "ANALOGY"
title: "Multiple Keys to the Same House"
---

Imagine you live in an apartment building with a modern smart lock system. When you first moved in, you were given a physical key - your primary way to enter. Over time, you added more access methods: a fingerprint scan for when your hands are full with groceries, a PIN code for when you forgot your key, a smartphone app that unlocks the door via Bluetooth, and even a key card you keep in your wallet as a backup.

Every single one of these methods opens the SAME apartment door. They are not creating different apartments for you - they are simply different ways to prove you are authorized to enter YOUR apartment. The physical key does not care that you also have a fingerprint registered. The smartphone app does not invalidate the PIN code. They coexist peacefully, each providing a convenient option depending on your situation.

External login providers work identically in ShopFlow. Your ShopFlow account is your apartment - it contains your orders, preferences, shopping cart, and history. Google login is like your fingerprint - quick and convenient when you have your phone. Microsoft login is like your key card - useful when you are on a work computer already signed into Microsoft. GitHub login is like the PIN code - perhaps you use it from your development machine. And a traditional password is like your physical key - the original method that always works.

Linking an external login is exactly like adding a new fingerprint or key card to your apartment's smart lock. You prove you own the apartment (by being already logged in), then you prove you control the new access method (by authenticating with Google), and the building's system creates the connection. From then on, either method opens the same door.

The security consideration is crucial: you would never let someone add their fingerprint to your apartment lock without proving they already have access, right? That would defeat the entire purpose of having a lock. Similarly, ShopFlow requires you to be authenticated before you can link a new external provider. The act of linking MUST happen from within an authenticated session, never as part of initial registration from an unknown source.

This pattern - multiple authentication methods pointing to a single identity - is called account linking or identity federation. It provides flexibility for users while maintaining a unified view of their data and permissions.