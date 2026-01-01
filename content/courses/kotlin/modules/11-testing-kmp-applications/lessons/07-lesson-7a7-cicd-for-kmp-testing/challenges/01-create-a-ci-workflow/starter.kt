# TODO: Create .github/workflows/test.yml that:
# 1. Runs JVM tests on every push (fast, cheap)
# 2. Runs iOS tests only on PRs to main (expensive)
# 3. Uses Gradle caching
# 4. Uploads test results as artifacts
# 5. Fails the build if tests fail
# 6. Shows test count in the workflow summary