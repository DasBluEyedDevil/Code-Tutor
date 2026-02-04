---
type: "KEY_POINT"
title: "Key Takeaways"
---

**MVVM (Model-View-ViewModel) separates UI from business logic**. Views observe ViewModels via StateFlow/SharedFlow, ViewModels coordinate use cases and repositories, Models represent domain data.

**ViewModels survive configuration changes and screen rotations** because they're scoped to navigation destinations, not composable lifecycles. This preserves user state during UI recreation.

**Use repositories to abstract data sources**—ViewModels shouldn't know whether data comes from network, database, or cache. Repositories provide a clean API and handle the implementation details.
