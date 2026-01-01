---
type: "THEORY"
title: "What is CI/CD?"
---

**CI/CD = Continuous Integration / Continuous Deployment**

Automation that runs every time you push code:

**Continuous Integration (CI):**
- Automatically run tests on every push
- Lint and format check your code
- Type check with static analysis
- Catch bugs before they reach production
- Build confidence in every change

**Continuous Deployment (CD):**
- Automatically deploy passing builds
- Staging environment for testing
- Production deployment after approval
- Rollback capabilities

**Why CI/CD matters:**

1. **Catch bugs early**
   - Tests run on every push
   - Failing tests block merges
   - Problems found before users see them

2. **Consistent quality**
   - Same checks every time
   - No "it works on my machine"
   - Enforced code standards

3. **Faster feedback**
   - Know within minutes if code is broken
   - Fix issues while context is fresh
   - Ship with confidence

4. **Team collaboration**
   - Everyone's changes tested together
   - Integration problems caught early
   - Shared quality standards

**For our Personal Finance Tracker:**
- Run pytest on every push
- Lint with ruff
- Type check with mypy
- Test against PostgreSQL
- Deploy to Fly.io on main branch