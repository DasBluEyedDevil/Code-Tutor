---
type: "WARNING"
title: "Riverpod Version Note"
---

**Riverpod 3.x** was released in September 2025, introducing a unified `Ref` type and other API changes. This course teaches **Riverpod 2.x** Notifier patterns, which remain fully functional and are the most widely used in production apps.

If you encounter Riverpod 3.x code online, the main differences are:
- **Unified Ref**: Riverpod 3.x merges `WidgetRef` and `Ref` into a single `Ref` type
- **Simplified API**: Some provider types are consolidated

The 2.x patterns you learn here work correctly with both Riverpod 2.x and 3.x (via legacy imports). A migration guide is available at [riverpod.dev/docs/3.0_migration](https://riverpod.dev/docs/migration/from_riverpod_2_to_3).
