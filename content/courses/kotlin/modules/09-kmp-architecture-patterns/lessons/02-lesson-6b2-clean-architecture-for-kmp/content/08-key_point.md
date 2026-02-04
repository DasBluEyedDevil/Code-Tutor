---
type: "KEY_POINT"
title: "Key Takeaways"
---

**Clean Architecture organizes code in concentric layers**: Domain (entities, use cases), Data (repositories, data sources), and Presentation (ViewModels, UI). Dependencies point inward—domain has zero dependencies on outer layers.

**The domain layer lives entirely in commonMain** with no platform dependencies. It defines business logic, entities, and repository interfaces that data and presentation layers implement.

**Use case classes encapsulate single business operations**, making them reusable, testable, and composable. Write `class GetUserProfileUseCase(private val repository: UserRepository)` instead of scattering logic across ViewModels.
