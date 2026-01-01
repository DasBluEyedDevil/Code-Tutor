---
type: "EXAMPLE"
title: "Unified Fastfile for KMP"
---

A root Fastfile that orchestrates both platforms:

```ruby
# Root fastlane/Fastfile
desc "Build both platforms"
lane :build_all do
  # Build Android
  Dir.chdir("../composeApp") do
    sh "fastlane build_release"
  end
  
  # Build iOS
  Dir.chdir("../iosApp") do
    sh "fastlane build_release"
  end
end

desc "Deploy both platforms to testing"
lane :beta_all do
  # Deploy Android to internal
  Dir.chdir("../composeApp") do
    sh "fastlane internal"
  end
  
  # Deploy iOS to TestFlight
  Dir.chdir("../iosApp") do
    sh "fastlane beta"
  end
end

desc "Release both platforms to production"
lane :release_all do
  # Deploy Android to Play Store
  Dir.chdir("../composeApp") do
    sh "fastlane production"
  end
  
  # Deploy iOS to App Store
  Dir.chdir("../iosApp") do
    sh "fastlane release"
  end
end

# Environment setup for CI
before_all do
  # Ensure environment variables are set
  required_env = [
    "KEYSTORE_PATH",
    "KEYSTORE_PASSWORD",
    "KEY_ALIAS",
    "KEY_PASSWORD"
  ]
  
  required_env.each do |var|
    UI.user_error!("Missing #{var}") unless ENV[var]
  end
end
```
