---
type: "THEORY"
title: "Fastlane Match for Team Certificate Management"
---

### What is Match?

Match stores certificates and profiles in a Git repo or cloud storage, syncing them across your team.

### Setup

```bash
# Initialize match
fastlane match init

# Select storage:
# - git: Private Git repo
# - google_cloud: GCS bucket
# - s3: AWS S3 bucket

# Generate certificates
fastlane match appstore   # App Store distribution
fastlane match development # Development
fastlane match adhoc       # Ad Hoc distribution
```

### Matchfile Configuration

```ruby
# fastlane/Matchfile
git_url("git@github.com:yourcompany/certificates.git")

type("appstore")
app_identifier(["com.yourcompany.yourapp"])
username("your@email.com")
team_id("ABCD1234")

# Use readonly in CI
readonly(true) if is_ci
```

### Using in CI

```yaml
# GitHub Actions
- name: Install certificates
  env:
    MATCH_PASSWORD: ${{ secrets.MATCH_PASSWORD }}
    MATCH_GIT_BASIC_AUTHORIZATION: ${{ secrets.MATCH_GIT_AUTH }}
  run: |
    cd iosApp
    fastlane match appstore --readonly
```