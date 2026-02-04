---
type: "ANALOGY"
title: "Testing as Quality Inspection"
---

Testing software is like quality inspection in a factory producing cars.

**Unit tests are component inspections**—testing individual parts like brakes, steering wheels, and engines in isolation. If the brake pedal works perfectly in a test rig, you know that component is solid before it goes into any car.

**Integration tests are assembly line checks**—testing that the engine connects properly to the transmission, that the steering wheel actually turns the wheels. Components work individually, but do they work together?

**UI tests are final test drives**—putting a complete car on the track and verifying everything works as a driver experiences it. These are expensive (slow, require full setup) but catch issues that component testing misses.

**In KMP, shared code is like universal components** (the engine, brakes) that go into every car model. Test those heavily in `commonTest` because bugs affect every car. Platform-specific code is like model-specific features (dashboard layouts)—test those less rigorously since they're localized to one car model.

You wouldn't skip testing engines (shared logic) to test every dashboard variant (platform UI). Focus testing on high-value, high-reuse components.
