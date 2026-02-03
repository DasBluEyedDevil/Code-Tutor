---
type: "KEY_POINT"
title: "Docker Image Pipeline"
---

## Key Takeaways

- **Multi-stage Dockerfiles separate build from runtime** -- the build stage uses the SDK image (large). The final stage uses the runtime image (small). Only the runtime artifacts are copied to the final image.

- **Use `docker/build-push-action` in GitHub Actions** -- it handles building, tagging, and pushing to registries (GitHub Container Registry, Docker Hub, Azure Container Registry) with caching for faster builds.

- **Tag images with both commit SHA and semantic version** -- `ghcr.io/org/app:v1.2.3` and `ghcr.io/org/app:abc1234` provide both human-readable versions and exact commit traceability.
