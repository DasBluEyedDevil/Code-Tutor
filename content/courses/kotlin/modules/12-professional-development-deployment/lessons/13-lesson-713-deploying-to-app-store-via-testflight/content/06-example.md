---
type: "EXAMPLE"
title: "Automating with Fastlane"
---

Use Fastlane for automated iOS deployments:

```ruby
# Install Fastlane
gem install fastlane

# Initialize in iOS directory
cd iosApp
fastlane init

# Create Fastfile
# fastlane/Fastfile
default_platform(:ios)

platform :ios do
  desc "Build shared framework"
  lane :build_framework do
    sh "cd .. && ./gradlew :shared:linkReleaseFrameworkIosArm64"
  end

  desc "Deploy to TestFlight"
  lane :beta do
    build_framework
    
    # Use match for certificate management
    match(type: "appstore", readonly: true)
    
    build_app(
      workspace: "iosApp.xcworkspace",
      scheme: "iosApp",
      configuration: "Release",
      export_method: "app-store"
    )
    
    upload_to_testflight(
      skip_waiting_for_build_processing: true
    )
  end

  desc "Deploy to App Store"
  lane :release do
    build_framework
    
    match(type: "appstore", readonly: true)
    
    build_app(
      workspace: "iosApp.xcworkspace",
      scheme: "iosApp",
      configuration: "Release",
      export_method: "app-store"
    )
    
    upload_to_app_store(
      submit_for_review: true,
      automatic_release: true,
      force: true,
      precheck_include_in_app_purchases: false
    )
  end
end

# Run deployment
fastlane beta    # Deploy to TestFlight
fastlane release # Deploy to App Store
```
