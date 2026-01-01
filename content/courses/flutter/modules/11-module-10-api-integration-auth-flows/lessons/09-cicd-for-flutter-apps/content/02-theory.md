---
type: "THEORY"
title: "Introduction"
---


### What is CI/CD?

**Concept First:**
Imagine you're running a bakery. Without automation, you:
1. Manually mix ingredients for every bread loaf
2. Check each loaf by hand to ensure quality
3. Drive each delivery to customers yourself
4. Work 20 hours a day, exhausted

With automation (CI/CD), you:
1. Machines mix ingredients consistently
2. Quality sensors check each loaf automatically
3. Delivery trucks automatically route to customers
4. You oversee the process, focus on new recipes
5. Run 24/7 without exhaustion

**CI/CD** brings the same automation to software development.

**Jargon:**
- **CI (Continuous Integration)**: Automatically test and integrate code changes
- **CD (Continuous Deployment)**: Automatically deploy tested code to users
- **Pipeline**: A series of automated steps (test → build → deploy)
- **Workflow**: Configuration file defining what CI/CD should do
- **Runner**: Server that executes your CI/CD pipeline
- **Artifact**: Build output (APK, IPA, test reports)

### Why This Matters

**Without CI/CD:**
- Developer pushes code → manually run tests → might forget → bugs slip through
- Building APKs/IPAs locally → "works on my machine" syndrome
- Manual deployment → error-prone, time-consuming
- No consistent quality checks

**With CI/CD:**
- Every code push → automatic tests ✅
- Pull requests blocked if tests fail 🚫
- Builds created automatically on consistent machines
- Deploy to stores with one click or automatically
- Catch bugs before they reach users

**Real-world impact:**
- **Faster releases**: Deploy multiple times per day instead of per month
- **Higher quality**: Every change is tested automatically
- **Less stress**: No manual deployment at 2 AM
- **Team scalability**: 10 developers can work together safely

