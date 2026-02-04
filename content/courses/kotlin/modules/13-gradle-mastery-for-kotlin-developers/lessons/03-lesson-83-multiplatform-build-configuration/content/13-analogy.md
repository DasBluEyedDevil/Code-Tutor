---
type: "ANALOGY"
title: "KMP Builds as Multilingual Publishing"
---

Building Kotlin Multiplatform projects is like publishing a book in multiple languages.

**`commonMain` is your original manuscript**—the core story that's the same regardless of which language it's translated to. Characters, plot, dialogue, and themes are universal.

**Source sets (`androidMain`, `iosMain`, `jsMain`) are localized editions**—adapting the book for specific markets. The Japanese edition might include cultural notes, the American edition uses US spelling, but the core story stays the same.

**Platform-specific code is like localization examples**—in the Chinese edition, you might replace "hot dog" with "jianbing" so readers understand. In `iosMain`, you use `UIColor` instead of Android's `Color` class, but the concept (colors) is universal.

**The Gradle build is your publishing house**—it manages translators (compilers) for each language, ensures all editions stay consistent with the original manuscript, and produces final books (APK, IPA, JS bundle) for each market.

**Hierarchical source sets are language families**—`appleMain` is like a "Romance languages" edition shared by French and Spanish versions. Instead of translating from scratch twice, you translate once to Romance base, then make minor adjustments for French vs Spanish.

You write the story once (`commonMain`), and Gradle's "publishing house" produces localized editions for every platform, ensuring consistency while adapting to each market's needs.
