---
type: "WARNING"
title: "Common Pitfalls"
---

## CI/CD Adoption Pitfalls

**Deploying Without a Rollback Strategy**: Automating deployment without a way to undo it means a broken release stays broken until you fix-forward. Always have a rollback plan: keep the previous container image tagged, use blue-green or canary deployments, or at minimum know how to redeploy the last working version within minutes.

**Skipping Tests to Ship Faster**: Removing test steps from the CI pipeline because "they slow down the build" eliminates the safety net that prevents broken code from reaching production. If tests are slow, fix the tests (parallelize, use faster test databases) rather than removing them. A 10-minute pipeline that catches bugs beats a 2-minute pipeline that ships them.

**Not Pinning Action Versions**: Using `uses: actions/checkout@main` instead of `uses: actions/checkout@v4` means your workflow depends on whatever the action maintainer pushed most recently. A breaking change or supply chain attack affects your builds immediately. Always pin to specific major versions or commit SHAs for critical actions.

**CI/CD Pipeline as an Afterthought**: Building the entire application first and adding CI/CD at the end means you discover integration problems late. Set up CI on day one, even if the pipeline only builds and runs a single test. Incremental pipeline complexity is much easier than retrofitting CI into a mature codebase.
