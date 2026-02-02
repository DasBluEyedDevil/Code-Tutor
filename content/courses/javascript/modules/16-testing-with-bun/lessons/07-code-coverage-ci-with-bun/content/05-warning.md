---
type: "WARNING"
title: "Coverage & CI Pitfalls"
---

Common mistakes:

1. **Chasing 100% coverage**:
   100% coverage doesn't mean bug-free code. You can have 100% coverage and still miss edge cases.

2. **Ignoring branch coverage**:
   ```javascript
   // Line covered, but only one branch tested!
   function status(age) {
     return age >= 18 ? 'adult' : 'minor';
   }
   status(25);  // Only tests 'adult' branch
   ```

3. **Using old setup-bun version**:
   ```yaml
   # WRONG - v1 is outdated
   uses: oven-sh/setup-bun@v2
   
   # CORRECT - use v2
   uses: oven-sh/setup-bun@v2
   ```

4. **Not failing CI on coverage drop**:
   Without thresholds, coverage can silently regress.

5. **Testing generated/third-party code**:
   Exclude node_modules and generated files from coverage.

6. **Forgetting to run tests on PRs**:
   ```yaml
   on:
     push:
       branches: [main]
     pull_request:      # Don't forget this!
       branches: [main]
   ```