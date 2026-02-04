---
type: "ANALOGY"
title: "The Notarized Document"
---

App signing is like getting a document notarized. A notary verifies your identity, stamps the document with their official seal, and now anyone who sees the stamp knows the document is authentic and has not been tampered with. Without the notary's seal, the document has no legal standing -- just like an unsigned app cannot be distributed through app stores.

Your signing key is your identity. Android uses a keystore file; iOS uses certificates and provisioning profiles managed through Apple Developer. **If you lose your signing key, it is like losing the notary's stamp** -- you can never update your existing app again, because the store cannot verify that you are the same publisher. This is why the plan emphasizes backing up your keystore and never committing it to version control.

The provisioning profile adds another layer: it specifies which devices can run your app (during development) and which app IDs are authorized. Think of it as the notarized document also listing the specific offices where it can be filed.
