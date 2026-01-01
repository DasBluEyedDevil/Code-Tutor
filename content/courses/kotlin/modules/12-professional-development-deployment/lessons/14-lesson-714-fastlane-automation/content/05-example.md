---
type: "EXAMPLE"
title: "Complete iOS Fastfile"
---

Full Fastlane configuration for iOS:

```ruby
# iosApp/fastlane/Fastfile
default_platform(:ios)

platform :ios do
  before_all do
    # Build KMP shared framework before any lane
    sh "cd ../.. && ./gradlew :shared:linkReleaseFrameworkIosArm64"
  end

  desc "Sync certificates with match"
  lane :sync_certs do
    match(
      type: "appstore",
      readonly: is_ci  # Read-only in CI, can create locally
    )
  end

  desc "Run tests"
  lane :test do
    run_tests(
      workspace: "iosApp.xcworkspace",
      scheme: "iosApp",
      devices: ["iPhone 15 Pro"]
    )
  end

  desc "Build for simulator"
  lane :build_debug do
    build_app(
      workspace: "iosApp.xcworkspace",
      scheme: "iosApp",
      configuration: "Debug",
      destination: "generic/platform=iOS Simulator",
      skip_archive: true
    )
  end

  desc "Deploy to TestFlight"
  lane :beta do
    sync_certs
    
    increment_build_number(
      build_number: latest_testflight_build_number + 1
    )
    
    build_app(
      workspace: "iosApp.xcworkspace",
      scheme: "iosApp",
      configuration: "Release",
      export_method: "app-store"
    )
    
    upload_to_testflight(
      skip_waiting_for_build_processing: true,
      distribute_external: false
    )
  end

  desc "Deploy to TestFlight for external testing"
  lane :beta_external do
    beta
    
    # Wait for processing and distribute to external testers
    upload_to_testflight(
      skip_submission: false,
      distribute_external: true,
      groups: ["Beta Testers"],
      changelog: "Bug fixes and improvements"
    )
  end

  desc "Deploy to App Store"
  lane :release do
    sync_certs
    
    # Capture screenshots (optional)
    # snapshot
    
    build_app(
      workspace: "iosApp.xcworkspace",
      scheme: "iosApp",
      configuration: "Release",
      export_method: "app-store"
    )
    
    deliver(
      submit_for_review: true,
      automatic_release: true,
      force: true,
      skip_screenshots: true,
      skip_metadata: false
    )
  end

  error do |lane, exception|
    # Cleanup on error
    clean_build_artifacts
  end
end
```
